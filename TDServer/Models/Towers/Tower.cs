using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Strategy;

namespace TDServer.Models.Towers
{
    public class Tower : EnemyAttacker, ICloneable
    {
        public Tower(Position position)
        {
            Id = idCounter++;
            Position = position;
            AttackMode = new SelectClosestMinion();
            Upgrades = new Dictionary<string, int>();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
