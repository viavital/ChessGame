﻿using ChessGameWithFogOfWar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
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
        public HttpStatusCode Post([FromBody] string value)
        {
            // example "{\"Player\":{\"Name\":\"John\"},\"PlayersColor\":{\"Color\":\"White\"}}"
            var ReceivedPlayersData = JsonConvert.DeserializeObject<ReceivedPostData>(value);
            if (ReceivedPlayersData == null)
            {
                return HttpStatusCode.BadRequest;
            }
            _queueProvider.Enqueue(ReceivedPlayersData.Player, ReceivedPlayersData.PlayersColor.ReturnColorEnum());
            if (_queueProvider.Contains(ReceivedPlayersData.Player))
            {
                return HttpStatusCode.Accepted;
            }
            return HttpStatusCode.Conflict;
        }


        // DELETE api/<GameQueueController>/5
        [HttpDelete("{name}")]
        public void Delete(int id)
        {
        }

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
                SendChangingColorProposal(PotentialTrailor); // todo
            }
            if (_queueProvider.CountWhite > 0 && _queueProvider.CountBlack > 0)
            {
              var CompletedRivals = _queueProvider.Dequeue();
                RedirectToGameProcessController(CompletedRivals); // todo
            }
        }
    }
}
