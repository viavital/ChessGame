using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConnectionSignalR
{
    internal class ClientsPlayer
    {
      
        [JsonProperty("player")]
        public PlayerSubData Player { get; set; }

        [JsonProperty("playersColor")]
        public PlayersColor playersColor { get; set; }
    }
    public class PlayerSubData
    {
        [JsonIgnore]
        public string id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonIgnore]
        public string IdConnection { get; set; }       
    }
    public class PlayersColor
    {
        [JsonProperty("color")]
        public string Color { get; set; }

    }

}

