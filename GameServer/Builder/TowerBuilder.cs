using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Builder
{
    public class TowerBuilder : ITowerBuilder
    {
        private readonly Tower _tower = new Tower();

        public TowerBuilder(Coordinates Position)
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
