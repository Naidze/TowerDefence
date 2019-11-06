using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace GameClient.Decorator
{
    public class TowerDecorator : EnemyAttacker
    {
        protected EnemyAttacker _attacker;

        public TowerDecorator(EnemyAttacker attacker)
        {
            Damage = attacker.Damage;
            Range = attacker.Range;
            Rate = attacker.Rate;
            Position = attacker.Position;
            _attacker = attacker;
        }
    }
}
