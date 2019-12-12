using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Iterator;
using TDServer.Models;
using TDServer.State;

namespace TDServer.Memento
{
    public class Originator
    {

        private readonly Player _player;

        public Originator(Player player)
        {
            _player = player;
        }

        public void RestoreState(PlayerMemento memento)
        {
            _player.Money += memento.GetPrice();
            _player.Towers = memento.GetTowers();
        }

        public PlayerMemento SaveState(int price, TowerCollection towers)
        {
            return new PlayerMemento(price, towers);
        }


    }
}
