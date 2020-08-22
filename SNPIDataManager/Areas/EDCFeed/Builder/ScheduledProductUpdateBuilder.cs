using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Areas.EDCFeed.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace SNPIDataManager.Areas.EDCFeed.Builder
{
    public class ScheduledProductUpdateBuilder : SupplierFeedHelper
    {
        public void UpdateProductData(JObject productsRaw, string feedPath)
        {
            var shopProducts = productsRaw["products"][0];
            var productsSku = (string)shopProducts["sku"];

            var supplierFeedProducutsNodes = SelectProductNodesById(productsSku, feedPath).ToList();
            
            ValidateProductData(shopProducts, supplierFeedProducutsNodes);
        }

        public bool ValidateProductData(JToken shopProducts, List<XElement> supplierProducts)
        {
            for (var i = 0; i < supplierProducts.Count; i++)
            {
                bool isSameSku = (string)shopProducts["sku"] == (string)supplierProducts[i].Element("artnr");
                if (isSameSku)
                    Console.WriteLine();

                Console.WriteLine();
            }

            return true;
        }
    }
}