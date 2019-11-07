using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TDServer.Adapter;
using TDServer.Helpers;
using TDServer.Models.Minions;

namespace TDServer.Facade
{
    public class MinionManager
    {
        private readonly Game _game;

        public MinionManager(Game game)
        {
            _game = game;
        }

        public void SpawnMinions()
        {
            if (_game.leftToSpawn == 0)
            {
                _game.leftToSpawn = -1;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(GameUtils.WAVE_INTERVAL);
                    _game.wave++;
                    _game.leftToSpawn = 5 + (_game.wave * 3);
                });
            }
            else if (_game.leftToSpawn > 0 && _game.ticksBeforeSpawn-- == 0)
            {
                SpawnMinion();
                _game.ticksBeforeSpawn = GameUtils.SPAWN_EVERY_X_TICK;
            }
        }

        private void SpawnMinion()
        {
            var minion = _game.unitFactory.CreateMinion(Enums.MinionType.NOOB);
            _game.leftToSpawn--;
            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                _game.players[i].Minions.Add(minion.Clone());
                //if (players[i].Health > 0)
                //{
                //    players[i].Minions.Add(minion);
                //}
            }
        }
        public void MoveMinions()
        {
            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                for (int j = _game.players[i].Minions.Count - 1; j >= 0; j--)
                {
                    var minion = _game.players[i].Minions[j];
                    if (((minion is Crawler) && !new CrawlerAdapter(minion as Crawler, minion.Name).Move())
                        || ((minion is Lizard) && !new LizardAdapter(minion as Lizard, minion.Name).Move())
                        || ((minion is Noob) && !(minion as Noob).Move()))
                    {
                        _game.players[i].Minions.RemoveAt(j);
                        _game.players[i].Health--;
                    }
                }
            }
        }
    }
}
