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
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Message;
using System.Reflection;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Util.Diagnostics.Performance;
using Tsb.Fontos.Core.Util.Diagnostics.Type;
using Tsb.Fontos.Core.Environments;
using System.Diagnostics;
using Tsb.Fontos.Core.Util.Type;

namespace Tsb.Fontos.Core.Util.Diagnostics.Proxy
{
    public class PerformanceProxy : TsbBaseObject
    {
        #region FIELD AREA *************************************
        private ITsbDynamicProxy _tsbDynamicProxy = null;
        private string[] _startsWithMethodNames = null;
        private bool _isShowDetailException = false;
        #endregion

        #region PROPERTY AREA **********************************
        public PerformanceType PerformanceType { get; set; }
        public System.Type DefineTargetType { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default constructor
        /// </summary>
        public PerformanceProxy()
            : this(PerformanceType.NONE)
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PerformanceProxy(PerformanceType performanceType)
            : this(performanceType, null)
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PerformanceProxy(PerformanceType performanceType, System.Type defineTargetType)
        {
            try
            {
                this.ObjectID = "GNR-FTCO-UTL-PropermanceProxy";
                this.PerformanceType = performanceType;
                this.DefineTargetType = defineTargetType;
                this._isShowDetailException = DetailExceptionFormatter.GetShowDetailExceptionConfig();
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch(Exception ex)
            {
                //MSG:MSG_FTCO_00069	An error occurred when opening or reading the configuration file.	
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00006", null);
            }
        }
        #endregion

        #region METHOD AREA ************************************
        /// <summary>
        /// Create Performance Proxy
        /// </summary>
        /// <typeparam name="T">Performance Object Interface</typeparam>
        /// <returns>Performance interface</returns>
        public T CreatePerformanceProxy<T>(object targetObject, params string[] startsWithMethodNames)
        {
            DynamicProxy dynamicProxy = null;
            T serviceInterface = default(T);

            try
            {
                this._startsWithMethodNames = startsWithMethodNames;
                dynamicProxy = new DynamicProxy(targetObject, new InvocationDelegate(InvocationHandler));
                _tsbDynamicProxy = (ITsbDynamicProxy)dynamicProxy.GetTransparentProxy();
                serviceInterface = (T)_tsbDynamicProxy;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : A [{0}] object creation failed. Contact your system administrator.		
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + typeof(T).Name);
            }

            return serviceInterface;
        }

        /// <summary>
        /// Invocation Handling Method (This method will be called when proxy is called reflectively)
        /// </summary>
        /// <param name="target">Target object to call</param>
        /// <param name="method">Calling Method</param>
        /// <param name="parameters">Calling Parameter</param>
        /// <returns>Return of actual serivce</returns>
        private object InvocationHandler(object target, MethodBase method, object[] parameters)
        {
            object returnObj = null;
            bool aopApply = ModuleInfo.UsePerformanceLog;
            PerformanceHandler performanceHandler = null;

            try
            {
                if (ModuleInfo.UsePerformanceLog &&
                    _startsWithMethodNames != null &&
                    _startsWithMethodNames.Count() > 0)
                {
                    IEnumerable<string> startsWithMethodNameList = _startsWithMethodNames.Where<string>(w => method.Name.StartsWith(w));

                    if (startsWithMethodNameList.Count() == 0)
                    {
                        aopApply = false;
                    }
                }

                if (aopApply)
                {
                    MethodBase targetMethod = this.GetTargetMethod(method);
                    performanceHandler = new PerformanceHandler(target, this.PerformanceType, this.DefineTargetType);
                    performanceHandler.Start(targetMethod);
                }

                returnObj = method.Invoke(target, parameters);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is TsbBaseException)
                {
                    ExceptionHandler.Propagate(ex.InnerException, this.ObjectID);
                }
                else if (ex.InnerException.InnerException != null && ex.InnerException.InnerException.GetType().ToString().Contains("Oracle.ManagedDataAccess") && ex.InnerException.InnerException.ToString().Contains("TNS:"))
                {
                    ExceptionHandler.Wrap(ex.InnerException.InnerException, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00255", null);
                }
                else
                {
                    string fullMassage = null;

                    if (_isShowDetailException && ex.InnerException != null)
                    {
                        fullMassage = DetailExceptionFormatter.GetInnerMassage(ex);
                        ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00000", DefaultMessage.NON_REG_WRD + fullMassage);
                    }
                    else
                    {
                        ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00001", DefaultMessage.NON_REG_WRD + target.ToString());
                    }
                }
            }
            finally
            {
                if (aopApply &&
                    performanceHandler != null)
                {
                    performanceHandler.End();
                }
            }

            return returnObj;
        }

        /// <summary>
        /// Gets Target Method
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private MethodBase GetTargetMethod(MethodBase method)
        {
            if (this.PerformanceType == PerformanceType.DAO)
            {
                return MethodInfoUtil.GetCallerMethodWithType(this.DefineTargetType);
            }
            else
            {
                return method;
            }
        }
        #endregion
    }
}
