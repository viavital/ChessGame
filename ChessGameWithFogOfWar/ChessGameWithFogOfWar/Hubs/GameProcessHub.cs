using ChessGameWithFogOfWar.Model;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Hubs
{
    public class GameProcessHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
           await Clients.Caller.SendAsync("Notify",
                 JsonConvert.SerializeObject(new WelcomeMessage()
                 { Id = Context.ConnectionId, Type = "welcome"})); // client should whiteConnectionId in Player model           
           await base.OnConnectedAsync();
        }
    }
}
