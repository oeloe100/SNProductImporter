using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Helpers
{
    public class ApiHttpClientHelper
    {
        public HttpClient ApiHttpClient;
        private readonly string _AccessToken;

        public ApiHttpClientHelper(string accessToken)
        {
            _AccessToken = accessToken;
            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            ApiHttpClient = new HttpClient();
            ApiHttpClient.BaseAddress = new Uri(api);
            ApiHttpClient.DefaultRequestHeaders.Accept.Clear();
            if (!string.IsNullOrEmpty(_AccessToken))
                ApiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _AccessToken);
            ApiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}