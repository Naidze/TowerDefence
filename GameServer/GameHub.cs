using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer
{
    public class GameHub : Hub
    {

        private readonly Game _game;

        public GameHub(Game game)
        {
            _game = game;
        }

        public override Task OnConnectedAsync()
        {
            _game.AddPlayer(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _game.RemovePlayer(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public void SendToAll(string name, string message)
        {
            Clients.All.SendAsync("sendToAll", name, message);
        }

        public void ChangeName(string name)
        {
            _game.ChangeName(Context.ConnectionId, name);
        }


    }
}
