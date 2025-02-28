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
* 2011.04.25  Tonny.Kim     Multiple DB Connection
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Context;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.SessionStore;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Constant;
using System.Transactions;
using Tsb.Fontos.Core.Util.Diagnostics.Performance;
using Tsb.Fontos.Core.Util.Diagnostics.Type;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Logging;

namespace Tsb.Fontos.Core.Service
{
    /// <summary>
    /// Client Service Proxy class
    /// </summary>
    public class ClientServiceProxy : TsbBaseObject
    {
        #region FIELD AREA *************************************
        private readonly ITsbLog log = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ITsbDynamicProxy _tsbDynamicProxy = null;
        private string _serviceID = string.Empty;
        private static object syncRoot = new Object();
        private static Nullable<TransactionScopeTypes> PrevTransactionType = null;
        #endregion
                
        #region PROPERTY AREA **********************************
        /// <summary>
        /// Service's Type reference to call
        /// </summary>
        public string ServiceID
        {
            get { return _serviceID; }
        }
        #endregion
        
        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Default constructor
        /// </summary>
        public ClientServiceProxy()
        {
            this.ObjectID = "GNR-FTCO-SVC-ClientServiceProxy";
        }

        /// <summary>
        /// Initialize using service ID
        /// </summary>
        public ClientServiceProxy(string serviceID)
        {
            this._serviceID = serviceID;
        }

        #endregion
        
