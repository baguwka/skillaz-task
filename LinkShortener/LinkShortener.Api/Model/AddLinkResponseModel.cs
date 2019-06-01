using Newtonsoft.Json;

namespace LinkShortener.Api.Model
{
    public class AddLinkResponseModel
    {
        [JsonProperty("identity")]
        public string Identity { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }

        [JsonProperty("shorten")]
        public string Shorten { get; set; }
    }
}