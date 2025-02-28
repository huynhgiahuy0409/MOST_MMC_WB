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
* 2010.02.05     CHOI       1.0	First release.
*  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Tsb.Fontos.Core.Security.Profile;

namespace Tsb.Fontos.Core.Security.Authentication
{
    /// <summary>
    /// Represents Base Login View
    /// </summary>
    public interface ILoginView
    {
        /// <summary>
        /// Gets or Sets Authentication Handler object reference
        /// </summary>
        IAuthenticationHandler AuthenHandler { get; set; }

        /// <summary>
        /// Show Login View
        /// </summary>
        /// <returns>BaseUserInfo object reference</returns>
        /// <param name="authenHandler">Authentication handler object reference</param>
        BaseUserInfo ShowLoginView(IAuthenticationHandler authenHandler);

        /// <summary>
        /// Show Login View
        /// </summary>
        /// <returns>BaseUserInfo object reference</returns>
        /// <param name="authenHandler">Authentication handler object reference</param>
        /// <param name="useMessageServer">Use Message Server</param>
        BaseUserInfo ShowLoginView(IAuthenticationHandler authenHandler, bool useMsgServer);

        /// <summary>
        /// Do login by external process.
        /// </summary>
        /// <param name="txtUserId"></param>
        /// <param name="txtPassword"></param>
        /// <returns></returns>
        bool DoLogin(string txtUserId, string txtPassword, bool doRetry);

        /// <summary>
        /// Show Password Change View with specified view icon, user id.
        /// </summary>
        void ShowPasswordView();

        /// <summary>
        /// Show Password Change View with specified view icon.
        /// </summary>
        /// <param name="viewIcon"></param>
        void ShowPasswordView(System.Drawing.Icon viewIcon);

        /// <summary>
        /// Show Password Change View.
        /// </summary>
        /// <param name="viewIcon"></param>
        /// <param name="initialUserID"></param>
        void ShowPasswordView(System.Drawing.Icon viewIcon, string initialUserID);
    }
}
