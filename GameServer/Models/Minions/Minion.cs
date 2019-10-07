using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Minions
{
    public abstract class Minion
    {
        public Minion()
        {
        }

        public abstract void Move();
    }
}
