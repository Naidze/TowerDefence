using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Iterator;

namespace TDServer.Memento
{
    public class PlayerMemento
    {

        private readonly int price;
        private readonly TowerCollection towers;

        public PlayerMemento(int price, TowerCollection towers)
        {
            this.price = price;
            this.towers = towers;
        }

        public int GetPrice()
        {
            return price;
        }

        public TowerCollection GetTowers()
        {
            return towers;
        }

    }
}
