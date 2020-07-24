using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncModels
{
    public class ProductSyncModelAttributeValues
    {
        [JsonProperty("type_id")]
        public int TypeId { get; set; }
        [JsonProperty("associated_product_id")]
        public int AssociatedProductId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("color_squares_rgb")]
        public string ColorSquaresRGB { get; set; }
        [JsonProperty("price_adjustment")]
        public decimal PriceAdjustment { get; set; }
        [JsonProperty("weight_adjustment")]
        public decimal WeightAdjustment { get; set; }
        [JsonProperty("cost")]
        public decimal Cost { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("is_pre_selected")]
        public bool IsPreSeleted { get; set; }
        [JsonProperty("display_order")]
        public int DisplayOrder { get; set; }
        [JsonProperty("product_image_id")]
        public int ProductPictureId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}