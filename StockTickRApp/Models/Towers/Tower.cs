﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Towers
{
    public class Tower
    {
        private static int idCounter = 0;

        public int Id { get; set; }
        public Position Position { get; set; }
        public int Cost { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int Rate { get; set; }
        public int TicksBeforeShot { get; set; }

        public Tower()
        {
            Id = idCounter++;
            Damage = 70;
            Range = 30;
            Rate = 1;
            TicksBeforeShot = 0;
        }

        public Tower(int x, int y)
        {
            Id = idCounter++;
            Position = new Position(x, y);
            Damage = 30;
            Range = 30;
            Rate = 10;
            TicksBeforeShot = 0;
        }
    }
}
