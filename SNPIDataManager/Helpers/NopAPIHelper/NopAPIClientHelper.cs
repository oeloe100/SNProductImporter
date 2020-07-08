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
        private readonly APIAuthMiddelwareHelper _ApiClient;
        private readonly string _ServerUrl;

        public NopAPIClientHelper(string accessToken, string serverUrl)
        {
            _ApiClient = new APIAuthMiddelwareHelper(accessToken);
            _ServerUrl = serverUrl;
        }

        public async Task<object> Get(string path)
        {
            string requestUriString = string.Format("{0}{1}", _ServerUrl, path);

            using (_ApiClient.ApiMiddelwareClient)
            {
                var result = await _ApiClient.ApiMiddelwareClient.GetAsync(requestUriString);

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