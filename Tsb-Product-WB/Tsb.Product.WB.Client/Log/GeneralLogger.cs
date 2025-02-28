using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Tsb.Fontos.Core.Logging;

namespace Tsb.Most.Wb.Client.Log
{
    public class GeneralLogger
    {
         #region FIELD AREA ***************************************
    private static ITsbLog _logger = LogFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    private static readonly string LOG_PREFIX = "[WB.Application]";
        #endregion

        #region METHOD AREA **************************************
        
        public static void Debug(object message)
    {
        LogWriteAdapter.WriteLog(LogLevel.DEBUG, _logger, LOG_PREFIX, message);
    }

    public static void Info(object message)
    {
        LogWriteAdapter.WriteLog(LogLevel.INFORMATION, _logger, LOG_PREFIX, message);
    }

    public static void Warn(object message)
    {
        LogWriteAdapter.WriteLog(LogLevel.WARNING, _logger, LOG_PREFIX, message);
    }

    public static void Error(object message)
    {
        LogWriteAdapter.WriteLog(LogLevel.ERROR, _logger, LOG_PREFIX, message);
    }

    public static void Fatal(object message)
    {
        LogWriteAdapter.WriteLog(LogLevel.FATAL, _logger, LOG_PREFIX, message);
    }
    #endregion

}
}
