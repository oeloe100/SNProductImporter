using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNPIDataManager.Models.NopCustomerModel;
using SNPIDataManager.Models.NopProductsModel;
using SNPIDataManager.Wrapper;
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
using System.Web.Http;
using System.Web.Http.Results;

namespace SNPIDataManager.Helpers.NopAPIHelper
{
    public class NopAPIClientHelper
    {
        private readonly ApiHttpClientHelper _ApiClient;
        private readonly string _ServerUrl;

        public NopAPIClientHelper(string accessToken, string serverUrl)
        {
            _ApiClient = new ApiHttpClientHelper(accessToken);
            _ServerUrl = serverUrl;
        }

        public async Task<object> Get(string path)
        {
            string requestUriString = string.Format("{0}{1}", _ServerUrl, path);

            using (_ApiClient.ApiHttpClient)
            {
                var result = await _ApiClient.ApiHttpClient.GetAsync(requestUriString);

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

        public async Task Post(List<JObject> data, string path)
        {
            string requestUriString = string.Format("{0}{1}", _ServerUrl, path);

            using (_ApiClient.ApiHttpClient)
            {
                foreach (var item in data)
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                    var result = await _ApiClient.ApiHttpClient.PostAsync(requestUriString, stringContent);
                }
            }
        }
    }
}