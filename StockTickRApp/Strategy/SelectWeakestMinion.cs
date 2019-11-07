using System.Collections.Generic;
using TDServer.Helpers;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Strategy
{
    public class SelectWeakestMinion : ISelectMinion
    {
        public string Name => "weakest";

        public Minion SelectEnemy(EnemyAttacker tower, List<Minion> minions)
        {
            int weakestHealth = int.MaxValue;
            Minion weakest = null;
            foreach (Minion minion in minions)
            {
                double distance = GameUtils.CalculateDistance(tower.Position, minion.Position);
                if (distance < tower.Range && minion.Health < weakestHealth)
                {
                    weakestHealth = minion.Health;
                    weakest = minion;
                }
            }
            return weakest;
        }
    }
}
