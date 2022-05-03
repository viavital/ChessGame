using ChessGameWithFogOfWar.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChessGameWithFogOfWar.Controllers
{
    public class GameProcessCotroller
    {
        private readonly IHubContext<GameProcessHub> _hubContext;

        public GameProcessCotroller(IHubContext<GameProcessHub> hubContext)
        {
            _hubContext = hubContext;
        }
    }
}
