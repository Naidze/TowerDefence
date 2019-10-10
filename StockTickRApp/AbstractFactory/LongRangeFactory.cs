using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.AbstractFactory
{
    public class LongRangeFactory : TowerFactory
    {
        public override LongRangeGroundTower CreateGroundTower()
        {
            return new LongRangeGroundTower();
        }

        public override LongRangeSkyTower CreateSkyTower()
        {
            return new LongRangeSkyTower();
        }

        public override LongRangeUniversalTower CreateUniversalTower()
        {
            return new LongRangeUniversalTower();
        }
    }
}
