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
* 2009.06.22    CHOI 1.0	First release.
* 2010.04.28  Tonny Kim 1.1 Modify
* 
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Configuration.Common;

namespace Tsb.Fontos.Core.Item
{
    /// <summary>
    /// Represents Data Item
    /// </summary>
    public interface IDataItem
    {
        /// <summary>
        /// Gets or Sets GUID of item
        /// </summary>
        Guid GUID{get; set;}

        /// <summary>
        /// Gets or Sets OpCode of item
        /// </summary>
        OpCodes OpCode { get; set; }

        /// <summary>
        /// Gets or Sets Key string of item
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets or Sets whether the NotifyPropertyChanged method lock
        /// </summary>
        bool LockNotifyPropertyChanged { get; set; }

        /// <summary>
        /// Gets or Sets Item of Backup
        /// </summary>
        BaseDataItem BackupItem { get; set; }

        /// <summary>
        /// Make backup Item from current information
        /// </summary>
        void MakeBackupItem();
    }
}
