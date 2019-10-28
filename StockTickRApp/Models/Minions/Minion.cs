using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Enums;

namespace TDServer.Models.Minions
{
    public abstract class Minion
    {

        private static int idCounter = 0;

        public int Id { get; set; }
        public int Health { get; set; }
        public float MoveSpeed { get; set; }
        public MoveType MoveType { get; set; }

        public Minion()
        {
            Id = idCounter++;
            Health = 100;
            Debug.WriteLine("Minion created.");
        }

        public abstract void Move();
    }
}
