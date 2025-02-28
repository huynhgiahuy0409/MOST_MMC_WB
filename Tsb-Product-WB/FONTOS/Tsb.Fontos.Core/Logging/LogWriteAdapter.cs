using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Logging;
using System.Reflection;

namespace Tsb.Fontos.Core.Logging
{
    public class LogWriteAdapter
    {
        private static ITsbLog _logger = LogFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Debug(ITsbLog logger, string prefix, object message)
        {
            if (logger.IsDebugEnabled == true)
            {
                string tempMessage = GetMessage(prefix, message);
                logger.Debug(tempMessage);
            }
        }

        private static void Info(ITsbLog logger, string prefix, object message)
        {
            if (logger.IsInfoEnabled == true)
            {
                string tempMessage = GetMessage(prefix, message);
                logger.Info(tempMessage);
            }
        }

        private static void Warn(ITsbLog logger, string prefix, object message)
        {
            if (logger.IsWarnEnabled == true)
            {
                string tempMessage = GetMessage(prefix, message);
                logger.Warn(tempMessage);
            }
        }

        private static void Error(ITsbLog logger, string prefix, object message)
        {
            if (logger.IsErrorEnabled == true)
            {
                string tempMessage = GetMessage(prefix, message);
                logger.Error(tempMessage);
            }
        }

        private static void Fatal(ITsbLog logger, string prefix, object message)
        {
            if (logger.IsFatalEnabled == true)
            {
                string tempMessage = GetMessage(prefix, message);
                logger.Fatal(tempMessage);
            }
        }

        private static string GetMessage(string prefix, object message)
        {
            string returnMsg = string.Empty;

            try
            {
                if (message == null)
                {
                    return returnMsg;
                }

                if (message is Exception)
                {
                    var ex = message as Exception;
                    returnMsg = prefix + ex.Message + " /+/ " + ex.StackTrace;
                }
                else if (message is string)
                {
                    returnMsg = prefix + message as string;
                }
                else
                {
                    throw new Exception("The log message is unsupported format");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return returnMsg;
        }

        public static void WriteLog(LogLevel logLevel, ITsbLog logger, string prefix, object message)
        {
            switch (logLevel)
            {
                case LogLevel.DEBUG: LogWriteAdapter.Debug(logger, prefix, message); break;
                case LogLevel.INFORMATION: LogWriteAdapter.Info(logger, prefix, message); break;
                case LogLevel.WARNING: LogWriteAdapter.Warn(logger, prefix, message); break;
                case LogLevel.ERROR: LogWriteAdapter.Error(logger, prefix, message); break;
                case LogLevel.FATAL: LogWriteAdapter.Fatal(logger, prefix, message); break;



            }
        }
    }
}
