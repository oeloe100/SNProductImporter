using SNPIDataManager.Models.NopCategoriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Models.CategoryModels
{
    public class CategoriesViewModel
    {
        public IDictionary<string, List<CategoriesModel>> NopCategoriesDict {get;set;}
        public IDictionary<string, List<SupplierModel>> EDCCategoriesDict { get; set; }
    }
}