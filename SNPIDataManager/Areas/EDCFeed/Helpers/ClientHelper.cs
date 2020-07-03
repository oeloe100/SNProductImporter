using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public class ClientHelper
    {
        public HttpClient nopClient;
        public readonly string _serverUrl;
        private readonly string _accessToken;

        public ClientHelper(string accessToken, string serverUrl)
        {
            _accessToken = accessToken;
            _serverUrl = serverUrl;

            InitializeClient();
        }

        public void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            nopClient = new HttpClient();
            nopClient.BaseAddress = new Uri(api);
            nopClient.DefaultRequestHeaders.Accept.Clear();
            nopClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            nopClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}