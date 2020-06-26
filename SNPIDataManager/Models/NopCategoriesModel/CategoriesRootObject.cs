using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopCategoriesModel
{
    public class CategoriesRootObject
    {
        [JsonProperty("categories")]
        public List<CategoriesModel> Categories { get; set; }
    }
}