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
* 2009.05.16    CHOI 1.0	First release.
* 2010.02.20  Tonny.Kim     Add to MapConnStrKeyValueDic
* 2011.04.25  Tonny.Kim     Multiple DB Connection
*                           GetSqlMapConfigToDBType() 
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Spring.Dao.Support;
using IBatisNet.DataMapper;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Exceptions.System;
using System.Windows.Forms;
using System.Collections.Specialized;
using IBatisNet.DataMapper.Configuration;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Environments;
using System.Xml;
using Tsb.Fontos.Core.Xml;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Logging;
using System.Data;
using Tsb.Fontos.Core.Data.Information;
using Tsb.Fontos.Core.Util.Diagnostics.Performance;
using Tsb.Fontos.Core.Util.Diagnostics.Proxy;
using Tsb.Fontos.Core.Util.Diagnostics.Type;
using Tsb.Fontos.Core.Data.IBatis.Batch;
using Tsb.Fontos.Core.Log;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Configuration.Statements;


namespace Tsb.Fontos.Core.Data.IBatis
{
    /// <summary>
    /// iBatis DAO support class (iBatis DAO base class)
    /// </summary>
    public class SqlMapDaoSupport : DaoSupport, IUnifiedDao, IBatchDao, ITsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************
        private readonly ITsbLog tsbLog = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly string CUSTOM_CONFIG = "#Custom";
        private SqlMapperHandler _sqlMapperHandler = null;
#if DEBUG
        private static string[] PERFORMANCE_PROXY_METHOD_NAMES = { "Delete", "Insert", "Update", "Query", "CallProcedure" };
#endif

        private static Dictionary<string, ISqlMapper> _sqlMapperDic = new Dictionary<string, ISqlMapper>();
        public static Dictionary<string, string> MapConnStrKeyValueDic = new Dictionary<string, string>();

        private static object _syncRoot = new Object();
        private const string OBJECT_ID = "GNR-FTCO-DAT-SqlMapDaoSupport";

        private bool _isShowDetailException = false;

        /// <summary>
        /// Gets or Sets DBTYPE String
        /// </summary>
        private string _dBTypeString = string.Empty;

        /// <summary>
        /// Gets or Sets DBTYPE
        /// </summary>
        private string _dBType = ConfigConstant.SERVER_ROLE_MAIN_DATABASE; // Default Setting

        /// <summary>
        /// SQL Batch Manager
        /// </summary>
        private SqlBatchManager _sqlBatchManager;

        /// <summary>
        /// SQL Statement Name Handler
        /// </summary>
        private StatementNameHandler _stmtNameHandler;

        private string _classModuleName;

        /// <summary>
        /// Gets or Sets DBTYPE
        /// </summary>
        public string DBType
        {
            get { return this._dBType; }
            set { this._dBType = value; }
        }

        /// <summary>
        /// Gets and Sets Object ID
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// Gets and Sets Object Type
        /// </summary>
        public ObjectType ObjectType { get; set; }

        // CHOI ADD START
        private String _sqlMapConfig = null;
        /// <summary>
        /// Sets IBatis Sql map config path
        /// </summary>
        public string SqlMapConfig
        {
            get { return this._sqlMapConfig; }
            set { _sqlMapConfig = value; }
        }

        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnString
        {
            get { return SqlMapDaoSupport.MapConnStrKeyValueDic[this.SqlMapConfig]; }
        }

        /// <summary>
        /// Gets IBatis Mapper object reference
        /// </summary>
        public ISqlMapper Mapper
        {
            get
            {
                this.SqlMapConfig = this.GetSqlMapConfigToDBType();
                ISqlMapper rtnMapper = SqlMapDaoSupport.GetMapperFromDic(this.SqlMapConfig, _classModuleName, this.DBType);

#if DEBUG
                if (ModuleInfo.UsePerformanceLog)
                {
                    rtnMapper =
                        PerformanceProxyFactory.CreateProxy<ISqlMapper>(rtnMapper, PerformanceType.DAO, this.GetType(), PERFORMANCE_PROXY_METHOD_NAMES);
                }
#endif
                return rtnMapper;
            }
        }

        //CHOI ADD END

        /// <summary>
        /// Gets or Sets IBatis Sql mapper hadling object referecne
        /// </summary>
        public SqlMapperHandler SqlMapperHandler
        {
            get { return _sqlMapperHandler; }
            set { _sqlMapperHandler = value; }
        }

