using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Models.Minions;

namespace GameServer.Factory
{
    public class MinionFactory : Factory
    {
        public override Minion CreateMinion(string type)
        {
            switch (type)
            {
                case "C":
                    return new Crawler();
                case "L":
                    return new Lizard();
                case "N":
                    return new Noob();
                default: return null;
            }
        }
    }
}
