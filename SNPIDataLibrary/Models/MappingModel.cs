using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataLibrary.Models
{
    public class MappingModel
    {
        public string rowId { get; set; }
        public string id { get; set; }
        public string shopCategory { get; set; }
        public string supplierCategory { get; set; }
        public string shopCategoryId { get; set; }
        public string supplierCategoryId {get; set;}
    }
}