using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Iterator;

namespace TDServer.Memento
{
    public class PlayerMemento
    {

        private readonly int money;
        private readonly TowerCollection towers;

        public PlayerMemento(int money, TowerCollection towers)
        {
            this.money = money;
            this.towers = towers;
        }

        public int GetMoney()
        {
            return money;
        }

        public TowerCollection GetTowers()
        {
            return towers;
        }

    }
}
