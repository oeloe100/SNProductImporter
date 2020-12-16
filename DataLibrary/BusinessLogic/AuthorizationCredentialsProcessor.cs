using DataLibrary.DataAccess;
using InventoryManager.Models.NopAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public class AuthorizationCredentialsProcessor
    {
        public static int InsertAuthorizationCredentials(NopAuthorizationModel model, string connectionString)
        {
            string sql = @"INSERT INTO dbo.NopComAuthorization (UserId, NopKey, NopSecret, ServerUrl, RedirectURl) VALUES (@UserId, @NopKey, @NopSecret, @ServerUrl, @RedirectURl);";

            return SQLDataAccess.SaveData(sql, model, connectionString);
        }
    }
}
