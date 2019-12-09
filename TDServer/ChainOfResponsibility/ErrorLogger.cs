using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.ChainOfResponsibility
{
    public class ErrorLogger : AbstractLogger
    {

        public ErrorLogger(LogLevel level)
        {
            this.level = level;
        }

        protected override void Write(string message)
        {
            string pretext = DateTime.Now.ToString(datetimeFormat) + " [ERROR]   ";
            Console.Error.WriteLine("Error Console::Logger: " + message);
        }
    }
}
