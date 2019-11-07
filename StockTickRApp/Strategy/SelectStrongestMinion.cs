using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Helpers;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Strategy
{
    public class SelectStrongestMinion : ISelectMinion
    {

        private readonly EnemyAttacker Tower;

        public SelectStrongestMinion(EnemyAttacker tower)
        {
            Tower = tower;
        }

        public string Name => "strongest";

        public Minion SelectEnemy(List<Minion> minions)
        {
            int strongestHealth = int.MinValue;
            Minion strongest = null;
            foreach (Minion minion in minions)
            {
                double distance = GameUtils.CalculateDistance(Tower.Position, minion.Position);
                if (distance < Tower.Range && minion.Health > strongestHealth)
                {
                    strongestHealth = minion.Health;
                    strongest = minion;
                }
            }
            return strongest;
        }

    }
}
