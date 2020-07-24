using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductUpdateModelAttributes
    {
        [JsonProperty("product_attribute_id")]
        public int ProductAttributeId { get; set; }
        [JsonProperty("product_attribute_name")]
        public string ProductAttributeName { get; set; }
        [JsonProperty("text_prompt")]
        public string TextPrompt { get; set; }
        [JsonProperty("is_required")]
        public bool IsRequired { get; set; }
        [JsonProperty("attribute_control_type_id")]
        public int AttributeControlTypeId { get; set; }
        [JsonProperty("display_order")]
        public int DisplayOrder { get; set; }
        [JsonProperty("default_value")]
        public string DefaultValue { get; set; }
        [JsonProperty("attribute_control_type_name")]
        public string AttributeControlTypeName { get; set; }

        [JsonProperty("attribute_values")]
        public List<ProductUpdateModelAttributeValues> AttributeValues { get; set; }
    }
}