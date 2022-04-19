using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessGameWithFogOfWar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameQueueController : ControllerBase
    {
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

        // GET api/<GameQueueController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GameQueueController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GameQueueController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GameQueueController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
