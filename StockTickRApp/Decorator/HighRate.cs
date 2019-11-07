using GameClient.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Decorator
{
    public class HighRate : TowerDecorator
    {
        public HighRate(EnemyAttacker attacker) : base(attacker)
        {
            Rate = (int) (_attacker.Rate * 1.2);
            string upgrade = "rate";
            if (!attacker.Upgrades.ContainsKey(upgrade))
            {
                attacker.Upgrades.Add(upgrade, 0);
            }
            attacker.Upgrades[upgrade]++;
        }
    }
}
