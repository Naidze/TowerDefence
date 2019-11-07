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
            Rate = _attacker.Rate + 10;
            string upgrade = "rate";
            if (!attacker.Upgrades.ContainsKey(upgrade))
            {
                attacker.Upgrades.Add(upgrade, 0);
            }
            attacker.Upgrades[upgrade]++;
        }
    }
}
