using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace restaurant_booking_Application.Common
{
    public class ChatHub : Hub
    {
        private readonly IConfiguration _config;
        public ChatHub(IConfiguration config)
        {
            _config = config;
        }

        HubConnection _connection = null;

       /* private async Task ConnectToServer()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(_config["HostUrl:Url"]).Build();
            await _connection.StartAsync();
        }*/
        

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

        }
    }
}
