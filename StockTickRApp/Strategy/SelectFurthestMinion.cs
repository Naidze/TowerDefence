using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Helpers;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Strategy
{
    public class SelectFurthestMinion : ISelectMinion
    {

        private readonly EnemyAttacker Tower;

        public SelectFurthestMinion(EnemyAttacker tower)
        {
            Tower = tower;
        }

        public Minion SelectEnemy(List<Minion> minions)
        {
            double furthestDistance = double.MinValue;
            Minion furthest = null;
            foreach (Minion minion in minions)
            {
                double distance = GameUtils.CalculateDistance(Tower.Position, minion.Position);
                if (distance < Tower.Range && distance > furthestDistance)
                {
                    furthestDistance = distance;
                    furthest = minion;
                }
            }
            return furthest;
        }
    }
}
