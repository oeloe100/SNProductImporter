using Microsoft.Ajax.Utilities;
using SNPIDataManager.Models.NopAuthorizationModels;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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