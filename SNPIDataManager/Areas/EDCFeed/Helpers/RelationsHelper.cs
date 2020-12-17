﻿using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Builder;
using SNPIDataManager.Areas.EDCFeed.Filter;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using SNPIDataManager.Areas.EDCFeed.Models.ProductSpecificationModels;
using SNPIDataManager.Config;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopProductsModel;
using SNPIHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public class RelationsHelper
    {
        private static MappingProductBuilder _MappingProductBuilder;
        private static ProductSpecificationAttributeFilter _ProductSpecsAttributeFilter;
        private static readonly NopAPIClientHelper _NopApiClientHelper;
        private static ScheduledProductUpdateBuilder scheduledProductUpdateBuilder;

        static RelationsHelper()
        {
            _MappingProductBuilder = new MappingProductBuilder(FeedPathHelper.Path());
            _ProductSpecsAttributeFilter = new ProductSpecificationAttributeFilter(FeedPathHelper.Path());

            _NopApiClientHelper = new NopAPIClientHelper(
                NopAccessHelper.AccessToken(),
                NopAccessHelper.ServerURL());

            scheduledProductUpdateBuilder = new ScheduledProductUpdateBuilder(
                NopAccessHelper.AccessToken(),
                NopAccessHelper.ServerURL());
        }

        /// <summary>
        /// Update Selected product Properties
        /// </summary>
        /// <returns></returns>
        public static JObject UpdateProductProperties(int productId, List<int> attributeValuesId, int id, int index)
        {
            var testData = _MappingProductBuilder.UpdateProductWithAttributes(productId, attributeValuesId, id, index);

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

        public static async Task UpdateProductPropertiesScheduled()
        {
            //First retrieve total amount of products in shop
            //string NopRestAPICountUrl = $"/api/products/count";
            var productCountAsJson = await _NopApiClientHelper.GetProductCount(LocationsConfig.ReadLocations("apiProductsCount"));

            //Total amount of products in double format. 
            //Also the maximum allow products count per page in double format.
            double productCount = (double)productCountAsJson["count"];
            double maxProductsOnPage = 50;

            //Use math.ceiling to round up the amount of products. a.e. 122/50 = 2,44 pages > round up to 3 pages.
            var pageCount = Math.Ceiling(productCount / maxProductsOnPage);

            for (var i = 1; i <= pageCount; i++)
            {
                //string NopRestAPIUrl = $"/api/products?page=" + i + "";

                var products = await _NopApiClientHelper.GetProductData(LocationsConfig.ReadLocations("apiProductsPage") + i);

                await scheduledProductUpdateBuilder.UpdateProductData(products, FeedPathHelper.Path());
            }
        }

        public static async Task UpdateProductStockScheduled(Uri stockFeedUri)
        {
            await scheduledProductUpdateBuilder.UpdateProductStock(stockFeedUri);
        }
    }
}