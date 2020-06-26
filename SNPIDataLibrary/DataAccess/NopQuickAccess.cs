using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNPIDataLibrary.Models;
using SNPIDataLibrary.BusinessLogic;

namespace SNPIDataLibrary.DataAccess
{
    public class NopQuickAccess
    {
        private List<TokenModel> tokenDetails = CredentialsProcessor.LoadToken<TokenModel>();
        private List<ClientModel> credentialsDetails = CredentialsProcessor.LoadCredentials<ClientModel>();

        public string NopAccessToken()
        {
            foreach (var obj in tokenDetails)
            {
                return obj.AccessToken;
            }

            return null;
        }

        public string NopServerUrl()
        {
            foreach (var obj in credentialsDetails)
            {
                return obj.ServerUrl;
            }

            return null;
        }
    }
}
