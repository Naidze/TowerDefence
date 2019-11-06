using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Towers
{
    public class Tower : ICloneable
    {
        private static int idCounter = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public int Price { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int Rate { get; set; }
        public int TicksBeforeShot { get; set; }

        public Tower(Position position)
        {
            Id = idCounter++;
            Position = position;
            TicksBeforeShot = 0;
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
