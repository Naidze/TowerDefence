using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Builder;
using TDServer.Enums;
using TDServer.Models;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Factory
{
    public class UnitFactory : Factory
    {
        public override Minion CreateMinion(MinionType type)
        {
            switch (type)
            {
                case MinionType.CRAWLER:
                    return new Crawler();
                case MinionType.LIZARD:
                    return new Lizard();
                case MinionType.NOOB:
                    return new Noob();
                default: return null;
            }
        }

        public override Tower CreateTower(TowerType type, Position position)
        {
            switch (type)
            {

                case TowerType.SOLDIER:
                    return new TowerBuilder(position)
                        .SetName(type.ToString().ToLower())
                        .SetRate(1)
                        .SetDamage(20)
                        .SetRange(30)
                        .SetPrice(50)
                        .GetResult();
                case TowerType.ARCHER:
                    return new TowerBuilder(position)
                        .SetName(type.ToString().ToLower())
                        .SetRate(10)
                        .SetDamage(5)
                        .SetRange(50)
                        .SetPrice(30)
                        .GetResult();
                default: return null;
            }
        }
    }
}
