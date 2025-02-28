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
* 2009.07.10    Jindols 1.0	First release.
* 2009.10.15    CHOI        Refactoring
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
    /// Data Sync Callback Information class
    /// </summary>
    public class DataSyncCallbackInfo
    {
        #region FIELD AREA *************************************
        private string _syncGroupID = null;
        private DataSyncNotifiedHandler _dataSyncNotifiedHandler = null;
        #endregion


        #region EVENT DECLARATION AREA**************************
        /// <summary>
        /// Occure Data Sync Manager notify topic to subscriber
        /// </summary>
        public event DataSyncNotifiedHandler DataSyncNotified;
        #endregion


        #region PROPERTY AREA **********************************
        
        /// <summary>
        /// Gets or Sets a topic Interesting Group ID
        /// </summary>
        public string SyncGroupID
        {
            get { return _syncGroupID; }
            set { _syncGroupID = value; }
        }

        /// <summary>
        /// Gets or Sets Data Sync Notified Handler
        /// </summary>
        public DataSyncNotifiedHandler DataSyncNotifiedHandler
        {
            get { return _dataSyncNotifiedHandler; }
            set { _dataSyncNotifiedHandler = value; }
        }

        #endregion


        #region INSTNACE METHOD AREA (Initialize) **************
        /// <summary>
        /// Initilaize instance using specified topic group id and data sync notified handler
        /// </summary>
        /// <param name="syncGroupID">Syncronization group ID</param>
        /// <param name="dataSyncNotifiedHandler">Data Sync Notified Handler</param>
        public DataSyncCallbackInfo(string syncGroupID, DataSyncNotifiedHandler dataSyncNotifiedHandler)
        {
            this.SyncGroupID = syncGroupID;
            this.DataSyncNotifiedHandler = dataSyncNotifiedHandler;
        }
        #endregion


        #region EVENT HANDLER AREA *****************************
        /// <summary>
        /// DataSyncNotified Event Handler
        /// </summary>
        /// <param name="targetDataToSync">Target Object to sync</param>
        /// <param name="addtionInfo">Addtion Info Object to sync</param>
        public void OnDataSyncNotified(object targetDataToSync, object addtionInfo)
        {
            this.DataSyncNotified(this, new DataSyncNotifiedEventArgs(targetDataToSync, addtionInfo));
        }
        #endregion
    }

}
