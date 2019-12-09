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
        public ITowerIterator CreateIterator()
        {
            return new TowerIterator(this);
        }

        public List<EnemyAttacker> Towers { get; } = new List<EnemyAttacker>();

        public int Count
        {
            get { return Towers.Count; }
        }

        public EnemyAttacker this[int index]
        {
            get { return Towers[index]; }
            set
            {
                if (Count > index && Count > 0)
                {
                    Towers[index] = value;
                }
                else
                {
                    Towers.Add(value);
                }
            }
        }

        public void Remove(EnemyAttacker tower)
        {
            Towers.Remove(tower);
        }
    }
}
