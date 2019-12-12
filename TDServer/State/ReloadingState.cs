using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Helpers;
using TDServer.Models;
using TDServer.Models.Towers;

namespace TDServer.State
{
    [Serializable]
    public class ReloadingState : TowerActionState
    {

        private int ticksBeforeShot = GameUtils.SHOOT_EVERY_X_TICK;

        public override bool ActionOperation(EnemyAttacker tower, Player player)
        {
            ticksBeforeShot--;
            var finishedReloading = ticksBeforeShot == 0;
            if (ticksBeforeShot == 0)
            {
                ticksBeforeShot = GameUtils.SHOOT_EVERY_X_TICK;
            }
            return finishedReloading;
        }
    }
}
