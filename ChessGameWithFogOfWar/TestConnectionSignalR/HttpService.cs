using ChessGameWithFogOfWar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestConnectionSignalR
{
    internal class HttpService
    {

        public async Task<ClientsPlayer> ConnectToGameServer(string playersName, string colorOfFigures)
        {
           ClientsPlayer clientsPlayer = new ClientsPlayer();
            clientsPlayer.Player = new PlayerSubData() { Name = playersName };
            clientsPlayer.playersColor = new PlayersColor() { Color = colorOfFigures };

            string TargetUrl = "http://localhost:5069/api/GameQueue";
            string newPlayer = JsonConvert.SerializeObject(clientsPlayer);
            HttpContent httpContent = new StringContent(newPlayer, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                var responseForEnqueue = await httpClient.PostAsync(TargetUrl, httpContent);
                var response = await responseForEnqueue.Content.ReadAsStringAsync();
                var clientsPlayerfromServer = JsonConvert.DeserializeObject<ReceivedPostData>(response);
                clientsPlayer.Player.id = clientsPlayerfromServer.Player.Id.ToString();
                clientsPlayer.playersColor.Color = clientsPlayerfromServer.playersColor.Color;
            }         
           
            return clientsPlayer;
        }


    }
}
