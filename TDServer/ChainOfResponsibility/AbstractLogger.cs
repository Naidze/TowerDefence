using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.ChainOfResponsibility
{
    public abstract class AbstractLogger
    {
        private static readonly object threadLock = new object();
        private const string FILE_EXT = ".log";
        protected readonly string datetimeFormat;
        protected readonly string logFilename;

        protected AbstractLogger()
        {
            datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            logFilename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + FILE_EXT;

            //    // Log file header line
            string logHeader = logFilename + " is created.";
            if (!System.IO.File.Exists(logFilename))
            {
                LogMessage(LogLevel.FILE, DateTime.Now.ToString(datetimeFormat) + " " + logHeader);
                //WriteLine(System.DateTime.Now.ToString(datetimeFormat) + " " + logHeader, false);
            }
        }

        protected LogLevel level;

        //next element in chain or responsibility
        protected AbstractLogger nextLogger;

        public void SetNextLogger(AbstractLogger nextLogger)
        {
            this.nextLogger = nextLogger;
        }

        public void LogMessage(LogLevel level, string message)
        {
            if (this.level <= level)
            {
                Write(message);
            }
            if (nextLogger != null)
            {
                nextLogger.LogMessage(level, message);
            }
        }

        abstract protected void Write(string message);

        public static AbstractLogger GetChainOfLoggers()
        {

            AbstractLogger errorLogger = new ErrorLogger(LogLevel.ERROR);
            AbstractLogger fileLogger = new FileLogger(LogLevel.DEBUG);
            AbstractLogger consoleLogger = new ConsoleLogger(LogLevel.INFO);
            AbstractLogger debugLogger = new DebugLogger(LogLevel.DEBUG);

            errorLogger.SetNextLogger(fileLogger);
            fileLogger.SetNextLogger(debugLogger);
            debugLogger.SetNextLogger(consoleLogger);

            return errorLogger;
        }
    }
}
