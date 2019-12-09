using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TDServer.ChainOfResponsibility
{
    public class FileLogger : AbstractLogger
    {

        public FileLogger(LogLevel level)
        {
            this.level = level;
        }

        protected override void Write(string message)
        {
            if (File.Exists(logFilename))
            {
                string pretext = DateTime.Now.ToString(datetimeFormat) + " [FILE]   ";
                try
                {
                    using (StreamWriter writer = new StreamWriter(logFilename, true, System.Text.Encoding.UTF8))
                    {
                        if (!string.IsNullOrEmpty(message))
                        {
                            writer.WriteLine(pretext + message);
                        }
                    }
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
