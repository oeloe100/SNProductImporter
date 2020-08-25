using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncModels
{
    public class ProductSyncUpdateModel
    {
        [JsonProperty("product_cost")]
        public decimal ProductCost { get; set; }
        [JsonProperty("images")]
        public List<ProductSyncModelImages> Images { get; set; }
    }
}