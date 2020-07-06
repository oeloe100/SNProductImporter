using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataLibrary.Models
{
    public class MappingModel
    {
        public string RowId { get; set; }
        public string Id { get; set; }
        public string ShopCategory { get; set; }
        public string SupplierCategory { get; set; }
        public string ShopCategoryId { get; set; }
        public string SupplierCategoryId {get; set;}
    }
}