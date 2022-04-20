using ChessGameWithFogOfWar.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChessGameWithFogOfWar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameProcessController : Controller
    {
        private  Player _whitePlayer;
        private  Player _blackPlater;
        

        public async Task StartGame()
        {

        }

    }
}
