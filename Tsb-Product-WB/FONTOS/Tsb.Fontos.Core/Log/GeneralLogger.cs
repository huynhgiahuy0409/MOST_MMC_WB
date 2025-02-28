#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2013 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		    REVISION    	
* 2013.01.31     hs.Kim 1.0    	First Release
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Tsb.Fontos.Core.Logging;

namespace Tsb.Fontos.Core.Log
{
    internal class GeneralLogger
    {
        #region FIELD AREA ***************************************
        private static ITsbLog _logger = LogFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string LOG_PREFIX = "[FT.Core] ";
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
