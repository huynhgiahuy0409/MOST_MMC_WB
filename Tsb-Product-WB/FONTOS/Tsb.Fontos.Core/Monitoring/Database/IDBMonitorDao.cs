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

namespace Tsb.Fontos.Core.Monitoring.Database
{
    /// <summary>
    /// Represents Database Monitoring Data Access Object
    /// </summary>
    public interface IDBMonitorDao
    {
        /// <summary>
        /// Returns true if connection is enable using a specified connection string
        /// </summary>
        /// <param name="connString">connection string</param>
        /// <returns>if connection is enable using a specified connection string</returns>
        bool CheckConnection(string connString);
    }
}
