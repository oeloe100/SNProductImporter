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
using SNPIDataManager.Areas.EDCFeed.Builder;

namespace SNPIDataManager.Controllers.NopControllers.ApiControllers
{
    [Authorize]
    public class ProductsSyncController : Controller
    {
        private readonly log4net.ILog _Logger;
        private readonly MappingProductBuilder _MappingProductBuilder;
        private readonly NopAPIClientHelper _NopApiClientHelper;

        public ProductsSyncController()
        {
            _Logger = log4net.LogManager.GetLogger("FileAppender");
            _MappingProductBuilder = new MappingProductBuilder(FeedPathHelper.Path());
            _NopApiClientHelper = new NopAPIClientHelper(
                NopAccessHelper.AccessToken(),
                NopAccessHelper.ServerURL());
        }

        [HttpPost]
        public async Task MapProducts()
        {
            var productsCount = await _NopApiClientHelper.GetProductData(LocationsConfig.ReadLocations("apiProducts"));

            try
            {
                await _NopApiClientHelper.PostProductData(
                    _MappingProductBuilder.SelectProductsForMappingByCategory(), 
                    LocationsConfig.ReadLocations("apiProducts"));
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }
    }
}
