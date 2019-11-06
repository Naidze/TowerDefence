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
            Range = _attacker.Range + 10;
        }
    }
}
