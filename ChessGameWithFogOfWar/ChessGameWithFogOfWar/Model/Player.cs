using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Model
{
    public class ReceivedPostData
    {
       
        public ReceivedPostData(Player player,  ColorOfTeamEnum colorOfTeam)
        {
            this.Player = player;
            this.playersColor = new PlayersColor() { Color = colorOfTeam.ToString() };
        }
        public ReceivedPostData()
        {

        }
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
            else if (Color == "random")
            {
                    Random random = new Random();
                    var randomColorOfTeam = random.Next(0, 1);
                    if (randomColorOfTeam == 0)
                    {
                        return ColorOfTeamEnum.White;
                    }
                    else
                        return ColorOfTeamEnum.Black;
            }
            return ColorOfTeamEnum.Black;
        }
    }
}
