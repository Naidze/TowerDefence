using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Helpers;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Strategy
{
    [Serializable]
    public class SelectStrongestMinion : ISelectMinion
    {
        public string Name => "strongest";

        public Minion SelectEnemy(EnemyAttacker tower, List<Minion> minions)
        {
            int strongestHealth = int.MinValue;
            Minion strongest = null;
            foreach (Minion minion in minions)
            {
                double distance = GameUtils.CalculateDistance(tower.Position, minion.Position);
                if (distance < tower.Range && minion.Health > strongestHealth)
                {
                    strongestHealth = minion.Health;
                    strongest = minion;
                }
            }
            return strongest;
        }

    }
}
