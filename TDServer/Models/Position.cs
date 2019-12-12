using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models
{
    [Serializable]
    public class Position : ICloneable
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

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Path = 0;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
