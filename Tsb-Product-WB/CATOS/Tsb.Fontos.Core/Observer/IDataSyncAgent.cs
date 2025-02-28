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
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Observer
{
    /// <summary>
    /// Represents Data Sync Agent
    /// </summary>
    public interface IDataSyncAgent
    {
        /// <summary>
        /// SyncData to its subscribers
        /// </summary>
        /// <param name="subscriberGroupID">Subscriber group ID</param>
        /// <param name="anObject"></param>
        /// <param name="addtionInfo">Addtion Info Object to sync</param>
        void SyncData(string subscriberGroupID, object anObject, object addtionInfo);
    }
}
