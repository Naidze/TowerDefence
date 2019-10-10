using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Enums;

namespace TDServer.Models.Minions
{
    public abstract class Minion
    {
        public int Health { get; set; }
        public float MoveSpeed { get; set; }
        public MoveType MoveType { get; set; }

        public Minion()
        {
            Console.WriteLine("Minion created.");
        }

        public abstract void Move();
    }
}
