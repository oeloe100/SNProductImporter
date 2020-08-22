using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopProductsModel;
using SNPIHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using Newtonsoft.Json.Linq;

namespace SNPIDataManager.Controllers.NopControllers.ApiControllers
{
    [Authorize]
    public class ProductsSyncController : Controller
    {
        private readonly NopAccessHelper _NopAccessHelper;
        private readonly NopAPIClientHelper _NopApiClientHelper;

        public ProductsSyncController()
        {
            _NopAccessHelper = new NopAccessHelper();
            _NopApiClientHelper = new NopAPIClientHelper(
                _NopAccessHelper.AccessToken,
                _NopAccessHelper.ServerUrl);
        }

        [HttpPost]
        public async Task<string> MapProducts()
        {
            try
            {
                string NopRestAPIUrl = $"/api/products";

                await _NopApiClientHelper.PostProductData(RelationsHelper.MappingProductBuilder(), NopRestAPIUrl);
                var products = await _NopApiClientHelper.GetProductData(NopRestAPIUrl);

                return await UpdateSelectedProductAttributes(products);
            }
            catch (Exception ex)
            {
                var err = ex.Message + ex.StackTrace;
                return err;
            }
        }

        private async Task<string> UpdateSelectedProductAttributes(JObject products)
        {
            string updateJsonProductsUrl = $"/api/products/";
            var index = 0;

            try
            {
                foreach (var product in products["products"])
                {
                    int productId = (int)product["id"];
                    int attributeId = (int)product["attributes"][0]["id"];

                    List<int> attributeValuesIds = new List<int>();
                    foreach (var item in product["attributes"][0]["attribute_values"])
                    {
                        attributeValuesIds.Add((int)item["id"]);
                    }

                    await _NopApiClientHelper.UpdateProductData(RelationsHelper.UpdateProductProperties(
                    productId, attributeValuesIds, attributeId, index), updateJsonProductsUrl, productId);

                    index ++;
                }

                return "Done";
            }
            catch (Exception ex)
            {
                var err = ex.Message + ex.StackTrace;
                return err;
            }
        }
    }
}
