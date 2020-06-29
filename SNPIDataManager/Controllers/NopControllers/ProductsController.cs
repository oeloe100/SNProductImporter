﻿using System;
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
            var tokenDetails = TokenProcessor.LoadToken<TokenModel>();
            var credentialsDetails = CredentialsProcessor.LoadCredentials<ClientModel>();
            var clientHelper = new NopAPIClientHelper(NopAccessHelper.accessToken, NopAccessHelper.serverUrl);

            string jsonUrl = $"/api/products?fields=id,name,images,sku";
            object customerData = await clientHelper.Get(jsonUrl);

            var customerRootObject = JsonConvert.DeserializeObject<ProductsRootObject>(customerData.ToString());

            var products = customerRootObject.Customers.Where(
                    product => !string.IsNullOrEmpty(product.id.ToString()) &&
                    !string.IsNullOrEmpty(product.name) &&
                    product.images != null);

            return View(products);
        }
    }
}