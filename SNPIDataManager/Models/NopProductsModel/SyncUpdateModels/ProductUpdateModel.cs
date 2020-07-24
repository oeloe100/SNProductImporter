using Newtonsoft.Json;
using SNPIDataManager.Models.NopProductsModel.SyncModels;
using System.Collections.Generic;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductUpdateModel
    {
        [JsonProperty("attributes")]
        public List<ProductUpdateModelAttributes> Attributes { get; set; }

        [JsonProperty("product_attribute_combinations")]
        public List<ProductSyncModelAttributeCombinations> AttributeCombinations { get; set; }
    }
}