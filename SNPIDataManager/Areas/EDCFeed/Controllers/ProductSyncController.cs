using Newtonsoft.Json;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Areas.EDCFeed.Controllers.API;
using SNPIDataManager.Areas.EDCFeed.Models;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopCategoriesModel;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        List<TokenModel> tokenDetails = CredentialsProcessor.LoadToken<TokenModel>();
        List<ClientModel> credentialsDetails = CredentialsProcessor.LoadCredentials<ClientModel>();

        // GET: EDCFeed/ProductSync
        public async Task<ActionResult> Index()
        {
            EasyAccess();
            var Result = await NopCategorieResource();

            var InventoryDataController = new InventoryDataController();
            var newData = InventoryDataController.CategoryBuilder();

            List<IndexViewModel> model = new List<IndexViewModel>();

            var indexViewModel = new IndexViewModel()
            {
                CategoriesModel = Result,
                EDCCategoriesFiltered = newData
            };

            model.Add(indexViewModel);

            return View(model);
        }

        public ActionResult CreateDataMappings()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoriesModel>> NopCategorieResource()
        {
            var clientHelper = new NopAPIClientHelper(accessToken, serverUrl);

            string jsonUrl = $"/api/categories";
            object customerData = await clientHelper.Get(jsonUrl);

            var categoriesRootObject = JsonConvert.DeserializeObject<CategoriesRootObject>(customerData.ToString());
            var categories = categoriesRootObject.Categories.Where(categorie => !string.IsNullOrEmpty(categorie.Name));

            return categories;
        }

        public void EasyAccess() 
        {
            foreach (var obj in tokenDetails)
            {
                accessToken = obj.AccessToken;
            }

            foreach (var obj in credentialsDetails)
            {
                serverUrl = obj.ServerUrl;
            }

        }
    }
}