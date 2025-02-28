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
* 2009.05.05    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Message;
using System.Collections.Specialized;
using System.Xml;
using Tsb.Fontos.Core.Xml;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Environments;

namespace Tsb.Fontos.Core.Data.IBatis
{
    /// <summary>
    /// IBatis Sql Mapper Handler class
    /// </summary>
    public class SqlMapperHandler : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************

        private static Dictionary<string, ISqlMapper> _sqlMapperDic = new Dictionary<string, ISqlMapper>();
        private static object _syncRoot = new Object();

        public static bool ConnEnable = false;

        private String _sqlMapConfig = null;
        /// <summary>
        /// Sets IBatis Sql map config path
        /// </summary>
        public string SqlMapConfig
        {
            get { return this._sqlMapConfig; }
            set { _sqlMapConfig = value; }
        }
        
        public static string _connString = null;
        /// <summary>
        /// Connection String
        /// </summary>
        public string ConnString
        {
            get { return _connString; }
            set { _connString = value; }
        }
        
        private ISqlMapper _mapper   = null;
        /// <summary>
        /// Gets IBatis Mapper object reference
        /// </summary>
        public ISqlMapper Mapper
        {
            get
            {
                DomSqlMapBuilder sqlMapBuilder = null;
                string mapConfig = null;
                string mapConfigFile = null;

                if (string.IsNullOrEmpty(this._sqlMapConfig))
                {
                    //MSG : The [{0}] property should not be null or empty.		
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00057", DefaultMessage.NON_REG_WRD + this.GetType().Name + "." + Mapper);
                }
                else
                {
                    if (FileUtil.Exists(this._sqlMapConfig))
                    {
                        mapConfigFile = this._sqlMapConfig;
                    }
                    else
                    {
                        mapConfigFile = PathUtil.Combine(AppPathInfo.PATH_APP_BASE, this.SqlMapConfig);

                        if (!FileUtil.Exists(mapConfigFile))
                        {
                            //MSG:[{0}] file does not exist. Please check this file (Path:{1})	
                            throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00126", DefaultMessage.NON_REG_WRD + "IBATIS SqlMapConfig", DefaultMessage.NON_REG_WRD + mapConfigFile);
                        }
                    }
                }

                if (_mapper == null)
                {
                    try
                    {
                        mapConfig = _sqlMapConfig.Replace('/', '\\');

                        if (SqlMapperHandler._sqlMapperDic.ContainsKey(mapConfig))
                        {
                            _mapper = SqlMapperHandler._sqlMapperDic[mapConfig];
                        }
                        else
                        {
                            lock (SqlMapperHandler._syncRoot)
                            {
                                XmlDocument xmlDocument = null;
                                XmlNodeList xmlNodelist = null;

                                sqlMapBuilder = new DomSqlMapBuilder();

                                xmlDocument = XmlUtil.GetXmlDocument(mapConfigFile);
                                xmlNodelist = xmlDocument.GetElementsByTagName("properties");
                                if (xmlNodelist == null || xmlNodelist.Count == 0)
                                {
                                    NameValueCollection properties = new NameValueCollection();
                                    properties.Add("connectionString", this.ConnString);
                                    sqlMapBuilder.Properties = properties;

                                    this._mapper = sqlMapBuilder.Configure(mapConfig);
                                }
                                else
                                {
                                    this._mapper = sqlMapBuilder.Configure(mapConfig);
                                }

                                SqlMapperHandler._sqlMapperDic.Add(mapConfig, _mapper);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MSG : The system encountered an error when configuring IBATIS Sql Mapper [Path: {0}].		
                        ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00058", DefaultMessage.NON_REG_WRD + this._sqlMapConfig);
                    }
                }
                return _mapper;
            }
        }
        #endregion


        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Initialize Instance
        /// </summary>
        public SqlMapperHandler() : base()
        {
            this.ObjectID = "GNR-FTCO-DAT-SqlMapperHandler";
        }
        
        /// <summary>
        /// Initialize Instance using a specified sql map config path string
        /// </summary>
        /// <param name="sqlMapConfigPath"></param>
        public SqlMapperHandler(string sqlMapConfigPath) : this()
        {
            this.SqlMapConfig = sqlMapConfigPath;
        }
        #endregion

    }
}
