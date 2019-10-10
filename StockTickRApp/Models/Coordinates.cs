using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models
{
    public class Coordinates
    {
        public long Id { get; set; }
        public long PosX { get; set; }
        public long PosY { get; set; }

        public Coordinates()
        {
        }

        public Coordinates(long PosX, long PosY)
        {
            this.PosX = PosX;
            this.PosY = PosY;
        }

    }
}
