using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Models.ProductSpecificationModels
{
    public class ProductSpecificationAttributeDataModel
    {
        public IDictionary<string, List<string>> ProductSpecAttributesWithValues { get; set; }
    }
}