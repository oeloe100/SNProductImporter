using Newtonsoft.Json;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductUpdateModelAttributeValues
    {
        [JsonProperty("type_id")]
        public int? AttributeValueTypeId { get; set; }
        [JsonProperty("associated_product_id")]
        public int? AssociatedProductId { get; set; }

        [JsonProperty("type")]
        public string AttributeValueType { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}