﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.ChainOfResponsibility;
using TDServer.Factory;
using TDServer.Helpers;
using TDServer.Hubs;
using TDServer.Iterator;
using TDServer.Models;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Facade
{
    public class GameStarter
    {
        private readonly Game _game;
        private readonly Ticker _ticker;
        private readonly AbstractLogger logger = AbstractLogger.GetChainOfLoggers();

        public GameStarter(Game game, Ticker ticker)
        {
            _game = game;
            _ticker = ticker;
        }

        public bool AddPlayer(string connectionId)
        {
            bool added = false;
            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                if (_game.players[i] == null)
                {
                    _game.players[i] = new Player(connectionId, _game);
                    logger.LogMessage(LogLevel.INFO, "Player " + (i + 1) + " has joined, id: " + connectionId);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                return false;
            }
            if (ReadyToStart())
            {
                StartGame();
            }
            return true;
        }

        private bool ReadyToStart()
        {
            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                if (_game.players[i] == null)
                {
                    return false;
                }
            }
            return true;
        }

        public void StartGame()
        {
            _game.unitFactory = new UnitFactory();
            _game.gameStarted = true;
            foreach (Player player in _game.players)
            {
                player.Minions = new List<Minion>();
                player.Towers = new TowerCollection();
                player.Health = Player.STARTING_HEALTH;
                player.Money = Player.STARTING_MONEY;
            }
            _game.wave = 0;
            _game.leftToSpawn = 0;
            _game.ticksBeforeSpawn = 0;

            logger.LogMessage(LogLevel.FILE, "Game is starting!");
            _game.Hub.Clients.All.SendAsync("gameStarting");
            _game.gameLoop = new System.Threading.Timer(
                e => _ticker.Tick(),
                null,
                TimeSpan.Zero,
            TimeSpan.FromMilliseconds(GameUtils.TICK_INTERVAL));
        }
    }
}
