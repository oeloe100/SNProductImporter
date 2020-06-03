using Microsoft.Ajax.Utilities;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopAuthorizationParametersModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SNPIDataManager.Managers
{
    public class NopAuthorizationManager
    {
        private string _clientId;
        private string _clientSecret;
        private string _serverUrl;

        private readonly NopAPIHelper _nopAPIHelper;

        public NopAuthorizationManager(string clientId, string clientSecret, string serverUrl)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _serverUrl = serverUrl;

            _nopAPIHelper = new NopAPIHelper(clientId, clientSecret, serverUrl);
        }

        public string GetAuthorizationUrl(string redirectUrl, string[] scope, string state = null)
        {
            var stringBuilder = new StringBuilder();
            string callbackUrl = new Uri(redirectUrl).ToString();

            stringBuilder.AppendFormat("{0}/oauth/authorize", _serverUrl);
            stringBuilder.AppendFormat("?client_id={0}", HttpUtility.UrlEncode(_clientId));
            stringBuilder.AppendFormat("&redirect_uri={0}", HttpUtility.UrlEncode(callbackUrl));
            stringBuilder.Append("&response_type=code");

            if (!string.IsNullOrEmpty(state))
            {
                stringBuilder.AppendFormat("&state={0}", state);
            }

            if (scope != null && scope.Length > 0)
            {
                string scopeJoined = string.Join(",", scope);
                stringBuilder.AppendFormat("&scope={0{", HttpUtility.UrlEncode(scopeJoined));
            }

            return stringBuilder.ToString();
        }

        public string GetAuthorizationData(AuthorizationParametersModel authorizationParemeters)
        {
            // make sure we have the necessary parameters
            ValidateParameter("code", authorizationParemeters.Code);
            ValidateParameter("storeUrl", authorizationParemeters.ServerUrl);
            ValidateParameter("clientId", authorizationParemeters.ClientId);
            ValidateParameter("clientSecret", authorizationParemeters.ClientSecret);
            ValidateParameter("redirectUrl", authorizationParemeters.RedirectUrl);
            ValidateParameter("grantType", authorizationParemeters.GrantType);

            string accessToken = _nopAPIHelper.AuthorizeClient(authorizationParemeters.Code, authorizationParemeters.GrantType, authorizationParemeters.RedirectUrl);

            return accessToken;
        }

        private void ValidateParameter(string parameterName, string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new Exception(string.Format("{0} parameter is missing", parameterName));
            }
        }
    }
}