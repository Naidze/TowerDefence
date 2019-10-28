using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using TDServer.Hubs;
using TDServer.Models;
using Microsoft.AspNetCore.SignalR;
using TDServer.Models.Minions;
using TDServer.Factory;

namespace TDServer
{
    public class Game
    {

        public Game(IHubContext<GameHub> hub)
        {
            Hub = hub;
        }

        private IHubContext<GameHub> Hub
        {
            get;
            set;
        }

        private MinionFactory minionFactory;

        private Player player1;
        private Player player2;
        private int wave = 0;
        private Timer minionSpawnTimer;
        private int minionsSpawned = 0;
        private List<Minion> minions = new List<Minion>();

        public void AddPlayer(string connectionId)
        {
            if (player1 == null)
            {
                player1 = new Player(connectionId);
                Logger.GetInstance().Info("Player one has joined, id: " + connectionId);
            }
            else if (player2 == null)
            {
                player2 = new Player(connectionId);
                Logger.GetInstance().Info("Player two has joined, id: " + connectionId);
            }

            if (player1 != null && player2 != null)
            {
                StartGame();
            }
        }

        public void RemovePlayer(string connectionId)
        {
            if (player1 != null && player1.Id == connectionId)
            {
                player1 = null;
                Logger.GetInstance().Info("Player one has left, id: " + connectionId);
            }
            else if (player2 != null && player2.Id == connectionId)
            {
                player2 = null;
                Logger.GetInstance().Info("Player two has left, id: " + connectionId);
            }
        }

        public void ChangeName(string connectionId, string name)
        {
            if (player1 != null && player1.Id == connectionId)
            {
                player1.Name = name;
                Logger.GetInstance().Info("Set player one name to: " + name);
            }
            else if (player2 != null && player2.Id == connectionId)
            {
                player2.Name = name;
                Logger.GetInstance().Info("Set player two name to: " + name);
            }
        }

        public void StartGame()
        {
            minionFactory = new MinionFactory();
            Logger.GetInstance().Info("Game is starting!");
            Hub.Clients.All.SendAsync("gameStarting");
            StartWave();
        }

        public void StartWave()
        {
            wave++;
            minionSpawnTimer = new System.Threading.Timer(
                e => SpawnMinion(),
                null,
                TimeSpan.Zero,
            TimeSpan.FromSeconds(1));
        }

        private void SpawnMinion()
        {
            minionsSpawned++;
            if (minionsSpawned > 10)
            {
                minionSpawnTimer.Dispose();
                return;
            }

            var minion = minionFactory.CreateMinion(Enums.MinionType.NOOB);
            minions.Add(minion);
            Hub.Clients.All.SendAsync("spawnMinion", Enums.MinionType.NOOB.ToString());
        }

    }

}