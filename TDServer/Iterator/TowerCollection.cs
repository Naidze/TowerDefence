using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Towers;

namespace TDServer.Iterator
{
    public class TowerCollection : ITowerCollection
    {
        private ArrayList _towers = new ArrayList();

        public ITowerIterator CreateIterator()
        {
            return new TowerIterator(this);
        }

        public ArrayList Towers
        {
            get { return _towers; }
        }

        public int Count
        {
            get { return _towers.Count; }
        }

        public object this[int index]
        {
            get { return _towers[index]; }
            set { _towers.Add(value);  }
        }

        public void Remove(Tower tower)
        {
            _towers.Remove(tower);
        }
    }
}
