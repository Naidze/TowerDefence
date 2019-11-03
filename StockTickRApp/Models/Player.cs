using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;

namespace TDServer.Models
{
    public class Player
    {

        private static readonly int STARTING_HEALTH = 20;

        public string Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public List<Minion> Minions { get; set; }

        public Player(string id)
        {
            Id = id;
            Health = STARTING_HEALTH;
            Minions = new List<Minion>();
        }
    }
}
