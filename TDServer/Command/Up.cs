using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;

namespace TDServer.Command
{
    public class Up : IMoveCommand
    {
        readonly Minion _minion;

        public Up(Minion minion)
        {
            _minion = minion;
        }

        public int Execute()
        {
            return _minion.MoveUp();
        }

        public int Undo()
        {
            return _minion.MoveDown();
        }
    }
}
