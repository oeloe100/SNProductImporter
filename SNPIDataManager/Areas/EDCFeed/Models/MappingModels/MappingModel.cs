using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Models.MappingModels
{
    public class MappingModel
    {
        public string id { get; set; }
        public string shopCategory { get; set; }
        public string supplierCategory { get; set; }
        public string shopId { get; set; }
        public string supplierId {get; set;}
    }
}