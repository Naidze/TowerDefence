using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Models.Towers
{
    public abstract class EnemyAttacker
    {
        protected static int idCounter = 0;

        public int Id { get; set; }
        public Position Position { get; set; }
        public int Range { get; set; }
        public int Damage { get; set; }
        public int Rate { get; set; }
        public int Price { get; set; }
        public int TicksBeforeShot { get; set; }

        public EnemyAttacker()
        {
            Id = idCounter++;
            Price = 25;
            TicksBeforeShot = 0;
            Damage = 70;
            Range = 30;
            Rate = 1;
        }

        public EnemyAttacker(int x, int y)
        {
            Id = idCounter++;
            Position = new Position(x, y);
            Price = 25;
            TicksBeforeShot = 0;
            Damage = 80;
            Range = 50;
            Rate = 5;
        }
    }
}
