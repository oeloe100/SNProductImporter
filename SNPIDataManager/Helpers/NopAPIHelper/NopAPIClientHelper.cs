using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async Task PostProductData(List<JObject> data, string path)
        {
            string requestUriString = string.Format("{0}{1}", _ServerUrl, path);

            foreach (var item in data)
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                await _ApiClient.ApiHttpClient.PostAsync(requestUriString, stringContent);
            }
        }

        public async Task UpdateProductData(JObject data, string path, int productId)
        {
            string requestUriString = string.Format("{0}{1}{2}", _ServerUrl, path, productId);

            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            await _ApiClient.ApiHttpClient.PutAsync(requestUriString, stringContent);
        }

        public async Task<JObject> GetProductData(string path)
        {
            string requestUriString = string.Format("{0}{1}", _ServerUrl, path);

            var result = await _ApiClient.ApiHttpClient.GetAsync(requestUriString);

            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsAsync<JObject>();

                return resultContent;
            }
            else
            {
                throw new Exception(result.ReasonPhrase);
            }
        }
    }
}