using SNPIDataManager.Models.NopCategoriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Models
{
    public class IndexViewModel
    {
        public IDictionary<string, List<CategoriesModel>> NopCategoriesModel {get;set;}
        public IDictionary<string, List<string>> EDCCategoriesFiltered { get; set; }
    }
}