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
* DATE           AUTHOR	     	REVISION    	
* 2010.09.03  Tonny.Kim 1.0	  First release.
* 2011.04.25  Tonny.Kim       Multiple DB Connection
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Security.Encryption;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Monitoring.Database;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Security.Authentication
{
    public abstract class BaseAuthenHandler : TsbBaseObject, IAuthenticationHandler
    {
        #region FIELD/PROPERTY AREA *********************************
        /// <summary>
        /// Gets or Sets Database Monitoring Service interface reference
        /// </summary>
        public IDBMonitorService DBMonitorService { get; set; }

        /// <summary>
        ///  Gets or Sets Previous User ID
        /// </summary>
        public string PrevLoginUserID { get; set; }

        /// <summary>
        ///  Gets or Sets Equipment Name
        /// </summary>
        public string EquipmentName { get; set; }

        private BaseAuthenHandler _nextHandler;
        /// <summary>
        /// Gets or Sets next AuthenHandler
        /// </summary>
        public BaseAuthenHandler NextHandler
        {
            get { return this._nextHandler; }
            set { this._nextHandler = value; }
        }

        /// <summary>
        ///  Gets or Sets Security Service Implements
        /// </summary>
        public virtual ISecurityService SecurityService { get; set; }

        /// <summary>
        /// Gets or Sets Password Encrypter
        /// </summary>
        public IBaseEncrypter PasswordEncrypter { get; set; }

        private bool _canChangePassword = true;
        /// <summary>
        /// Gets or Sets whether password is changeable on Login View
        /// </summary>
        public bool CanChangePassword
        {
            get { return this._canChangePassword; }
            set { this._canChangePassword = value; }
        }

        /// <summary>
        /// 이전 AuthenHandler의 인증과정을 통과했는지 유무
        /// </summary>
        protected bool IsPreAuthenCheckOK { get; set; }

        /// <summary>
        /// Gets or Sets whether user is expried or not
        /// </summary>
        public virtual bool DoExpireCheck { get; set; }

        protected abstract bool CheckHandler(string userId, string password);
        protected abstract bool CheckHandler(string userId, string password, bool isPwExpiredCheck);
        //protected abstract bool CheckHandler(BaseSecurityParam param, bool isPwExpiredCheck);

        protected virtual bool CheckHandler(Core.Security.BaseSecurityParam param, bool isPwExpiredCheck)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region METHOD AREA (OPERATOR OVERLOADING)**************
        /// <summary>
        /// operator overloading to make chain of validator
        /// </summary>
        /// <param name="leftValidator">left side validator</param>
        /// <param name="rightValidator">right side validator</param>
        /// <returns>chain of validators</returns>
        public static BaseAuthenHandler operator +(BaseAuthenHandler leftValidator, BaseAuthenHandler rightValidator)
        {
            BaseAuthenHandler last = leftValidator;

            while (last.NextHandler != null)
            {
                last = last.NextHandler;
            }

            last.NextHandler = rightValidator;

            return leftValidator;
        }
        #endregion

        #region METHOD AREA ()***************************************
        /// <summary>
        /// Sets Authen Handler
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SetAuthenHandler(string userId)
        {
            return this.SetAuthenHandler(userId, null);
        }

        /// <summary>
        /// Sets Authen Handler
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SetAuthenHandler(string userId, string password)
        {
            return this.SetAuthenHandler(userId, password, true);
        }

        /// <summary>
        /// Sets Authen Handler
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="isPwExpiredCheck"></param>
        /// <returns></returns>
        public bool SetAuthenHandler(string userId, string password, bool isPwExpiredCheck)
        {
            if (this.CheckHandler(userId, password, isPwExpiredCheck))
            {
                if (this.NextHandler != null)
                {
                    return this.NextHandler.SetAuthenHandler(userId, password, isPwExpiredCheck);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets Authen Handler
        /// </summary>
        /// <param name="param"></param>
        /// <param name="isPwExpiredCheck"></param>
        /// <returns></returns>
        public bool SetAuthenHandler(BaseSecurityParam param, bool isPwExpiredCheck)
        {
            if (this.CheckHandler(param, isPwExpiredCheck))
            {
                if (this.NextHandler != null)
                {
                    this.NextHandler.IsPreAuthenCheckOK = true;

                    return this.NextHandler.SetAuthenHandler(param, isPwExpiredCheck);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets DB Connection String
        /// </summary>
        /// <param name="dbUserId"></param>
        /// <param name="dbPassword"></param>
        protected void SetMainDBConnectionString(string dbUserId, string dbPassword)
        {
            this.SetDBConnectionString(ConfigConstant.SERVER_ROLE_MAIN_DATABASE, dbUserId, dbPassword);
        }

        /// <summary>
        /// Sets DB Conection String
        /// </summary>
        /// <param name="dBServer"></param>
        /// <param name="dbUserId"></param>
        /// <param name="dbPassword"></param>
        /// <param name="dataSource"></param>
        protected Server SetDBConnectionString(string dBSection, string dbUserId, string dbPassword)
        {
            return this.SetDBConnectionString(dBSection, dbUserId, dbPassword, null);
        }

        /// <summary>
        /// Sets DB Conection String
        /// </summary>
        /// <param name="dBServer"></param>
        /// <param name="dbUserId"></param>
        /// <param name="dbPassword"></param>
        /// <param name="dataSource"></param>
        protected Server SetDBConnectionString(string dBSection, string dbUserId, string dbPassword, string dataSource)
        {
            return this.SetDBConnectionString(dBSection, dbUserId, dbPassword, dataSource, null);
        }

        /// <summary>
        /// Sets DB Connection String
        /// </summary>
        /// <param name="dbUserId"></param>
        /// <param name="dbPassword"></param>
        protected Server SetDBConnectionString(string dBSection, string dbUserId, string dbPassword, string dataSource, string DBConnectionTimeout)
        {
            try
            {
                if (SysServersInfo.GetInstance().ServersInfoDic.ContainsKey(dBSection))
                {
                    Server dBServer = SysServersInfo.GetInstance().ServersInfoDic[dBSection];
                    dBServer.UserId = dbUserId;
                    dBServer.Password = dbPassword;
                    if (!string.IsNullOrEmpty(dataSource)) dBServer.Alias = dataSource;
                    if (!string.IsNullOrEmpty(DBConnectionTimeout)) dBServer.DBConnectionTimeout = DBConnectionTimeout;

                    dBServer.ConnString = "Data Source=" + dBServer.Alias
                        + ";User Id=" + dBServer.UserId
                        + ";Password=" + dBServer.Password;
                    if (!string.IsNullOrEmpty(dBServer.DBConnectionTimeout))
                    {
                        dBServer.ConnString += ";Connection Timeout=" + dBServer.DBConnectionTimeout;
                    }

                    this.CreateDBConnection(dBSection);

                    return dBServer;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return null;
        }

        /// <summary>
        /// Create DB Connection
        /// </summary>
        protected void CreateMainDBConnection()
        {
            this.CreateDBConnection(ConfigConstant.SERVER_ROLE_MAIN_DATABASE);
        }

        /// <summary>
        /// Create DB Connection
        /// </summary>
        /// <param name="dBSection"></param>
        protected void CreateDBConnection(string dBSection)
        {
            Server dBServer = SysServersInfo.GetInstance().ServersInfoDic[dBSection];
            PersistenceInfo.DatabaseInfo dBInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(dBSection);

            try
            {
                if (dBServer.ProductName.ToUpper().StartsWith(ConfigConstant.PRODUCT_NAME_DB_ORACLE)
                    || dBServer.ProductName.ToUpper().StartsWith(ConfigConstant.PRODUCT_NAME_DB_MSSQL)
                    || dBServer.ProductName.ToUpper().StartsWith(ConfigConstant.PRODUCT_NAME_DB_POSTGRE)
                    )
                {
                    string sqlMapConfig = PathUtil.Combine(AppPathInfo.PATH_ROOT_PERSISTENCE, dBInfo.SqlMapConfig);

                    if (SqlMapDaoSupport.MapConnStrKeyValueDic.ContainsKey(sqlMapConfig))
                    {
                        SqlMapDaoSupport.MapConnStrKeyValueDic[sqlMapConfig] = dBServer.ConnString;
                    }
                    else
                    {
                        SqlMapDaoSupport.MapConnStrKeyValueDic.Add(sqlMapConfig, dBServer.ConnString);
                    }

                    this.CheckDBConnection(dBServer);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion

        #region METHOD AREA (CHECK DB CONNECTION)***************
        /// <summary>
        /// Return true if DB connection is enable
        /// </summary>
        /// <returns>true if DB connection is enable</returns>
        private bool CheckDBConnection(Server dBServer)
        {
            string mainDBConnStr = string.Empty;
            bool isSuccess = false;

            try
            {
                mainDBConnStr = dBServer.ConnString;
                this.DBMonitorService = BizServiceLocator.GetService<IDBMonitorService>(SysObjectSpec.SYS_OBJECT_DB_MONITOR_SERVICE);
                isSuccess = this.DBMonitorService.CheckConnectionEnable(mainDBConnStr);

                if (isSuccess == false)
                {
                    //MSG:Fail to connect to Database Server. Please check Database setting.	
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00081", null);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_99998", ex.Message);
            }

            return isSuccess;
        }
        #endregion

        #region INITIALIZE AREA ********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseAuthenHandler()
        {
            this.ObjectID = "ITM-FT-FTCO-SEC-BaseAuthenHandler";
        }
        #endregion
    }
}
