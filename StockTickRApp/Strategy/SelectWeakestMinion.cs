using System.Collections.Generic;
using TDServer.Helpers;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Strategy
{
    public class SelectWeakestMinion : ISelectMinion
    {

        private readonly EnemyAttacker Tower;

        public SelectWeakestMinion(EnemyAttacker tower)
        {
            Tower = tower;
        }

        public Minion SelectEnemy(List<Minion> minions)
        {
            int weakestHealth = int.MaxValue;
            Minion weakest = null;
            foreach (Minion minion in minions)
            {
                double distance = GameUtils.CalculateDistance(Tower.Position, minion.Position);
                if (distance < Tower.Range && minion.Health < weakestHealth)
                {
                    weakestHealth = minion.Health;
                    weakest = minion;
                }
            }
            return weakest;
        }
    }
}
