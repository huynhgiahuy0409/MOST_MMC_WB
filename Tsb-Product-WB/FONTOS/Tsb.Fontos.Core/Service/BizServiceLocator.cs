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
* 2009.06.29    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Tsb.Fontos.Core.Exceptions;
using System.Reflection;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Service
{
    /// <summary>
    /// Business Service Locator Object. Client will get service reference which is responsible for its a business
    /// operation
    /// </summary>
    public sealed class BizServiceLocator : TsbBaseObject
    {
        #region FIELD AREA *************************************
        private static object syncRoot = new Object();
        private static Hashtable _serviceRefCache = new Hashtable();
        private static readonly string OBJECT_ID = "GNR-FTCO-SVC-BizServiceLocator";
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BizServiceLocator()
        {
            this.ObjectID = OBJECT_ID;
        }
        #endregion


        #region METHOD AREA (Add Service)***********************
        /// <summary>
        /// An object that represents the service to add using reference of service object
        /// </summary>
        /// <typeparam name="T">Type parameter to add</typeparam>
        /// <param name="t">Service Object reference to add</param>
        public static void AddService<T>(string serviceID, T t) where T : ITsbService
        {
            try
            {
                _serviceRefCache.Add(serviceID, t);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion


        #region METHOD AREA (Get Service)***********************
        /// <summary>
        /// Get the reference of service object using param. Before you use this method, param.TransactionInfo.TxServiceID should be set.
        /// </summary>
        /// <typeparam name="T">Type parameter to get</typeparam>
        /// <param name="fullyQualifiedServiceClassNameWithAssemplyPath"> Fully qualified service's class name with Assemply path</param>
        /// <returns>Service object reference</returns>
        public static T GetService<T>(BaseParam param) where T : ITsbService
        {
            return GetService<T>(param.TransactionInfo.TxServiceID);
        }

        /// <summary>
        /// Get the reference of service object using service ID
        /// </summary>
        /// <typeparam name="T">Type parameter to get</typeparam>
        /// <param name="serviceID"> Service ID</param>
        /// <returns>Service object reference</returns>
        public static T GetService<T>(string serviceID) where T : ITsbService
        {
            T service = default(T);

            try
            {
                if (_serviceRefCache.Contains(serviceID))
                {
                    service = (T)_serviceRefCache[serviceID];
                }
                else
                {
                    service = (T)ClientServiceProxyFactory.CreateProxy<T>(serviceID);

                    lock (syncRoot)
                    {
                        AddService<T>(serviceID, service);
                    }
                }
            }
            catch (TsbSysBaseException sysEx)
            {
                ExceptionHandler.Propagate(sysEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when calling {0} service. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), OBJECT_ID, "MSG_FTCO_00001", DefaultMessage.NON_REG_WRD + serviceID);
            }

            return service;
        }

        /// <summary>
        /// Get the reference of service object using service ID
        /// </summary>
        /// <typeparam name="T">Type parameter to get</typeparam>
        /// <param name="serviceID"> Service ID</param>
        /// <param name="targetObject">Target object to create proxy</param>
        /// <returns>Service object reference</returns>
        public static T GetService<T>(string serviceID, object targetObject) where T : ITsbService
        {
            T service = default(T);

            try
            {
                if (_serviceRefCache.Contains(serviceID))
                {
                    service = (T)_serviceRefCache[serviceID];
                }
                else
                {
                    service = (T)ClientServiceProxyFactory.CreateProxy<T>(serviceID, targetObject);

                    lock (syncRoot)
                    {
                        AddService<T>(serviceID, service);
                    }
                }
            }
            catch (TsbSysBaseException sysEx)
            {
                ExceptionHandler.Propagate(sysEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when calling {0} service. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), OBJECT_ID, "MSG_FTCO_00001", DefaultMessage.NON_REG_WRD + serviceID);
            }

            return service;
        }
        #endregion
    }
}
