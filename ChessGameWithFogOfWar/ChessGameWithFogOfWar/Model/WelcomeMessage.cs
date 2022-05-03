using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Model
{
    public class WelcomeMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
