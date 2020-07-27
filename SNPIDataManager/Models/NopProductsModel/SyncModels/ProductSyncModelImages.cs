using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncModels
{
    public class ProductSyncModelImages
    {
        [JsonProperty("src")]
        public string Src { get; set; }
        [JsonProperty("attachment")]
        public string Attachment { get; set; }
    }
}