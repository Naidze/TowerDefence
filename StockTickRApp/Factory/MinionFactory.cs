using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Enums;
using TDServer.Models.Minions;

namespace TDServer.Factory
{
    public class MinionFactory : Factory
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
    }
}
