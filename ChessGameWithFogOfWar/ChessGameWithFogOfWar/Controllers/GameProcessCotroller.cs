using ChessCore;
using ChessGameWithFogOfWar.Hubs;
using ChessGameWithFogOfWar.Model;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChessGameWithFogOfWar.Controllers
{
    public class GameProcessCotroller
    {
        private readonly IHubContext<GameProcessHub> _hubContext;

        private List<Chess> _chess;
        TaskCompletionSource tcs = new TaskCompletionSource();

        public GameProcessCotroller(IHubContext<GameProcessHub> hubContext)
        {
            _hubContext = hubContext;
            _chess = new List<Chess>();
        }
        
        public async Task OnMove(string moveByGameId) // { "gameid" : "**********", "move" : "Pb2b4" }
        {
            MoveByGameId _moveByGameId = JsonConvert.DeserializeObject<MoveByGameId>(moveByGameId);

            if (_moveByGameId.Move == "" || _moveByGameId.Move == null)
                return;
            var requiredChess = _chess.FirstOrDefault(c => c.GameId == _moveByGameId.GameId);
            if (requiredChess == null)
                return;

            await Task.Run(() => {requiredChess = requiredChess.Move(_moveByGameId.Move); });
            tcs.TrySetResult();
        }
        public async Task StartGame (Rivals _rivals)
        {
            Chess chess = new Chess(_rivals.GameId);

            await _hubContext.Clients.All.SendAsync("NewGameId", _rivals.GameId);
            await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync("NewGameId", _rivals.GameId); 

            while (true)
            {
                await _hubContext.Clients.Client(_rivals.WhitePlayer.IdConnection).SendAsync(chess.Fen); 
                await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync(chess.Fen); 

                foreach (var moves in chess.GetAllMoves())
                {
                    if (chess.ReturnMoveColor() == Color.white)
                    {
                        await _hubContext.Clients.Client(_rivals.WhitePlayer.IdConnection).SendAsync(moves);
                    }
                    else
                    {
                        await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync(moves);
                    }
                }
                await tcs.Task;
            }           
        }
    }
}
