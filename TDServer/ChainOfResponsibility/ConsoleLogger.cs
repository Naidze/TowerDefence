using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.ChainOfResponsibility
{
    public class ConsoleLogger : AbstractLogger
    {
        public ConsoleLogger(LogLevel level)
        {
            this.level = level;
        }

        protected override void Write(string message)
        {
            string pretext = DateTime.Now.ToString(datetimeFormat) + " [INFO]    ";
            Console.WriteLine(pretext + message);
        }
    }
}
