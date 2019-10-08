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
        private static Logger INSTANCE = null;
        private static object threadLock = new object();
        private static StreamWriter output;

        private Logger()
        {
            output = new StreamWriter(LOG_FILE, true);
        }

        public static Logger GetInstance()
        {
            lock (threadLock)
            {
                if (INSTANCE == null)
                {
                    INSTANCE = new Logger();
                }
            }
            return INSTANCE;
        }

        public void Log()
        {
            output.WriteLine("logger");
            output.Close();
        }
    }
}
