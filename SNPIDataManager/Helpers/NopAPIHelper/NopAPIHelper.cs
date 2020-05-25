using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace SNPIDataManager.Helpers.NopAPIHelper
{
    public class NopAPIHelper
    {
        public HttpClient nopApiClient;

        public NopAPIHelper()
        {
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
    }
}