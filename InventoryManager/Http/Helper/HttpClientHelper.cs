using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InventoryManager.Http.Helper
{
    public class HttpClientHelper
    {
        public HttpClient ApiHttpClient;
        private readonly string _AccessToken;

        public HttpClientHelper(string accessToken)
        {
            _AccessToken = accessToken;
            InitializeClient();
        }

        private void InitializeClient()
        {
            HttpContextAccessor accessor = new HttpContextAccessor();
            var scheme = accessor.HttpContext.Request.Scheme + "://" + accessor.HttpContext.Request.Host + "/";

            ApiHttpClient = new HttpClient();
            ApiHttpClient.BaseAddress = new Uri(scheme);
            ApiHttpClient.DefaultRequestHeaders.Accept.Clear();

            if (!string.IsNullOrEmpty(_AccessToken))
            {
                ApiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _AccessToken);
            }

            ApiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
