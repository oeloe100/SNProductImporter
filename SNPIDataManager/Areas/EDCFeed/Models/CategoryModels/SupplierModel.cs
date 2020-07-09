using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Models.CategoryModels
{
    public class SupplierModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Layer { get; set; }
        public string ParentId { get; set; }
        public string ParentTitle { get; set; }
    }
}