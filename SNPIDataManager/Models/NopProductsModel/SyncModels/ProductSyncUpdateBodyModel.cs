using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncModels
{
    public class ProductSyncUpdateBodyModel
    {
        [JsonProperty("product")]
        public ProductSyncUpdateModel Product { get; set; }
    }
}