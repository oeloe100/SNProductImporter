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
                string jsonUrl = $"/api/products";
                await _NopApiClientHelper.Post(InventoryDataHelper.MappingProductBuilder(), jsonUrl);

                return "Done!";
            }
            catch (Exception ex)
            {
                var err = ex.Message + ex.StackTrace;
                return err;
            }
        }
    }
}
