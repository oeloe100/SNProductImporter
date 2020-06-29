using Microsoft.Ajax.Utilities;
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
using SNPIHelperLibrary;

namespace SNPIDataManager.Controllers.NopControllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        NopAccessHelper NopAccessHelper;
        public CustomersController()
        {
            NopAccessHelper helper = new NopAccessHelper();
            NopAccessHelper = helper;
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerInformation()
        {
            var tokenDetails = TokenProcessor.LoadToken<TokenModel>();
            var credentialsDetails = CredentialsProcessor.LoadCredentials<ClientModel>();
            var clientHelper = new NopAPIClientHelper(NopAccessHelper.accessToken, NopAccessHelper.serverUrl);

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
