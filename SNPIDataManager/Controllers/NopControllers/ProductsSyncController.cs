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
using SNPIDataManager.Config;

namespace SNPIDataManager.Controllers.NopControllers.ApiControllers
{
    [Authorize]
    public class ProductsSyncController : Controller
    {
        private readonly log4net.ILog _Logger;
        private readonly NopAPIClientHelper _NopApiClientHelper;

        public ProductsSyncController()
        {
            _Logger = log4net.LogManager.GetLogger("FileAppender");
            _NopApiClientHelper = new NopAPIClientHelper(
                NopAccessHelper.AccessToken(),
                NopAccessHelper.ServerURL());
        }

        [HttpPost]
        public async Task MapProducts()
        {
            //string ab = $"/api/products";
            var productsCount = await _NopApiClientHelper.GetProductData(LocationsConfig.ReadLocations("apiProducts"));

            try
            {
                //string NopRestAPIUrl = $"/api/products";
                await _NopApiClientHelper.PostProductData(
                    RelationsHelper.MappingProductBuilder(), 
                    LocationsConfig.ReadLocations("apiProducts"));
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }
    }
}
