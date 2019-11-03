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

        private const int PLAYER_COUNT = 2;

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

        private Player[] players = new Player[PLAYER_COUNT];
        private int wave = 0;
        private Timer gameLoop;
        private Timer minionSpawnTimer;
        private int minionsSpawned = 0;

        public void AddPlayer(string connectionId)
        {
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                if (players[i] == null)
                {
                    players[i] = new Player(connectionId);
                    Logger.GetInstance().Info("Player " + (i + 1) + " has joined, id: " + connectionId);
                    break;
                }
            }

            if (ReadyToStart())
            {
                StartGame();

            }
        }

        private bool ReadyToStart()
        {
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                if (players[i] == null)
                {
                    return false;
                }
            }
            return true;
        }

        public void RemovePlayer(string connectionId)
        {
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                if (players[i] != null && players[i].Id == connectionId)
                {
                    players[i] = null;
                    Logger.GetInstance().Info("Player " + (i + 1) + " has left, id: " + connectionId);
                    break;
                }
            }
        }

        public void ChangeName(string connectionId, string name)
        {
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                if (players[i] != null && players[i].Id == connectionId)
                {
                    players[i].Name = name;
                    Logger.GetInstance().Info("Set player " + (i + 1) + " name to: " + name);
                    break;
                }
            }
        }

        public void StartGame()
        {
            minionFactory = new MinionFactory();
            Logger.GetInstance().Info("Game is starting!");
            Hub.Clients.All.SendAsync("gameStarting");
            StartWave();
            gameLoop = new System.Threading.Timer(
                e => Tick(),
                null,
                TimeSpan.Zero,
            TimeSpan.FromMilliseconds(1000));
        }

        private void Tick()
        {
            MoveMinions();
            Hub.Clients.All.SendAsync("tick", players);
        }

        private void MoveMinions()
        {
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                foreach (var minion in players[i].Minions)
                {
                    minion.Move();
                }
            }
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
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                players[i].Minions.Add(minion);
            }
            Hub.Clients.All.SendAsync("spawnMinion", minion.Id, Enums.MinionType.NOOB.ToString());
        }

    }

}