﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Minions
{
    public class Lizard : Minion
    {
        public Lizard() : base("crawler")
        {
            Debug.WriteLine("Hi, I'm Lizard!");
        }

    }
}