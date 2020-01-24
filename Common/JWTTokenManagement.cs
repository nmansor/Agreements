using Newtonsoft.Json;


namespace Common
{
    
    public class JWTTokenManagement
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }

    }
}
