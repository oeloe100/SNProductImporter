using SNPIDataManager.Models.NopAuthorizationModels;
using SNPIDataManager.Models.NopAuthorizationParametersModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.PopulateModels
{
    public static class PopulateModels
    {
        public static AuthorizationParametersModel PopulateAuthenticationModel(string clientId, string clientSecret, string serverUrl, string redirectUrl, string grantType, string code)
        {
            var authoriationParameters = new AuthorizationParametersModel()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                ServerUrl = serverUrl,
                Code = code,
                GrantType = grantType,
                RedirectUrl = redirectUrl
            };

            return authoriationParameters;
        }

        public static void PopulateUserAccessModel(AccessModel model, string clientId, string clientSecret, string serverUrl, string redirectUrl)
        {
            model.userAccessModel = new UserAccessModel()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                ServerUrl = serverUrl,
                RedirectUrl = redirectUrl
            };
        }
    }
}