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

        private Player player1;
        private Player player2;

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
            Logger.GetInstance().Info("Game is starting!");
            Hub.Clients.All.SendAsync("gameStarting");
        }

    }

}