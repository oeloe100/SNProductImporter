using SNPIDataLibrary.DataAccess;
using SNPIDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPIDataLibrary.BusinessLogic
{
    public static class CredProcessor
    {
        public static int InsertCredentials(string clientID, string clientSecret, string serverURL, string redirectURL)
        {
            UserModel data = new UserModel
            {
                ClientId = clientID,
                ClientSecret = clientSecret,
                ServerUrl = serverURL,
                RedirectUrl = redirectURL
            };

            string sql = @"INSERT INTO dbo.dataAccess (clientId, clientSecret, serverUrl, redirectUrl) VALUES (@ClientId, @ClientSecret, @ServerUrl, @RedirectUrl);";

            return SqlDataAccess.SaveData<UserModel>(sql, data);
        }

        public static List<UserModel> LoadCredentials<UserModel>()
        {
            string sql = @"SELECT clientId, clientSecret, serverUrl, redirectUrl FROM dbo.dataAccess;";

            return SqlDataAccess.LoadData<UserModel>(sql);
        }
    }
}
