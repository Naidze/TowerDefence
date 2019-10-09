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
            Console.WriteLine("Minion created.");
        }

        public abstract void Move();
    }
}
