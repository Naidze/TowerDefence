using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;

namespace TDServer.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Minion> Minions { get; set; }

        public Player(string id)
        {
            Id = id;
            Minions = new List<Minion>();
        }
    }
}
