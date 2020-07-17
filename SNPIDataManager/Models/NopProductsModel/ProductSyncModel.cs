using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel
{
    public class ProductSyncModel
    {
        [JsonProperty("gtin")]
        public string Gtin { get; set; }
        [JsonProperty("sku")]
        public string Sku { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }
        [JsonProperty("full_description")]
        public string FullDescription { get; set; }

        [JsonProperty("manage_inventory_method_id")]
        public int ManageInventoryMethodId { get; set; }
        [JsonProperty("display_stock_availability")]
        public bool DisplayStockAvailability { get; set; }
        [JsonProperty("display_stock_quantity")]
        public bool DisplayStockQuantity { get; set; }
        [JsonProperty("stock_quantity")]
        public string StockQuantity { get; set; }

        [JsonProperty("meta_keywords")]
        public string MetaKeywords { get; set; }
        [JsonProperty("meta_description")]
        public string MetaDescription { get; set; }
        [JsonProperty("meta_title")]
        public string MetaTitle { get; set; }

        [JsonProperty("is_free_shipping")]
        public bool IsFreeShipping { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("old_price")]
        public decimal OldPrice { get; set; }
        [JsonProperty("product_cost")]
        public decimal ProductCost { get; set; }
        [JsonProperty("special_price")]
        public decimal SpecialPrice { get; set; }
        
        [JsonProperty("weight")]
        public decimal Weight { get; set; }
        [JsonProperty("length")]
        public decimal Length { get; set; }
        [JsonProperty("width")]
        public decimal Width { get; set; }
        [JsonProperty("height")]
        public decimal Height { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }
    }
}