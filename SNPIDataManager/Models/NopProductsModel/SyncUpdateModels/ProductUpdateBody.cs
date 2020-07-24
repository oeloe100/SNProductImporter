using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductUpdateBody
    {
        [JsonProperty("product")]
        public ProductUpdateModel Product { get; set; }
    }
}