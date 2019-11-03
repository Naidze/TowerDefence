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
        private readonly Tower _tower = new Tower();

        public TowerBuilder(Position Position)
        {
            _tower.Position = Position;
        }

        public TowerBuilder SetDamage(int Damage)
        {
            _tower.Damage = Damage;
            return this;
        }

        public TowerBuilder SetRange(int Range)
        {
            _tower.Range = Range;
            return this;
        }

        public TowerBuilder SetRate(int Rate)
        {
            _tower.Rate = Rate;
            return this;
        }

        public Tower GetResult()
        {
            return _tower;
        }
    }
}
