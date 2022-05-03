using ChessCore;
using ChessGameWithFogOfWar.Hubs;
using ChessGameWithFogOfWar.Model;
using Microsoft.AspNetCore.SignalR;

namespace ChessGameWithFogOfWar.Controllers
{
    public class GameProcessCotroller
    {
        private readonly IHubContext<GameProcessHub> _hubContext;

        private readonly Rivals _rivals;
        private Chess _chess;
        TaskCompletionSource tcs = new TaskCompletionSource();

        public GameProcessCotroller(IHubContext<GameProcessHub> hubContext, Rivals rivals)
        {
            _hubContext = hubContext;
            _rivals = rivals;
            _chess = new Chess();
        }
        public async Task OnMove(string move)
        {
            if (move == "" || move == null)
                return;
            await Task.Run(() => { _chess = _chess.Move(move); });

            tcs.TrySetResult();
        }
        public async Task StartGame ()
        {
            while (true)
            {
                await _hubContext.Clients.Client(_rivals.WhitePlayer.IdConnection).SendAsync(_chess.Fen);
                await _hubContext.Clients.Client(_rivals.BlackPlayer.IdConnection).SendAsync(_chess.Fen);

                foreach (var moves in _chess.GetAllMoves())
                {
                    if (_chess.ReturnMoveColor() == Color.white)
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
