using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Model
{
    public class ReceivedPostData
    {
        [JsonProperty ("player")]
        public Player Player { get; set; }

        [JsonProperty("playersColor")]
        public PlayersColor playersColor { get; set; }
    }
    public class Player
    {
        public Player()
        {
            Guid _guid = Guid.NewGuid();
            Id = _guid;
        }
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }        
    }
    public class PlayersColor
    {
        public string Color { get; set; }

        public ColorOfTeamEnum ReturnColorEnum()
        {
            if (Color == "White" || Color == "white" )
            {
                return ColorOfTeamEnum.White;
            }
            return ColorOfTeamEnum.Black;
        }
    }
}
