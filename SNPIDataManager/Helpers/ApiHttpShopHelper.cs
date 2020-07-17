using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SNPIDataManager.Helpers
{
    public class ApiHttpShopHelper
    {
        public HttpClient ApiHttpShopClient;
        private readonly string _AccessToken;

        public ApiHttpShopHelper(string accessToken)
        {
            _AccessToken = accessToken;
            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["shopApi"];

            ApiHttpShopClient = new HttpClient();
            ApiHttpShopClient.BaseAddress = new Uri(api);
            ApiHttpShopClient.DefaultRequestHeaders.Accept.Clear();
            if (!string.IsNullOrEmpty(_AccessToken))
                ApiHttpShopClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _AccessToken);
            ApiHttpShopClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}