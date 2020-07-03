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

namespace SNPIDataManager.Areas.EDCFeed.Controllers
{
    [Authorize]
    public class EDCFeedMappingController : Controller
    {
        //private APIAuthMiddelwareHelper _APIAuthMiddelwareHelper;
        private UserInformation _UserInformation;
        private MappingProcessor _MappingProcessor;

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

        [Authorize]
        public JsonResult InsertMappingModel(List<CategoryModel> model)
        {
            Console.WriteLine();
            if (Session["ApplicationLoginToken"] != null)
            {
                //_APIAuthMiddelwareHelper = new APIAuthMiddelwareHelper(Session["ApplicationLoginToken"].ToString());
                _UserInformation = new UserInformation();
                _MappingProcessor = new MappingProcessor();

                try
                {
                    var mappingModel = CategoryToMappingModel(model);
                    mappingModel.id = _UserInformation.UserId();
                    _MappingProcessor.InsterCreatedMapping(mappingModel);

                    return Json(_MappingProcessor);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message + ex.StackTrace);
                }
            }
            else
            {
                return Json("Logout");
            }
        }

        public ActionResult DisplayMappings() 
        {
            _UserInformation = new UserInformation();
            _MappingProcessor = new MappingProcessor();

            try
            {
                var mappings = _MappingProcessor.RetrieveMapping<MappingModel>();
                return View(mappings);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [HttpPost]
        // POST: EDCFeed/EDCFeedMapping/DeleteMapping{id}
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
        // POST: EDCFeed/EDCFeedMapping/DeleteMapping
        public string DeleteMapping(List<int> idList)
        {
            try
            {
                var data = SQLDataAccess.DeleteMappings(idList, "SNPI_Mappings_db");
                return data;
            }
            catch (Exception ex)
            {
                return ex.Message + ex.StackTrace;
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
                        mappingModel.shopCategory = attribute.Title;
                        mappingModel.shopCategoryId = attribute.Id;
                        break;
                    case "supplier":
                        mappingModel.supplierCategory = attribute.Title;
                        mappingModel.supplierCategoryId = attribute.Id;
                        break;
                }
            }

            return mappingModel;
        }
    }
}
