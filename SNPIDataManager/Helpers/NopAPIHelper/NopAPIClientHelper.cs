using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNPIDataManager.Areas.EDCFeed.Builder;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SNPIDataManager.Helpers.NopAPIHelper
{
    public class NopAPIClientHelper
    {
        private readonly log4net.ILog _Logger;
        private readonly MappingProductBuilder _MappingProductBuilder;
        private readonly ApiHttpClientHelper _ApiClient;
        private readonly string _ServerUrl;

        private int index = 0;

        public NopAPIClientHelper(string accessToken, string serverUrl)
        {
            _Logger = log4net.LogManager.GetLogger("FileAppender");
            _MappingProductBuilder = new MappingProductBuilder(FeedPathHelper.Path());
            _ApiClient = new ApiHttpClientHelper(accessToken);
            _ServerUrl = serverUrl;
        }

        /// <summary>
        /// Get Data from NopCommerce shop
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Post product(s) to NopCommerce shop.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task PostProductData(Dictionary<string,List<JObject>> data, string path)
        {
            string requestUriString = string.Format("{0}{1}", _ServerUrl, path);

            foreach (var item in data)
            {
                var productList = data[item.Key];

                for (var i = 0; i < productList.Count; i++)
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(productList[i]), Encoding.UTF8, "application/json");
                    var responseData = await _ApiClient.ApiHttpClient.PostAsync(requestUriString, stringContent).Result.Content.ReadAsAsync<JObject>();
                    await UpdateSelectedProductAttributes(responseData);
                }
            }
        }

        /******************* EXCEPTIONAL TASK *******************/
        private async Task UpdateSelectedProductAttributes(JObject products)
        {
            try
            {
                foreach (var product in products["products"])
                {
                    int productId = (int)product["id"];
                    int attributeId = (int)product["attributes"][0]["id"];

                    List<int> attributeValuesIds = new List<int>();
                    foreach (var item in product["attributes"][0]["attribute_values"])
                    {
                        attributeValuesIds.Add((int)item["id"]);
                    }

                    await UpdateProductData(_MappingProductBuilder.UpdateProductWithAttributes(
                    productId, attributeValuesIds, attributeId, index), LocationsConfig.ReadLocations("apiProducts"), productId);

                    index++;
                }

                _Logger.Debug("Successfully updated product attributes");
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
            }
        }
        /******************************************************/

        /// <summary>
        /// Update selected product data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task UpdateProductData(JObject data, string path, int productId)
        {
            string requestUriString = string.Format("{0}{1}{2}", _ServerUrl, path + "/", productId);
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            await _ApiClient.ApiHttpClient.PutAsync(requestUriString, stringContent);
        }

        /// <summary>
        /// Get products from NopCommerce recourse, using the 250 products page count pagination.
        /// Using pagination. Default is Max. 50 products. Maximum is 250 Products.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get total amount of products. Used to count amount of pages.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<JObject> GetProductCount(string path)
        {
            string requestUriString = string.Format("{0}{1}", _ServerUrl, path);

            var result = await _ApiClient.ApiHttpClient.GetAsync(requestUriString).Result.Content.ReadAsAsync<JObject>();

            return result;
        }
    }
}
 