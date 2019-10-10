using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GameServer
{
    public class Game
    {

        private IHubContext<GameHub> _hub
        {
            get;
            set;
        }

        public Game(IHubContext<GameHub> hub)
        {
            _hub = hub;
        }

        private Player player1;
        private Player player2;

        public void AddPlayer(string connectionId)
        {
            if (player1 == null)
            {
                player1 = new Player(connectionId);
                Logger.GetInstance().Info("Player one has joined, id: " + connectionId);
            } else if (player2 == null)
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
            } else if (player2 != null && player2.Id == connectionId)
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
            Logger.GetInstance().Info("Game is starting!");
            //GameHub.Clients.All.SendAsync("gameStarting");
        }

    }
}
