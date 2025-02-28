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
* 2009.07.15    CHOI 1.0	First release.
* 2011.04.22  Tonny.Kim     Multiple DB Connection
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Transaction.Type;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Xml;
using System.Xml;
using System.Xml.XPath;
using Tsb.Fontos.Core.Constant;
using System.Diagnostics;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Data.IBatis;
using System.Windows.Forms;
using System.Reflection;
using Tsb.Fontos.Core.Security.Encryption;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Persistence Information class
    /// </summary>
    [Serializable]
    public class PersistenceInfo  : BaseEnvironmentInfo
    {
        #region CONST AREA *************************************
        public const string XML_ATT_VALUE_TRANSACTION  = "TRANSACTION";
        public const string XML_ATT_VALUE_TR_MANAGER   = "TransactionManager";
        public const string XML_ATT_VALUE_PRODUCT      = "Product";
        public const string XML_ATT_VALUE_VERSION      = "Version";
        public const string XML_ATT_VALUE_ASSEM_NAME   = "AssemblyName";
        public const string XML_ATT_VALUE_SQLMAPCONFIG = "SqlMapConfig";
        public const string XML_ATT_VALUE_DMS = "DMS";
        public const string XML_ATT_VALUE_DSN = "DSN";
        public const string XML_ATT_VALUE_UID = "UID";
        public const string XML_ATT_VALUE_PWD = "PWD";
        public const string XML_ATT_VALUE_DBQ = "DBQ";
        public const string XML_ATT_VALUE_DB_CONNECTION_TIMEOUT = "DB CONNECTION TIMEOUT";
        public const string XML_ATT_VALUE_STATEMENT_NAME_PREFIX = "StatementNamePrefix";
        public const string XML_ATT_VALUE_DB_CONNECTION_CUSTOM_ATTRIBUTES = "DB Connection Custom Attributes";
        
        #endregion

        #region FIELD/PROPERTY AREA*****************************
        private static PersistenceInfo _instance = null;

        private Dictionary<string, DatabaseInfo> _databaseInfoDic = null;

        /// <summary>
        /// DataBase info dictionary class. You can retrieve database information using dabase name string
        /// </summary>
        public Dictionary<string, DatabaseInfo> DatabaseInfoDic
        {
            get { return _databaseInfoDic; }
            set { _databaseInfoDic = value; }
        }

        private TransactionManagerTypes _tRManager;

        /// <summary>
        /// Transaction manager
        /// </summary>
        public TransactionManagerTypes TransactionManager
        {
            get { return _tRManager; }
        }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        private PersistenceInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-PersistenceInfo";
            this._databaseInfoDic = new Dictionary<string, DatabaseInfo>();
        }

        /// <summary>
        /// Gets Persistence information object reference.
        /// </summary>
        /// <returns>Persistence information object reference</returns>
        public static PersistenceInfo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PersistenceInfo();
                _instance.LoadPersistenceInfo();
            }

            return _instance;
        }
        #endregion

        #region METHOD AREA (LOAD INFORMATION)******************
        /// <summary>
        /// Load persistence info
        /// </summary>
        public void LoadPersistenceInfo()
        {
            string trManager     = string.Empty;
            XmlConfigProvider configProvider = null;

            try
            {
                this._configFileName = AppPathInfo.FILE_NAME_PERSISTENCE_INFO;
                this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, AppPathInfo.FILE_NAME_PERSISTENCE_INFO);
            }
            catch (System.TypeInitializationException initEx)
            {
                if (initEx.InnerException is TsbBaseException)
                {
                    TsbBaseException tsbEx = initEx.InnerException as TsbBaseException;
                    ExceptionHandler.Replace(initEx, initEx.InnerException.GetType(), tsbEx.SourceObjectID, tsbEx.MsgCode, tsbEx.MsgArgs);
                }
                else
                {
                    //MSG:An error occurred when checking the configuration path
                    ExceptionHandler.Wrap(initEx, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00005", null);
                }
            }

            if (string.IsNullOrEmpty(this._sourcePath))
            {
                //MSG:{0} does not exist. Please check {1}.
                throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00121",
                     this._sourcePath,
                    "WRD_FTCO_thisfile"
                    );
            }

            try
            {
                if (!string.IsNullOrEmpty(this._configFileName))
                {
                    configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this._sourcePath);

                    trManager = this.GetValidValue(ref configProvider, PersistenceInfo.XML_ATT_VALUE_TRANSACTION, PersistenceInfo.XML_ATT_VALUE_TR_MANAGER);
                    this._tRManager = this.GetValidType<TransactionManagerTypes>(trManager, PersistenceInfo.XML_ATT_VALUE_TRANSACTION, PersistenceInfo.XML_ATT_VALUE_TR_MANAGER);
                    this.SetDBSectionInfo(configProvider);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        #endregion

        #region METHOD(GetDBSectionNames) AREA *****************
        /// <summary>
        /// Sets DB Section Names
        /// </summary>
        /// <returns></returns>
        private void SetDBSectionInfo(XmlConfigProvider configProvider)
        {
            string mainDBProduct = string.Empty;
            string[] dbSectionNames = configProvider.GetSectionNames();

            try
            {
                foreach (string sectionName in dbSectionNames)
                {
                    if (sectionName.Equals(PersistenceInfo.XML_ATT_VALUE_TRANSACTION)) continue;

                    DatabaseInfo databaseInfo = new DatabaseInfo();

                    databaseInfo.DBSection = sectionName;
                    mainDBProduct = this.GetValidValue(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_PRODUCT);
                    databaseInfo.DBProduct = this.GetValidType<DBProductTypes>(mainDBProduct, sectionName, PersistenceInfo.XML_ATT_VALUE_PRODUCT);
                    databaseInfo.DBVersion = this.GetValidValue(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_VERSION);
                    databaseInfo.DBAssemblyName = this.GetValidValue(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_ASSEM_NAME);
                    databaseInfo.SqlMapConfig = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_SQLMAPCONFIG); ;
                    databaseInfo.DMS = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_DMS); ;
                    databaseInfo.DSN = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_DSN); ;
                    databaseInfo.UID = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_UID); ;
                    databaseInfo.PWD = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_PWD); ;
                    databaseInfo.DBQ = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_DBQ); ;
                    databaseInfo.DBConnTimeout = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_DB_CONNECTION_TIMEOUT);
                    databaseInfo.StatementNamePrefix = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_STATEMENT_NAME_PREFIX);

                    databaseInfo.DBConnCustomAttributes = this.GetValidValueNotException(ref configProvider, sectionName, PersistenceInfo.XML_ATT_VALUE_DB_CONNECTION_CUSTOM_ATTRIBUTES);

                    PersistenceInfo.GetInstance().AddDatabaseInfoToDic(sectionName, databaseInfo);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
        }
        #endregion

        #region METHOD(SavePersistenceInfo) AREA ***************
        public void SavePersistenceInfo(DatabaseInfo databaseInfo)
        {
            XmlConfigProvider configProvider = null;

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this._sourcePath);
                configProvider.SetValue(databaseInfo.DBSection, PersistenceInfo.XML_ATT_VALUE_SQLMAPCONFIG, databaseInfo.SqlMapConfig);
                configProvider.SetValue(databaseInfo.DBSection, PersistenceInfo.XML_ATT_VALUE_DSN,          databaseInfo.DSN);
                configProvider.SetValue(databaseInfo.DBSection, PersistenceInfo.XML_ATT_VALUE_UID,          databaseInfo.UID);
                configProvider.SetValue(databaseInfo.DBSection, PersistenceInfo.XML_ATT_VALUE_PWD,          databaseInfo.PWD);
                configProvider.SetValue(databaseInfo.DBSection, PersistenceInfo.XML_ATT_VALUE_DBQ,          databaseInfo.DBQ);
                //configProvider.SetValue(databaseInfo.DBSection, PersistenceInfo.XML_ATT_VALUE_DB_CONNECTION_TIMEOUT, databaseInfo.DBConnTimeout);
                //configProvider.SetValue(databaseInfo.DBSection, PersistenceInfo.XML_ATT_VALUE_DB_CONNECTION_CUSTOM_ATTRIBUTES, databaseInfo.DBConnCustomAttributes);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }
        #endregion

        #region CLASS(DatabaseInfo) AREA ***********************
        /// <summary>
        /// Database Information Class
        /// </summary>
        public class DatabaseInfo : BaseDataItem
        {
            #region PROPERTY AREA ******************************
            /// <summary>
            /// Database Section
            /// </summary>
            public string DBSection { get; set; }

            /// <summary>
            /// Main Database Product
            /// </summary>
            public DBProductTypes DBProduct { get; set; }

            /// <summary>
            /// Main Database Version
            /// </summary>
            public string DBVersion { get; set; }

            /// <summary>
            /// Applied Tiered Architecture
            /// </summary>
            public string DBAssemblyName { get; set; }

            /// <summary>
            /// SqlMap Config File Path
            /// </summary>
            public string SqlMapConfig { get; set; }

            /// <summary>
            /// DMS
            /// </summary>
            public string DMS { get; set; }

            /// <summary>
            /// DSN
            /// </summary>
            public string DSN { get; set; }
            
            /// <summary>
            /// DB User ID
            /// </summary>
            public string UID { get; set; }

            /// <summary>
            /// DB Password
            /// </summary>
            public string PWD { get; set; }

            /// <summary>
            /// DBQ
            /// </summary>
            public string DBQ { get; set; }

            /// <summary>
            /// DB Connection TimeOut
            /// </summary>
            public string DBConnTimeout { get; set; }

            /// <summary>
            /// Gets or Sets the value of statement name prefix of Ibatis.
            /// </summary>
            public string StatementNamePrefix { get; set; }

            /// <summary>
            /// Gets or Sets the value of DB Connection Custom Attributes
            /// </summary>
            public string DBConnCustomAttributes { get; set; }
            #endregion

            #region INITIALIZE AREA ****************************
            public DatabaseInfo()
                : base()
            {
                this.ObjectID = "GNR-FTCO-ENV-DatabaseInfo";
            }
            #endregion
        }
        #endregion

        #region METHOD AREA (HADLING SERVER INFO DIC)***********
        /// <summary>
        /// Add a database information object to system server info dictionary
        /// </summary>
        /// <param name="serverRole">A databse's section</param>
        /// <param name="serverInfo">A database information object reference</param>
        public void AddDatabaseInfoToDic(string databaseSection, DatabaseInfo databaseInfo)
        {
            if (!PersistenceInfo.GetInstance().DatabaseInfoDic.ContainsKey(databaseSection))
            {
                PersistenceInfo.GetInstance().DatabaseInfoDic.Add(databaseSection, databaseInfo);
            }
        }

        /// <summary>
        /// Retrieve  a specified role database information object reference
        /// </summary>
        /// <param name="serverRole">A dabase section</param>
        public DatabaseInfo GetDatabaseInfo(string databaseSection)
        {
            if (PersistenceInfo.GetInstance().DatabaseInfoDic.ContainsKey(databaseSection))
            {
                return PersistenceInfo.GetInstance().DatabaseInfoDic[databaseSection];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Changes main db connection.
        /// </summary>
        /// <param name="dbq"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChangeMainDBConnection(string dbq, string uid, string pwd)
        {
            try
            {
                ChangeMainDBConnection(dbq, uid, pwd, null);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                return false;
            }

            return false;
        }

        /// <summary>
        /// Changes main db connection.
        /// </summary>
        /// <param name="dbq"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="dBConnCustomAttributes"></param>
        /// <returns></returns>
        public bool ChangeMainDBConnection(string dbq, string uid, string pwd, string dBConnCustomAttributes)
        {
            try
            {
                if (_databaseInfoDic.ContainsKey(ConfigConstant.SERVER_ROLE_MAIN_DATABASE) == true)
                {
                    DatabaseInfo tempMainDB = _databaseInfoDic[ConfigConstant.SERVER_ROLE_MAIN_DATABASE].Clone() as DatabaseInfo;

                    if (string.IsNullOrEmpty(uid) == false)
                    {
                        tempMainDB.UID = uid;
                    }

                    if (string.IsNullOrEmpty(dbq) == false)
                    {
                        tempMainDB.DBQ = dbq;
                    }

                    if (string.IsNullOrEmpty(pwd) == false)
                    {
                        tempMainDB.PWD = pwd;
                    }

                    if (string.IsNullOrEmpty(dBConnCustomAttributes) == false)
                    {
                        tempMainDB.DBConnCustomAttributes = dBConnCustomAttributes;
                    }

                    return SqlMapDaoSupport.ChangeMapper(tempMainDB, tempMainDB.PWD, true);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                return false;
            }

            return false;
        }

        /// <summary>
        /// Change DB ConnectionString.
        /// If a parameter(uid, pwd or dbq) is null or empty, it would not applied.
        /// </summary>
        /// <param name="databaseSection"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="dbq"></param>
        public bool ChangeConnString(string databaseSection, string dbq, string uid, string pwd)
        {
            DatabaseInfo infoClone = null;
            bool isSuccess = true;

            try
            {
                if (PersistenceInfo.GetInstance().DatabaseInfoDic.ContainsKey(databaseSection) && databaseSection != ConfigConstant.SERVER_ROLE_MAIN_DATABASE)
                {
                    DatabaseInfo info = PersistenceInfo.GetInstance().DatabaseInfoDic[databaseSection];
                    infoClone = (DatabaseInfo)info.Clone();
                    IBaseEncrypter encrypter = this.GetEncrypter();

                    if (string.IsNullOrEmpty(uid) == false)
                    {
                        info.UID = uid;
                    }

                    if (string.IsNullOrEmpty(dbq) == false)
                    {
                        info.DBQ = dbq;
                    }

                    if (encrypter != null)
                    {
                        if (string.IsNullOrEmpty(pwd) == false)
                        {
                            info.PWD = encrypter.EncryptString(pwd, SecurityPolicyInfo.ENCRYPTION_KEY);
                        }
                        else if (string.IsNullOrEmpty(info.PWD) == false)
                        {
                            pwd = encrypter.Decrypt(info.PWD, SecurityPolicyInfo.ENCRYPTION_KEY);
                        }
                    }

                    if (SqlMapDaoSupport.ChangeMapper(info, pwd) == false)
                    {
                        PersistenceInfo.GetInstance().DatabaseInfoDic[databaseSection] = infoClone;
                        isSuccess = false;
                    }
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                if (infoClone != null)
                {
                    PersistenceInfo.GetInstance().DatabaseInfoDic[databaseSection] = infoClone; 
                }
                GeneralLogger.Error(ex);

                return false;
            }

            return isSuccess;
        }

        public string DecryptPWD(string pwd)
        {
            string decryptedPWD = string.Empty;
            IBaseEncrypter encrypter = GetEncrypter();

            if (encrypter != null)
            {
                if (string.IsNullOrEmpty(pwd) == false)
                {
                    decryptedPWD = encrypter.Decrypt(pwd, SecurityPolicyInfo.ENCRYPTION_KEY);
                }
            }

            return decryptedPWD;
        }

        private IBaseEncrypter GetEncrypter()
        {
            IBaseEncrypter encrypter = null;

            try
            {
                FormCollection frmCol = Application.OpenForms;
                if (frmCol.Count != 0 && frmCol[0].GetType().ToString().Contains("MenuView"))
                {
                    PropertyInfo appPropInfo = frmCol[0].GetType().GetProperty("AppInitializer");

                    if (appPropInfo != null)
                    {
                        object appInit = appPropInfo.GetValue(frmCol[0], null);

                        if (appInit != null)
                        {
                            PropertyInfo adapterPropInfo = appInit.GetType().GetProperty("ConfigInitAdapter");

                            if (adapterPropInfo != null)
                            {
                                IConfigInitAdapter adapter = adapterPropInfo.GetValue(appInit, null) as IConfigInitAdapter;

                                if (adapter != null)
                                {
                                    encrypter = adapter.PasswordEncrypter;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return encrypter;
        }
        #endregion
    }
}
