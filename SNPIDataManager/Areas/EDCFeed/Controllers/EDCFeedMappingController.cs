using Newtonsoft.Json;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.DataAccess;
using SNPIDataLibrary.Models;
using SNPIDataManager.Areas.EDCFeed.Controllers.API;
using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using SNPIDataManager.Areas.EDCFeed.Models.ProductModels;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopCategoriesModel;
using SNPIDataManager.Models.NopProductsModel;
using SNPIHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace SNPIDataManager.Areas.EDCFeed.Controllers
{
    [Authorize]
    public class EDCFeedMappingController : Controller
    {
        NopAccessHelper NopAccessHelper;
        public EDCFeedMappingController()
        {
            NopAccessHelper helper = new NopAccessHelper();
            NopAccessHelper = helper;
        }

        // GET: EDCFeed/ProductSync
        public async Task<ActionResult> Index()
        {
            var nopCategoriesDict = await NopShopCategorizationHelper.NopCategoriesResource(NopAccessHelper.accessToken, NopAccessHelper.serverUrl);

            var InventoryDataController = new InventoryDataController();
            var edcCategoriesDict = InventoryDataController.CategoryBuilder();

            List<CategoriesViewModel> model = new List<CategoriesViewModel>();

            var categoriesViewModel = new CategoriesViewModel()
            {
                NopCategoriesDict = nopCategoriesDict,
                EDCCategoriesDict = edcCategoriesDict
            };

            model.Add(categoriesViewModel);

            return View(model);
        }
    }
}
