using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TDServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly Game _game;

        public GameHub(Game stockTicker)
        {
            _game = stockTicker;
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

        public void ChangeName(string name)
        {
            _game.ChangeName(Context.ConnectionId, name);
        }

        public void PlaceTower(string name, string towerName, int x, int y)
        {
            _game.PlaceTower(name, towerName, x, y);
        }

    }
}