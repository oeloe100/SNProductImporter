using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductStockUpdateModelAttributeCombinations
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("product_id")]
        public int ProductId { get; set; }
        [JsonProperty("attributes_xml")]
        public string AttributesXml { get; set; }
        [JsonProperty("stock_quantity")]
        public int StockQuantity { get; set; }
        [JsonProperty("sku")]
        public string Sku { get; set; }
        [JsonProperty("gtin")]
        public string Gtin { get; set; }
    }
}