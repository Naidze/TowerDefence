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
            var path = Game.map[Position.Path];
            if (Position.X > path.X)
            {
                Position.X = Math.Max(Position.X - MoveSpeed, path.X);
            } else if (Position.X < path.X)
            {
                Position.X = Math.Min(Position.X + MoveSpeed, path.X);
            } else if (Position.Y > path.Y)
            {
                Position.Y = Math.Max(Position.Y - MoveSpeed, path.Y);
            } else if (Position.Y < path.Y)
            {
                Position.Y = Math.Min(Position.Y + MoveSpeed, path.Y);
            }

            if (Position.X == path.X && Position.Y == path.Y)
            {
                Position.Path++;
            }
        }
    }
}
