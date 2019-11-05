﻿using System;
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
using TDServer.AbstractFactory;
using TDServer.Models.Towers;

namespace TDServer
{
    public class Game
    {

        public static readonly Path[] map = new Path[] {
            new Path(-20, 185),
            new Path(68, 185),
            new Path(68, 68),
            new Path(188, 68),
            new Path(188, 228),
            new Path(350, 228),
            new Path(350, 147),
            new Path(550, 147)
        };

        private const int PLAYER_COUNT = 2;

        private const int TICK_INTERVAL = 10;
        private const int WAVE_INTERVAL = 1000;

        private const int SPAWN_EVERY_X_TICK = 10;
        private const int SHOOT_EVERY_X_TICK = 50;

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
        private bool gameStarted = false;
        private int wave;
        private Timer gameLoop;
        private int leftToSpawn;
        private int ticksBeforeSpawn;

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
            if (gameStarted)
            {
                StopGame();
            }
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

        public void PlaceTower(string name, string towerName, int x, int y)
        {
            Player player = GetPlayer(name);
            if (player == null)
            {
                return;
            }

            ShortRangeFactory factory = new ShortRangeFactory();
            player.Towers.Add(factory.CreateUniversalTower(x, y));
        }

        private void StopGame()
        {
            gameStarted = false;
            gameLoop.Dispose();
            Hub.Clients.All.SendAsync("gameStopping");
        }

        public void StartGame()
        {
            minionFactory = new MinionFactory();
            gameStarted = true;
            wave = 0;
            leftToSpawn = 0;
            ticksBeforeSpawn = 0;

            Logger.GetInstance().Info("Game is starting!");
            Hub.Clients.All.SendAsync("gameStarting");
            gameLoop = new System.Threading.Timer(
                e => Tick(),
                null,
                TimeSpan.Zero,
            TimeSpan.FromMilliseconds(TICK_INTERVAL));
        }

        private void Tick()
        {
            SpawnMinions();
            MoveMinions();
            FireTowers();
            Hub.Clients.All.SendAsync("tick", wave, players);
        }

        private void SpawnMinions()
        {
            //if (leftToSpawn == 0)
            if (leftToSpawn == 0 && wave == 0)
            {
                leftToSpawn = -1;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(WAVE_INTERVAL);
                    wave++;
                    leftToSpawn = 1;
                    //leftToSpawn = 5 + (wave * 3);
                });
            }
            else if (leftToSpawn > 0 && ticksBeforeSpawn-- == 0)
            {
                SpawnMinion();
                ticksBeforeSpawn = SPAWN_EVERY_X_TICK;
            }
        }

        private void SpawnMinion()
        {
            var minion = minionFactory.CreateMinion(Enums.MinionType.NOOB);
            leftToSpawn--;
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                players[i].Minions.Add(minion);
                //if (players[i].Health > 0)
                //{
                //    players[i].Minions.Add(minion);
                //}
            }
        }

        private void MoveMinions()
        {
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                for (int j = players[i].Minions.Count - 1; j >= 0; j--)
                {
                    var minion = players[i].Minions[j];
                    if (!minion.Move())
                    {
                        players[i].Minions.RemoveAt(j);
                        players[i].Health--;
                    }
                }
            }
        }

        private void FireTowers()
        {
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                foreach (Tower tower in players[i].Towers)
                {
                    if (tower.TicksBeforeShot-- > 0)
                    {
                        continue;
                    }

                    Minion minion = FindClosestMinion(tower, players[i].Minions);
                    if (minion == null)
                    {
                        continue;
                    }
                    DamageMinion(players[i], tower, minion);
                }
            }
        }

        private void DamageMinion(Player player, Tower tower, Minion minion)
        {
            tower.TicksBeforeShot = SHOOT_EVERY_X_TICK / tower.Rate;
            minion.Health -= tower.Damage;
            if (minion.Health <= 0)
            {
                player.Minions.Remove(minion);
            }
        }

        private Minion FindClosestMinion(Tower tower, List<Minion> minions)
        {
            double closestDistance = double.MaxValue;
            Minion closest = null;
            foreach (Minion minion in minions)
            {
                double distance = CalculateDistance(tower.Position, minion.Position);
                if (distance < tower.Range && distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = minion;
                }
            }
            return closest;
        }

        private double CalculateDistance(Position a, Position b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }

        private Player GetPlayer(string name)
        {
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                if (players[i].Name == name)
                {
                    return players[i];
                }
            }
            return null;
        }

    }

}