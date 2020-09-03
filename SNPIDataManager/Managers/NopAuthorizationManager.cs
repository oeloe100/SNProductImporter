using SNPIDataManager.Config;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopAuthorizationParametersModel;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SNPIDataManager.Managers
{
    public class NopAuthorizationManager
    {
        public string _ClientId;
        public string _ClientSecret;
        public string _ServerUrl;

        private readonly NopAPIHelper _nopAPIHelper;

        public NopAuthorizationManager(string clientId, string clientSecret, string serverUrl)
        {
            _ClientId = clientId;
            _ClientSecret = clientSecret;
            _ServerUrl = serverUrl;

            _nopAPIHelper = new NopAPIHelper(clientId, clientSecret, serverUrl);
        }

        public string GetAuthorizationUrl(string redirectUrl, string[] scope, string state = null)
        {
            var stringBuilder = new StringBuilder();
            string callbackUrl = new Uri(redirectUrl).ToString();

            stringBuilder.AppendFormat("{0}" + LocationsConfig.ReadLocations("apiOauthAuthorize") + "", _ServerUrl);
            stringBuilder.AppendFormat("?client_id={0}", HttpUtility.UrlEncode(_ClientId));
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

        public async Task<string> GetAuthorizationData(AuthorizationParametersModel authorizationParemeters)
        {
            // make sure we have the necessary parameters
            ValidateParameter("code", authorizationParemeters.Code);
            ValidateParameter("storeUrl", authorizationParemeters.ServerUrl);
            ValidateParameter("clientId", authorizationParemeters.ClientId);
            ValidateParameter("clientSecret", authorizationParemeters.ClientSecret);
            ValidateParameter("redirectUrl", authorizationParemeters.RedirectUrl);
            ValidateParameter("grantType", authorizationParemeters.GrantType);

            string accessToken = await _nopAPIHelper.AuthorizeClient(authorizationParemeters.Code, authorizationParemeters.GrantType, authorizationParemeters.RedirectUrl);

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