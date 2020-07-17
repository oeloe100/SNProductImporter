using Microsoft.Ajax.Utilities;
using SNPIDataManager.Managers;
using SNPIDataManager.Models.NopAuthorizationModels;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;

namespace SNPIDataManager.Helpers.NopAPIHelper
{
    public class NopAPIHelper
    {
        private ApiHttpClientHelper _ApiClient;

        private readonly string _ClientId;
        private readonly string _ClientSecret;
        private readonly string _ServerUrl;

        public NopAPIHelper(string clientId, string clientSecret, string serverUrl)
        {
            _ApiClient = new ApiHttpClientHelper("");

            _ClientId = clientId;
            _ClientSecret = clientSecret;
            _ServerUrl = serverUrl;
        }

        public async Task<string> AuthorizeClient(string code, string grantType, string redirectUrl)
        {
            string requestUriString = string.Format("{0}/api/token", _ServerUrl);

            var data = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("client_id", _ClientId),
                new KeyValuePair<string, string>("client_secret", _ClientSecret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("grant_type", grantType),
                new KeyValuePair<string, string>("redirect_uri", redirectUrl)
            });

            using (HttpResponseMessage response = await _ApiClient.ApiHttpClient.PostAsync(requestUriString, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return jsonResult;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}