using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Enums;
using TDServer.Models;
using TDServer.Models.Minions;
using TDServer.Models.Towers;

namespace TDServer.Factory
{
    public abstract class Factory
    {
        public abstract Minion CreateMinion(MinionType type);
        public abstract Tower CreateTower(TowerType type, Position position);
    }
}
