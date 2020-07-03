using SNPIDataManager.Models.NopCustomerModel;
using SNPIHelperLibrary;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SNPIDataManager.Helpers.NopAPIHelper
{
    public class NopAPIClientHelper
    {
        private HttpClient nopClient;

        private readonly string _accessToken;
        private readonly string _serverUrl;

        public NopAPIClientHelper(string accessToken, string serverUrl)
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

        public async Task<object> Get(string path)
        {
            string requestUriString = string.Format("{0}{1}", _serverUrl, path);

            using (nopClient)
            {
                var result = await nopClient.GetAsync(requestUriString);

                if (result.IsSuccessStatusCode)
                {
                    string resultContent = await result.Content.ReadAsStringAsync();

                    return resultContent;
                }
                else
                {
                    throw new Exception(result.ReasonPhrase);
                }
            }
        }
    }
}