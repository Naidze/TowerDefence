using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Command;

namespace TDServer.Models.Minions
{
    public class Crawler : Minion
    {
        public Crawler() : base("crawler")
        {
            Debug.WriteLine("Hi, I'm Crawler!");
        }

        public bool Crawl()
        {
            if (Position.Path == Game.map.Length)
            {
                return false;
            }

            var path = Game.map[Position.Path];
            if (Position.X > path.X)
            {
                Position.X = Math.Max(new Left(this).Execute(), path.X);
            }
            else if (Position.X < path.X)
            {
                Position.X = Math.Min(new Right(this).Execute(), path.X);
            }
            else if (Position.Y > path.Y)
            {
                Position.Y = Math.Max(new Up(this).Execute(), path.Y);
            }
            else if (Position.Y < path.Y)
            {
                Position.Y = Math.Min(new Down(this).Execute(), path.Y);
            }

            if (Position.X == path.X && Position.Y == path.Y)
            {
                Position.Path++;
            }
            return Position.Path < Game.map.Length;
        }
    }
}
