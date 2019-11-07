using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TDServer.Facade;

namespace TDServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly Game _game;
        private readonly GameStarter _gameStarter;
        private readonly GameStopper _gameStopper;
        private readonly TowerManager _towerManager;
        private readonly GameManager _gameManager;
        private readonly MinionManager _minionManager;

        public GameHub(Game game)
        {
            _game = game;
            _gameStopper = new GameStopper(game);
            _towerManager = new TowerManager(game);
            _minionManager = new MinionManager(game);
            _gameManager = new GameManager(game, _towerManager, _minionManager);
            _gameStarter = new GameStarter(game, _gameManager);
        }

        public override Task OnConnectedAsync()
        {
            if (_gameStarter.AddPlayer(Context.ConnectionId))
            {
                Clients.Caller.SendAsync("getConnectionId", Context.ConnectionId);
            } else
            {
                Clients.Caller.SendAsync("gameFull");
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _gameStopper.RemovePlayer(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public void ChangeName(string name)
        {
            _game.ChangeName(Context.ConnectionId, name);
        }

        public void AskForMap() {
            Clients.Caller.SendAsync("getMap", Game.map);
        }

        public void PlaceTower(string name, string towerName, int x, int y)
        {
            _towerManager.PlaceTower(name, towerName, x, y);
        }

        public void ChangeAttackMode(string name, int towerId, string mode)
        {
            _game.ChangeAttackMode(name, towerId, mode);
        }
    }
}