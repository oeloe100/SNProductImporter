using SNPIDataManager.Areas.EDCFeed.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Filter
{
    public class ProductSpecificationAttributeFilter : SupplierFeedHelper
    {
        private readonly string _FeedPath;

        public ProductSpecificationAttributeFilter(string feedPath)
        {
            _FeedPath = feedPath;
        }

        public IDictionary<string, List<string>> FilterProductSpecificationAttributes()
        {
            var attributeWithValueVariations = RetrieveProductSpecificationAttributes(_FeedPath);

            return attributeWithValueVariations;
        }
    }
}