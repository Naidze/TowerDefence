﻿using GameClient.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Decorator
{
    public class HighDamage : TowerDecorator
    {
        public HighDamage(EnemyAttacker attacker) : base(attacker)
        {
            Damage = (int) (_attacker.Damage * 1.1);
            string upgrade = "damage";
            if (!attacker.Upgrades.ContainsKey(upgrade)) {
                attacker.Upgrades.Add(upgrade, 0);
            }
            attacker.Upgrades[upgrade]++;
        }
    }
}
