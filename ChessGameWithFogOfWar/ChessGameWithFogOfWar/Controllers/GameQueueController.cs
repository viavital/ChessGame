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
            //example "{\"player\":{\"name\":\"john\"},\"playersColor\":{\"color\":\"random\"}}"
            if (value == null)
            {
                return new BadRequestResult();
            }
            ColorOfTeamEnum playersColor = value.playersColor.ReturnColorEnum();
            var addedPlayer = _queueProvider.Enqueue(value.Player, playersColor);
            if (_queueProvider.Contains(value.Player))
            {
                return new JsonResult(new ReceivedPostData (addedPlayer, playersColor));
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
    }
}
