using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Iterator
{
    public class TowerIterator : ITowerIterator
    {
        private readonly TowerCollection _towers;
        private int _current = 0;
        private readonly int _step = 1;

        public TowerIterator(TowerCollection towers)
        {
            _towers = towers;
        }

        public EnemyAttacker CurrentTower
        {
            get { return _towers[_current] as Tower; }
        }

        public EnemyAttacker First()
        {
            _current = 0;
            if (_towers.Count == 0)
                return null;
            return _towers[_current];
        }

        public bool IsDone
        {
            get { return _current >= _towers.Count; }
        }

        public EnemyAttacker Next()
        {
            _current += _step;
            if (!IsDone)
                return _towers[_current];
            else
                return null;
        }
    }
}
