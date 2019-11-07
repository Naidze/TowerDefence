using GameClient.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Decorator
{
    public class LongRange : TowerDecorator
    {
        public LongRange(EnemyAttacker attacker) : base(attacker)
        {
            Range = (int) (_attacker.Range * 1.1);
            string upgrade = "range";
            if (!attacker.Upgrades.ContainsKey(upgrade))
            {
                attacker.Upgrades.Add(upgrade, 0);
            }
            attacker.Upgrades[upgrade]++;
        }
    }
}
