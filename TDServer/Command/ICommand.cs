using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.Command
{
    public interface IMoveCommand
    {
        int Execute();

        int Undo();
    }
}
