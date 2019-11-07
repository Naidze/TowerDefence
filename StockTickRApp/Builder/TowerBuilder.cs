using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models;
using TDServer.Models.Towers;

namespace TDServer.Builder
{
    public class TowerBuilder : ITowerBuilder
    {
        private Position Position;
        private string Name { get; set; }
        private int Damage { get; set; }
        private int Range { get; set; }
        private int Rate { get; set; }
        private int Price { get; set; }

        public TowerBuilder(Position position)
        {
            Position = position;
        }

        public TowerBuilder SetName(string name)
        {
            Name = name;
            return this;
        }

        public TowerBuilder SetDamage(int damage)
        {
            Damage = damage;
            return this;
        }

        public TowerBuilder SetRange(int range)
        {
            Range = range;
            return this;
        }

        public TowerBuilder SetRate(int rate)
        {
            Rate = rate;
            return this;
        }

        public TowerBuilder SetPrice(int price)
        {
            Price = price;
            return this;
        }

        public Tower GetResult()
        {
            return new Tower(Position)
            {
                Name = Name,
                Damage = Damage,
                Range = Range,
                Rate = Rate,
                Price = Price
            };
        }

    }
}
