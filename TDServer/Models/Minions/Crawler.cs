﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Command;
using TDServer.Helpers;

namespace TDServer.Models.Minions
{
    [Serializable]
    public class Crawler : Minion
    {
        public Crawler() : base("crawler", 20, 4, 15)
        {
            
        }

        protected override void HandleCloned(Minion clone)
        {
            base.HandleCloned(clone);

            Crawler obj = (Crawler)clone;
            obj.Position = (Position)this.Position.Clone();
        }

        public bool Crawl()
        {
            if (Position.Path == GameUtils.map.Length)
            {
                return false;
            }

            var path = GameUtils.map[Position.Path];
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
            return Position.Path < GameUtils.map.Length;
        }
    }
}
