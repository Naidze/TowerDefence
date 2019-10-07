using GameServer.Models.Minions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Factory
{
    public abstract class Factory
    {
        public abstract Minion CreateMinion(string type);
    }
}
