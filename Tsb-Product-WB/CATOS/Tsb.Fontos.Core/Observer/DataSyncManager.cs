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
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Observer
{
    /// <summary>
    /// Data Sync Manager Class
    /// </summary>
    public class DataSyncManager : IDataSyncManager
    {
        #region FIELD AREA *************************************
        private static DataSyncManager _dataSyncManager = null;
        private Dictionary<string, List<IDataSyncAgent>> _dataSyncAgentDic = null;
        private Dictionary<string, object> _targetSyncDataDic = null;
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initilaize Instance
        /// </summary>
        private DataSyncManager()
        {
            this._dataSyncAgentDic = new Dictionary<string, List<IDataSyncAgent>>();
            this._targetSyncDataDic = new Dictionary<string, object>();
        }

        /// <summary>
        /// Returns Sigleton DataSyncManager Instance
        /// </summary>
        public static DataSyncManager GetInstance()
        {
            if (DataSyncManager._dataSyncManager == null)
            {
                _dataSyncManager = new DataSyncManager();
            }

            return _dataSyncManager;
        }
        #endregion

        
        #region METHOD AREA (DATA SYNC AGENT HANDLING)**********
        /// <summary>
        /// Check data sync agent exist or not using a specified Syncronization group ID
        /// </summary>
        /// <param name="syncGroupID">Syncronization group ID</param>
        /// <returns>true if synchronization group data sync agent exist otherwise false</returns>
        private bool ExistDataSyncAgent(string syncGroupID)
        {
            return this._dataSyncAgentDic.ContainsKey(syncGroupID);
        }


        /// <summary>
        /// Register DataSyncAgent to a specified synchronization group
        /// </summary>
        /// <param name="syncGroupID">Syncronization group ID</param>
        /// <param name="dataSyncAgent">Data Sync Agent to register</param>
        public void RegisterDataSyncAgent(string syncGroupID, IDataSyncAgent dataSyncAgent)
        {
            List<IDataSyncAgent> list = this.GetDataSyncAgentList(syncGroupID);

            if (!list.Contains(dataSyncAgent))
            {
                list.Add(dataSyncAgent);
            }

            return;
        }

        /// <summary>
        /// Un-Register DataSyncAgent from a specified synchronization group
        /// </summary>
        /// <param name="syncGroupID">Syncronization group ID</param>
        /// <param name="dataSyncAgent">Data Sync Agent to un-register</param>
        public void UnRegisterDataSyncAgent(string syncGroupID, IDataSyncAgent dataSyncAgent)
        {
            try
            {
                if (this.ExistDataSyncAgent(syncGroupID))
                {
                    this.GetDataSyncAgentList(syncGroupID).Remove(dataSyncAgent);

                    if (this.GetDataSyncAgentList(syncGroupID).Count == 0)
                    {
                        _dataSyncAgentDic.Remove(syncGroupID);
                        _targetSyncDataDic.Remove(syncGroupID);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }

        /// <summary>
        /// Gets Data sync agents list for a specified synchronization group
        /// </summary>
        /// <param name="syncGroupID">Syncronization group ID</param>
        /// <returns>DataSyncAgent objects list for a specified synchronization group</returns>
        private List<IDataSyncAgent> GetDataSyncAgentList(string syncGroupID)
        {
            List<IDataSyncAgent> list = null;

            try
            {
                if (this._dataSyncAgentDic.ContainsKey(syncGroupID) == false)
                {
                    list = new List<IDataSyncAgent>();
                    _dataSyncAgentDic.Add(syncGroupID, list);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return _dataSyncAgentDic[syncGroupID];
        }
        #endregion


        #region METHOD AREA (TARGET SYNC DATA HANDLING)*********
        /// <summary>
        /// Check Taget sync data exist in cache using a specified Syncronization group ID
        /// </summary>
        /// <param name="syncGroupID">Syncronization group ID</param>
        /// <returns>true if target sync data exist in cache otherwise false</returns>
        private bool ExistTargetSyncDataInCache(string syncGroupID)
        {
            return _targetSyncDataDic.ContainsKey(syncGroupID);
        }


        /// <summary>
        /// Returns Target Sync Data for a specified synchronization group
        /// </summary>
        /// <param name="syncGroupID">Syncronization group ID</param>
        /// <returns>Target sync data for a specified synchronization group</returns>
        public object GetTargetSyncData(string syncGroupID)
        {
            if (this._targetSyncDataDic.ContainsKey(syncGroupID))
            {
                return this._targetSyncDataDic[syncGroupID];
            }

            return null;
        }
        #endregion

        
        #region METHOD AREA (NOTIFY DATA SYNC MEMBERS TO SYNC)**
        /// <summary>
        /// Notify synchronization members to synchroize target data
        /// </summary>
        /// <param name="syncGroupID">Synchronization group ID</param>
        /// <param name="targetSyncData">Target data to sync</param>
        /// <param name="addtionInfo">Addtion Info Object to sync</param>
        public void NotifyToSync(string syncGroupID, object targetSyncData, object addtionInfo)
        {
            List<IDataSyncAgent> dataSyncAgentList = null;

            try
            {
                if (this.ExistDataSyncAgent(syncGroupID))
                {
                    if (this.ExistTargetSyncDataInCache(syncGroupID))
                    {
                        this._targetSyncDataDic.Remove(syncGroupID);
                    }

                    this._targetSyncDataDic.Add(syncGroupID, targetSyncData);

                    dataSyncAgentList = GetDataSyncAgentList(syncGroupID);

                    foreach (IDataSyncAgent dataSyncAgent in dataSyncAgentList)
                    {
                        dataSyncAgent.SyncData(syncGroupID, targetSyncData, addtionInfo);
                    }
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

