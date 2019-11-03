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
        public string Name { get; set; }
        public int Health { get; set; }
        public int MoveSpeed { get; set; }
        public MoveType MoveType { get; set; }
        public Position Position { get; set; }

        public Minion(string name)
        {
            Id = idCounter++;
            Name = name;
            Health = 100;
            MoveSpeed = 1;
            Position = new Position(Game.map[0].X, Game.map[0].Y);
        }

        public bool Move()
        {
            if (Position.Path == Game.map.Length)
            {
                return false;
            }

            var path = Game.map[Position.Path];
            if (Position.X > path.X)
            {
                Position.X = Math.Max(Position.X - MoveSpeed, path.X);
            }
            else if (Position.X < path.X)
            {
                Position.X = Math.Min(Position.X + MoveSpeed, path.X);
            }
            else if (Position.Y > path.Y)
            {
                Position.Y = Math.Max(Position.Y - MoveSpeed, path.Y);
            }
            else if (Position.Y < path.Y)
            {
                Position.Y = Math.Min(Position.Y + MoveSpeed, path.Y);
            }

            if (Position.X == path.X && Position.Y == path.Y)
            {
                Position.Path++;
            }
            return Position.Path < Game.map.Length;
        }

        public override bool Equals(object obj)
        {
            Minion minion = obj as Minion;
            if (minion == null)
            {
                return false;
            }
            return Id.Equals(minion.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
