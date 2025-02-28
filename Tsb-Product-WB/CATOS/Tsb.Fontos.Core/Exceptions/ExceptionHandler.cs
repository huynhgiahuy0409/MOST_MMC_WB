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
* DATE           AUTHOR		REVISION    	
* 2009.07.11    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Tsb.Fontos.Core.Exceptions.Formatter;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Exceptions
{
    /// <summary>
    /// Exception Handler class
    /// </summary>
    public class ExceptionHandler : TsbBaseObject
    {

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ExceptionHandler()
        {
            this.ObjectID = "GNR-FTCO-EXP-ExceptionHandler";
        }
        #endregion


        #region METHOD AREA (Propagate)*************************
        /// <summary>
        /// Propagate up an unchanged original exception
        /// </summary>
        /// <param name="originalException">The original exception.</param>
        /// <param name="objectID">Source object ID</param>
        /// <param name="msgCode">Message code to handle user defined message.</param>
        /// <param name="msgArgs">Message arguments sting array to bind resource message</param>
        public static void Propagate(Exception originalException, string objectID, string msgCode, params string[] msgArgs)
        {

            if (IsTsbException(originalException))
            {
                AddTraceInfo((TsbBaseException)originalException, objectID, msgCode, msgArgs);
            }
            else
            {
                ExceptionHandler.Wrap(originalException, typeof(TsbBaseException), objectID, "MSG_FTCO_99998", originalException.Message);
            }

            throw originalException;
        }

        /// <summary>
        /// Propagate up an unchanged original exception
        /// </summary>
        /// <param name="originalException">The original exception.</param>
        /// <param name="objectID">Source object ID</param>
        public static void Propagate(Exception originalException, string objectID)
        {
            if (IsTsbException(originalException))
            {
                ExceptionHandler.AddTraceInfo((TsbBaseException)originalException, objectID, null, null);
            }
            else
            {
                ExceptionHandler.Wrap(originalException, typeof(TsbBaseException), objectID, "MSG_FTCO_99998", originalException.Message);
            }

            throw originalException;
        }
        #endregion


        #region METHOD AREA (Wrap)******************************
        /// <summary>
        /// Wrap a original exception with a specified exception.
        /// </summary>
        /// <param name="originalException">The original exception.</param>
        /// <param name="wrapExceptionType">The type of exception to wrap the original exception with.</param>
        /// <param name="objectID">Source object ID</param>
        /// <param name="msgCode">Message code to handle user defined message.</param>
        /// <param name="msgArgs">Message arguments sting array to bind resource message</param>
        public static void Wrap(Exception originalException, Type wrapExceptionType, string objectID, string msgCode, params string[] msgArgs)
        {
            TsbBaseException wrapException = null;

            wrapException = CreateException(originalException, wrapExceptionType, objectID, msgCode, msgArgs); 

            if (IsTsbException(originalException))
            {
                wrapException.Tracer = ((TsbBaseException)originalException).Tracer;
            }
            
            AddTraceInfo(wrapException, objectID, msgCode, msgArgs);

            throw wrapException;
        }
        #endregion


        #region METHOD AREA (Replace)***************************
        /// <summary>
        /// Replaces an exception with a new exception of a specified type.
        /// </summary>
        /// <param name="originalException">The original exception.</param>
        /// <param name="replaceExceptionType">The type of exception to replace.</param>
        /// <param name="objectID">Source object ID</param>
        /// <param name="msgCode">Message code to handle user defined message.</param>
        /// <param name="msgArgs">Message arguments sting array to bind resource message</param>
        public static void Replace(Exception originalException, Type replaceExceptionType, string objectID, string msgCode, params string[] msgArgs)
        {
            TsbBaseException replaceException = null;

            replaceException = CreateException(originalException, replaceExceptionType, objectID, msgCode, msgArgs); 

            if (IsTsbException(originalException))
            {
                replaceException.Tracer = ((TsbBaseException)originalException).Tracer;
            }

            AddTraceInfo(replaceException, objectID, msgCode, msgArgs);

            throw replaceException;
        }
        #endregion


        #region METHOD AREA (Resume)****************************
        /// <summary>
        /// Process an exception and resume executing. 
        /// </summary>
        /// <param name="originalException">The original exception.</param>
        /// <param name="objectID">Source object ID</param>
        /// <param name="msgCode">Message code to handle user defined message.</param>
        /// <param name="msgArgs">Message arguments sting array to bind resource message</param>
        public static void Resume(Exception originalException, string objectID, string msgCode, params string[] msgArgs)
        {
            //TODO : Logging and other process

            return;
        }
        #endregion


        #region METHOD AREA (Trace Handling)********************
        /// <summary>
        /// Create trace information to TSB Exception
        /// </summary>
        /// <param name="tsbException">A TSB exception class object reference to add trace information</param>
        /// <param name="objectID">Source object ID</param>
        /// <param name="msgCode">Message code to handle user defined message.</param>
        /// <param name="msgArgs">Message arguments sting array to bind resource message</param>
        public static void CreateTraceInfo(TsbBaseException tsbException)
        {
            ExceptionInfo exInfo = null;

            exInfo = new ExceptionInfo();
            exInfo.ExceptionName = tsbException.GetType().Name;
            exInfo.SourceObjectID = tsbException.SourceObjectID;
            exInfo.MsgCode = tsbException.MsgCode;
            exInfo.MsgArgs = tsbException.MsgArgs;
            exInfo.MethodName = MethodInfoUtil.GetCallerMethodName(3);

            if (tsbException.Tracer == null)
            {
                tsbException.Tracer = new List<ExceptionInfo>();
            }
            tsbException.Tracer.Add(exInfo);

            return;
        }

        /// <summary>
        /// Add trace information to TSB Exception
        /// </summary>
        /// <param name="tsbException">A TSB exception class object reference to add trace information</param>
        /// <param name="objectID">Source object ID</param>
        /// <param name="msgCode">Message code to handle user defined message.</param>
        /// <param name="msgArgs">Message arguments sting array to bind resource message</param>
        private static void AddTraceInfo(TsbBaseException tsbException, string objectID, string msgCode, params string[] msgArgs)
        {
            ExceptionInfo exInfo = null;

            exInfo = new ExceptionInfo();
            exInfo.ExceptionName = tsbException.GetType().Name;
            exInfo.SourceObjectID = objectID;
            exInfo.MsgCode = msgCode;
            exInfo.MethodName = MethodInfoUtil.GetCallerMethodName(3);
            exInfo.MsgArgs = msgArgs;

            if (tsbException.Tracer == null)
            {
                tsbException.Tracer = new List<ExceptionInfo>();
            }

            tsbException.Tracer.Add(exInfo);

            return;
        }
        #endregion

        
        #region METHOD AREA (Create Exception)******************
        /// <summary>
        /// Creates TSB exception
        /// </summary>
        /// <param name="sourceException">a parameter of source exception</param>
        /// <param name="exceptionType">The type of exception to create</param>
        /// <param name="objectID">Source object ID</param>
        /// <param name="msgCode">Message code to handle user defined message.</param>
        /// <param name="msgArgs">Message arguments sting array to bind resource message</param>
        /// <returns>Newly created TsbBaseException</returns>
        private static TsbBaseException CreateException(Exception sourceException, Type exceptionType, string sourceObjectID, string msgCode, params string[] msgArgs)
        {
            TsbBaseException rtnException = null;
            int argLength = -1;

            argLength = 3 + (msgArgs == null ? 0 : msgArgs.Length);
            object[] extraParameters = new object[argLength];

            extraParameters[0] = sourceException;
            extraParameters[1] = sourceObjectID;
            extraParameters[2] = msgCode;

            for (int i = 3, j = 0; i < argLength; i++, j++)
            {
                extraParameters[i] = msgArgs[j];
            }

            rtnException = (TsbBaseException)Activator.CreateInstance(exceptionType, extraParameters);

            return rtnException;
        }

        #endregion


        #region METHOD AREA (ETC)*******************************
        /// <summary>
        /// Returns a Boolean value indicating whether a specified exception is a Tsb Exception or not
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static bool IsTsbException(Exception exception)
        {
            return exception is TsbBaseException;
        }
        #endregion
    }
}
