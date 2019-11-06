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
        private readonly Tower _tower;

        public TowerBuilder(Position position)
        {
            _tower = new Tower(position);
        }

        public TowerBuilder SetName(string name)
        {
            _tower.Name = name;
            return this;
        }

        public TowerBuilder SetPosition(Position position)
        {
            _tower.Position = position;
            return this;
        }

        public TowerBuilder SetDamage(int damage)
        {
            _tower.Damage = damage;
            return this;
        }

        public TowerBuilder SetRange(int range)
        {
            _tower.Range = range;
            return this;
        }

        public TowerBuilder SetRate(int rate)
        {
            _tower.Rate = rate;
            return this;
        }

        public TowerBuilder SetPrice(int price)
        {
            _tower.Price = price;
            return this;
        }

        public Tower GetResult()
        {
            return _tower;
        }

    }
}
