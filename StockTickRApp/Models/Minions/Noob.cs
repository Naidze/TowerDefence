using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Minions
{
    public class Noob : Minion
    {
        public Noob() : base("noob")
        {
            Debug.WriteLine("Hi, I'm Noob!");
        }

        public override void Move()
        {
            Position.X += MoveSpeed;
        }
    }
}
