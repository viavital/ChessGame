using ChessCore;
using ChessGameWithFogOfWar.Hubs;
using ChessGameWithFogOfWar.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameProcessController: ControllerBase
    {
        private readonly IHubContext<GameProcessHub> _hubContext;

        private List<Chess> _chess;
        TaskCompletionSource tcs = new TaskCompletionSource();

        public GameProcessController(IHubContext<GameProcessHub> hubContext)
        {
            _hubContext = hubContext;
            _chess = new List<Chess>();
        }

        [HttpPost]
        public async Task Post (MoveByGameId _moveByGameId) // { "gameid" : "**********", "move" : "Pb2b4" }
        {
           // MoveByGameId _moveByGameId = JsonConvert.DeserializeObject<MoveByGameId>(moveByGameId);

            if (_moveByGameId.Move == "" || _moveByGameId.Move == null)
                return;
            var requiredChess = _chess.FirstOrDefault(c => c.GameId == _moveByGameId.GameId);
            if (requiredChess == null)
                return;

            requiredChess = requiredChess.Move(_moveByGameId.Move);
            tcs.TrySetResult();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task StartGame (Rivals _rivals)
        {
            Chess chess = new Chess(_rivals.GameId);
            _chess.Add(chess);

            await _hubContext.Clients.Client(_rivals.WhitePlayer.IdConnection).SendAsync("NewGameId", _rivals.GameId);
            await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync("NewGameId", _rivals.GameId); 

            while (true)
            {
                tcs = new TaskCompletionSource();
                await _hubContext.Clients.Client(_rivals.WhitePlayer.IdConnection).SendAsync("NewFen",chess.Fen); 
                await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync("NewFen",chess.Fen); 

                //foreach (var moves in chess.GetAllMoves())
                //{
                //    if (chess.ReturnMoveColor() == Color.white)
                //    {
                //        await _hubContext.Clients.Client(_rivals.WhitePlayer.IdConnection).SendAsync("PossibleMoves",moves);
                //    }
                //    else
                //    {
                //        await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync("PossibleMoves",moves);
                //    }
                //}
               await tcs.Task;
            }
            await _hubContext.Clients.All.SendAsync("WTF?");
        }
    }
}
