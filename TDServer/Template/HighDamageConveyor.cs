using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Template
{
    public class HighDamageConveyor : TowerConveyor
    {
        public override void AddDamage()
        {
            enemyAttacker.Damage = 20;
        }

        public override void AddRange()
        {
        }

        public override void AddRate()
        {
        }

        public override bool NeedMoreAttackRate()
        {
            return false;
        }

        public override bool NeedMoreDamage()
        {
            return true;
        }

        public override bool NeedMoreRange()
        {
            return false;
        }
    }
}
