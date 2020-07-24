﻿using Newtonsoft.Json;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel.SyncModels
{
    public class ProductBody
    {
        [JsonProperty("product")]
        public ProductSyncModel Product { get; set; }
    }
}