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

namespace SNPIDataManager.Controllers.NopControllers
{
    public class CategoriesController : Controller
    {
        private string accessToken;
        private string serverUrl;

        [HttpGet]
        public async Task<ActionResult> GetCategories()
{
            var tokenDetails = CredentialsProcessor.LoadToken<TokenModel>();
            var credentialsDetails = CredentialsProcessor.LoadCredentials<ClientModel>();
            foreach (var obj in tokenDetails)
            {
                accessToken = obj.AccessToken;
            }

            foreach (var obj in credentialsDetails)
            {
                serverUrl = obj.ServerUrl;
            }

            var clientHelper = new NopAPIClientHelper(accessToken, serverUrl);

            string jsonUrl = $"/api/categories";
            object customerData = await clientHelper.Get(jsonUrl);

            var categoriesRootObject = JsonConvert.DeserializeObject<CategoriesRootObject>(customerData.ToString());
            var categories = categoriesRootObject.Categories.Where(categorie => !string.IsNullOrEmpty(categorie.Name));

            var matchingValues = categories.Where(item => item.ParentId == item.Id);

            return View(categories);
        }
    }
}