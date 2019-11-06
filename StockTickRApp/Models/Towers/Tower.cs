using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Towers
{
    public class Tower : EnemyAttacker, ICloneable
    {
        public Tower()
        {
        }

        public Tower(int x, int y) : base(x, y)
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
