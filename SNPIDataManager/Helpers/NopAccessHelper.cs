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

namespace SNPIHelperLibrary
{
    public class NopAccessHelper
    {
        public string accessToken;
        public string serverUrl;

        public NopAccessHelper()
        {
            var tokenDetails = TokenProcessor.LoadToken<TokenModel>();
            var credentialsDetails = CredentialsProcessor.LoadUserCredentials<ClientModel>();
            
            foreach (var obj in tokenDetails)
            {
                accessToken = obj.AccessToken;
            }

            foreach (var obj in credentialsDetails)
            {
                serverUrl = obj.ServerUrl;
            }
        }
    }
}
