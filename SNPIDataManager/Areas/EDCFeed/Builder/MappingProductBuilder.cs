using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Models.NopProductsModel;
using SNPIDataManager.Models.NopProductsModel.SyncModels;
using SNPIDataManager.Models.NopProductsModel.SyncUpdateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using System.Xml;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Builder
{
    public class MappingProductBuilder : SupplierFeedHelper
    {
        private readonly ProductAttributeBuilder _ProductAttributeBuilder;
        private readonly List<MappingModel> _LoadedMapping;
        private readonly string _FeedPath;

        public MappingProductBuilder(string feedPath)
        {
            _ProductAttributeBuilder = new ProductAttributeBuilder();
            _LoadedMapping = MappingProcessor.RetrieveMapping<MappingModel>().ToList();
            _FeedPath = feedPath;
        }

        public List<JObject> SelectProductsForMapping()
        {
            var testCategory = _LoadedMapping[7];
            var supplierCategoryId = testCategory.SupplierCategoryId;
            var nodeList = SelectProductNodesByCategory(_FeedPath, supplierCategoryId).ToList();

            var jsonString = JsonConvert.SerializeObject(PopulateProductModelList(nodeList));
            var obj = JsonConvert.DeserializeObject<JArray>(jsonString).ToObject<List<JObject>>().ToList();

            return obj;
        }

        private List<ProductBody> PopulateProductModelList(List<XElement> nodeList)
        {
            var productBody = new List<ProductBody>();

            for (var i = 0; i < nodeList.Count; i++)
            {
                productBody.Add(new ProductBody()
                {
                    Product = new ProductSyncModel()
                    {
                        Gtin = _ProductAttributeBuilder.ProductGtinByVariant(nodeList[i]),
                        Sku = (string)nodeList[i].Element("artnr"),
                        Name = (string)nodeList[i].Element("title"),
                        ManageInventoryMethodId = 2,
                        DisplayStockAvailability = true,
                        DisplayStockQuantity = true,
                        AllowAddingOnlyExistingAttributeCombinations = false,
                        StockQuantity = (string)nodeList[i].Element("casecount"),
                        AdditionalShippingCharge = 0,
                        ShortDescription = (string)nodeList[i].Element("description"),
                        FullDescription = (string)nodeList[i].Element("description"),
                        MetaDescription = (string)nodeList[i].Element("description"),
                        MetaTitle = (string)nodeList[i].Element("title"),
                        IsFreeShipping = false,
                        Price = (decimal)nodeList[i].Element("price").Element("b2c"),
                        ProductCost = (decimal)nodeList[i].Element("price").Element("b2b"),
                        Weight = _ProductAttributeBuilder.CheckElementAvailabilityByDecimal(
                            nodeList[i].Element("measures").Element("weight")),
                        Length = _ProductAttributeBuilder.CheckElementAvailabilityByDecimal(
                            nodeList[i].Element("measures").Element("length")),
                        Published = true,
                        ProductType = "SimpleProduct",
                        VisibleIndividually = true,
                        AllowCustomerReviews = true,
                        Attributes = _ProductAttributeBuilder.SetAttribute(nodeList[i]),
                    }
                });

                //Console.WriteLine();
            }

            return productBody;
        }

        public List<JObject> UpdateProductModelList(int productId, int attributeCount)
        {
            var productBody = new List<ProductUpdateBody>();

            productBody.Add(new ProductUpdateBody()
            {
                Product = new ProductUpdateModel()
                { 
                    Attributes = _ProductAttributeBuilder.UpdateAttribute(productId, attributeCount)
                }
            });

            /*
            //for (var i = 0; i < nodeList.Count; i++)
            //{
            productBody.Add(new ProductBody()
            {
                Product = new ProductSyncModel()
                {
                    Attributes = _ProductAttributeBuilder.SetAttribute(null, productId),
                }
            });

            if (productId != 0 && attrId != 0 && attrValuesId != null)
            {
                foreach (var attribute in productBody)
                {
                    attribute.Product.AttributeCombinations = _ProductAttributeBuilder.SetAttributeCombinations(null, productId, attrId, attrValuesId);
                }
            }
            //}
            */

            var jsonString = JsonConvert.SerializeObject(productBody);
            var obj = JsonConvert.DeserializeObject<JArray>(jsonString).ToObject<List<JObject>>().ToList();

            return obj;
        }
    }
}
 