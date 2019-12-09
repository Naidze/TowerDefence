using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Iterator
{
    public class TowerIterator : ITowerIterator
    {
        private TowerCollection _towers;
        private int _current = 0;
        private int _step = 1;

        public TowerIterator(TowerCollection towers)
        {
            _towers = towers;
        }

        public Tower CurrentTower
        {
            get { return _towers[_current] as Tower; }
        }

        public Tower First()
        {
            _current = 0;
            if (_towers.Count == 0)
                return null;
            return _towers[_current] as Tower;
        }

        public bool IsDone
        {
            get { return _current >= _towers.Count; }
        }

        public Tower Next()
        {
            _current += _step;
            if (!IsDone)
                return _towers[_current] as Tower;
            else
                return null;
        }
    }
}
