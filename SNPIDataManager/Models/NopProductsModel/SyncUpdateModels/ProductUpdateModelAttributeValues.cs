using Newtonsoft.Json;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductUpdateModelAttributeValues
    {
        [JsonProperty("associated_product_id")]
        public int AssociatedProductId { get; set; }
    }
}