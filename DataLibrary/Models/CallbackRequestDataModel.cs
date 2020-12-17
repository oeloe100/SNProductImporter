namespace DataLibrary.Models
{
    public class CallbackRequestDataModel
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string NopKey { get; set; }
        public string NopSecret { get; set; }
        public string ServerUrl { get; set; }
    }
}
