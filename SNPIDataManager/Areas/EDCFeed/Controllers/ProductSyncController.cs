﻿using Newtonsoft.Json;
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

namespace SNPIDataManager.Areas.EDCFeed.Controllers
{
    public class ProductSyncController : Controller
    {
        private string accessToken;
        private string serverUrl;

        NopShopCategorizationHelper nopShopCategoriesHelper = new NopShopCategorizationHelper();

        // GET: EDCFeed/ProductSync
        public async Task<ActionResult> Index()
        {
            accessToken = new NopQuickAccess().NopAccessToken();
            serverUrl = new NopQuickAccess().NopServerUrl();

            var nopCategoriesDict = await nopShopCategoriesHelper.NopCategoriesResource(accessToken, serverUrl);

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
