using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.State
{
    [Serializable]
    public class ObservingState : TowerActionState
    {
        public override bool ActionOperation(EnemyAttacker tower, Player player)
        {
            Minion minion = tower.AttackMode.SelectEnemy(tower, player.Minions);
            tower.Target = minion;
            return tower.Target != null;
        }
    }
}
