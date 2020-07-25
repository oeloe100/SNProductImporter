using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductUpdateModelAttributes
    {
        [JsonProperty("attribute_values")]
        public List<ProductUpdateModelAttributeValues> AttributeValues { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}