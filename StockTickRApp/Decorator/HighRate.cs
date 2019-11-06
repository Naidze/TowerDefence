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
        }
    }
}