        #region INSTNACE METHOD AREA ***************************
        /// <summary>
        /// Create Client Service Proxy
        /// </summary>
        /// <typeparam name="T">Serivce Interface</typeparam>
        /// <returns>Service interface which is wrapped by Transparent Proxy object</returns>
        public T CreateClientServiceProxy<T>()
        {
            DynamicProxy dynamicProxy = null;
            T serviceInterface = default(T);
            object targetObject = null;

            try 
            {
                targetObject = ObjectBuilder.BuildUp(this.ServiceID);

                dynamicProxy = new DynamicProxy(targetObject, new InvocationDelegate(invocationHandler));
                
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
        /// Create Client Service Proxy
        /// </summary>
        /// <typeparam name="T">Serivce Interface</typeparam>
        /// <param name="targetObject">Target object to create proxy</param>
        /// <returns>Service interface which is wrapped by Transparent Proxy object</returns>
        public T CreateClientServiceProxy<T>(object targetObject)
        {
            DynamicProxy dynamicProxy = null;
            T serviceInterface = default(T);

            try
            {
                dynamicProxy = new DynamicProxy(targetObject, new InvocationDelegate(invocationHandler));

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
        private object invocationHandler(object target, MethodBase method, object[] parameters)
        {
            TransactionOptionAttribute trOptionAttr = null;
            ITsbService service = null;
            object returnObj = null;
            ORMTechTypes ormTech = default(ORMTechTypes);
            TransactionManagerTypes trManager = default(TransactionManagerTypes);

#if DEBUG
            bool aopApply = ModuleInfo.UsePerformanceLog;
            PerformanceHandler performanceHandler = null;
#endif

            try
            {
#if DEBUG
                if (aopApply)
                {
                    performanceHandler = new PerformanceHandler(target, PerformanceType.BIZ_SERIVCE);
                    performanceHandler.Start(method);
                }
#endif
                Attribute tempAttr = Attribute.GetCustomAttribute(method, typeof(Tsb.Fontos.Core.Transaction.TransactionOptionAttribute));

                if (tempAttr == null)
                {
                    ///MSG: There is no [TransactionOption] attribute definition for [{0}] method. Please define TransactionOption attribute.	
                    throw new TsbSysTypeException(this.ObjectID, "MSG_FTCO_00095", DefaultMessage.NON_REG_WRD + method.DeclaringType.Name + "." + method.Name);
                }
                else
                {
                    trOptionAttr = tempAttr as TransactionOptionAttribute;
                    service = (ITsbService)target;
                }

                if (trOptionAttr.TRScopeType == TransactionScopeTypes.NotSupport)
                {
                    returnObj = method.Invoke(service, parameters);
                    return returnObj;
                }

                ormTech = ArchitectureInfo.GetInstance().ORMTech;
                trManager = PersistenceInfo.GetInstance().TransactionManager;

                if (trManager == TransactionManagerTypes.ORMTechInternal && ormTech == ORMTechTypes.iBatis)
                {
                    bool useMultiDataSource = ModuleInfo.UseMultiDataSource; // Use Multi DataSource

                    if (useMultiDataSource)
                    { // Multi DB Connection - Distributed Transactions(OraMTS)
                        returnObj = this.InvokeMultiDataSource(trOptionAttr, service, method, parameters);
                    }
                    else
                    { // Single DB Connection
                        returnObj = this.InvokeSingleDataSource(trOptionAttr, service, method, parameters);
                    }
                }
            }
            catch (TsbBaseException tsbEx)
            {
                log.Error(tsbEx);
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                log.Error(ex);

                if (ex.InnerException is TsbBaseException)
                {
                    ExceptionHandler.Propagate(ex.InnerException, this.ObjectID);
                }
                else
                {
                    ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00001", DefaultMessage.NON_REG_WRD + target.ToString());
                }
            }
            finally
            {
#if DEBUG
                if (aopApply &&
                    performanceHandler != null)
                {
                    performanceHandler.End();
                }
#endif
            }

            return returnObj;
        }

        #endregion

        #region METHOD AREA(SingleDataSource) ******************
        /// <summary>
        /// Invoke SingleDataSource
        /// </summary>
        /// <param name="trOptionAttr"></param>
        /// <param name="service"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private object InvokeSingleDataSource(TransactionOptionAttribute trOptionAttr, ITsbService service, MethodBase method, object[] parameters)
        {
            object returnObj = null;
            bool isInitTran = false;
            ISqlMapSession sqlMapSession = null;
            PersistenceInfo.DatabaseInfo databaseInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(ConfigConstant.SERVER_ROLE_MAIN_DATABASE);
            SqlMapDaoSupport tempMapDao = new SqlMapDaoSupport(PathUtil.Combine(AppPathInfo.PATH_ROOT_PERSISTENCE, databaseInfo.SqlMapConfig));

            sqlMapSession = tempMapDao.Mapper.LocalSession;

            if (sqlMapSession == null)
            {
                sqlMapSession = tempMapDao.Mapper.OpenConnection();
                isInitTran = true;
            }
            else
            {
                isInitTran = false;
            }

            if (trOptionAttr.TRScopeType == TransactionScopeTypes.RequiresNew)
            {
                try
                {
                    bool isNestedTransaction = false;

                    if (sqlMapSession.IsTransactionStart)
                    { // Nested Transaction
                        sqlMapSession.CommitTransaction();
                        isNestedTransaction = true;
                    }

                    sqlMapSession.BeginTransaction();
                    returnObj = method.Invoke(service, parameters);
                    sqlMapSession.CommitTransaction();

                    if (isNestedTransaction)
                    { // Nested Transaction
                        sqlMapSession.BeginTransaction();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        sqlMapSession.RollBackTransaction();

                        if (isInitTran)
                        {
                            tempMapDao.Mapper.CloseConnection();
                        }
                    }
                    catch (Exception)
                    {
                        tempMapDao.Mapper.CloseConnection();
                    }

                    throw ex;
                }
            }
            else if (trOptionAttr.TRScopeType == TransactionScopeTypes.Required)
            {
                if (sqlMapSession.IsTransactionStart)
                {
                    try
                    {
                        returnObj = method.Invoke(service, parameters);
                    }
                    catch (Exception ex)
                    {
                        if (isInitTran)
                        {
                            tempMapDao.Mapper.CloseConnection();
                        }
                        throw ex;
                    }
                }
                else
                {
                    try
                    {
                        sqlMapSession.BeginTransaction();
                        returnObj = method.Invoke(service, parameters);
                        sqlMapSession.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            sqlMapSession.RollBackTransaction();
                            
                            if (isInitTran)
                            {
                                tempMapDao.Mapper.CloseConnection();
                            }
                        }
                        catch (Exception)
                        {
                            tempMapDao.Mapper.CloseConnection();
                        }
                        throw ex;
                    }
                }
            }
            else
            {
                try
                {
                    returnObj = method.Invoke(service, parameters);
                }
                catch (Exception ex)
                {
                    if (isInitTran)
                    {
                        tempMapDao.Mapper.CloseConnection();
                    }
                    throw ex;
                }
            }

            if (isInitTran)
            {
                tempMapDao.Mapper.CloseConnection();
            }

            return returnObj;
        }
        #endregion

        #region METHOD AREA(MultiDataSource) *******************
        /// <summary>
        /// Invoke Multi DataSource
        /// </summary>
        /// <param name="trOptionAttr"></param>
        /// <param name="service"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private object InvokeMultiDataSource(TransactionOptionAttribute trOptionAttr, ITsbService service, MethodBase method, object[] parameters)
        {
            object returnObj = null;

            try
            {
                TransactionScopeOption transScopeOption = default(TransactionScopeOption);

                lock (syncRoot)
                {
                    transScopeOption = this.GetTransactionScopeOption(trOptionAttr.TRScopeType);
                    PrevTransactionType = trOptionAttr.TRScopeType;
                }

                using (TransactionScope tx = new TransactionScope(transScopeOption))
                {
                    returnObj = method.Invoke(service, parameters);
                    tx.Complete();
                }
            }
            finally
            {
                lock (syncRoot)
                {
                    PrevTransactionType = null;
                }
            }

            return returnObj;
        }
        #endregion

        #region METHOD AREA(GetTransactionScopeOption) *********
        /// <summary>
        /// Gets TransactionScopeOption
        /// </summary>
        /// <param name="transScopeType"></param>
        /// <returns></returns>
        private TransactionScopeOption GetTransactionScopeOption(TransactionScopeTypes transScopeType)
        {
            TransactionScopeOption transOption = default(TransactionScopeOption);

            try
            {
                if (transScopeType == TransactionScopeTypes.NotSupport)
                {
                    transOption = TransactionScopeOption.Suppress;
                }
                else if (transScopeType == TransactionScopeTypes.Required)
                {
                    transOption = TransactionScopeOption.Required;
                }
                else if (transScopeType == TransactionScopeTypes.RequiresNew)
                {
                    transOption = TransactionScopeOption.RequiresNew;
                }
                else if (transScopeType == TransactionScopeTypes.Support)
                {
                    if (PrevTransactionType == TransactionScopeTypes.Required ||
                        PrevTransactionType == TransactionScopeTypes.RequiresNew)
                    {
                        transOption = TransactionScopeOption.Required;
                    }
                    else
                    {
                        transOption = TransactionScopeOption.Suppress;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return transOption;
        }
        #endregion
    }
}
