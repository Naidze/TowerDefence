using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Helpers;

namespace TDServer.Facade
{
    public class GameStopper
    {
        private readonly Game _game;

        public GameStopper(Game game)
        {
            _game = game;
        }

        public void RemovePlayer(string connectionId)
        {
            if (_game.gameStarted)
            {
                StopGame();
            }
            for (int i = 0; i < GameUtils.PLAYER_COUNT; i++)
            {
                if (_game.players[i] != null && _game.players[i].Id == connectionId)
                {
                    _game.players[i] = null;
                    Logger.GetInstance().Info("Player " + (i + 1) + " has left, id: " + connectionId);
                    break;
                }
            }
        }

        private void StopGame()
        {
            _game.gameStarted = false;
            _game.gameLoop.Dispose();
            _game.Hub.Clients.All.SendAsync("gameStopping");
        }
    }
}
