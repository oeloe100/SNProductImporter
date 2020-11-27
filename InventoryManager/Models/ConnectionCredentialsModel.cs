using Newtonsoft.Json;

namespace InventoryManager.Models
{
    public class ConnectionCredentialsModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
        [JsonProperty("serverUrl")]
        public string ServerUrl { get; set; }
    }
}
