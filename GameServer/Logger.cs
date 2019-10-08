using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer
{
    public class Logger : ILogger
    {
        private const string LOG_FILE = "";
        private static Lazy<Logger> INSTANCE = null;
        private static object threadLock = new object();
        private static StreamWriter output;

        private Logger()
        {
            output = new StreamWriter(LOG_FILE);
        }

        public static Lazy<Logger> GetInstance()
        {
            lock (threadLock)
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new Lazy<Logger>();
                }
            }
            return INSTANCE;
        }

        public void Log()
        {
            output.WriteLine();
        }
    }
}
