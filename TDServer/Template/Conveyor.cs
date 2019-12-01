using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models;
using TDServer.Models.Towers;

namespace TDServer.Template
{
    public abstract class Conveyor
    {
        protected EnemyAttacker enemyAttacker;
        public virtual EnemyAttacker BuildTower(Position position)
        {
            enemyAttacker = new Tower(position);
            return enemyAttacker;
        }
    }
}
