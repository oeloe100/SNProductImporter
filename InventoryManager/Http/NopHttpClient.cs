using InventoryManager.Http.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InventoryManager.Http
{
    public class NopHttpClient
    {
        private readonly HttpClientHelper _ApiClient;

        private readonly string _ClientId;
        private readonly string _ClientSecret;
        private readonly string _ServerUrl;

        public NopHttpClient(string clientId, string clientSecret, string serverUrl)
        {
            _ApiClient = new HttpClientHelper("");

            _ClientId = clientId;
            _ClientSecret = clientSecret;
            _ServerUrl = serverUrl;
        }

        public async Task<string> AuthorizeClient(string code, string grantType, string redirectUrl)
        {
            string requestUriString = string.Format("{0}/api/token", _ServerUrl);

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _ClientId),
                new KeyValuePair<string, string>("client_secret", _ClientSecret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("grant_type", grantType),
                new KeyValuePair<string, string>("redirect_uri", redirectUrl)
            });

            using (HttpResponseMessage response = await _ApiClient.ApiHttpClient.PostAsync(requestUriString, data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    return jsonResult;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
