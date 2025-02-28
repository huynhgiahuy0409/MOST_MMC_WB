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
* 2010.02.05     CHOI       1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using System.Collections.Specialized;
using IBatisNet.DataMapper;


namespace Tsb.Fontos.Core.Monitoring.Database
{
    /// <summary>
    /// Databae Monitoring Data access object
    /// </summary>
    public class DBMonitorDao : SqlMapDaoSupport, IDBMonitorDao
    {
        private static object syncRoot = new object();
        
        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DBMonitorDao()
            : base()
        {
            this.ObjectID = "DAO-FT-FTCO-MON-DBMonitorDao";
        }

        /// <summary>
        /// Initialize Instance using sql map config file 
        /// </summary>
        public DBMonitorDao(string sqlMapConfigPath)
            : base(sqlMapConfigPath)
        {
            this.ObjectID = "DAO-FT-FTCO-MON-DBMonitorDao";
        }
        #endregion


        #region METHOD AREA (DB CONNECTION CHECK)***************
        /// <summary>
        /// Returns true if connection is enable using a specified connection string
        /// </summary>
        /// <param name="connString">connection string</param>
        /// <returns>if connection is enable using a specified connection string</returns>
        public bool CheckConnection(string connString)
        {
            bool canConnect = false;

            try
            {
                #region Backup(By jindols) ===============================
                //lock (DBMonitorDao.syncRoot)
                //{
                //    if (SqlMapDaoSupport.MapConnStrKeyValueDic.ContainsKey(this.SqlMapConfig))
                //    {
                //        if (SqlMapDaoSupport.MapConnStrKeyValueDic[this.SqlMapConfig].Equals(connString) == false)
                //        {
                //            SqlMapDaoSupport.MapConnStrKeyValueDic[this.SqlMapConfig] = connString;
                //        }
                //    }
                //    else
                //    {
                //        SqlMapDaoSupport.MapConnStrKeyValueDic.Add(this.SqlMapConfig, connString);
                //    }
                //}
                //this.Mapper.DataSource.ConnectionString = connString;
                //canConnect = this.CanConnect();
                #endregion

                canConnect = this.Mapper.IsConnectionSuccess;
            }
            catch (Exception)
            {
                canConnect = false;
            }
            return canConnect;
        }
        #endregion


    }
}
