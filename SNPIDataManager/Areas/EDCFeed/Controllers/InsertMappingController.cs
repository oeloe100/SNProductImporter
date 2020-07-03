using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using SNPIDataManager.Areas.EDCFeed.Models.MappingModels;
using SNPIDataManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SNPIDataManager.Areas.EDCFeed.Controllers
{
    public class InsertMappingController : Controller
    {
        //private APIAuthMiddelwareHelper _APIAuthMiddelwareHelper;
        private UserInformation _UserInformation;
        private MappingProcessor _MappingProcessor;

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

        private MappingModel CategoryToMappingModel(List<CategoryModel> model)
        {
            var mappingModel = new MappingModel();

            foreach (var attribute in model)
            {
                switch (attribute.Vendor)
                {
                    case "shop":
                        mappingModel.shopCategory = attribute.Title;
                        mappingModel.shopId = attribute.Id;
                        break;
                    case "supplier":
                        mappingModel.supplierCategory = attribute.Title;
                        mappingModel.supplierId = attribute.Id;
                        break;
                }
            }

            return mappingModel;
        }
    }
}