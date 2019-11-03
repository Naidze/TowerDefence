using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.AbstractFactory
{
    public class ShortRangeFactory : TowerFactory
    {
        public override LongRangeGroundTower CreateGroundTower(int x, int y)
        {
            return new LongRangeGroundTower(x, y);
        }

        public override LongRangeSkyTower CreateSkyTower(int x, int y)
        {
            return new LongRangeSkyTower(x, y);
        }

        public override LongRangeUniversalTower CreateUniversalTower(int x, int y)
        {
            return new LongRangeUniversalTower(x, y);
        }
    }
}
