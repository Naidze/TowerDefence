using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Minions
{
    public class Crawler : Minion
    {
        public Crawler() : base("crawler")
        {
            Debug.WriteLine("Hi, I'm Crawler!");
        }

    }
}
