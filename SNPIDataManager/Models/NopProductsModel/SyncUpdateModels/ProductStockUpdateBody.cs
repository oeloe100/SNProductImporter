using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductStockUpdateBody
    {
        [JsonProperty("product")]
        public ProductStockUpdateModel Product { get; set; }
    }
}