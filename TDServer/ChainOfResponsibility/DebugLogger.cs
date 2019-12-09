using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.ChainOfResponsibility
{
    public class DebugLogger : AbstractLogger
    {
        public DebugLogger(LogLevel level)
        {
            this.level = level;
        }

        protected override void Write(string message)
        {
            string pretext = DateTime.Now.ToString(datetimeFormat) + " [DEBUG]   ";
            Debug.WriteLine(pretext + message);
        }
    }
}
