using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models;
using TDServer.Models.Towers;

namespace TDServer.State
{
    public abstract class TowerActionState
    {
        private TowerActionState nextState;

        public void SetNextState(TowerActionState nextState)
        {
            this.nextState = nextState;
        }

        public void GetNextState(TowerAction context)
        {
            context.currentState = nextState;
        }

        public abstract bool ActionOperation(EnemyAttacker tower, Player player);

    }
}
