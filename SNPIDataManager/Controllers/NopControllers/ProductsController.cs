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
        NopAccessHelper NopAccessHelper;
        public ProductsController()
        {
            NopAccessHelper helper = new NopAccessHelper();
            NopAccessHelper = helper;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var clientHelper = new NopAPIClientHelper(NopAccessHelper.accessToken, NopAccessHelper.serverUrl);

            string jsonUrl = $"/api/products?fields=id,name,images,sku";
            object productsData = await clientHelper.Get(jsonUrl);

            var productsRootObject = JsonConvert.DeserializeObject<ProductsRootObject>(productsData.ToString());

            var products = productsRootObject.Customers.Where(
                    product => !string.IsNullOrEmpty(product.id.ToString()) &&
                    !string.IsNullOrEmpty(product.name) &&
                    product.images != null);

            return View(products);
        }
    }
}