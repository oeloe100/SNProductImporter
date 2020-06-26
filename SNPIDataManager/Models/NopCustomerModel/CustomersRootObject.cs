using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Models.NopCustomerModel
{
    public class CustomersRootObject
    {
        [JsonProperty("customers")]
        public List<CustomerModel> Customers { get; set; }
    }
}