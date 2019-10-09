using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models.Minions
{
    public class Noob : Minion
    {
        public Noob() : base()
        {
            Console.WriteLine("Hi, I'm Noob!");
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
