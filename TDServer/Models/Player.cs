using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Composite;
using TDServer.Iterator;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Models
{
    public class Player
    {

        public const int STARTING_HEALTH = 20;
        public const int STARTING_MONEY = 200;

        public string Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Money { get; set; }
        public List<Minion> Minions { get; set; }
        public TowerCollection Towers { get; set; }
        public int Score { get; set; }
        private readonly Game Game;

        public Player(string id, Game game)
        {
            Id = id;
            Health = STARTING_HEALTH;
            Money = STARTING_MONEY;
            Minions = new List<Minion>();
            Towers = new TowerCollection();
            Game = game;
        }

        public void NotifyConsole(string formattedMessage)
        {
            Game.Hub.Clients.All.SendAsync("notifyConsole", formattedMessage);
        }
    }
}
