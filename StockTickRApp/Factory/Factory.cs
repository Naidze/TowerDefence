using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Enums;
using TDServer.Models.Minions;

namespace TDServer.Factory
{
    public abstract class Factory
    {
        public abstract Minion CreateMinion(MinionType type);
    }
}
