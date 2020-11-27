namespace InventoryManager.Models
{
    public class AuthorizationParametersModel
    {
        public string ServerUrl { get; set; }
        public string Code { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public string GrantType { get; set; }
    }
}
