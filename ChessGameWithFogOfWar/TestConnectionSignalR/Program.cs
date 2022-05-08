using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace TestConnectionSignalR
{
    class Program
    {
        static HubConnection _hubConnection;
        static async  Task Main(string[] args)
        {
            
            Console.WriteLine("press any key to start Connection");
            Console.ReadLine();
            HttpService httpService = new HttpService();
            await httpService.ConnectToGameServer("Vitalii", "white");
            Console.WriteLine("to start socket:");
            Console.ReadLine();
            _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5069/GameProcessHub").Build();

            _hubConnection.On<string>("Notify", mess => Console.WriteLine(mess));
            await _hubConnection.StartAsync();
            Console.ReadLine();
        }
    }
}
