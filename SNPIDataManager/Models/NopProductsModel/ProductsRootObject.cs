using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopProductsModel
{
    public class ProductsRootObject
    {
        [JsonProperty("products")]
        public List<ProductsModel> Customers { get; set; }
    }
}