using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Memento
{
    public class Caretaker
    {
        private List<PlayerMemento> states;

        public Caretaker()
        {
            states = new List<PlayerMemento>();
        }

        public void Add(PlayerMemento memento)
        {
            states.Add(memento);
        }

        public PlayerMemento Restore()
        {
            int index = Size() - 1;
            PlayerMemento memento = states[index];
            states.RemoveAt(index);
            return memento;
        }

        public int Size()
        {
            return states.Count;
        }

    }
}
