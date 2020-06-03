using Microsoft.Ajax.Utilities;
using SNPIDataManager.Models.NopAuthorizationModels;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
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

        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _serverUrl;

        public NopAPIHelper(string clientId, string clientSecret, string serverUrl)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _serverUrl = serverUrl;

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

        public string AuthorizeClient(string code, string grantType, string redirectUrl)
        {
            string requestUriString = string.Format("{0}/api/token", _serverUrl);
            string queryParameters = string.Format("client_id={0}&client_secret={1}&code={2}&grant_type={3}&redirect_uri={4}", _clientId, _clientSecret, code, grantType, redirectUrl);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            using (new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(queryParameters);
                    streamWriter.Close();
                }
            }

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string json = string.Empty;

            using (Stream responseStream = httpWebResponse.GetResponseStream())
            {
                if (responseStream != null)
                {
                    var streamReader = new StreamReader(responseStream);
                    json = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }

            return json;
        }
    }
}