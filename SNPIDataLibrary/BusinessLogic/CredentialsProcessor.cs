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
        public static int InsertCredentials(string userId, string clientId, string clientSecret, string serverUrl, string redirectUrl)
        {
            ClientModel clientDataModel = new ClientModel
            {
                UserId = userId,
                ClientId = clientId,
                ClientSecret = clientSecret,
                ServerUrl = serverUrl,
                RedirectUrl = redirectUrl
            };

            string sql = @"INSERT INTO dbo.dataAccess (userId, clientId, clientSecret, serverUrl, redirectUrl) VALUES (@UserId, @ClientId, @ClientSecret, @ServerUrl, @RedirectUrl);";

            return SQLDataAccess.SaveData(sql, clientDataModel);
        }

        public static List<ClientModel> LoadCredentials<ClientModel>()
        {
            string sql = @"SELECT userId, clientId, clientSecret, serverUrl, redirectUrl FROM dbo.dataAccess;";

            return SQLDataAccess.LoadData<ClientModel>(sql);
        }

        public static int InsertToken(string id, string accessToken, string refreshToken)
        {
            string sql;

            DateTime date = DateTime.Now;

            TokenModel tokenModel = new TokenModel()
            {
                UserId = id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Date = date
            };

            sql = @"INSERT INTO dbo.NopTokens (userId, accessToken, refreshToken, date) VALUES (@UserId, @AccessToken, @RefreshToken, @Date);";

            return SQLDataAccess.SaveData(sql, tokenModel);
        }

        public static List<TokenModel> LoadToken<TokenModel>()
        {
            string sql = @"SELECT userId, accessToken, refreshToken, date FROM dbo.NopTokens;";

            return SQLDataAccess.LoadData<TokenModel>(sql);
        }
    }
}
