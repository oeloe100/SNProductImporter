using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopProductsModel;
using Newtonsoft.Json;
using SNPIHelperLibrary;

namespace SNPIDataManager.Controllers.NopControllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly NopAccessHelper _NopAccessHelper;

        public ProductsController()
        {
            _NopAccessHelper = new NopAccessHelper();
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var clientHelper = new NopAPIClientHelper(_NopAccessHelper.AccessToken, _NopAccessHelper.ServerUrl);

            string jsonUrl = $"/api/products?fields=id,name,images,sku";
            object productsData = await clientHelper.Get(jsonUrl);

            var productsRootObject = JsonConvert.DeserializeObject<ProductsRootObject>(productsData.ToString());

            var products = productsRootObject.Customers.Where(
                    product => !string.IsNullOrEmpty(product.Id.ToString()) &&
                    !string.IsNullOrEmpty(product.Name) &&
                    product.Images != null);

            return View(products);
        }
    }
}