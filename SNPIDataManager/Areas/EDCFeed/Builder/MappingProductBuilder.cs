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
        private List<XElement> _NodeList;
        private readonly ProductImageBuilder _ProductImageBuilder;
        private readonly ProductAttributeBuilder _ProductAttributeBuilder;
        private readonly List<MappingModel> _LoadedMapping;
        private readonly string _FeedPath;

        public MappingProductBuilder(string feedPath)
        {
            _NodeList = new List<XElement>();
            _ProductImageBuilder = new ProductImageBuilder();
            _ProductAttributeBuilder = new ProductAttributeBuilder();
            _LoadedMapping = MappingProcessor.RetrieveMapping<MappingModel>().ToList();
            _FeedPath = feedPath;
        }

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger("FileAppender");

        public Dictionary<string, List<JObject>> SelectProductsForMappingByCategory()
        {
            var productCategoryCollection = new Dictionary<string, List<JObject>>();

            for (var i = 0; i < _LoadedMapping.Count; i++)
            {
                var productNodeList = SelectProductNodesByCategory(_FeedPath, _LoadedMapping[i].SupplierCategoryId).ToList();
                var productAsJsonString = JsonConvert.SerializeObject(PopulateProductModelList(productNodeList));
                var productListAsJobject = JsonConvert.DeserializeObject<JArray>(productAsJsonString).ToObject<List<JObject>>().ToList();

                productCategoryCollection.Add(_LoadedMapping[i].ShopCategory, productListAsJobject);

                _Logger.Debug("Collected Category: " + i + "");
            }

            _Logger.Debug("Successfully created product collection");
            return productCategoryCollection;
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
                        Images = _ProductImageBuilder.SelectImageProperties(nodeList[i]),
                        Attributes = _ProductAttributeBuilder.SetAttribute(nodeList[i]),
                    }
                });
            }

            return productBody;
        }

        public JObject UpdateProductWithAttributes(int productId, List<int> attributeValuesId, int id, int index)
        {
            AddNodesToNodelist();

            var productBody = new ProductUpdateBody();
            productBody.Product = new ProductUpdateModel()
            {
                Attributes = _ProductAttributeBuilder.UpdateAttribute(productId, attributeValuesId, id),
                AttributeCombinations = _ProductAttributeBuilder.SetAttributeCombinations(_NodeList[index], productId, attributeValuesId, id),
            };

            var jsonString = JsonConvert.SerializeObject(productBody);
            var obj = JsonConvert.DeserializeObject<JObject>(jsonString);

            return obj;
        }

        private void AddNodesToNodelist()
        {
            for (var i = 0; i < _LoadedMapping.Count; i++)
            {
                var productNodeList = SelectProductNodesByCategory(
                    _FeedPath,
                    _LoadedMapping[i].SupplierCategoryId).ToList();

                foreach (var productNode in productNodeList)
                    _NodeList.Add(productNode);
            }
        }
    }
}
 