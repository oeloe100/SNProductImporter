using SNPIDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPIDataLibrary.BusinessLogic
{
    public class CompareDBData
    {
        public bool DataIsExisting(string clientId, string clientSecret, string serverUrl, string redirectUrl)
        {
            var data = CredentialsProcessor.LoadCredentials<ClientModel>();

            foreach (var row in data)
            {
                if (clientId == row.ClientId &&
                    clientSecret == row.ClientSecret &&
                    serverUrl == row.ServerUrl &&
                    redirectUrl == row.RedirectUrl)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
