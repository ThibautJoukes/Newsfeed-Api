using Newtonsoft.Json;

namespace Newsfeed.Api.ConfigModels
{
    public class CorsPolicy
    {
        //public readonly string name = "corspolicy";

        [JsonProperty("PolicyName")]
        public string PolicyName { get; set; }

        [JsonProperty("AllowOrigins")]
        public string[] AllowOrigins { get; set; }
    }
}
