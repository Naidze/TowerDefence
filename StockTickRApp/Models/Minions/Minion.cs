using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Enums;
using TDServer.Helpers;

namespace TDServer.Models.Minions
{
    public abstract class Minion : ICloneable
    {

        private static int idCounter = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MoveSpeed { get; set; }
        public int Reward { get; set; }
        public MoveType MoveType { get; set; }
        public Position Position { get; set; }

        public Minion(string name)
        {
            Id = idCounter++;
            Name = name;
            Reward = 10;
            Health = 100;
            MoveSpeed = 1;
            Position = new Position(GameUtils.map[0].X, GameUtils.map[0].Y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Minion minion))
            {
                return false;
            }
            return Id.Equals(minion.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public object Clone()
        {
            var clone = (Minion)this.MemberwiseClone();
            HandleCloned(clone);
            return clone;
        }

        public int MoveUp()
        {
            return Position.Y - MoveSpeed;
        }

        public int MoveDown()
        {
            return Position.Y + MoveSpeed;
        }

        public int MoveLeft()
        {
            return Position.X - MoveSpeed;
        }

        public int MoveRight()
        {
            return Position.X + MoveSpeed;
        }

        protected virtual void HandleCloned(Minion clone)
        {
            //Nothing particular in the base class, but maybe useful for children.
            //Not abstract so children may not implement this if they don't need to.
        }
    }
}
