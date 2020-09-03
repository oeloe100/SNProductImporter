using Newtonsoft.Json;
using SNPIDataManager.Config;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopProductsModel;
using SNPIHelperLibrary;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SNPIDataManager.Controllers.NopControllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly NopAccessHelper _NopAccessHelper;
        private readonly NopAPIClientHelper _NopApiClientHelper;

        public ProductsController()
        {
            _NopAccessHelper = new NopAccessHelper();
            _NopApiClientHelper = new NopAPIClientHelper(
                _NopAccessHelper.AccessToken,
                _NopAccessHelper.ServerUrl);
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            string jsonUrl = LocationsConfig.ReadLocations("apiProducts") + "?fields=id,name,images,sku";
            object productsData = await _NopApiClientHelper.Get(jsonUrl);

            var productsRootObject = JsonConvert.DeserializeObject<ProductsRootObject>(productsData.ToString());

            var products = productsRootObject.Customers.Where(
                    product => !string.IsNullOrEmpty(product.Id.ToString()) &&
                    !string.IsNullOrEmpty(product.Name) &&
                    product.Images != null);

            return View(products);
        }
    }
}