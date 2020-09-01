using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopProductsModel.SyncModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

                    await _NopApiClientHelper.UpdateProductData(jObjectModel, $"/api/products/", _ProductId);
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
            //Product index
            var pI = 0;

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
                        var productId = (int)productsListed[x]["id"];
                        var productAttributeCombos = productsListed[x]["product_attribute_combinations"];
                        
                        foreach (var productAttributeCombo in productAttributeCombos)
                        {
                            var productAttributeSku = (string)productAttributeCombo["sku"];
                            var productNodes = SelectProductQtyNodeById(productAttributeSku, stockFeedUri.ToString());

                            if (productNodes != null)
                            {
                                var qtyNodeAsXElement = XElement.Parse(productNodes.ToString());
                                var qty = (int)qtyNodeAsXElement;

                                //Create model to and convert to Jobject. Send data in method below >>
                                await _NopApiClientHelper.UpdateProductData(new JObject(), $"/api/products/", productId);
                            }

                            Console.WriteLine();
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }

            Console.WriteLine();
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