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

        public GameQueueController(QueueProvider queueProvider, GameProcessCotroller gameProcessCotroller)
        {
            this._queueProvider = queueProvider;  
            this._gameProcessCotroller = gameProcessCotroller;
            _queueProvider.RivalsCompletedEvent += OnRivalsCompleted;
        }

        private void OnRivalsCompleted(Rivals obj)
        {
            _gameProcessCotroller.StartGame(obj);
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
                return new JsonResult(new ReceivedPostData (value.Player, playersColor));
            }
            return new BadRequestResult();
        }
        //POST api/<GameQueueController>
        [HttpPut]
        public  IActionResult Put (UpdateIdConnectionMessage value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }
            Player UpdatedPlayer = _queueProvider.ReturnPlayer(value.PlayersId);
            if (UpdatedPlayer == null)
            {
                return new BadRequestResult();
            }
            UpdatedPlayer.IdConnection = value.ConnectionId;
            _queueProvider.CheckIsRivalsКCompleted();
            return new OkResult();           
        }

        // DELETE api/<GameQueueController>/
        [HttpDelete("{Id}")]
        public IActionResult Delete(string Id)
        {
          var response =  _queueProvider.DeletePlayerFromQueue(Id);
            return new JsonResult(response);
        }

    }
}
