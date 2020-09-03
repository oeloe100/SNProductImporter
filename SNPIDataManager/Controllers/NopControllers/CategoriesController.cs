using Microsoft.Owin.Security.Twitter.Messages;
using Newtonsoft.Json;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopCategoriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SNPIDataManager.Helpers;
using SNPIHelperLibrary;
using SNPIDataManager.Config;

namespace SNPIDataManager.Controllers.NopControllers
{
    [Authorize]
    public class CategoriesController : Controller
    {

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var clientHelper = new NopAPIClientHelper(NopAccessHelper.AccessToken(), NopAccessHelper.ServerURL());

            string jsonUrl = LocationsConfig.ReadLocations("apiCategories");
            object categoriesData = await clientHelper.Get(jsonUrl);

            var categoriesRootObject = JsonConvert.DeserializeObject<CategoriesRootObject>(categoriesData.ToString());
            var categories = categoriesRootObject.Categories.Where(categorie => !string.IsNullOrEmpty(categorie.Name));

            var matchingValues = categories.Where(item => item.ParentId == item.Id);

            return View(categories);
        }
    }
}