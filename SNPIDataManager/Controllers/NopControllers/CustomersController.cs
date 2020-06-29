﻿using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopCategoriesModel;
using SNPIDataManager.Models.NopCustomerModel;
using SNPIDataManager.Models.NopProductsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SNPIDataManager.Controllers.NopControllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private string accessToken;
        private string serverUrl;

        [HttpGet]
        public async Task<ActionResult> GetCustomerInformation()
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

            //string jsonUrl = $"/api/customers?fields=id,first_name,last_name";
            string jsonUrl = $"/api/customers?fields=id,first_name,last_name";
            object customerData = await clientHelper.Get(jsonUrl);

            var customerRootObject = JsonConvert.DeserializeObject<CustomersRootObject>(customerData.ToString());

            var customers = customerRootObject.Customers.Where(
                    customer => !string.IsNullOrEmpty(customer.FirstName) || !string.IsNullOrEmpty(customer.LastName));

            return View(customers);
        }
    }
}
