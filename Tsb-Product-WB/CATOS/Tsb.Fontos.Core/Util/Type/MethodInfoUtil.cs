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
using System.Reflection;

namespace Tsb.Fontos.Core.Util.Type
{
    /// <summary>
    /// Method Information Utility class
    /// </summary>
    public class MethodInfoUtil : BaseUtil
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MethodInfoUtil() : base()
        {
            this.ObjectID = "GNR-FTCO-UTL-MethodInfoUtil";
        }

        /// <summary>
        /// Returns caller's method name
        /// </summary>
        /// <param name="frameNo">Calling stack frame number to find method name</param>
        /// <returns>Caller's method name</returns>
        public static string GetCallerMethodName(int frameNo)
        {
            StackTrace callingStack = null;
            StackFrame callerFrame = null;
            string rtnMethodName = string.Empty;

            callingStack = new StackTrace();
            callerFrame = callingStack.GetFrame(frameNo);

            rtnMethodName = callerFrame.GetMethod().DeclaringType.FullName + "." + callerFrame.GetMethod().Name;

            callerFrame = null;
            callingStack = null;

            return rtnMethodName;
        }

        /// <summary>
        /// Returns caller's method name without class name
        /// </summary>
        /// <returns>Caller's method name</returns>
        public static string GetCallerMethodNameWithoutClassName()
        {
            StackTrace callingStack = null;
            StackFrame callerFrame = null;
            string rtnMethodName = string.Empty;

            callingStack = new StackTrace();
            callerFrame = callingStack.GetFrame(1);

            rtnMethodName = callerFrame.GetMethod().Name;

            callerFrame = null;
            callingStack = null;

            return rtnMethodName;
        }

        /// <summary>
        /// Returns caller's method with type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Caller's method</returns>
        public static MethodBase GetCallerMethodWithType(System.Type type)
        {
            MethodBase rtnMethod = null;
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            foreach (StackFrame stackFrame in stackFrames)
            {
                if (stackFrame.GetMethod().DeclaringType == type)
                {
                    rtnMethod = stackFrame.GetMethod();
                    break;
                }
            }

            return rtnMethod;
        }
    }
}
