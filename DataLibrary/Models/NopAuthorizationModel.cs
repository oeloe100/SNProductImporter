using System;

namespace InventoryManager.Models.NopAccess
{
    public class NopAuthorizationModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string NopKey { get; set; }
        public string NopSecret { get; set; }
        public string ServerUrl { get; set; }
        public string RedirectUrl { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int? ExpiresIn { get; set; }
        public DateTime Created_At { get; set; }
    }
}
