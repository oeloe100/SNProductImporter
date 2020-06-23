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
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using SNPIDataManager.Areas.EDCFeed.Models.ProductModels.TestModels;

namespace SNPIDataManager.Areas.EDCFeed.Controllers.API
{
    public class InventoryDataController : ApiController
    {
        /// <summary>
        /// Local FeedPath Bv. C:/ Etc. (Use this only in Development)
        /// </summary>
        private string feedPath = "C:/Users/sexxnation/Downloads/TEMP/eg_xml_feed_2015_nl.xml";

        /// <summary>
        /// Test API request. Get Every Product (id, title, HsCode) from Supplier XML Formatted PRODUCTS.
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult InventoryData()
        {
            List<EDCTestProductModel> productModel = new List<EDCTestProductModel>();

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
                    var newModel = new EDCTestProductModel()
                    {
                        Id = xn["id"].InnerText,
                        Title = xn["title"].InnerText,
                        Popularity = xn["popularity"].InnerText
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

        /// <summary>
        /// Build Supplier Categories and return to View.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, List<string>> CategoryBuilder()
        {
            XmlDocument edcFeed = new XmlDocument();
            edcFeed.Load(feedPath);

            XmlElement root = edcFeed.DocumentElement;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CategoryModelList> ParentedCategories = EDCCategoriesHelper.CategorizeNodes(root).ToList();
            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;

            IDictionary<string, List<string>> RelationToView = new Dictionary<string, List<string>>();
            EDCCategoriesHelper.ChildParentRelationForView(ParentedCategories, RelationToView);

            return RelationToView;
        }
    }
}
