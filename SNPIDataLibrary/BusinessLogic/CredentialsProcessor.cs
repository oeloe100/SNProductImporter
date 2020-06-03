using SNPIDataLibrary.DataAccess;
using SNPIDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPIDataLibrary.BusinessLogic
{
    public static class CredentialsProcessor
    {
        public static int InsertCredentials(string clientId, string clientSecret, string serverUrl, string redirectUrl)
        {
            ClientModel clientDataModel = new ClientModel
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                ServerUrl = serverUrl,
                RedirectUrl = redirectUrl
            };

            string sql = @"INSERT INTO dbo.dataAccess (clientId, clientSecret, serverUrl, redirectUrl) VALUES (@ClientId, @ClientSecret, @ServerUrl, @RedirectUrl);";

            return SQLDataAccess.SaveData(sql, clientDataModel);
        }

        public static List<ClientModel> LoadCredentials<ClientModel>()
        {
            string sql = @"SELECT clientId, clientSecret, serverUrl, redirectUrl FROM dbo.dataAccess;";

            return SQLDataAccess.LoadData<ClientModel>(sql);
        }

        public static int InsertToken(string id, string accessToken, string refreshToken)
        {
            string sql;

            TokenModel tokenModel = new TokenModel()
            {
                ID = id,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            sql = @"INSERT INTO dbo.NopTokens (Id, AccessToken, RefreshToken) VALUES (@ID, @AccessToken, @RefreshToken);";

            return SQLDataAccess.SaveData(sql, tokenModel);
        }
    }
}
