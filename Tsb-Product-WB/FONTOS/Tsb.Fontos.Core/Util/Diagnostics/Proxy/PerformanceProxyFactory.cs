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
* 2012.05.03  Tonny.Kim 1.0   First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Util.Diagnostics.Type;

namespace Tsb.Fontos.Core.Util.Diagnostics.Proxy
{
    public class PerformanceProxyFactory : TsbBaseObject
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PerformanceProxyFactory()
        {
            this.ObjectID = "GNR-FTCO-UTL-PerformanceProxyFactory";
        }

        /// <summary>
        /// Create Performance Proxy Object
        /// </summary>
        /// <typeparam name="T">Interface of Performance</typeparam>
        /// <param name="targetObject">Target object to create proxy</param>
        /// <param name="filterPropertyName">Match Method Name</param>
        /// <returns>Performance interface<</returns>
        public static T CreateProxy<T>(object targetObject, PerformanceType performanceType, params string[] matchMethodName)
        {
            return CreateProxy<T>(targetObject, performanceType, null, matchMethodName);
        }

        /// <summary>
        /// Create Performance Proxy Object
        /// </summary>
        /// <typeparam name="T">Interface of Performance</typeparam>
        /// <param name="targetObject">Target object to create proxy</param>
        /// <param name="filterPropertyName">Match Method Name</param>
        /// <returns>Performance interface<</returns>
        public static T CreateProxy<T>(object targetObject, PerformanceType performanceType, System.Type defineTargetType, params string[] matchMethodName)
        {
            PerformanceProxy performanceProxy = null;
            T performanceInterface = default(T);

            try
            {
                performanceProxy = new PerformanceProxy(performanceType, defineTargetType);
                performanceInterface = performanceProxy.CreatePerformanceProxy<T>(targetObject, matchMethodName);
            }
            catch (Exception)
            {
                throw;
            }

            return performanceInterface;
        }
    }
}
