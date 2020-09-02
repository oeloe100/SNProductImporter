using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductStockUpdateQtyModel
    {
        [JsonProperty("stock_quantity")]
        public int? StockQuantity { get; set; }
    }
}