using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.ChainOfResponsibility
{
    [Flags]
    public enum LogLevel
    {
        INFO,
        DEBUG,
        FILE,
        ERROR
    }
}
