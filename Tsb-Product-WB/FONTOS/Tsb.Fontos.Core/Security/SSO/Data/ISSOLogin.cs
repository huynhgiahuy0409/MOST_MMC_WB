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

namespace Tsb.Fontos.Core.Security.SSO
{
    /// <summary>
    /// This interface is used by the FONTOS application to communicate with Single sign-on Agent.
    /// </summary>
    public interface ISSOLogin
    {
        /// <summary>
        /// Gets the user ID.
        /// </summary>
        /// <returns></returns>
        string GetUserID();
        /// <summary>
        /// Sets the user ID.
        /// </summary>
        /// <param name="userID"></param>
        void SetUserID(string userID);

        /// <summary>
        /// Gets the user password.
        /// </summary>
        /// <returns></returns>
        string GetUserPassword();
        /// <summary>
        /// Sets the user password.
        /// </summary>
        /// <param name="userPassword"></param>
        void SetUserPassword(string userPassword);
        /// <summary>
        /// Gets the value whether indicating the single sign-on function is enabled.
        /// </summary>
        /// <returns></returns>
        bool GetEnabled();
        /// <summary>
        /// Sets the value whether indicating the single sign-on function is enabled.
        /// </summary>
        /// <param name="enabled"></param>
        void SetEnabled(bool enabled);
    }
}
