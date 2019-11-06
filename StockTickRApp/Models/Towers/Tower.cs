using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Towers
{
    public class Tower : EnemyAttacker, ICloneable
    {
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
