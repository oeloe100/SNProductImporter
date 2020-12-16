using DataLibrary.Models;
using InventoryManager.Models.NopAccess;
using System;

namespace InventoryManager.ModelManager
{
    public class SetAuthorizationCredentialsData
    {
        public static NopAuthorizationModel SetAuthData(string userId, string name, string key, string secret, string serverUrl, string redirectUrl)
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

        public static CallbackRequestDataModel SetCallbackData(string access_token, string refresh_token, int expiresIn)
        {
            return new CallbackRequestDataModel
            {
                AccessToken = access_token,
                RefreshToken = refresh_token,
                ExpiresIn = expiresIn
            };
        }
    }
}