        /// <summary>
        /// Sets IBatis sql map config path string
        /// </summary>
        public string ConfigPath
        {
            set { SqlMapperHandler.SqlMapConfig = value; }
        }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SqlMapDaoSupport()
        {
            this.ObjectID = OBJECT_ID;

            try
            {
                //TODO:아래라인은 전체 FONTOS 적용시 삭제할것
                this.SqlMapperHandler = new SqlMapperHandler();
                string[] pathArr = this.GetType().AssemblyQualifiedName.Split('.');
                this._classModuleName = pathArr[1] + pathArr[2];
                this.SetConnectionDataBase(this.DBType);
                _isShowDetailException = DetailExceptionFormatter.GetShowDetailExceptionConfig();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Initialize Instance using a specified IBATIS sql map config path string
        /// </summary>
        /// <param name="sqlMapConfigPath">IBATIS sql map config path string</param>
        public SqlMapDaoSupport(string sqlMapConfigPath)
        {
            this.ObjectID = OBJECT_ID;

            try
            {
                //TODO:아래라인은 전체 FONTOS 적용시 삭제할것
                this.SqlMapperHandler = new SqlMapperHandler(sqlMapConfigPath);
                string[] pathArr = this.GetType().AssemblyQualifiedName.Split('.');
                this._classModuleName = pathArr[1] + pathArr[2];
                this.SqlMapConfig = sqlMapConfigPath;
                SqlMapDaoSupport.GetMapperFromDic(this.SqlMapConfig);
                _isShowDetailException = DetailExceptionFormatter.GetShowDetailExceptionConfig();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion


        private static ISqlMapper GetMapperFromDic(string fileFullPathOfsqlMapConfig)
        {
            ISqlMapper rtnMapper = null;
            string mapConfigFile = fileFullPathOfsqlMapConfig.Replace('/', '\\');

            if (SqlMapDaoSupport.MapConnStrKeyValueDic.ContainsKey(mapConfigFile) == false)
            {
                return null;
            }

            try
            {
                lock (SqlMapDaoSupport._syncRoot)
                {
                    if (SqlMapDaoSupport._sqlMapperDic.ContainsKey(mapConfigFile))
                    {
                        rtnMapper = SqlMapDaoSupport._sqlMapperDic[mapConfigFile];
                    }
                    else
                    {
                        rtnMapper = CreateMapper(mapConfigFile);
                        SqlMapDaoSupport._sqlMapperDic.Add(mapConfigFile, rtnMapper);
                    }
                }
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error when configuring IBATIS Sql Mapper [Path: {0}].		
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), SqlMapDaoSupport.OBJECT_ID, "MSG_FTCO_00058", DefaultMessage.NON_REG_WRD + mapConfigFile);
            }

            return rtnMapper;
        }

        private static ISqlMapper GetMapperFromDic(string fileFullPathOfsqlMapConfig, string classAssemblyName, string dbType)
        {
            ISqlMapper rtnMapper = null;
            string mapConfigFile = fileFullPathOfsqlMapConfig.Replace('/', '\\');

            if (SqlMapDaoSupport.MapConnStrKeyValueDic.ContainsKey(mapConfigFile) == false)
            {
                return null;
            }

            try
            {
                lock (SqlMapDaoSupport._syncRoot)
                {
                    if (SqlMapDaoSupport._sqlMapperDic.ContainsKey(mapConfigFile))
                    {
                        if (AppPathInfo.FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS == null || AppPathInfo.FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS.Length == 0)
                        {
                            rtnMapper = SqlMapDaoSupport._sqlMapperDic[mapConfigFile];
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(classAssemblyName) == false && AppPathInfo.FILE_NAME_ADDITIONAL_MODULE_NAMES.Contains(classAssemblyName))
                            {
                                int index = -1;

                                for(int i = 0; i < AppPathInfo.FILE_NAME_ADDITIONAL_MODULE_NAMES.Length; i++)
                                {
                                    if(AppPathInfo.FILE_NAME_ADDITIONAL_MODULE_NAMES[i] == classAssemblyName)
                                    {
                                        index = i;
                                        break;
                                    }
                                }

                                PersistenceInfo.DatabaseInfo databaseInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(dbType);
                                PersistenceInfo.DatabaseInfo databaseInfoClone = databaseInfo.Clone() as PersistenceInfo.DatabaseInfo;
                                mapConfigFile = PathUtil.Combine(AppPathInfo.PATH_ROOT_PERSISTENCE, AppPathInfo.FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS[index]);
                                databaseInfoClone.SqlMapConfig = AppPathInfo.FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS[index];
                                string pwd = PersistenceInfo.GetInstance().DecryptPWD(databaseInfoClone.PWD);
                                string newConnString = "Data Source=" + databaseInfoClone.DBQ + ";User Id=" + databaseInfoClone.UID + ";Password=" + pwd;

                                if (MapConnStrKeyValueDic.ContainsKey(mapConfigFile) == false)
                                {
                                    MapConnStrKeyValueDic.Add(mapConfigFile, newConnString);
                                }
                                else
                                {
                                    MapConnStrKeyValueDic[mapConfigFile] = newConnString;
                                }

                                if (_sqlMapperDic.ContainsKey(mapConfigFile) == false)
                                {
                                    _sqlMapperDic.Add(mapConfigFile, null);
                                }

                                ChangeMapper(databaseInfoClone, pwd);
                            }

                            rtnMapper = SqlMapDaoSupport._sqlMapperDic[mapConfigFile];
                        }
                    }
                    else
                    {
                        rtnMapper = CreateMapper(mapConfigFile);
                        SqlMapDaoSupport._sqlMapperDic.Add(mapConfigFile, rtnMapper);
                    }
                }
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error when configuring IBATIS Sql Mapper [Path: {0}].		
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), SqlMapDaoSupport.OBJECT_ID, "MSG_FTCO_00058", DefaultMessage.NON_REG_WRD + mapConfigFile);
            }

