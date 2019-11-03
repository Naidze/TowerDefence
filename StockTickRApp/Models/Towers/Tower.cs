using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Towers
{
    public class Tower
    {
        public Position Position { get; set; }
        public int Cost { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int Rate { get; set; }

        public Tower()
        {
        }
    }
}
