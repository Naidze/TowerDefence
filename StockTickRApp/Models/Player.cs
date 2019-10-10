using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Player(string id)
        {
            Id = id;
        }
    }
}
