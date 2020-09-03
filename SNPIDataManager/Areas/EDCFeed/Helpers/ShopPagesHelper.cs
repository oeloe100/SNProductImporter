using Newtonsoft.Json.Linq;
using SNPIDataManager.Config;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIHelperLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public static class ShopPagesHelper
    {
        private static readonly NopAccessHelper _NopAccessHelper;
        private static readonly NopAPIClientHelper _NopApiClientHelper;

        static ShopPagesHelper()
        {
            _NopAccessHelper = new NopAccessHelper();
            _NopApiClientHelper = new NopAPIClientHelper(
                _NopAccessHelper.AccessToken,
                _NopAccessHelper.ServerUrl);
        }

        public static async Task<double> ReturnsShopPageCount()
        {
            //First retrieve total amount of products in shop
            //string NopRestAPICountUrl = $"/api/products/count";
            var productCountAsJson = await _NopApiClientHelper.GetProductCount(LocationsConfig.ReadLocations("apiProductsCount"));

            //Total amount of products in double format. 
            //Also the maximum allow products count per page in double format.
            double productCount = (double)productCountAsJson["count"];
            double maxProductsOnPage = 50;

            //Use math.ceiling to round up the amount of products. a.e. 122/50 = 2,44 pages > round up to 3 pages.
            return Math.Ceiling(productCount / maxProductsOnPage);
        }

        public static Task<JObject> ReturnsProductsByPage(double page)
        {
            //string NopRestAPIUrl = $"/api/products?page=" + page + "";
            string NopRestAPIUrl = LocationsConfig.ReadLocations("apiProductsPage") + page;

            return _NopApiClientHelper.GetProductData(NopRestAPIUrl);
        }
    }
}