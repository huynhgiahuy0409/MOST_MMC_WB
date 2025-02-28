#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2013 TOTAL SOFT BANK LIMITED. All Rights
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
* 2013.11.25   Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Security.SSO.IPC;
using Microsoft.Win32;

namespace Tsb.Fontos.Core.Security.SSO
{
    public class SSOClientProxy
    {
        #region CONSTRUCTOR AREA *********************************
        public SSOClientProxy()
        {
        }
        #endregion

        #region METHOD AREA **************************************
        private bool GetEnabled(RegistryKey localRkeyParm)
        {
            RegistryKey localRkey = localRkeyParm;

            if(localRkey == null)
            {
                localRkey = Registry.CurrentUser.OpenSubKey(SSOConstant.PGM_USER_LOC, true);
            }

            object enableObj = localRkey.GetValue(SSOConstant.AGENT_ENABLED);
            bool enable = true;

            if (enableObj == null || Boolean.TryParse(enableObj.ToString(), out enable) == false)
            {
                enable = IsSSOAgentStarted();
            }

            return enable;
        }

        public string GetRegisteredUserInfo(string info)
        {
            RegistryKey localRkey = Registry.CurrentUser.OpenSubKey(SSOConstant.PGM_USER_LOC, true);

            if (localRkey == null || localRkey.GetValue(info) == null || this.GetEnabled(localRkey) == false) return string.Empty;

            return localRkey.GetValue(info).ToString();
        }

        /// <summary>
        /// Indicates whether the single sign-on agent was started.
        /// </summary>
        /// <returns>true if it was started, otherwise false.</returns>
        public bool IsSSOAgentStarted()
        {
            return SSOAppUtil.IsSSOAgentStarted();
        }

        /// <summary>
        /// Run the single sign-on agent.
        /// if it is already running, not run the new single sign-on agent.
        /// </summary>
        public void RunSSOAgent()
        {
            SSOAppUtil.RunSSOAgent();
        }

        /// <summary>
        /// Sets the user ID.
        /// </summary>
        /// <param name="userID"></param>
        public void SetUserID(string userID)
        {
            Registry.CurrentUser.CreateSubKey(SSOConstant.PGM_USER_LOC);
            RegistryKey localRkey = Registry.CurrentUser.OpenSubKey(SSOConstant.PGM_USER_LOC, true);

            if (this.GetEnabled(localRkey))
            {
                localRkey.SetValue(SSOConstant.USER_ID, userID); 
            }
        }

        /// <summary>
        /// Sets the user password.
        /// </summary>
        /// <param name="userPassword"></param>
        public void SetUserPassword(string userPassword)
        {
            Registry.CurrentUser.CreateSubKey(SSOConstant.PGM_USER_LOC);
            RegistryKey localRkey = Registry.CurrentUser.OpenSubKey(SSOConstant.PGM_USER_LOC, true);

            if (this.GetEnabled(localRkey))
            {
                localRkey.SetValue(SSOConstant.USER_PASSWORD, userPassword);
            }
        }

        /// <summary>
        /// Gets the value whether indicating the single sign-on function is enabled.
        /// </summary>
        /// <returns></returns>
        public bool GetEnabled()
        {
            return IPCClient.GetInstance().GetEnabled();
        }

        /// <summary>
        /// Sets the value whether indicating the single sign-on function is enabled.
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            IPCClient.GetInstance().SetEnabled(enabled);
        }
        #endregion
    }
}
