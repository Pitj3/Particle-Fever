using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace Particle_Fever
{
    public enum LogLevel
    {
        FATAL = 0,
        ERROR,
        WARNING,
        INFO,
        DEBUG
    };

    public class Logger
    {
        private static ILog _log;
        
        public Logger()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static void Log(LogLevel level, string text)
        {
            switch(level)
            {
                case LogLevel.DEBUG:
                {
                    _log.Debug(text);
                    break;
                }

                case LogLevel.INFO:
                {
                    _log.Info(text);
                    break;
                }

                case LogLevel.WARNING:
                {
                    _log.Warn(text);
                    break;
                }

                case LogLevel.ERROR:
                {
                    _log.Error(text);
                    break;
                }

                case LogLevel.FATAL:
                {
                    _log.Fatal(text);
                    break;
                }
            }
           
        }
    }
}
