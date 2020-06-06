using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        public List<string> CategoryBuilder()
        {
            List<CategoryModel> categoryModel = new List<CategoryModel>();
            List<string> noDuplicates = new List<string>();

            XmlDocument fullFeedXmlDoc = new XmlDocument();
            fullFeedXmlDoc.Load(feedPath);

            XmlNodeList xmlNodeList = fullFeedXmlDoc.SelectNodes("/products/product/categories/category/cat");

            try
            {
                foreach (XmlNode xn in xmlNodeList)
                {
                    var newModel = new CategoryModel()
                    {
                        //Id = xn["id"].InnerText,
                        Title = xn["title"].InnerText
                    };

                    categoryModel.Add(newModel);
                }

                List<string> duplicates = new List<string>();

                for (var i = 0; i < categoryModel.Count; i++)
                {
                    duplicates.Add(categoryModel[i].Title);
                }

                //LAZY CODE Returing only product TITLE as STRING. 
                //For best Practice return CATEGORYMODEL (filtered without DUPS).
                noDuplicates = new HashSet<string>(duplicates).ToList();
            }
            catch (Exception ex)
            {
                List<string> message = new List<string>();

                message.Add(ex.Message);
                message.Add(ex.StackTrace);

                return message;
            }

            return noDuplicates;
        }
    }
}
