using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models
{
    public class Path
    {

        public int X { get; set; }
        public int Y { get; set; }

        public Path(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
