using DataLibrary.DataAccess;
using DataLibrary.Models;
using InventoryManager.Models.NopAccess;
using System.Collections.Generic;

namespace DataLibrary.BusinessLogic
{
    public class AuthorizationCredentialsProcessor
    {
        public static int InsertAuthorizationCredentials(NopAuthorizationModel model, string connectionString)
        {
            string sql = @"INSERT INTO dbo.NopComAuthorization (UserId, Name, NopKey, NopSecret, ServerUrl, RedirectURl, Created_At) VALUES (@UserId, @Name, @NopKey, @NopSecret, @ServerUrl, @RedirectURl, @Created_At);";

            return SQLDataAccess.SaveData(sql, model, connectionString);
        }

        public static List<CallbackRequestDataModel> LoadLastAccessData(string connectionString)
        {
            string sql = @"SELECT * FROM dbo.NopComAuthorization";

            return SQLDataAccess.LoadData<CallbackRequestDataModel>(sql, connectionString);
        }
    }
}
