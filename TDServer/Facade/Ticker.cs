using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Facade
{
    public class Ticker
    {
        private readonly Game _game;
        private readonly TowerManager _towerManager;
        private readonly MinionManager _minionManager;

        public Ticker(Game game, MinionManager minionManager, TowerManager towerManager)
        {
            _game = game;
            _towerManager = towerManager;
            _minionManager = minionManager;
        }

        public void Tick()
        {
            _minionManager.SpawnMinions();
            _minionManager.MoveMinions();
            _towerManager.TowersAction();
            _game.Hub.Clients.All.SendAsync("tick", _game.wave, _game.players);
        }
    }
}