            return rtnMapper;
        }

        private static ISqlMapper CreateMapper(string mapConfigFile)
        {
            ISqlMapper rtnMapper = null;
            string fileDir = mapConfigFile;

            try
            {
                if (string.IsNullOrEmpty(mapConfigFile) == false && mapConfigFile.Contains(CUSTOM_CONFIG))
                {
                    fileDir = mapConfigFile.Replace(CUSTOM_CONFIG, "");
                }

                if (FileUtil.Exists(fileDir) == false)
                {
                    //MSG:[{0}] file does not exist. Please check this file (Path:{1})	
                    throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00126", DefaultMessage.NON_REG_WRD + "IBATIS SqlMapConfig", DefaultMessage.NON_REG_WRD + mapConfigFile);
                }

                DomSqlMapBuilder sqlMapBuilder = new DomSqlMapBuilder();

                XmlDocument xmlDocument = null;
                XmlNodeList xmlNodelist = null;

                xmlDocument = XmlUtil.GetXmlDocument(fileDir);
                xmlNodelist = xmlDocument.GetElementsByTagName(ConfigConstant.XML_ELE_SQLMAP_PROPERTIES);

                if (xmlNodelist == null || xmlNodelist.Count == 0)
                {
                    NameValueCollection properties = new NameValueCollection();
                    string connString = SqlMapDaoSupport.MapConnStrKeyValueDic[mapConfigFile];
                    properties.Add(ConfigConstant.XML_ATT_SQLMAP_CONNECT_STRING, connString);
                    sqlMapBuilder.Properties = properties;

                    //iBatis 예외 처리.
                    try
                    {
                        rtnMapper = sqlMapBuilder.Configure(fileDir);
                    }
                    catch (IBatisNet.Common.Exceptions.ConfigurationException configEx)
                    {
                        string appendDetailMsg = SqlMapDaoSupport.GetProviderNotFoundExceptionMsg(configEx);

                        Exception newEx = new Exception(configEx.Message + appendDetailMsg + "\r\n", configEx.InnerException);
                        GeneralLogger.Error(configEx);
                        throw newEx;
                    }
                }
                else
                {
                    rtnMapper = sqlMapBuilder.Configure(mapConfigFile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rtnMapper;
        }

        private static string GetProviderNotFoundExceptionMsg(IBatisNet.Common.Exceptions.ConfigurationException configEx)
        {
            string appendDetailMsg = string.Empty;

            if (configEx.InnerException != null)
            {
                string[] detailStrArr = configEx.InnerException.Message.Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries);

                if (detailStrArr.Length > 0)
                {
                    StringBuilder detailMsgAppender = new StringBuilder();

                    for (int i = 0; i < detailStrArr.Length; i++)
                    {
                        if (i == detailStrArr.Length - 1)
                        {
                            detailMsgAppender.Append(detailStrArr[i]);
                        }
                        else
                        {
                            detailMsgAppender.AppendLine(detailStrArr[i] + ".");
                        }
                    }

                    appendDetailMsg = "\r\n- " + detailMsgAppender.ToString();
                }
            }

            return appendDetailMsg;
        }

        public static bool ChangeMapper(PersistenceInfo.DatabaseInfo info, string pwd)
        {
            return ChangeMapper(info, pwd, false);
        }

        public static bool ChangeMapper(PersistenceInfo.DatabaseInfo info, string pwd, bool allowChangeMainDB)
        {
            try
            {
                string sqlMapConfig = PathUtil.Combine(AppPathInfo.PATH_ROOT_PERSISTENCE, info.SqlMapConfig);
                ISqlMapper mapper = null;

                if (string.IsNullOrEmpty(sqlMapConfig)) return false;

                if (MapConnStrKeyValueDic != null && MapConnStrKeyValueDic.ContainsKey(sqlMapConfig))
                {
                    Dictionary<string, string> copyDic = new Dictionary<string, string>();

                    foreach (var item in MapConnStrKeyValueDic)
                    {
                        copyDic.Add(item.Key, item.Value);
                    }

                    //Check whether the map config file is for the main database.
                    PersistenceInfo.DatabaseInfo mainDBInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(ConfigConstant.SERVER_ROLE_MAIN_DATABASE);
                    bool isMain = false;

                    if (mainDBInfo != null && allowChangeMainDB == false)
                    {
                        string mainDBMapConfig = mainDBInfo.SqlMapConfig;
                        isMain = info.SqlMapConfig == mainDBMapConfig;
                    }

                    //Create ConnString.
                    string newConnString = "Data Source=" + info.DBQ + ";User Id=" + info.UID + ";Password=" + pwd;

                    if (string.IsNullOrEmpty(info.DBConnTimeout) == false)
                    {
                        newConnString += ";Connection Timeout=" + info.DBConnTimeout;
                    }

                    if (string.IsNullOrEmpty(info.DBConnCustomAttributes) == false)
                    {
                        newConnString += ";" + info.DBConnCustomAttributes;
                    }

                    //Set to ConnString dictionary.
                    if (isMain && MapConnStrKeyValueDic.ContainsKey(sqlMapConfig + CUSTOM_CONFIG) == false)
                    {
                        MapConnStrKeyValueDic.Add(sqlMapConfig + CUSTOM_CONFIG, newConnString);
                        info.SqlMapConfig += CUSTOM_CONFIG;
                    }
                    else
                    {
                        if (isMain == false)
                        {
                            MapConnStrKeyValueDic[sqlMapConfig] = newConnString;
                        }
                        else if (MapConnStrKeyValueDic.ContainsKey(sqlMapConfig + CUSTOM_CONFIG))
                        {
                            MapConnStrKeyValueDic[sqlMapConfig + CUSTOM_CONFIG] = newConnString;
                            info.SqlMapConfig += CUSTOM_CONFIG;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    //Create new map config file and set it to map config file dictionary.
                    mapper = CreateMapper(PathUtil.Combine(AppPathInfo.PATH_ROOT_PERSISTENCE, info.SqlMapConfig));

                    if (mapper != null && _sqlMapperDic.ContainsKey(sqlMapConfig))
                    {
                        if (isMain && _sqlMapperDic.ContainsKey(sqlMapConfig + CUSTOM_CONFIG) == false)
                        {
                            _sqlMapperDic.Add(sqlMapConfig + CUSTOM_CONFIG, mapper);
                        }
                        else
                        {
                            if (isMain == false)
                            {
                                _sqlMapperDic[sqlMapConfig] = mapper;
                            }
                            else if (_sqlMapperDic.ContainsKey(sqlMapConfig + CUSTOM_CONFIG))
                            {
                                _sqlMapperDic[sqlMapConfig + CUSTOM_CONFIG] = mapper;
                            }
                            else
                            {
                                MapConnStrKeyValueDic = copyDic;
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                return false;
            }

            return true;
        }


        #region METHOD AREA (INSERT) ***************************

        /// <summary>
        /// Executes a mapped SQL INSERT statement using Item Object
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns>If RDBMS support, return the primary key of the newly inserted row</returns>
        public object InsertItem(string statementName, object parameterObject)
        {
            object resultObj = null;
            try
            {
                statementName = this.GetStatementName(statementName);

                if (this.AddBatch(statementName, parameterObject) == true)
                {
                    return null;
                }

                resultObj = this.Mapper.Insert(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00115", null);
            }
            return resultObj;
        }

        /// <summary>
        /// Executes a mapped SQL INSERT statement
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns>If RDBMS support, return the primary key of the newly inserted row</returns>
        public object Insert(string statementName, object parameterObject)
        {
            object resultObj = null;

            try
            {
                statementName = this.GetStatementName(statementName);

                if (this.AddBatch(statementName, parameterObject) == true)
                {
                    return null;
                }

                resultObj = this.Mapper.Insert(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00115", null);
            }
            return resultObj;
        }

        #endregion


        #region METHOD AREA (UPDATE) ***************************
        /// <summary>
        /// Executes a mapped SQL UPDATE statement using Item object 
        /// Update can also be used for any other update statement type, 
        /// such as inserts and deletes. Update returns the number of rows effected. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns>The number of rows effected. </returns>
        public int UpdateItem(string statementName, object parameterObject)
        {
            int result = -1;

            try
            {
                statementName = this.GetStatementName(statementName);

                if (this.AddBatch(statementName, parameterObject) == true)
                {
                    return result;
                }

                result = this.Mapper.Update(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00117", null);
            }

            return result;
        }

        /// <summary>
        /// Executes a mapped SQL UPDATE statement.
        /// Update can also be used for any other update statement type, 
        /// such as inserts and deletes. Update returns the number of rows effected. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns>The number of rows effected. </returns>
        public int Update(string statementName, object parameterObject)
        {
            int result = -1;

            try
            {
                statementName = this.GetStatementName(statementName);

                if (this.AddBatch(statementName, parameterObject) == true)
                {
                    return result;
                }

                result = this.Mapper.Update(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00117", null);
            }
            return result;
        }
        #endregion


        #region METHOD AREA (DELETE) ***************************
        /// <summary>
        /// Executes a mapped SQL DELETE statement using Item object.
        /// Delete returns the number of rows effected. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns>The number of rows effected. </returns>
        public int DeleteItem(string statementName, object parameterObject)
        {
            int result = -1;

            try
            {
                statementName = this.GetStatementName(statementName);

                if (this.AddBatch(statementName, parameterObject) == true)
                {
                    return result;
                }

                result = this.Mapper.Delete(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00118", null);
            }
            return result;
        }

        /// <summary>
        /// Executes a mapped SQL DELETE statement.
        /// Delete returns the number of rows effected. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns>The number of rows effected. </returns>
        public int Delete(string statementName, object parameterObject)
        {
            int result = -1;

            try
            {
                statementName = this.GetStatementName(statementName);

                if (this.AddBatch(statementName, parameterObject) == true)
                {
                    return result;
                }

                result = this.Mapper.Delete(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00118", null);
            }
            return result;
        }

        #endregion


        #region METHOD AREA (SELECT) ***************************
        /// <summary>
        /// Executes a mapped SQL SELECT statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns><A List of result objects. /returns>
        public IList QueryForList(string statementName, object parameterObject)
        {
            IList resultList = null;

            try
            {
                statementName = this.GetStatementName(statementName);
                resultList = this.Mapper.QueryForList(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                string fullMassage = null;
                this.SetQueryErrorLog(statementName, ex);

                if (_isShowDetailException && ex.InnerException != null)
                {
                    fullMassage = DetailExceptionFormatter.GetInnerMassage(ex);
                    ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00000", DefaultMessage.NON_REG_WRD + fullMassage);
                }
                else
                {
                    //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
                    ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00116", null);
                }
            }
            return resultList;
        }

        /// <summary>
        /// Executes a mapped SQL SELECT statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns><A Generic List of result objects. /returns>
        public IList<T> QueryForList<T>(string statementName, object parameterObject)
        {
            IList<T> resultList = null;

            try
            {
                statementName = this.GetStatementName(statementName);
                resultList = this.Mapper.QueryForList<T>(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                string fullMassage = null;
                this.SetQueryErrorLog(statementName, ex);

                if (_isShowDetailException && ex.InnerException != null)
                {
                    fullMassage = DetailExceptionFormatter.GetInnerMassage(ex);
                    ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00000", DefaultMessage.NON_REG_WRD + fullMassage);
                }
                else
                {
                    //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
                    ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00116", null);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Executes a mapped SQL SELECT statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns><A List of result objects. /returns>
        public object QueryForObject(string statementName, object parameterObject)
        {
            object resultObject = null;

            try
            {
                statementName = this.GetStatementName(statementName);
                resultObject = this.Mapper.QueryForObject(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                string fullMassage = null;
                this.SetQueryErrorLog(statementName, ex);

                if (_isShowDetailException && ex.InnerException != null)
                {
                    fullMassage = DetailExceptionFormatter.GetInnerMassage(ex);
                    ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00000", DefaultMessage.NON_REG_WRD + fullMassage);
                }
                else
                {
                    //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
                    ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00116", null);
                }
            }
            return resultObject;
        }

        /// <summary>
        /// Executes a mapped SQL SELECT statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns><A Generic List of result objects. /returns>
        public T QueryForObject<T>(string statementName, object parameterObject)
        {
            T resultObject = default(T);

            try
            {
                statementName = this.GetStatementName(statementName);
                resultObject = this.Mapper.QueryForObject<T>(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                string fullMassage = null;
                this.SetQueryErrorLog(statementName, ex);

                if (_isShowDetailException && ex.InnerException != null)
                {
                    fullMassage = DetailExceptionFormatter.GetInnerMassage(ex);
                    ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00000", DefaultMessage.NON_REG_WRD + fullMassage);
                }
                else
                {
                    //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
                    ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00116", null);
                }
            }

            return resultObject;
        }

        /// <summary>
        /// Executes a mapped SQL PROCEDURE statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns><A List of result objects. /returns>
        public IList CallProcedureForList(string statementName, object parameterObject)
        {
            IList resultList = null;

            try
            {
                statementName = this.GetStatementName(statementName);
                resultList = this.Mapper.QueryForList(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                //MSG_FTCO_00192 : An error occurred during a database PROCEDURE CALL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00192", null);
            }
            return resultList;
        }

        /// <summary>
        /// Executes a mapped SQL RPOCEDURE statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns><A Generic List of result objects. /returns>
        public IList<T> CallProcedureForList<T>(string statementName, object parameterObject)
        {
            IList<T> resultList = null;

            try
            {
                statementName = this.GetStatementName(statementName);
                resultList = this.Mapper.QueryForList<T>(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                //MSG_FTCO_00192 : An error occurred during a database PROCEDURE CALL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00192", null);
            }

            return resultList;
        }

        /// <summary>
        /// Executes a mapped SQL PROCEDURE statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns><A List of result objects. /returns>
        public object CallProcedureForObject(string statementName, object parameterObject )
        {
            return CallProcedureForObject(statementName, parameterObject, false);
        }
        /// <summary>
        /// Executes a mapped SQL PROCEDURE statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <param name="isBindByName">The binding method in the parameter collection.</param>
        /// <returns><A List of result objects. /returns>
        public object CallProcedureForObject(string statementName, object parameterObject, bool isBindByName )
        {
            object resultObject = null;
            Procedure procedure = null;

            try
            {
                statementName = this.GetStatementName(statementName);
                if (this.AddBatch(statementName, parameterObject) == true)
                {
                    return resultObject;
                }

                if (isBindByName == true)
                {
                    IMappedStatement statement = this.Mapper.GetMappedStatement(statementName);

                    if (statement != null && statement.Statement is Procedure)
                    {
                        procedure = (Procedure)statement.Statement;
                        procedure.BindByName = true;
                    }
                }

                resultObject = this.Mapper.QueryForObject(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                //MSG_FTCO_00192 : An error occurred during a database PROCEDURE CALL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00192", null);
            }
            finally
            {
                if (procedure != null)
                {
                    procedure.BindByName = false;
                }
            }


            return resultObject;
        }

        /// <summary>
        /// Executes a mapped SQL PROCEDURE statement that returns data 
        /// to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object </param>
        /// <returns><A Generic List of result objects. /returns>
        public T CallProcedureForObject<T>(string statementName, object parameterObject)
        {
            T resultObject = default(T);

            try
            {
                statementName = this.GetStatementName(statementName);
                resultObject = this.Mapper.QueryForObject<T>(statementName, parameterObject);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                this.SetQueryErrorLog(statementName, ex);
                //MSG_FTCO_00192 : An error occurred during a database PROCEDURE CALL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00192", null);
            }

            return resultObject;
        }

        /// <summary>
        /// Executes a mapped SQL SELECT statement that returns datatable to populate a number of result objects. 
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object</param>
        /// <returns></returns>
        public DataTable QueryForDataTable(string statementName, object parameterObject)
        {
            DataTable dataTable = null;
            bool isOpenConnection = false;

            try
            {
                statementName = this.GetStatementName(statementName);
                dataTable = new System.Data.DataTable(statementName);

                if (this.Mapper.LocalSession == null)
                {
                    if (!Mapper.IsSessionStarted) Mapper.OpenConnection();
                    isOpenConnection = true;
                }

                ISqlMapSession session = this.Mapper.LocalSession;
                IBatisNet.DataMapper.MappedStatements.IMappedStatement statement = Mapper.GetMappedStatement(statementName);
                IBatisNet.DataMapper.Scope.RequestScope request = statement.Statement.Sql.GetRequestScope(statement, parameterObject, session);
                statement.PreparedCommand.Create(request, session, statement.Statement, parameterObject);

                using (request.IDbCommand)
                {
                    dataTable.Load(request.IDbCommand.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.GetType().ToString().Contains("Oracle.ManagedDataAccess") && ex.InnerException.InnerException.ToString().Contains("TNS:"))
                {
                    ExceptionHandler.Wrap(ex.InnerException, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00255", null);
                }
                else
                {
                    string fullMassage = null;
                    this.SetQueryErrorLog(statementName, ex);

                    if (_isShowDetailException && ex.InnerException != null)
                    {
                        fullMassage = DetailExceptionFormatter.GetInnerMassage(ex);
                        ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00000", DefaultMessage.NON_REG_WRD + fullMassage);
                    }
                    else
                    {
                        //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
                        ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00116", null);
                    }
                }
            }
            finally
            {
                if (isOpenConnection == true)
                {
                    Mapper.CloseConnection();
                }
            }

            return dataTable;
        }
        #endregion


        #region METHOD AREA (TRANSACTION HANDLING)**************

        /// <summary>
        /// Begin a transactional session. 
        /// </summary>
        /// <returns>SqlMapSession object reference</returns>
        public ISqlMapSession BeginTransaction()
        {
            ISqlMapSession session = null;

            try
            {
                session = this.Mapper.BeginTransaction();
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return session;
        }

        /// <summary>
        /// Commit a transactional session
        /// </summary>
        public void CommitTransaction()
        {
            this.Mapper.CommitTransaction();
            return;
        }

        /// <summary>
        /// Rollback a transactional session
        /// </summary>
        public void RollBackTransaction()
        {
            try
            {
                this.Mapper.RollBackTransaction();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return;
        }
        #endregion


        #region METHOD AREA (Connection HANDLING)**************

        /// <summary>
        /// Open Database Connection
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                this.Mapper.OpenConnection();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return;
        }

        /// <summary>
        /// Close Database Connection
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                this.Mapper.CloseConnection();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return;
        }

        /// <summary>
        /// Check whether connection is enable or not
        /// </summary>
        /// <returns>true, if connection is enable</returns>
        public bool CanConnect()
        {
            bool canConnect = false;
            ISqlMapSession mapSession = null;

            try
            {
                mapSession = this.Mapper.CreateSqlMapSession();
                mapSession.Connection.Open();
                mapSession.Connection.Close();

                SqlMapperHandler.ConnEnable = true;
                canConnect = true;
            }
            catch (Exception)
            {
                SqlMapperHandler.ConnEnable = false;
                canConnect = false;
            }
            finally
            {
                if (mapSession != null)
                {
                    if (mapSession.Connection.State == System.Data.ConnectionState.Open)
                    {
                        mapSession.Connection.Close();
                    }
                    mapSession.Dispose();
                }
            }
            return canConnect;
        }

        /// <summary>
        /// Raises the DatabaseAvailabilityChanged event.
        /// </summary>
        /// <param name="e">DatabaseAvailabilityChangedEventArgs</param>
        public void OnDatabaseAvailabilityChanged(DatabaseAvailabilityChangedEventArgs e)
        {
            try
            {
                if (DatabaseChange.DatabaseAvailabilityChanged != null)
                {
                    DatabaseChange.DatabaseAvailabilityChanged(this, e);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        public bool CheckConnection()
        {
            return this.Mapper.IsConnectionSuccess;
        }
        #endregion
        
        #region METHOD AREA (VALIDATION)************************

        /// <summary>
        /// Check DAO Configuration
        /// </summary>
        protected override void CheckDaoConfig()
        {
            if (this.SqlMapConfig == null)
            {
                throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00119", "WRD_FTCO_SqlMapperHandlersetting", "WRD_FTCO_Applicationcontextfile");
            }
        }
        #endregion

        #region METHOD(Change DataBase Connection String) AREA *
        /// <summary>
        /// Sets SqlMapper
        /// </summary>
        /// <param name="dbType"></param>
        private void SetConnectionDataBase(string dbType)
        {
            this.DBType = dbType;

            try
            {
                string sqlMapConfigPath = this.GetSqlMapConfigToDBType();

                if (!string.IsNullOrEmpty(sqlMapConfigPath))
                {
                    this.SqlMapConfig = sqlMapConfigPath;
                    SqlMapDaoSupport.GetMapperFromDic(this.SqlMapConfig);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Gets SqlMapConfig to DB Type
        /// </summary>
        /// <returns></returns>
        private string GetSqlMapConfigToDBType()
        {
            string sqlMapConfigPath = string.Empty;

            PersistenceInfo.DatabaseInfo databaseInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(this.DBType);

            if (databaseInfo != null &&
                !string.IsNullOrEmpty(databaseInfo.SqlMapConfig))
            {
                sqlMapConfigPath = PathUtil.Combine(AppPathInfo.PATH_ROOT_PERSISTENCE, databaseInfo.SqlMapConfig);
            }
            else
            {
                if (databaseInfo == null)
                {
                    //MSG_FTCO_00158 : DataBase key [{0}] does not exist. Please check {1}.
                    throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00158", DefaultMessage.NON_REG_WRD + this.DBType, DefaultMessage.NON_REG_WRD + AppPathInfo.FILE_NAME_PERSISTENCE_INFO);
                }
                else if (string.IsNullOrEmpty(databaseInfo.SqlMapConfig))
                {
                    //MSG_FTCO_00159 : A specified <{0}> value in the <{1}>  can not be null.
                    throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00159", DefaultMessage.NON_REG_WRD + PersistenceInfo.XML_ATT_VALUE_SQLMAPCONFIG, DefaultMessage.NON_REG_WRD + this.DBType);
                }
            }

            return sqlMapConfigPath;
        }
        #endregion

        #region METHOD(SetQueryErrorLog) AREA ******************
        /// <summary>
        /// Sets Query Error Log
        /// </summary>
        /// <param name="statementName"></param>
        /// <param name="errorMessage"></param>
        private void SetQueryErrorLog(string statementName, Exception ex)
        {
            this.SetDatabaseAvailability(ex);

            //MSG_FTCO_00170 : Statement ID : <{0}>, Message : '{1}'
            string errorMessage = ex.Message;
            string logMessage =
                MessageBuilder.BuildMessage("MSG_FTCO_00170", DefaultMessage.NON_REG_WRD + statementName, DefaultMessage.NON_REG_WRD + errorMessage);
            tsbLog.Error(logMessage);

            if (ex.InnerException != null)
            {
                tsbLog.Error(ex.InnerException);
            }
        }

        /// <summary>
        /// Sets Database Availability
        /// </summary>
        /// <param name="ex"></param>
        private void SetDatabaseAvailability(Exception ex)
        {
            PersistenceInfo.DatabaseInfo databaseInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(this.DBType);

            if ((databaseInfo != null) &&
                (databaseInfo.DBProduct == Tsb.Fontos.Core.Environments.Type.DBProductTypes.ORACLE))
            {
                if (ex.Message.StartsWith(DatabaseConstant.DB_CONNETION_ERROR_ID_ORACLE))
                {
                    this.OnDatabaseAvailabilityChanged(new DatabaseAvailabilityChangedEventArgs(false));
                }
            }
        }
        #endregion

        #region METHOD(Batch) AREA **************
        /// <summary>
        /// Starts a new batch in which update statements will be cached before being sent to the database all at once.
        /// This can improve overall performance of updates update when dealing with numerous updates (e.g. inserting 1:M related data).    
        /// </summary>
        public void StartBatch()
        {
            try
            {
                PersistenceInfo.DatabaseInfo databaseInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(this.DBType);

                bool isSupportedBatch = false;

                if ((databaseInfo != null) && (databaseInfo.DBProduct == Tsb.Fontos.Core.Environments.Type.DBProductTypes.ORACLE))
                {
                    isSupportedBatch = true;
                }
                else if ((databaseInfo != null) && (databaseInfo.DBProduct == Tsb.Fontos.Core.Environments.Type.DBProductTypes.MSSQL))
                {
                    isSupportedBatch = true;
                }

                if (isSupportedBatch == false)
                {
                    //MSG_FTCO_00201 : SQL Batch Job is not supported by DB provider.[Command Type:{1}]
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00200", DefaultMessage.NON_REG_WRD + databaseInfo.DBProduct);
                }

                if (_sqlBatchManager == null)
                {
                    _sqlBatchManager = new SqlBatchManager(this.DBType, databaseInfo.DBProduct);
                }
            }
            catch (Exception ex)
            {
                //MSG_FTCO_00201 : An error occurred during a database batch SQL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00201", null);
            }
        }

        /// <summary>
        /// Executes (flushes) all statements currently batched.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <returns>returns the number of rows updated in the batch</returns>
        public int ExecuteBatch()
        {
            return this.ExecuteBatch(-1);
        }
        /// <summary>
        /// Executes(flushes) statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <batchsize>The maximum  batch size</batchsize>
        /// <returns>returns the number of rows updated in the batch</returns>
        public int ExecuteBatch(int batchsize)
        {
            return this.ExecuteBatch(batchsize, BatchConstant.BATCH_COMMAND_TIMEOUT);
        }
        /// <summary>
        /// Executes(flushes) statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <batchsize>
        /// The maximum  batch size
        /// if you don't applied batch size, Sets the -1 value.
        /// </batchsize>
        /// <commandTimeout> The wait time before terminating the attempt to execute a command</commandTimeout>
        /// <returns>returns the number of rows updated in the batch</returns>
        public int ExecuteBatch(int batchsize, int commandTimeout)
        {
            int count = 0;

            try
            {
                if (_sqlBatchManager == null)
                {
                    return count;
                }

                if (commandTimeout < 0)
                {
                    commandTimeout = BatchConstant.BATCH_COMMAND_TIMEOUT;
                }

                count = _sqlBatchManager.ExecuteBatch(this.Mapper, batchsize, SqlStatementTypes.STATIC, commandTimeout);
            }
            catch (Exception ex)
            {
                //MSG_FTCO_00201 : An error occurred during a database batch SQL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00201", null);
            }
            finally
            {
                this.ClearBatch();
            }

            return count;
        }

        /// <summary>
        /// Executes (flushes) all dynamic statements currently batched.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <returns>returns the number of rows updated in the batch</returns>
        public int ExecuteDynamicBatch()
        {
            return this.ExecuteDynamicBatch(-1);
        }
         /// <summary>
        /// Executes(flushes) dynamic statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <batchsize>The maximum  batch size</batchsize>
        /// <returns>returns the number of rows updated in the batch</returns>
        public int ExecuteDynamicBatch(int batchsize)
        {
            return this.ExecuteDynamicBatch(batchsize, BatchConstant.BATCH_COMMAND_TIMEOUT);
        }
        /// <summary>
        /// Executes(flushes) dynamic statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <batchsize>
        /// The maximum  batch size
        /// if you don't applied batch size, Sets the -1 value.
        /// </batchsize>
        /// <commandTimeout> The wait time before terminating the attempt to execute a command</commandTimeout>
        /// <returns>returns the number of rows updated in the batch</returns>
        public int ExecuteDynamicBatch(int batchsize, int commandTimeout)
        {
            int count = 0;

            try
            {
                if (_sqlBatchManager == null)
                {
                    return count;
                }

                if (commandTimeout < 0)
                {
                    commandTimeout = BatchConstant.BATCH_COMMAND_TIMEOUT;
                }

                count = _sqlBatchManager.ExecuteBatch(this.Mapper, batchsize, SqlStatementTypes.DYNAMIC, commandTimeout);
            }
            catch (Exception ex)
            {
                //MSG_FTCO_00201 : An error occurred during a database batch SQL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00201", null);
            }
            finally
            {
                this.ClearBatch();
            }

            return count;
        }

        /// <summary>
        /// Adds the given SQL command to the current list of commmands for this Statement object.
        /// The commands in this list can be executed as a batch by calling the method executeBatch.
        /// </summary>
        /// <param name="statementName">The name of the statement to execute</param>
        /// <param name="parameterObject">The parameter object</param>
        /// <returns></returns>
        private bool AddBatch(string statementName, object parameterObject)
        {
            bool returnValue = false;
            try
            {
                if (_sqlBatchManager == null)
                {
                    return returnValue;
                }

                _sqlBatchManager.AddBatch(statementName, parameterObject);

                returnValue = true;
            }
            catch (Exception ex)
            {
                //MSG_FTCO_00201 : An error occurred during a database batch SQL execution.
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00201", null);
            }

            return returnValue;
        }
        /// <summary>
        /// Empties this Statement object's current list of SQL commands.
        /// </summary>
        private void ClearBatch()
        {
            _sqlBatchManager = null;
        }

        ///// <summary>
        ///// Executes batch of a mapped SQL INSERT/UPDATE statement
        ///// Use the BULK INSERT(UPDATE) sql statement to insert large volumes of records 
        ///// </summary>
        ///// <param name="statementName"></param>
        ///// <param name="parameterObject"></param>
        //public int ExecuteBatch(string statementName, object[] parameterObject)
        //{
        //    int count = 0;
        //    SqlBatchHandler sqlBulkHandler = null;
        //    try
        //    {
        //        sqlBulkHandler = new SqlBatchHandler(this.Mapper, statementName, parameterObject);
        //        count = sqlBulkHandler.ExecuteBatch();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.SetQueryErrorLog(statementName, ex);
        //        //MSG_FTCO_00116 : An error occurred during a database SELECT execution.
        //        ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_FTCO_00116", null);
        //    }
        //    finally
        //    {
        //        if (sqlBulkHandler != null)
        //        {
        //            sqlBulkHandler.Dispose();
        //        }
        //    }

        //    return count;
        //}
        #endregion

        #region METHOD(Statement Name) AREA **************
        /// <summary>
        /// Gets the a mapped SQL statement name.
        /// </summary>
        /// <param name="statementName"></param>
        /// <returns></returns>
        private string GetStatementName(string statementName)
        {
            string returnValue = statementName;
            try
            {
                if (_stmtNameHandler == null)
                {
                    _stmtNameHandler = new StatementNameHandler();
                }

                returnValue = _stmtNameHandler.GetStatementName(this.Mapper, this.DBType, statementName);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }

            return returnValue;
        }
        #endregion
    }
}
