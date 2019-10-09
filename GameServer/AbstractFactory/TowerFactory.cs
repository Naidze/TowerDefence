using GameServer.Models.Towers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.AbstractFactory
{
    public abstract class TowerFactory
    {
        public TowerFactory() { }

        public abstract LongRangeGroundTower CreateGroundTower();
        public abstract LongRangeSkyTower CreateSkyTower();
        public abstract LongRangeUniversalTower CreateUniversalTower();
    }
}
