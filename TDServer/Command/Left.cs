using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDServer.Models.Minions;

namespace TDServer.Command
{
    public class Left : IMoveCommand
    {
        readonly Minion _minion;

        public Left(Minion minion)
        {
            _minion = minion;
        }

        public int Execute()
        {
            return _minion.MoveLeft();
        }

        public int Undo()
        {
            return _minion.MoveRight();
        }
    }
}
