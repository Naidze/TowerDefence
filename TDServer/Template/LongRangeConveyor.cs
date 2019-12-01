using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Template
{
    public class LongRangeConveyor : TowerConveyor
    {
        public override void AddDamage()
        {
        }

        public override void AddRange()
        {
            enemyAttacker.Range = 20;
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
            return false;
        }

        public override bool NeedMoreRange()
        {
            return true;
        }
    }
}
