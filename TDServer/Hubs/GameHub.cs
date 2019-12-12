using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TDServer.Facade;
using TDServer.Helpers;

namespace TDServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly Game _game;
        private readonly GameStarter _gameStarter;
        private readonly GameStopper _gameStopper;
        private readonly TowerManager _towerManager;
        private readonly MinionManager _minionManager;
        private readonly Ticker _ticker;


        public GameHub(Game game)
        {
            _game = game;
            _towerManager = new TowerManager(game);
            _minionManager = new MinionManager(game);
            _ticker = new Ticker(game, _minionManager, _towerManager);
            _gameStarter = new GameStarter(game, _ticker);
            _gameStopper = new GameStopper(game);
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
            Clients.Caller.SendAsync("getMap", GameUtils.map);
        }

        public void PlaceTower(string name, string towerName, int x, int y)
        {
            _towerManager.PlaceTower(name, towerName, x, y);
        }

        public void ChangeAttackMode(string name, string towerId, string mode)
        {
            _towerManager.ChangeAttackMode(name, towerId, mode);
        }

        public void UpgradeTower(string name, string towerId, string type)
        {
            _towerManager.UpgradeTower(name, towerId, type);
        }

        public void SellTower(string name, string towerId)
        {
            _towerManager.SellTower(name, towerId);

        }

        public void NotifyConsole(string formattedMessage)
        {
            Clients.Caller.SendAsync("notifyConsole", formattedMessage);
        }
    }
}