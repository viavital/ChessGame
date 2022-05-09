using ChessGameWithFogOfWar.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Text;

namespace TestConnectionSignalR
{
    class Program
    {
        static HubConnection _hubConnection;
        static TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        static bool IsGameOver = false;
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


            _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5069/GameProcessHub").Build();

            _hubConnection.On<string>("FirstConnectionSocket", async mess =>
            {
                Console.WriteLine(mess);
                RegisteredPlayer.Player.IdConnection = JsonConvert.DeserializeObject<WelcomeMessage>(mess).Id;
                Console.WriteLine($"Your Player - {RegisteredPlayer.Player.Name}, " +
                                  $"your colour - {RegisteredPlayer.playersColor.Color},\n " +
                                  $"your Id - {RegisteredPlayer.Player.id},\n" +
                                  $" your Connction id - {RegisteredPlayer.Player.IdConnection}.");
                using (HttpClient httpClient = new HttpClient())
                {
                    UpdateIdConnectionMessage updateIdConnectionMessage = new UpdateIdConnectionMessage()
                    {
                        PlayersId = RegisteredPlayer.Player.id,
                        ConnectionId = RegisteredPlayer.Player.IdConnection
                    };

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(updateIdConnectionMessage), Encoding.UTF8, "application/json");
                    var responce = await httpClient.PutAsync("http://localhost:5069/api/GameQueue", content);
                }
            });
            MoveByGameId moveByGameId = new MoveByGameId();

            _hubConnection.On<string>("NewGameId", NewGameId =>
            {
                moveByGameId.GameId = NewGameId;
                Console.WriteLine("Game is started, game id - " + moveByGameId.GameId);
                //_hubConnection.SendAsync("ClientReceivedGameId",
                //                         JsonConvert.SerializeObject(new OnReceivingGameIDMessage() { PlayersId = RegisteredPlayer.Player.id, GameId = NewGameId }));
            });

            
            _hubConnection.On<string>("NewFen", async Fen =>
            {
                Console.WriteLine(Fen);
                Console.WriteLine("Enter your Move or \"exit\" to exit");
                moveByGameId.Move = Console.ReadLine();
                if (moveByGameId.Move == "exit")
                {
                    taskCompletionSource.TrySetResult();
                    IsGameOver = true;
                }
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(moveByGameId), Encoding.UTF8, "application/json");
                    await httpClient.PostAsync("http://localhost:5069/api/GameProcess", content);
                }
                //_hubConnection.SendAsync("OnMove", JsonConvert.SerializeObject(moveByGameId));
            });

            _hubConnection.StartAsync();
            
            while (!IsGameOver)
            {
                await taskCompletionSource.Task;
            }
        }
    }
}
