using ChessCore;
using ChessGameWithFogOfWar.Model;
using ChessGameWithFogOfWar.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Hubs
{
    public class GameProcessHub : Hub
    {
        public override async Task OnConnectedAsync()
        {            
           await Clients.Caller.SendAsync("FirstConnectionSocket",
                 JsonConvert.SerializeObject(new WelcomeMessage()
                 { Type = "welcome", Id = Context.ConnectionId})); // client should whiteConnectionId in Player model           
           await base.OnConnectedAsync();
        }
        public async Task OnMove(string _moveByGameId) // { "gameid" : "**********", "move" : "Pb2b4" }
        {
             MoveByGameId moveByGameId = JsonConvert.DeserializeObject<MoveByGameId>(_moveByGameId);

            if (moveByGameId == null && moveByGameId.Move == "")
                return;
            var requiredChess = QueueProvider._chess.FirstOrDefault(c => c.GameId == moveByGameId.GameId);
            if (requiredChess == null)
                return;

            requiredChess = requiredChess.Move(moveByGameId.Move);
            await Clients.Client(requiredChess.WhitePlayerId).SendAsync("NewFen", requiredChess.Fen);
            await Clients.Client(requiredChess.BlackPlayerId).SendAsync("NewFen", requiredChess.Fen);
            foreach (var moves in requiredChess.GetAllMoves())
            {
                if (requiredChess.ReturnMoveColor() == Color.white)
                {
                    await Clients.Client(requiredChess.WhitePlayerId).SendAsync("PossibleMoves", moves);
                }
                else
                {
                    await Clients.Client(requiredChess.BlackPlayerId).SendAsync("PossibleMoves", moves);
                }
            }
        }
    }
}
