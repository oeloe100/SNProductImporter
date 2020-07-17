using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Models.NopProductsModel;
using SNPIDataManager.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Builder
{
    public class MappingProductBuilder : SupplierFeedHelper
    {
        //private ProductWrapper _ProductWrapper;
        private readonly List<MappingModel> _LoadedMapping;
        private readonly string _FeedPath;

        public MappingProductBuilder(string feedPath)
        {
            //_ProductWrapper = new ProductWrapper();
            //_ProductWrapper.ProductBody = new List<ProductBody>();

           _LoadedMapping = MappingProcessor.RetrieveMapping<MappingModel>().ToList();
            _FeedPath = feedPath;
        }

        public List<JObject> SelectProductsForMapping()
        {
            var testCategory = _LoadedMapping[0];
            var supplierCategoryId = testCategory.SupplierCategoryId;
            var nodeList = SelectProductNodesByCategory(_FeedPath, supplierCategoryId).ToList();

            var jsonString = JsonConvert.SerializeObject(PopulateProductModelList(nodeList));
            var obj = JsonConvert.DeserializeObject<JArray>(jsonString).ToObject<List<JObject>>().ToList();

            return obj;
        }

        public List<ProductBody> PopulateProductModelList(List<XElement> nodeList)
        {
            var productBody = new List<ProductBody>();

            for (var i = 0; i < nodeList.Count; i++)
            {
                productBody.Add(new ProductBody()
                {
                    Product = new ProductSyncModel()
                    {
                        Sku = (string)nodeList[i].Element("artnr"),
                        Name = (string)nodeList[i].Element("title"),
                        ManageInventoryMethodId = 1,
                        DisplayStockAvailability = true,
                        DisplayStockQuantity = true,
                        StockQuantity = (string)nodeList[i].Element("casecount"),
                        ShortDescription = (string)nodeList[i].Element("description"),
                        FullDescription = (string)nodeList[i].Element("description"),
                        MetaDescription = (string)nodeList[i].Element("description"),
                        MetaTitle = (string)nodeList[i].Element("title"),
                        IsFreeShipping = false,
                        Price = (decimal)nodeList[i].Element("price").Element("b2c"),
                        ProductCost = (decimal)nodeList[i].Element("price").Element("b2b"),
                        Weight = (decimal)nodeList[i].Element("measures").Element("weight"),
                        Length = CheckElementAvailabilityByDecimal(
                            nodeList[i].Element("measures").Element("length")),
                        Published = true
                    }
                });

                //Console.WriteLine();
            }

            return productBody;
        }

        private decimal CheckElementAvailabilityByDecimal(XElement element)
        {
            if (element != null)
                return (decimal)element;
            else
                return 0;
        }
    }
}
 