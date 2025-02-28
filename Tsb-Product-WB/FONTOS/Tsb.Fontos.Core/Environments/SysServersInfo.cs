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
* 2010.02.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Information class of System Servers 
    /// </summary>
    public class SysServersInfo : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************
        private static volatile SysServersInfo _instance;
        private static object syncRoot = new Object();
        private static readonly string OBJECT_ID = "GNR-FTCO-ENV-SysServersInfo";

        private Dictionary<string, Server> _serversInfoDic = null;
        /// <summary>
        /// System servers info dictionary class. You can retrieve server information using server role string
        /// </summary>
        public Dictionary<string, Server> ServersInfoDic
        {
            get { return _serversInfoDic; }
            set { _serversInfoDic = value; }
        }
        #endregion


        #region INITIALIZATION AREA ****************************
         /// <summary>
        /// Default Constructor
        /// </summary>
        private SysServersInfo()
            : base()
        {
            this.ObjectID = OBJECT_ID;
            this.ObjectType = ObjectType.HELPER;
        }

        /// <summary>
        /// Returns a reference to the current SysServersInfo object for the application
        /// </summary>
        /// <returns>A reference to the SysServersInfo object</returns>
        public static SysServersInfo GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new SysServersInfo();
                        _instance.ServersInfoDic = new Dictionary<string, Server>();
                    }
                }
            }

            return _instance;
        }

        #endregion


        #region METHOD AREA (HADLING SERVER INFO DIC)***********

        /// <summary>
        /// Add a server information object to system server info dictionary
        /// </summary>
        /// <param name="serverRole">A server's role</param>
        /// <param name="serverInfo">A server information object reference</param>
        public static void AddServerInfoToDic(string serverRole, Server serverInfo)
        {
            SysServersInfo.GetInstance().ServersInfoDic.Add(serverRole, serverInfo);
            
            return;
        }

        /// <summary>
        /// Retrieve  a specified role server information object reference
        /// </summary>
        /// <param name="serverRole">A server's role</param>
        public static Server GetServerInfo(string serverRole)
        {
            return SysServersInfo.GetInstance().ServersInfoDic[serverRole];
        }

        #endregion
    }
}
