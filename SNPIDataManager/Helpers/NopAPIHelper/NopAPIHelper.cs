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
        public HttpClient nopApiClient;

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _serverUrl;

        public NopAPIHelper(string clientId, string clientSecret, string serverUrl)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _serverUrl = serverUrl;

            InitializeClient();
        }

        public void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            nopApiClient = new HttpClient();
            nopApiClient.BaseAddress = new Uri(api);
            nopApiClient.DefaultRequestHeaders.Accept.Clear();
            nopApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<string> AuthorizeClient(string code, string grantType, string redirectUrl)
        {
            string requestUriString = string.Format("{0}/api/token", _serverUrl);

            var data = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("grant_type", grantType),
                new KeyValuePair<string, string>("redirect_uri", redirectUrl)
            });

            using (HttpResponseMessage response = await nopApiClient.PostAsync(requestUriString, data))
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