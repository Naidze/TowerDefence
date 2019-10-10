﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models.Towers;

namespace GameServer.AbstractFactory
{
    public class ShortRangeFactory : TowerFactory
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