using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer
{
    interface ILogger
    {
        void Debug(string text);
        void Error(string text);
        void Fatal(string text);
        void Info(string text);
        void Trace(string text);
        void Warning(string text);
    }
}
