using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Helpers;
using TDServer.Models;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.State
{
    [Serializable]
    public class ShootingState : TowerActionState
    {

        public override bool ActionOperation(EnemyAttacker tower, Player player)
        {
            tower.Target.Health -= tower.Damage;
            if (tower.Target.Health <= 0)
            {
                player.Money += tower.Target.Reward;
                player.Score++;
                if(player.Score % 50 == 0)
                {
                    player.NotifyConsole(string.Format("Player {0} hit {1} score!", player.Name, player.Score));
                }
;                player.Minions.Remove(tower.Target);
            }

            return true;
        }
    }
}
