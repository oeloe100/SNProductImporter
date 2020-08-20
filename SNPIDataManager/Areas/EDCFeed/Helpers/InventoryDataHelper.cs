using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Builder;
using SNPIDataManager.Areas.EDCFeed.Filter;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using SNPIDataManager.Areas.EDCFeed.Models.ProductSpecificationModels;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public class InventoryDataHelper
    {
        private static readonly XmlDocument _EDCFeed;
        private static XmlElement _Root;
        private static readonly string _FeedPath;
        private static SupplierCategoryBuilder _SupplierCategoryBuilder;
        private static MappingProductBuilder _MappingProductBuilder;
        private static ProductSpecificationAttributeFilter _ProductSpecsAttributeFilter;

        static InventoryDataHelper()
        {
            _EDCFeed = new XmlDocument();

            var currentDate = DateTime.Now;
            var shortDate = currentDate.Date.ToShortDateString();

            _FeedPath = @"C:\Users\sexxnation\source\repos\Sexxnation\Product Importer\SNProductImporter\SNPIDataManager\FeedDownloads\EDCFeed" + shortDate + ".xml";
            
            _EDCFeed.Load(_FeedPath);
            _Root = _EDCFeed.DocumentElement;
            
            _SupplierCategoryBuilder = new SupplierCategoryBuilder(_Root, _FeedPath);
            _MappingProductBuilder = new MappingProductBuilder(_FeedPath);
            _ProductSpecsAttributeFilter = new ProductSpecificationAttributeFilter(_FeedPath);
        }

        /// <summary>
        /// Build Supplier Categories and return to View.
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, List<SupplierModel>> CategoryBuilder()
        {
            var items = _SupplierCategoryBuilder.RelationToView;

            return items;
        }

        /// <summary>
        /// Build Product Models to sync with the shop.
        /// </summary>
        /// <returns></returns>
        public static List<JObject> MappingProductBuilder()
        {
            var testData = _MappingProductBuilder.SelectProductsForMapping();

            return testData;
        }

        /// <summary>
        /// Update Selected product Properties
        /// </summary>
        /// <returns></returns>
        public static JObject UpdateProductProperties(int productId, List<int> attributeValuesId, int id, int index)
        {
            var testData = _MappingProductBuilder.UpdateProductModelList(productId, attributeValuesId, id, index);

            return testData;
        }


        /// <summary>
        /// Retrieve Product Specification Attributes And Filter (Multiples)
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, List<string>> ProductSpecificationAttributesSelector()
        {
            var testData = _ProductSpecsAttributeFilter.FilterProductSpecificationAttributes();

            return testData;
        }
    }
}