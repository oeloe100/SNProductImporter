using SNPIDataLibrary.BusinessLogic;
using SNPIDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Setup
{
    public class NopAccessSetup
    {
        public NopAccessSetup()
        {
            IsSetup();
        }

        //ADD: Check if Current AccessToken is still valid. If not use refresh token to obtain new AccessToken.
        //First Create short lived AccessToken (1 Min. +-) And Long Lived Refresh token for testing purpose.

        public bool IsSetup()
        {
            var data = CredentialsProcessor.LoadCredentials<ClientModel>();

            if (data.Count > 0)
            {
                return false;
            }

            return true;
        }
    }
}