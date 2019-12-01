using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Template
{
    public class HighRateConveyor : TowerConveyor
    {
        public override void AddDamage()
        {
        }

        public override void AddRange()
        {
        }

        public override void AddRate()
        {
            enemyAttacker.Rate = 20;
        }

        public override bool NeedMoreAttackRate()
        {
            return true;
        }

        public override bool NeedMoreDamage()
        {
            return false;
        }

        public override bool NeedMoreRange()
        {
            return false;
        }
    }
}
