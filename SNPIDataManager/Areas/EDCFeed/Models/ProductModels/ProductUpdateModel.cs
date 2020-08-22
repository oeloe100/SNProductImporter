using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Models.ProductModels
{
    public static class ProductUpdateModel
    {
        public static string Sku { get; set; }
        public static string Gtin { get; set; }
        public static Decimal Price { get; set; }
        public static Decimal OldPrice { get; set; }
        public static Decimal ProductCost { get; set; }

        public static string ProductAttributeCombinations_Sku { get; set; }
        public static string ProductAttributeCombinations_Gtin { get; set; }
    }
}