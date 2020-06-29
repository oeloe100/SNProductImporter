using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNPIDataLibrary.Models;
using SNPIDataLibrary.DataAccess;

namespace SNPIDataLibrary.BusinessLogic
{
    public class TokenProcessor
    {
        public static int InsertToken(string id, string accessToken, string refreshToken)
        {
            string sql;

            DateTime date = DateTime.Now;

            var tokenModel = new TokenModel()
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
