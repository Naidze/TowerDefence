﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Minions
{
    public class Lizard : Minion
    {
        public Lizard() : base()
        {
            Console.WriteLine("Hi, I'm Lizard!");
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
