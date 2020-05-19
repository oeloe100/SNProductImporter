using SNPIDataManager.Areas.NopAPIAuthorizer.Models;
using SNPIDataManager.Areas.NopAPIAuthorizer.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.PopulateModels
{
    public class PopulateModels
    {
        public static AuthParameters PopulateAuthenticationModel(string refreshToken, string clientId, string clientSecret, string serverUrl, string redirectUrl, string grantType, string code)
        {
            var AuthorizationParameters = new AuthParameters()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                ServerUrl = serverUrl,
                RedirectUrl = redirectUrl,
                RefreshToken = refreshToken,
                GrantType = grantType,
                Code = code
            };

            return AuthorizationParameters;
        }

        public static void PopulateUserAccessModel(AccessModel model, string clientId, string clientSecret, string serverUrl, string redirectUrl)
        {
            model.UserAccessModel = new UserAccessModel()
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                ServerUrl = serverUrl,
                RedirectUrl = redirectUrl
            };
        }
    }
}