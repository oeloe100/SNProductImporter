using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNPIDataManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SNPIDataManager.Helpers
{
    public class APIHelper
    {
        private readonly APIAuthMiddelwareHelper _ApiClient;

        public APIHelper()
        {
            _ApiClient = new APIAuthMiddelwareHelper("");
        }

        public async Task<PreLoginModel> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });

            using (HttpResponseMessage response = await _ApiClient.ApiMiddelwareClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    result.IsLoggedIn = true;

                    var loginObject = new PreLoginModel()
                    {
                        AuthenticatedUser = result,
                    };

                    return loginObject;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<PreLoginModel> Registrate(string username, string password, string confirmPassword)
        {
            var obj = new RegisterModel()
            {
                Email = username,
                Password = password,
                ConfirmPassword = confirmPassword,
            };

            var jsonString = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            using (_ApiClient.ApiMiddelwareClient)
            {
                var result = await _ApiClient.ApiMiddelwareClient.PostAsync(_ApiClient.ApiMiddelwareClient.BaseAddress + "/api/Account/Register", content);

                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    var newObj = new PreLoginModel()
                    {
                        RegisterModel = obj
                    };

                    return newObj;
                }
                else 
                {
                    throw new Exception(result.ReasonPhrase);
                }
            }
        }
    }
}