using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Areas.EDCFeed.Controllers.API;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using SNPIDataManager.Helpers;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIHelperLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using SNPIDataLibrary.DataAccess;
using System.Text;
using SNPIDataManager.Setup;

namespace SNPIDataManager.Areas.EDCFeed.Controllers
{
    [Authorize]
    public class EDCFeedMappingController : Controller
    {
        private UserInformation _UserInformation;
        private NopAccessHelper _NopAccessHelper;
        private NopAccessSetup _NopAccessSetup;

        public EDCFeedMappingController()
        {
            _NopAccessHelper = new NopAccessHelper();
            _UserInformation = new UserInformation();
            _NopAccessSetup = new NopAccessSetup();
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var nopCategoriesDict = await NopShopCategorizationHelper.NopCategoriesResource(_NopAccessHelper.AccessToken, _NopAccessHelper.ServerUrl);

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
            catch (Exception ex)
            {
                if (_NopAccessSetup.IsSetup())
                    return View("~/Views/Home/Index.cshtml");
                else
                    return View(ex);
            }
        }

        [HttpPost]
        public JsonResult InsertMappingModel(List<CategoryModel> model)
        {
            try
            {
                var mappingModel = CategoryToMappingModel(model);
                mappingModel.Id = _UserInformation.UserId();
                var insertMapping = MappingProcessor.InsterCreatedMapping(mappingModel);

                return Json(insertMapping);
            }
            catch (Exception ex)
            {
                return Json(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet]
        public ActionResult DisplayMappings() 
        {
            try
            {
                var retrieveMapping = MappingProcessor.RetrieveMapping<MappingModel>();
                return View(retrieveMapping);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [HttpPost]
        public string DeleteMapping(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();

            try 
            {
                var data = SQLDataAccess.DeleteMapping(id, "SNPI_Mappings_db");
                stringBuilder.AppendFormat("Deleted Row {0}", id);
                return stringBuilder.ToString();
            } 
            catch (Exception ex)
            {
                return ex.Message + ex.StackTrace;
            }
        }

        [HttpPost]
        public JsonResult DeleteMappings(List<int> idList)
        {
            if (idList != null)
            {
                try
                {
                    var data = SQLDataAccess.DeleteMappings(idList, "SNPI_Mappings_db");
                    return Json("Deleted The following Mappings: " + idList);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message + ex.StackTrace);
                }
            }
            else
            {
                return null;
            }
        }

        private MappingModel CategoryToMappingModel(List<CategoryModel> model)
        {
            var mappingModel = new MappingModel();

            foreach (var attribute in model)
            {
                switch (attribute.Vendor)
                {
                    case "shop":
                        mappingModel.ShopCategory = attribute.Title;
                        mappingModel.ShopCategoryId = attribute.Id;
                        break;
                    case "supplier":
                        mappingModel.SupplierCategory = attribute.Title;
                        mappingModel.SupplierCategoryId = attribute.Id;
                        break;
                }
            }

            return mappingModel;
        }
    }
}
