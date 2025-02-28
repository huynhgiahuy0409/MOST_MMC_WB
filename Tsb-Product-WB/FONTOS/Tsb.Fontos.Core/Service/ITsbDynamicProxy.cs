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
* 2009.06.12    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Reflection;
using System.Runtime.Remoting;

namespace Tsb.Fontos.Core.Service
{
    /// <summary>
    /// Delegate for implementing the invocation tasek
    /// </summary>
    /// <param name="target">The target object for the proxy</param>
    /// <param name="method">MethodBase object reference</param>
    /// <param name="parameters">Method Parameter</param>
    /// <returns>Returend object by delegator</returns>
    public delegate object InvocationDelegate(object target, MethodBase method, object[] parameters);

    
    /// <summary>
    /// Represents Tsb Proxy
    /// </summary>
    public interface ITsbDynamicProxy
    {
        /// <summary>
        /// The target object
        /// </summary>
        object ProxyTarget
        {
            get;
            set;
        }

        /// <summary>
        /// The delegate which handles the invocation task
        /// </summary>
        InvocationDelegate InvocationHandler
        {
            get;
            set;
        }

        
    }
}
