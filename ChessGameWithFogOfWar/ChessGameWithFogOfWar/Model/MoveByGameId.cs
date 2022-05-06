using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Model
{
    public class MoveByGameId
    {
        [JsonProperty("gameid")]
        public string GameId { get; set; }

        [JsonProperty("move")]
        public string Move { get; set; }
    }
}
