using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Minions
{
    public class Crawler : Minion
    {
        public Crawler() : base()
        {
            Console.WriteLine("Hi, I'm Crawler!");
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
