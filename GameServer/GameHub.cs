using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer
{
    public class GameHub : Hub
    {

        private Game game;

        public GameHub()
        {
            game = Game.Instance;
        }

        public override Task OnConnectedAsync()
        {
            game.AddPlayer(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            game.RemovePlayer(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public void SendToAll(string name, string message)
        {
            Clients.All.SendAsync("sendToAll", name, message);
        }

        public void ChangeName(string name)
        {
            game.ChangeName(Context.ConnectionId, name);
        }


    }
}
