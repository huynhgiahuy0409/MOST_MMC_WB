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
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Configuration.Common;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;

namespace Tsb.Fontos.Core.Monitoring.Database
{
    /// <summary>
    /// Database Monitoring Service
    /// </summary>
    public class DBMonitorService : BaseService, IDBMonitorService
    {
        #region FIELD/PROPERTY AREA*****************************
        /// <summary>
        /// Gets or Sets DB Monitoring DAO
        /// </summary>
        public IDBMonitorDao DBMonitorDao { get; set; }
        #endregion


        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initializes a new instance of the DBMonitorService class.
        /// </summary>
        public DBMonitorService() 
            : base()
        {
            this.ObjectID = "SVC-FT-FTCO-MON-DBMonitorService";
        }

        #endregion

        #region METHOD AREA (DB CONNECTION CHECK)***************

        /// <summary>
        /// Check DB Connection is enable or not using a specified connect string
        /// </summary>
        /// <param name="connString">connection string</param>
        /// <returns>true, if connection is enable</returns>
        public bool CheckConnectionEnable(string connString)
        {
            bool rtnConnEnable = false;
            try
            {
                rtnConnEnable = this.DBMonitorDao.CheckConnection(connString);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_FTCO_00120", null);
            }
            return rtnConnEnable;
        }
        #endregion

    }
}
