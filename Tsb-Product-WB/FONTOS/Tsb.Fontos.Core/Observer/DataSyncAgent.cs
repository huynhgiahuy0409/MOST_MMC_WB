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
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Observer
{
    /// <summary>
    /// DataSync Agent class
    /// </summary>
    public class DataSyncAgent : IDisposable, IDataSyncAgent
    {
        #region FIELD AREA *************************************
        private Dictionary<string, DataSyncCallbackInfo> _dataSyncCallbackInfoDic;
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initilize DataSyncAgent Instance
        /// </summary>
        public DataSyncAgent()
        {
            this._dataSyncCallbackInfoDic = new Dictionary<string, DataSyncCallbackInfo>();
        }
        #endregion


        #region DESTRUCTOR AREA ********************************
        /// <summary>
        /// Destructor
        /// </summary>
        ~DataSyncAgent()
        {
            Dispose();
        }

        /// <summary>
        /// Dispose Method
        /// </summary>
        public void Dispose()
        {
            this.RemoveAllCallbackInfo();
            GC.SuppressFinalize(this);
            return;
        }
        #endregion


        #region METHOD AREA (Handling Callback Information Cache)
        /// <summary>
        /// Check existing of synchronization group ID  in cache
        /// </summary>
        /// <param name="syncGroupID">Synchronization group ID</param>
        /// <returns>true if a specified synchronization group ID exist, otherwise false</returns>
        private bool ExistSyncGroup(string syncGroupID)
        {
            return _dataSyncCallbackInfoDic.ContainsKey(syncGroupID);
        }

        /// <summary>
        /// Add data sync callback information to cache
        /// </summary>
        /// <param name="callbackInfo">Data synchronization callback information object referecne</param>
        public void AddCallbackInfo(DataSyncCallbackInfo callbackInfo)
        {
            try
            {
                if (this.ExistSyncGroup(callbackInfo.SyncGroupID))
                {
                    RemoveCallbackInfo(callbackInfo.SyncGroupID);
                }

                _dataSyncCallbackInfoDic.Add(callbackInfo.SyncGroupID, callbackInfo);

                DataSyncManager.GetInstance().RegisterDataSyncAgent(callbackInfo.SyncGroupID, this);

                callbackInfo.DataSyncNotified += callbackInfo.DataSyncNotifiedHandler;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }

        /// <summary>
        /// Add data sync callback information to cache
        /// </summary>
        /// <param name="syncGroupID">Syncronization group ID</param>
        /// <param name="dataSyncNotifiedHandler">Data Sync Notified Handler</param>
        public void AddCallbackInfo(string syncGroupID, DataSyncNotifiedHandler dataSyncNotifiedHandler)
        {
            this.AddCallbackInfo(new DataSyncCallbackInfo(syncGroupID,dataSyncNotifiedHandler));
            return;
        }

        /// <summary>
        /// Remove data sync callback information from cache
        /// </summary>
        /// <param name="syncGroupID">Synchronization group ID</param>
        public void RemoveCallbackInfo(string syncGroupID)
        {
            DataSyncCallbackInfo callbackInfo = null;

            try
            {
                if (this.ExistSyncGroup(syncGroupID))
                {
                    callbackInfo = this._dataSyncCallbackInfoDic[syncGroupID];

                    callbackInfo.DataSyncNotified -= callbackInfo.DataSyncNotifiedHandler;

                    DataSyncManager.GetInstance().UnRegisterDataSyncAgent(callbackInfo.SyncGroupID, this);

                    this._dataSyncCallbackInfoDic.Remove(syncGroupID);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            
            return;
        }

        /// <summary>
        /// Remove All of cached callback info
        /// </summary>
        public void RemoveAllCallbackInfo()
        {

            DataSyncCallbackInfo callbackInfo = null;

            try
            {
                foreach (string syncGroupID in this._dataSyncCallbackInfoDic.Keys)
                {
                    callbackInfo = this._dataSyncCallbackInfoDic[syncGroupID];

                    callbackInfo.DataSyncNotified -= callbackInfo.DataSyncNotifiedHandler;

                    DataSyncManager.GetInstance().UnRegisterDataSyncAgent(callbackInfo.SyncGroupID, this);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;

        }
        #endregion


        #region METHOD AREA (Notify Method)**********************
        /// <summary>
        /// Notify synchronization members to synchronize
        /// </summary>
        /// <param name="syncGroupID">Synchronization group ID</param>
        /// <param name="targetSyncData">Target data to sync</param>
        public void NotifyToSync(string syncGroupID, object targetSyncData)
        {
            this.NotifyToSync(syncGroupID, targetSyncData, null);
        }

        public void NotifyToSync(string syncGroupID, object targetSyncData, object receiveSyncData)
        {
            this.NotifyToSync(syncGroupID, targetSyncData, receiveSyncData, null);
        }

        /// <summary>
        /// Notify synchronization members to synchronize
        /// </summary>
        /// <param name="syncGroupID">Synchronization group ID</param>
        /// <param name="targetSyncData">Target data to sync</param>
        /// <param name="targetSyncData">Receive data to sync</param>
        public void NotifyToSync(string syncGroupID, object targetSyncData, object receiveSyncData, object addtionInfo)
        {
            try
            {
                if ((receiveSyncData != null) &&
                    (targetSyncData.GetType() == receiveSyncData.GetType()) &&
                    (targetSyncData is BaseDataItem))
                {
                    PropertyUtil.CopyAllSamePropertyValue(targetSyncData, receiveSyncData);
                }

                DataSyncManager.GetInstance().NotifyToSync(syncGroupID, targetSyncData, addtionInfo);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion


        #region METHOD AREA (Data Sync Method)*******************
        /// <summary>
        /// Synchronize target data
        /// </summary>
        /// <param name="syncGroupID">Synchronization group ID</param>
        /// <param name="targetSyncData">Target data to sync</param>
        /// <param name="addtionInfo">Addtion Info Object to sync</param>
        public void SyncData(string syncGroupID, object targetSyncData, object addtionInfo)
        {
            DataSyncCallbackInfo callbackInfo = null;

            try
            {
                if (ExistSyncGroup(syncGroupID))
                {
                    callbackInfo = _dataSyncCallbackInfoDic[syncGroupID];
                    callbackInfo.OnDataSyncNotified(targetSyncData, addtionInfo);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }
        #endregion

    }
}
