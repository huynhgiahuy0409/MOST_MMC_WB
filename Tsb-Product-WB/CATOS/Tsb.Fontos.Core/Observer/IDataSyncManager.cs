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
* 2009.07.10    Jindols 1.0	First release.
* 2009.10.15    CHOI        Refactoring
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Observer
{
    /// <summary>
    /// Represents DataSyncManager
    /// </summary>
    public interface IDataSyncManager
    {
        /// <summary>
        /// Register DataSyncAgent to topic group
        /// </summary>
        /// <param name="topicGroupID">Topic group ID</param>
        /// <param name="dataSyncAgent">Data Sync Agent to register</param>
        void RegisterDataSyncAgent(string topicGroupID, IDataSyncAgent dataSyncAgent);
        
        /// <summary>
        /// Un-Register DataSyncAgent to topic group
        /// </summary>
        /// <param name="topicGroupID">Topic group ID</param>
        /// <param name="dataSyncAgent">Data Sync Agent to un-register</param>
        void UnRegisterDataSyncAgent(string topicGroupID, IDataSyncAgent dataSyncAgent);
    }
}
