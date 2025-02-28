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
* 2009.08.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Security.Profile;
using System.Security.Principal;
using System.Security;
using Tsb.Fontos.Core.Security.Authorization;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Logined User Information class
    /// </summary>
    public class LoginedUserInfo : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************
        private static volatile LoginedUserInfo _instance;
        private static object syncRoot = new Object();
        private static readonly string OBJECT_ID = "GNR-FTCO-ENV-LoginedUserInfo";

        /// <summary>
        /// Current logined user info. This property will be set as logined user after user login
        /// </summary>
        public BaseUserInfo UserInfo { get; set; }

        /// <summary>
        /// Global application's operation(INQUIRY/UPDATE/DELETE/INSERT) authorization infomration item list
        /// </summary>
        public IList<AuthorInfoItem> OPAuthorInfoList { get; set; }

        /// <summary>
        /// The network domain name associated with the current user
        /// </summary>
        public static readonly string USER_DOMAIN_NAME = Environment.UserDomainName;

        /// <summary>
        /// The name of the person who is currently logged on to the Windows operating system.
        /// </summary>
        public static readonly string USER_WINLOGON_NAME = Environment.UserName;

        #endregion

        
        #region INITIALIZATION AREA ****************************
         /// <summary>
        /// Default Constructor
        /// </summary>
        private LoginedUserInfo()
            : base()
        {
            this.ObjectID = OBJECT_ID;
            this.ObjectType = ObjectType.HELPER;
        }

        /// <summary>
        /// Returns a reference to the current LoginedUserInfo object for the application
        /// </summary>
        /// <returns>A reference to the LoginedUserInfo object</returns>
        public static LoginedUserInfo GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new LoginedUserInfo();
                    }
                }
            }

            return _instance;
        }
        #endregion


        #region METHOD AREA (GET WINDOW IDENTITY)***************
        /// <summary>
        /// Returns Windows Identity
        /// </summary>
        /// <returns>WindowsIdentity object reference</returns>
        public static WindowsIdentity GetWindowsIdentity()
        {
            WindowsIdentity rtnWindowsIdentity = null;
            try
            {
                rtnWindowsIdentity = WindowsIdentity.GetCurrent();
            }
            catch (SecurityException)
            {
                rtnWindowsIdentity = null;
            }

            return rtnWindowsIdentity;
        }
        #endregion
    }
}
