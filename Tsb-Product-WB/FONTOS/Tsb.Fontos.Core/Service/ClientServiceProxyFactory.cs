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
using Tsb.Fontos.Core.Reflection;
using System.Reflection;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Service
{
    /// <summary>
    /// Client Service Proxy Factory
    /// </summary>
    public class ClientServiceProxyFactory : TsbBaseObject
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ClientServiceProxyFactory()
        {
            this.ObjectID = "GNR-FTCO-SVC-ClientServiceProxyFactory";
        }

        /// <summary>
        /// Create Client Service Proxy Object
        /// </summary>
        /// <typeparam name="T">Interface of Service</typeparam>
        /// <param name="serviceID">Service ID</param>
        /// <returns>Service interface which is wrapped by Transparent Proxy object<</returns>
        public static object CreateProxy<T>(string  serviceID)
        {
            ClientServiceProxy clientProxy = null;
            T serviceInterface = default(T);

            try
            {
                clientProxy = new ClientServiceProxy(serviceID);
                serviceInterface = clientProxy.CreateClientServiceProxy<T>();
            }
            catch (Exception)
            {
                
                throw;
            }

            return serviceInterface;
        }

        /// <summary>
        /// Create Client Service Proxy Object
        /// </summary>
        /// <typeparam name="T">Interface of Service</typeparam>
        /// <param name="serviceID">Service ID</param>
        /// <param name="targetObject">Target object to create proxy</param>
        /// <returns>Service interface which is wrapped by Transparent Proxy object<</returns>
        public static object CreateProxy<T>(string serviceID, object targetObject)
        {
            ClientServiceProxy clientProxy = null;
            T serviceInterface = default(T);

            try
            {
                clientProxy = new ClientServiceProxy(serviceID);
                serviceInterface = clientProxy.CreateClientServiceProxy<T>(targetObject);
            }
            catch (Exception)
            {

                throw;
            }

            return serviceInterface;
        }
        
       
    }
}
