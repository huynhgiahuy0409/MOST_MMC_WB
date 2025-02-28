#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2014 TOTAL SOFT BANK LIMITED. All Rights
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
* 2014.04.30    Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.BgWorker;
using Tsb.Fontos.Core.BgWorker.Info;
using System.Linq.Expressions;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Data.IBatis
{
    public class SqlMapConfigHelper
    {
        /// <summary>
        /// Load sql map config by using asynchronous.
        /// </summary>
        public static void LoadSqlMapConfig()
        {
            SqlMapConfigLaodAgent sqlMapConfigLaodAgent = new SqlMapConfigLaodAgent();
            sqlMapConfigLaodAgent.LoadSqlMapConfig();
        }
    }

    internal class SqlMapConfigLaodAgent
    {
        public SqlMapConfigLaodAgent()
        {
        }

        /// <summary>
        /// Load sql map config by using asynchronous.
        /// </summary>
        public void LoadSqlMapConfig()
        {
            AsyncAgent asyncAgent = new AsyncAgent(null);
            asyncAgent.CallBackResult += new ResultEventHandler(asyncAgent_CallBackResult);
            asyncAgent.CallBackError += new FaultEventHandler(asyncAgent_CallBackError);
            Expression<Action<SqlMapConfigLaodAgent>> expression = c => c.DoLoadSqlMapConfig();
            
            BaseAsyncWorker asyncWorkInfo = new AsyncActionWorker<SqlMapConfigLaodAgent>("", this, expression);

            GeneralLogger.Info("Load sql map config [Start]");
            asyncAgent.RunWorkerAsync(asyncWorkInfo);
        }

        /// <summary>
        /// Load sql map config.
        /// </summary>
        private void DoLoadSqlMapConfig()
        {
            PersistenceInfo.DatabaseInfo databaseInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(ConfigConstant.SERVER_ROLE_MAIN_DATABASE);
            SqlMapDaoSupport tempMapDao = new SqlMapDaoSupport(PathUtil.Combine(AppPathInfo.PATH_ROOT_PERSISTENCE, databaseInfo.SqlMapConfig));
        }

        private void asyncAgent_CallBackError(object sender, BgWorker.Event.AsyncFaultEventArgs e)
        {
            try
            {
                GeneralLogger.Error(e.ToString());
                GeneralLogger.Error(e.Error);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

        }

        private void asyncAgent_CallBackResult(object sender, BgWorker.Event.AsyncResultEventArgs e)
        {
            try
            {
                GeneralLogger.Info("Load sql map config [End]");

                if(e.Cancelled == true)
                {
                    GeneralLogger.Error("SqlMapConfigLaodAgent job is cancelled");
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
    }
}
