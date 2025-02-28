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
    /// Data Sync Notified Event Handler Delegator
    /// </summary>
    /// <param name="sender">Object that raised the event</param>
    /// <param name="e">Object of Event Arguments class that contains event data</param>
    public delegate void DataSyncNotifiedHandler(object sender, DataSyncNotifiedEventArgs e);


    /// <summary>
    /// Observer Notified Event Argument class
    /// </summary>
    public class DataSyncNotifiedEventArgs : EventArgs
    {
        #region FIELD AREA *************************************
        /// <summary>
        /// instance variable to store the notify data
        /// </summary>
        private object _targetDataToSync;

        /// <summary>
        /// Addition Info
        /// </summary>
        private object _additionInfo;
        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Gets Target data to sync
        /// </summary>
        public object TargetDataToSync 
        {
            get { return this._targetDataToSync; } 
        }

        /// <summary>
        /// Gets Target data to Addition Info
        /// </summary>
        public object AdditionInfo
        {
            get { return this._additionInfo; }
        }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// constructor that DataSyncNotifiedEventArgs
        /// </summary>
        /// <param name="targetDataToSync">Target Data to sync</param>
        public DataSyncNotifiedEventArgs(object targetDataToSync) 
            : this(targetDataToSync, null)
        {}

        /// <summary>
        /// constructor that DataSyncNotifiedEventArgs
        /// </summary>
        /// <param name="targetDataToSync">Target Data to sync</param>
        public DataSyncNotifiedEventArgs(object targetDataToSync, object addtionInfo)
        {
            this._targetDataToSync = targetDataToSync;
            this._additionInfo = addtionInfo;
        }
        #endregion
    }
}
