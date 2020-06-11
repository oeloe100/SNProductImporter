using SNPIDataManager.Models.NopCategoriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Models
{
    public class IndexViewModel
    {
        public IEnumerable<CategoriesModel> CategoriesModel {get;set;}
        public List<CategorizedCategoryModel> EDCCategoriesFiltered { get; set; }
    }
}