using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Path { get; set; }

        public Position()
        {
            X = 0;
            Y = 0;
            Path = 0;
        }

    }
}
