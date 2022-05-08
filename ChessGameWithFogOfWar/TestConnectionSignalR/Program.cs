using ChessGameWithFogOfWar.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;

namespace TestConnectionSignalR
{
    class Program
    {
        static HubConnection _hubConnection;
        static async  Task Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            var EnteredName = Console.ReadLine();
            Console.WriteLine("Enter the colour of figures you wanna play (white, black, random): ");
            string EnteredColor = null;
            while (EnteredColor == null)
            {
                EnteredColor = Console.ReadLine();
                if (EnteredColor != "white" || EnteredColor != "black" || EnteredColor != "random")
                {
                    break;
                }
                else
                    EnteredColor = "random";
            }
            Console.WriteLine($"Entered Name is - {EnteredName}, and colour - {EnteredColor}, press any key to proceed");
            Console.Read();
            HttpService httpService = new HttpService();
            var RegisteredPlayer = await httpService.ConnectToGameServer(EnteredName, EnteredColor);
            Console.WriteLine("Enter any string to connect socket:");
            Console.ReadLine();
            
            _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5069/GameProcessHub").Build();
            _hubConnection.On<string>("Notify", mess => 
            {
                Console.WriteLine(mess); 
                RegisteredPlayer.Player.IdConnection = JsonConvert.DeserializeObject<WelcomeMessage>(mess).Id;
                Console.WriteLine($"Your Player - {RegisteredPlayer.Player.Name}, " +
                                  $"your colour - {RegisteredPlayer.playersColor.Color},\n " +
                                  $"your Id - {RegisteredPlayer.Player.id},\n" +
                                  $" your Connction id - {RegisteredPlayer.Player.IdConnection}.");
            });

           // MoveByGameId moveByGameId = new MoveByGameId();
           
            //_hubConnection.On<string>("NewGameId", NewGameId => 
            //                        { 
            //                            moveByGameId.GameId = (NewGameId);
            //                            Console.WriteLine("Game is staeted, game id - " + moveByGameId.GameId);
            //                            _hubConnection.SendAsync("ClientReceivedGameId",
            //                                                     JsonConvert.SerializeObject(new OnReceivingGameIDMessage() { PlayersId = RegisteredPlayer.Player.id, GameId = NewGameId }));
            //                        });

            //bool IsGameOver = false;
            //while (!IsGameOver)
            //{

            //}
            await _hubConnection.StartAsync();
            
            Console.ReadLine();
        }
    }
}
