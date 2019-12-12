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
    public class SelectFurthestMinion : ISelectMinion
    {
        public string Name => "furthest";

        public Minion SelectEnemy(EnemyAttacker tower, List<Minion> minions)
        {
            double furthestDistance = double.MinValue;
            Minion furthest = null;
            foreach (Minion minion in minions)
            {
                double distance = GameUtils.CalculateDistance(tower.Position, minion.Position);
                if (distance < tower.Range && distance > furthestDistance)
                {
                    furthestDistance = distance;
                    furthest = minion;
                }
            }
            return furthest;
        }
    }
}
