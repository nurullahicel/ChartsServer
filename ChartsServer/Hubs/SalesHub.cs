﻿using Microsoft.AspNetCore.SignalR;

namespace ChartsServer.Hubs
{
    public class SalesHub : Hub
    {
        public async Task SendMessageAsync()
        {
            await Clients.All.SendAsync("receiveMessage", "alert");
        }
    }
}
