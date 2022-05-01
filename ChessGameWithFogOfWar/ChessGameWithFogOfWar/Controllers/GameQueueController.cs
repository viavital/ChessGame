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
        public GameQueueController(QueueProvider queueProveder)
        {
            this._queueProvider = queueProveder;     
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
        public IActionResult Post([FromBody] ReceivedPostData value)
        {
            //example "{\"player\":{\"name\":\"john\"},\"playersColor\":{\"color\":\"white\"}}"
           // var ReceivedPlayersData = JsonConvert.DeserializeObject<ReceivedPostData>(value);
            if (value == null)
            {
                return new BadRequestResult();
            }
            var addedPlayer = _queueProvider.Enqueue(value.Player, value.playersColor.ReturnColorEnum());
            if (_queueProvider.Contains(value.Player))
            {
                return new JsonResult(addedPlayer);
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
        public void CheckIsRivalsКCompleted()
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
                //  SendChangingColorProposal(PotentialTrailor); // todo
            }
            if (_queueProvider.CountWhite > 0 && _queueProvider.CountBlack > 0)
            {
                var CompletedRivals = _queueProvider.Dequeue();
                //  RedirectToGameProcessController(CompletedRivals); // todo
            }
        }
    }
}
