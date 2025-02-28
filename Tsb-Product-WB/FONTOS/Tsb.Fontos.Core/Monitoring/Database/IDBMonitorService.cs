#region Interface Definitions
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
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;

namespace Tsb.Fontos.Core.Monitoring.Database
{
    /// <summary>
    /// Represents Database Monitoring Service
    /// </summary>
    public interface IDBMonitorService : ITsbService
    {
        /// <summary>
        /// Gets or Sets DB Monitoring DAO
        /// </summary>
        IDBMonitorDao DBMonitorDao { get; set; }

        /// <summary>
        /// Check DB Connection is enable or not using a specified connect string
        /// </summary>
        /// <param name="connString">connection string</param>
        /// <returns>true, if connection is enable</returns>
        [TransactionOption(TransactionScopeTypes.NotSupport)]
        bool CheckConnectionEnable(string connString);
    }
}
