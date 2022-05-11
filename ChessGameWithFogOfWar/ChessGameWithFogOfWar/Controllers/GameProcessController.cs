using ChessCore;
using ChessGameWithFogOfWar.Hubs;
using ChessGameWithFogOfWar.Model;
using ChessGameWithFogOfWar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameProcessController
    {
        private readonly IHubContext<GameProcessHub> _hubContext;

        
        TaskCompletionSource tcs = new TaskCompletionSource();

        public GameProcessController(IHubContext<GameProcessHub> hubContext)
        {
            _hubContext = hubContext;
            
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task StartGame (Rivals _rivals)
        {
            Chess chess = new Chess(_rivals.GameId, _rivals.WhitePlayer.IdConnection, _rivals.BlackPlayer.IdConnection);
            QueueProvider._chess.Add(chess);

            await _hubContext.Clients.Client(_rivals.WhitePlayer.IdConnection).SendAsync("NewGameId", _rivals.GameId);
            await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync("NewGameId", _rivals.GameId);
            await _hubContext.Clients.Client(_rivals.WhitePlayer.IdConnection).SendAsync("NewFen", chess.Fen);
            await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync("NewFen", chess.Fen);
            
        }
    }
}

