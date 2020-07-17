using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Models.CategoryModels
{
    public class SupplierModel
    {
        public string RootId { get; set; }
        public string RootTitle { get; set; }
        public string ChildId { get; set; }
        public string ChildTitle { get; set; }
        public int ProductCount { get; set; }
        public int Layer { get; set; }
    }
}