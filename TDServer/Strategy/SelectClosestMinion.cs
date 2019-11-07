using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Helpers;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Strategy
{
    public class SelectClosestMinion : ISelectMinion
    {

        public string Name => "closest";

        public Minion SelectEnemy(EnemyAttacker tower, List<Minion> minions)
        {
            double closestDistance = double.MaxValue;
            Minion closest = null;
            foreach (Minion minion in minions)
            {
                double distance = GameUtils.CalculateDistance(tower.Position, minion.Position);
                if (distance < tower.Range && distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = minion;
                }
            }
            return closest;
        }
    }
}
