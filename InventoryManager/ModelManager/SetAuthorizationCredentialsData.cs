using InventoryManager.Models.NopAccess;
using System;

namespace InventoryManager.ModelManager
{
    public class SetAuthorizationCredentialsData
    {
        public static NopAuthorizationModel SetData(string userId, string name, string key, string secret, string serverUrl, string redirectUrl)
        {
            return new NopAuthorizationModel
            {
                UserId = userId,
                Name = name,
                NopKey = key,
                NopSecret = secret,
                ServerUrl = serverUrl,
                RedirectUrl = redirectUrl,
                Created_At = DateTime.UtcNow
            };
        }
    }
}
