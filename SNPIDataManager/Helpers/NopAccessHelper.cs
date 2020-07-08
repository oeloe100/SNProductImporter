using Microsoft.Owin.Security.Twitter.Messages;
using Newtonsoft.Json;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.Models.NopCategoriesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SNPIHelperLibrary;
using Microsoft.Ajax.Utilities;

namespace SNPIHelperLibrary
{
    public class NopAccessHelper
    {
        public string AccessToken;
        public string ServerUrl;

        public NopAccessHelper()
        {
            var tokenDetails = TokenProcessor.LoadToken<TokenModel>();
            var credentialDetails = CredentialsProcessor.LoadUserCredentials<ClientModel>();

            AccessToken = RetrieveToken(tokenDetails);
            ServerUrl = RetrieveServerUrl(credentialDetails);
        }

        private string RetrieveToken(List<TokenModel> tokenDetails)
        {
            foreach (var token in tokenDetails)
                return token.AccessToken;

            return "No Token has been found"; 
        }

        private string RetrieveServerUrl(List<ClientModel> credentialDetails)
        {
            foreach (var url in credentialDetails)
                return url.ServerUrl;

            return "No ServerUrl has been found";
        }
    }
}
