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

        public List<CategorizedCategoryModel> CategoryBuilder()
        {
            XmlDocument edcFeed = new XmlDocument();
            edcFeed.Load(feedPath);

            XmlElement root = edcFeed.DocumentElement;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            List<CategorizedCategoryModel> ParentedCategories = CategorizeNodes(root).ToList();
            watch.Stop();
            
            var elapsedMs = watch.ElapsedMilliseconds;

            return ParentedCategories;
        }

        public IEnumerable<CategorizedCategoryModel> CategorizeNodes(XmlElement root)
        {
            List<CategorizedCategoryModel> CategoriesParented = new List<CategorizedCategoryModel>();

            XmlNodeList productNodes = root.SelectNodes("//product");
            for (var i = 0; i < productNodes.Count; i++)
            {
                if (productNodes.Count > 0) 
                {
                    for (var n = 0; n < productNodes[i].ChildNodes.Count; n++) 
                    {
                        XmlNodeList categoriesChildNodes = productNodes[i].ChildNodes;
                        if (categoriesChildNodes[n].Name == "categories")
                        {
                            for (int x = 0; x < categoriesChildNodes[n].ChildNodes.Count; x++)
                            {
                                if (categoriesChildNodes[n].ChildNodes[x].Name == "category")
                                {
                                    List<CategoryModel> CategoryModelList = new List<CategoryModel>();
                                    List<bool> CheckEveryEntry = new List<bool>();

                                    bool isDuplicate = true;

                                    /*** Fill Category Model with Data From 'EDC XML FEED' per Product ***/
                                    for (var d = 0; d < categoriesChildNodes[n].ChildNodes[x].ChildNodes.Count; d++)
                                    {
                                        var categoryModel = new CategoryModel()
                                        {
                                            Id = categoriesChildNodes[n].ChildNodes[x].ChildNodes[d].ChildNodes[0].InnerText,
                                            Title = categoriesChildNodes[n].ChildNodes[x].ChildNodes[d].ChildNodes[1].InnerText
                                        };

                                        CategoryModelList.Add(categoryModel);
                                        CheckEveryEntry.Add(IsDuplicate(CategoriesParented, categoryModel.Title));
                                    }

                                    /*** Check if every category entry in CheckEveryEntry(Bool List) = false (duplicate). 
                                     * If not we Can Savely add Category (May Be Duplicate) With SubCategory (Unique) ***/
                                    if (CheckEveryEntry.Count == categoriesChildNodes[n].ChildNodes[x].ChildNodes.Count)
                                    {
                                        for (var g = 0; g < CheckEveryEntry.Count; g++)
                                        {
                                            if (CheckEveryEntry[g] == false)
                                            {
                                                isDuplicate = false;
                                            }
                                            else
                                            {
                                                isDuplicate = true;
                                            }
                                        }
                                    }

                                    /*** Create Model Without duplicated SUB CATEGORY Items And return to View. ***/
                                    if (!isDuplicate)
                                    {
                                        CreateFinalCategoriesModel(CategoriesParented, CategoryModelList);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return CategoriesParented;
        }

        public bool IsDuplicate(List<CategorizedCategoryModel> CategoriesParented, string title)
        {
            var item = CategoriesParented.Select(model => model.CategoryModel.Select(index => index.Title)).ToList();

            if (item.Count >= 0)
            {
                for (var p = 0; p < item.Count; p++)
                {
                    for (var c = 0; c < item[p].Count(); c++)
                    {
                        var obj = item[p].ElementAt(c);
                        if (obj == title)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void CreateFinalCategoriesModel(List<CategorizedCategoryModel> CategoriesParented, List<CategoryModel> categoryModel)
        {
            var test = new CategorizedCategoryModel()
            {
                CategoryModel = categoryModel
            };

            CategoriesParented.Add(test);
        }
    }
}

/*
    
*/
