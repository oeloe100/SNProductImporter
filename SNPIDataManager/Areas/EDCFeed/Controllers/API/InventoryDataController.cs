using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Xml;

namespace SNPIDataManager.Areas.EDCFeed.Controllers.API
{
    public class InventoryDataController : ApiController
    {
        private string feedPath = "C:/Users/sexxnation/Downloads/TEMP/eg_xml_feed_2015_nl.xml";

        public IHttpActionResult InventoryData()
        {
            List<EDCProductModel> productModel = new List<EDCProductModel>();

            //For production download xml data from url below. Like
            //Download Limit by EDC is 4x A Day. Scedual download for once a day. Like 24:00.
            //using (System.Net.WebClient client = new System.Net.WebClient())
            //{
            //    client.DownloadFile("http://api.edc.nl/b2b_feed.php?key=4500c66ct0e0w63c8r4129tc80e622rr&sort=xml&type=xml&lang=nl&version=2015", "some.xml");
            //}

            XmlDocument fullFeedXmlDoc = new XmlDocument();
            fullFeedXmlDoc.Load(feedPath);

            XmlNodeList xmlNodeList = fullFeedXmlDoc.SelectNodes("/products/product");

            try
            {
                foreach (XmlNode xn in xmlNodeList)
                {
                    var newModel = new EDCProductModel()
                    {
                        Id = xn["id"].InnerText,
                        Title = xn["title"].InnerText,
                        HsCode = xn["popularity"].InnerText
                    };

                    productModel.Add(newModel);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

            return Ok(productModel.ToList());
        }

        public IDictionary<string, List<string>> CategoryBuilder()
        {
            XmlDocument edcFeed = new XmlDocument();
            edcFeed.Load(feedPath);

            XmlElement root = edcFeed.DocumentElement;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CategorizedCategoryModel> ParentedCategories = SNPIDataManager.Helpers.EDCHelper.EDCFeedCategorizationHelper.CategorizeNodes(root).ToList();
            watch.Stop();
            
            var elapsedMs = watch.ElapsedMilliseconds;

            IDictionary<string, List<string>> RelationToView = new Dictionary<string, List<string>>();
            SNPIDataManager.Helpers.EDCHelper.EDCFeedCategorizationHelper.ChildParentRelationForView(ParentedCategories, RelationToView);

            return RelationToView;
        }
    }
}
