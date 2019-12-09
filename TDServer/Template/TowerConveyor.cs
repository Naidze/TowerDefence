using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models;
using TDServer.Models.Towers;

namespace TDServer.Template
{
    public abstract class TowerConveyor : Conveyor
    {
        public sealed override EnemyAttacker BuildTower(Position position)
        {
            base.BuildTower(position);
            if (NeedMoreRange())
            {
                AddRange();
            }
            if (NeedMoreDamage())
            {
                AddDamage();
            }
            if (NeedMoreAttackRate())
            {
                AddRate();
            }
            return enemyAttacker;
        }

        public abstract bool NeedMoreRange();
        public abstract bool NeedMoreDamage();
        public abstract bool NeedMoreAttackRate();
        public abstract void AddRange();
        public abstract void AddDamage();
        public abstract void AddRate();
    }
}
