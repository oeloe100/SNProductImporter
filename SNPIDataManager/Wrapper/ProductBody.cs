using Newtonsoft.Json;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Wrapper
{
    public class ProductBody
    {
        [JsonProperty("product")]
        public ProductSyncModel Product { get; set; }
    }
}