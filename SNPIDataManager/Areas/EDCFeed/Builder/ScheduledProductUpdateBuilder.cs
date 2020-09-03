using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Config;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopProductsModel.SyncModels;
using SNPIDataManager.Models.NopProductsModel.SyncUpdateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Builder
{
    public class ScheduledProductUpdateBuilder : SupplierFeedHelper
    {
        private readonly log4net.ILog _Logger;
        private string _AccessToken { get; set; }
        private string _ServerUrl { get; set; }
        private int _ProductId { get; set; }

        private readonly NopAPIClientHelper _NopApiClientHelper;

        public ScheduledProductUpdateBuilder(string accessToken, string serverUrl)
        {
            _Logger = log4net.LogManager.GetLogger("FileAppender");
            _AccessToken = accessToken;
            _ServerUrl = serverUrl;

            _NopApiClientHelper = new NopAPIClientHelper(
                _AccessToken,
                _ServerUrl);
        }

        /// <summary>
        /// Currently only updating product_cost & Images. This due to Seo >>
        /// If product gets fully updated, changes like descriptions and meta data will be set to factory/default. Changed to supplier data.
        /// </summary>
        /// <param name="productsRaw"></param>
        /// <param name="feedPath"></param>
        /// <returns></returns>
        public async Task UpdateProductData(JObject productsRaw, string feedPath)
        {
            foreach (var product in productsRaw["products"])
            {
                var productSku = (string)product["sku"];
                _ProductId = (int)product["id"];

                var supplierProduct = SelectProductNodesById(productSku, feedPath).ToList();

                try
                {
                    var productImageBuilder = new ProductImageBuilder();
                    var updateSyncBodyModel = new ProductSyncUpdateBodyModel();

                    for (var i = 0; i < supplierProduct.Count; i++)
                    {
                        var supplierProductCost = (decimal)supplierProduct[i].Element("price").Element("b2b");

                        updateSyncBodyModel.Product = new ProductSyncUpdateModel()
                        {
                            ProductCost = supplierProductCost,
                            Images = productImageBuilder.SelectImageProperties(supplierProduct[i])
                        };
                    }

                    var jObjectModel = (JObject)JToken.FromObject(updateSyncBodyModel);

                    await _NopApiClientHelper.UpdateProductData(jObjectModel,
                        LocationsConfig.ReadLocations("apiProducts"), 
                        _ProductId);

                    _Logger.Debug("Updated product: " + _ProductId);
                }
                catch (Exception ex)
                {
                    _Logger.Error(ex);
                }
            }
        }

        public async Task UpdateProductStock(Uri stockFeedUri)
        {
            //Retrieve product node from supplier feed
            string stringStockFeed = stockFeedUri.ToString();
            XElement stockFeedData = XElement.Load(stringStockFeed);
            var supplierProductNodes = stockFeedData.Elements("product");

            //Get amount of product pages in shop
            double pageCount = await ShopPagesHelper.ReturnsShopPageCount();

            for (var i = 1; i <= pageCount;)
            {
                var shopProductsByPage = await ShopPagesHelper.ReturnsProductsByPage(i);

                foreach (var productsNode in shopProductsByPage)
                {
                    var productsListed = productsNode.Value.ToList();
                    for (var x = 0; x < productsListed.Count; x++)
                    {
                        try
                        {
                            var productId = (int)productsListed[x]["id"];
                            var productAttributeCombos = productsListed[x]["product_attribute_combinations"];

                            ProductStockUpdateBody productUpdateBody = new ProductStockUpdateBody()
                            {
                                Product = new ProductStockUpdateModel()
                                {
                                    AttributeCombinations = CreateProductAttributeCombinationsWithQtyUpdated(productAttributeCombos, stockFeedUri)
                                }
                            };

                            var productQtyUpdateModelAsJObject = JObject.FromObject(productUpdateBody);

                            await _NopApiClientHelper.UpdateProductData(productQtyUpdateModelAsJObject,
                                LocationsConfig.ReadLocations("apiProducts"), 
                                productId);

                            _Logger.Debug("Product with id " + productId + " stock updated");

                            if (x >= productsListed.Count && i != pageCount)
                            {
                                i++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _Logger.Error(ex);
                        }
                    }
                }
            }
        }

        private List<ProductStockUpdateModelAttributeCombinations> CreateProductAttributeCombinationsWithQtyUpdated(JToken productAttributeCombos, Uri stockFeedUri)
        {
            var productAttrComboList = new List<ProductStockUpdateModelAttributeCombinations>();

            foreach (var productAttributeCombo in productAttributeCombos)
            {
                var productAttributeSku = (string)productAttributeCombo["sku"];
                var productNodes = SelectProductQtyNodeById(productAttributeSku, stockFeedUri.ToString());

                var qtyNodeAsXElement = XElement.Parse(productNodes.ToString());
                var qty = (int)qtyNodeAsXElement;

                var productAttrComboModel = new ProductStockUpdateModelAttributeCombinations()
                {
                    Id = (int)productAttributeCombo["id"],
                    ProductId = (int)productAttributeCombo["product_id"],
                    AttributesXml = (string)productAttributeCombo["attributes_xml"],
                    Gtin = (string)productAttributeCombo["gtin"],
                    Sku = (string)productAttributeCombo["sku"],
                    StockQuantity = qty
                };

                productAttrComboList.Add(productAttrComboModel);
            }

            return productAttrComboList;
        }

        private string SupplierProductEAN(IEnumerable<XElement> nodes)
        {
            foreach (var node in nodes)
            {
                return (string)node.Element("ean");
            }

            return null;
        }
    }
}