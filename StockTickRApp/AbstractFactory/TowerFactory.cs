using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.AbstractFactory
{
    public abstract class TowerFactory
    {
        public TowerFactory() { }

        public abstract LongRangeGroundTower CreateGroundTower(int x, int y);
        public abstract LongRangeSkyTower CreateSkyTower(int x, int y);
        public abstract LongRangeUniversalTower CreateUniversalTower(int x, int y);
    }
}
