﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncUpdateModels
{
    public class ProductStockUpdateModel
    {
        [JsonProperty("product_attribute_combinations")]
        public List<ProductStockUpdateQtyModel> ProductStockQuantity { get; set; }
    }
}