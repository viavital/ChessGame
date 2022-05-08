using ChessGameWithFogOfWar.Services;
using Microsoft.AspNetCore.Mvc;
using ChessGameWithFogOfWar.Model;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessGameWithFogOfWar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameQueueController : ControllerBase
    {
        private readonly QueueProvider _queueProvider;
        private readonly GameProcessCotroller _gameProcessCotroller;
        
        public GameQueueController(QueueProvider queueProveder, GameProcessCotroller gameProcessCotroller)
        {
            this._queueProvider = queueProveder;  
            this._gameProcessCotroller = gameProcessCotroller;
        }

        // GET: api/<GameQueueController>
        [HttpGet]
        public ContentResult Get()
        {
            var fileContents = System.IO.File.ReadAllText("./Frontend/View/Welcome.html");
            return new ContentResult
            {
                Content = fileContents,
                ContentType = "text/html"
            };           
        }

        //POST api/<GameQueueController>
        [HttpPost]
        public IActionResult Post( ReceivedPostData value)
        {
            //example "{\"player\":{\"name\":\"john\"},\"playersColor\":{\"color\":\"random\"}}"
            if (value == null)
            {
                return new BadRequestResult();
            }
            ColorOfTeamEnum playersColor = value.playersColor.ReturnColorEnum();
            var addedPlayer = _queueProvider.Enqueue(value.Player, playersColor);
            if (_queueProvider.Contains(value.Player))
            {
                Task.Run(() => { CheckIsRivalsКCompleted(); });
                return new JsonResult(new ReceivedPostData (value.Player, playersColor));
            }
            return new BadRequestResult();
        }


        // DELETE api/<GameQueueController>/
        [HttpDelete("{Id}")]
        public IActionResult Delete(string Id)
        {
          var response =  _queueProvider.DeletePlayerFromQueue(Id);
            return new JsonResult(response);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> CheckIsRivalsКCompleted()
        {
            Player PotentialTrailor = null;
            if (_queueProvider.CountWhite > 1 && _queueProvider.CountBlack == 0)
            {
                PotentialTrailor = _queueProvider.PeekedWhite;
            }
            if (_queueProvider.CountBlack > 1 && _queueProvider.CountWhite == 0)
            {
                PotentialTrailor = _queueProvider.PeekedBlack;
            }
            if (PotentialTrailor != null)
            {
                return false;
            }
            if (_queueProvider.CountWhite > 0 && _queueProvider.CountBlack > 0)
            {
               Rivals CompletedRivals = _queueProvider.Dequeue();
               _gameProcessCotroller.StartGame(CompletedRivals);
            }
            return true;
        }
    }
}
