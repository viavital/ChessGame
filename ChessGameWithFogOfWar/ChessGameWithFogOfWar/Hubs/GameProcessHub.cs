using ChessGameWithFogOfWar.Model;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Hubs
{
    public class GameProcessHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.User(Context.ConnectionId).SendAsync(
                 JsonConvert.SerializeObject(new WelcomeMessage()
                 { Id = Context.ConnectionId, Type = "welcome"})); // client should whiteConnectionId in Player model
            await base.OnConnectedAsync();
        }
      
        //public bool CheckIsRivalsКCompleted()
        //{
        //    Player PotentialTrailor = null;
        //    if (_queueProvider.CountWhite > 1 && _queueProvider.CountBlack == 0)
        //    {
        //        PotentialTrailor = _queueProvider.PeekedWhite;
        //    }
        //    if (_queueProvider.CountBlack > 1 && _queueProvider.CountWhite == 0)
        //    {
        //        PotentialTrailor = _queueProvider.PeekedBlack;
        //    }
        //    if (PotentialTrailor != null)
        //    {
        //        SendChangingColorProposal(PotentialTrailor); // todo
        //    }
        //    if (_queueProvider.CountWhite > 0 && _queueProvider.CountBlack > 0)
        //    {
        //        var CompletedRivals = _queueProvider.Dequeue();
        //        RedirectToGameProcessController(CompletedRivals); // todo
        //    }
        //}
    }
}
