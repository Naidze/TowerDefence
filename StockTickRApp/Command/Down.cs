using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;

namespace TDServer.Command
{
    public class Down : IMoveCommand
    {
        readonly Minion _minion;

        public Down(Minion minion)
        {
            _minion = minion;
        }

        public int Execute()
        {
            return _minion.MoveDown();
        }

        public int Undo()
        {
            return _minion.MoveUp();
        }
    }
}
