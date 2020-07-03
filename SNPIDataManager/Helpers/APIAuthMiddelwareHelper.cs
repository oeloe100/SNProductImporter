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
    public class APIAuthMiddelwareHelper
    {
        public HttpClient ApiMiddelwareClient;
        private readonly string _AccessToken;

        public APIAuthMiddelwareHelper(string accessToken)
        {
            _AccessToken = accessToken;
            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            ApiMiddelwareClient = new HttpClient();
            ApiMiddelwareClient.BaseAddress = new Uri(api);
            ApiMiddelwareClient.DefaultRequestHeaders.Accept.Clear();
            ApiMiddelwareClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _AccessToken);
            ApiMiddelwareClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}