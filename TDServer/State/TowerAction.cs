using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models;
using TDServer.Models.Towers;

namespace TDServer.State
{
    [Serializable]
    public class TowerAction
    {
        public TowerActionState currentState { get; set; }

        public TowerAction()
        {
            TowerActionState observingState = new ObservingState();
            TowerActionState shootingState = new ShootingState();
            TowerActionState reloadingState = new ReloadingState();

            observingState.SetNextState(shootingState);
            shootingState.SetNextState(reloadingState);
            reloadingState.SetNextState(observingState);

            currentState = observingState;
        }

        public TowerActionState Action(EnemyAttacker tower, Player player)
        {
            if (currentState.ActionOperation(tower, player))
            {
                currentState.GetNextState(this);
            }
            return currentState;
        }

    }
}
