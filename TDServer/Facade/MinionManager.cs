using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TDServer.Adapter;
using TDServer.Composite;
using TDServer.Enums;
using TDServer.Helpers;
using TDServer.Models.Minions;

namespace TDServer.Facade
{
    public class MinionManager
    {
        private readonly Game _game;
        private readonly Random _random;

        private readonly Queue<Minion> minions = new Queue<Minion>();

        private readonly MinionComponent allMinions = new MinionViewGroup("Attackers");
        private readonly MinionComponent slowMinions = new MinionViewGroup("Slow Minions");
        private readonly MinionComponent fastMinions = new MinionViewGroup("Fast Minions");
        private readonly MinionComponent crawlers = new MinionViewGroup("Crawlers");
        private readonly MinionComponent noobs = new MinionViewGroup("Noobs");
        private readonly MinionComponent lizards = new MinionViewGroup("Lizards");
        public MinionsHolder holder;

        public MinionManager(Game game)
        {
            _game = game;
            _random = new Random();
            holder = new MinionsHolder(allMinions);
            allMinions.Add(slowMinions);
            allMinions.Add(fastMinions);
            fastMinions.Add(crawlers);
            slowMinions.Add(noobs);
            slowMinions.Add(lizards);
        }

        public void SpawnMinions()
        {
            if (_game.leftToSpawn == 0)
            {
                (noobs as MinionViewGroup).MinionComponents.Clear();
                (crawlers as MinionViewGroup).MinionComponents.Clear();
                (lizards as MinionViewGroup).MinionComponents.Clear();
                _game.leftToSpawn = -1;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(GameUtils.WAVE_INTERVAL);
                    _game.Hub.Clients.All.SendAsync("notifyConsole", string.Format("Wave {0} starting now", _game.wave));
                    _game.wave++;
                    _game.leftToSpawn = 5 + (_game.wave * 3);
                    for (int j = 0; j < _game.leftToSpawn; j++)
                    {
                        var min = GetMinionToSpawn();
                        minions.Enqueue(min);
                        if (min is Noob)
                        {
                            noobs.Add(new MinionView("NOOB"));
                        }
                        if (min is Lizard)
                        {
                            lizards.Add(new MinionView("LIZARD"));
                        }
                        if (min is Crawler)
                        {
                            crawlers.Add(new MinionView("CRAWLER"));
                        }
                    }
                    //_game.leftToSpawn = 0;
                });
            }
            else if (_game.leftToSpawn > 0 && _game.ticksBeforeSpawn-- == 0)
            {
                if (minions.Count > 0)
                    SpawnMinion(minions.Dequeue());
                _game.ticksBeforeSpawn = GameUtils.SPAWN_EVERY_X_TICK;
            }
        }

        private void SpawnMinion(Minion minion)
        {
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

        private Minion GetMinionToSpawn()
        {
            int pct = _random.Next(1, 11);
            if (pct > 8 && _game.wave >= 8)
            {
                return _game.unitFactory.CreateMinion(MinionType.LIZARD);

            }
            else if (pct > 6 && _game.wave >= 3)
            {
                return _game.unitFactory.CreateMinion(MinionType.CRAWLER);
            }
            else
            {
                return _game.unitFactory.CreateMinion(MinionType.NOOB);
            }
        }
    }
}
