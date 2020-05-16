using Microsoft.Ajax.Utilities;
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
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SNPIDataManager.Helpers
{
    public class APIHelper
    {
        public HttpClient apiClient;

        public APIHelper()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PreLoginModel> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    result.IsLoggedIn = true;
                    
                    var loginObject = new PreLoginModel()
                    {
                        _AuthenticatedUser = result,
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

            using (apiClient)
            {
                var result = await apiClient.PostAsync(apiClient.BaseAddress + "/api/Account/Register", content);

                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    var newObj = new PreLoginModel()
                    {
                        _RegisterModel = obj
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