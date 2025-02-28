#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2009 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		       REVISION    	
* 2010.01.25    CHOI 1.0	    First release.
* 2011.01.18  Tonny.Kim 1.1     Add a comments
*/
#endregion

using System;
using log4net;
using log4net.Core;
using log4net.Config;
using Tsb.Fontos.Core.Objects;
using System.Diagnostics;

namespace Tsb.Fontos.Core.Logging
{
    public class TsbLogImpl : LogImpl, ITsbLog, ITsbBaseObject
    {
        #region READONLY AREA **********************************
        private readonly static Type ThisDeclaringType = typeof(TsbLogImpl);
        #endregion

        #region FIELDS/PROPERTY AREA ***************************
        private string _objectID;
        #endregion

        #region INITIALIZE AREA ********************************
        public TsbLogImpl(ILogger logger)
            : base(logger)
        {
            this._objectID = "GNR-FTCO-LOG-TsbLogImpl";
        }
        #endregion

        #region METHOD AREA ************************************
        public new void Debug(object message)
        {
            Debug(message, null);
        }

        public new void Debug(object message, Exception t)
        {
            if (this.IsDebugEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Debug, message, t);
                Logger.Log(loggingEvent);
            }
        }

        public new void Info(object message)
        {
            Info(message, null);
        }

        public new void Info(object message, System.Exception t)
        {
            if (this.IsInfoEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, t);
                Logger.Log(loggingEvent);
            }
        }

        public new void Warn(object message)
        {
            Warn(message, null);
        }

        public new void Warn(object message, Exception t)
        {
            if (this.IsWarnEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Warn, message, t);
                Logger.Log(loggingEvent);
            }
        }

        public new void Error(object message)
        {
            Error(message, null);
        }

        public new void Error(object message, Exception t)
        {
            if (this.IsErrorEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Error, message, t);
                Logger.Log(loggingEvent);
            }
        }

        public new void Fatal(object message)
        {
            Fatal(message, null);
        }

        public new void Fatal(object message, Exception t)
        {
            if (this.IsFatalEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Fatal, message, t);
                Logger.Log(loggingEvent);
            }
        }
        #endregion

        #region ITsbBaseObject IMPLEMENTS AREA *****************
        public string ObjectID
        {
            get
            {
                return this._objectID;
            }
            set
            {
                _objectID = value;
            }
        }

        public ObjectType ObjectType
        {
            get { return ObjectType.DEFAULT; }
        }
        #endregion
    }
}
