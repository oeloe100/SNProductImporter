using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopCategoriesModel
{
    public class CategoriesModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_category_id")]
        public string ParentId { get; set; }

        public IDictionary<string, CategoriesModel> NestedModel { get; set; }
    }
}