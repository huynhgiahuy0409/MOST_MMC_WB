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
* 2010.02.03    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Security.Encryption;
using Tsb.Fontos.Core.Monitoring.Database;
namespace Tsb.Fontos.Core.Security.Authentication
{
    /// <summary>
    /// Represents Authentication Handler
    /// </summary>
    public interface IAuthenticationHandler
    {
        /// <summary>
        ///  Gets or Sets Password Encrypter
        /// </summary>
        IBaseEncrypter PasswordEncrypter { get; set; }

        /// <summary>
        ///  Gets or Sets Security Service Implements
        /// </summary>
        ISecurityService SecurityService { get; set; }

        /// <summary>
        ///  Gets or Sets Previously Logined User ID
        /// </summary>
        string PrevLoginUserID { get; set; }

        /// <summary>
        /// Gets or Sets whether user is expried or not
        /// </summary>
        bool DoExpireCheck { get; set; }

        /// <summary>
        /// Gets or Sets whether password is changeable on Login View
        /// </summary>
        bool CanChangePassword { get; set; }

        /// <summary>
        /// Authenticates and returns true if authentication is successful
        /// </summary>
        /// <returns>true if authentication is successful</returns>
        bool SetAuthenHandler(string userId);

        /// <summary>
        /// Authenticates and returns true if authentication is successful
        /// </summary>
        /// <returns>true if authentication is successful</returns>
        bool SetAuthenHandler(string userId, string password);

        /// <summary>
        /// Authenticates and returns true if authentication is successful
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="isPwExpiredCheck"></param>
        /// <returns>true if authentication is successful</returns>
        bool SetAuthenHandler(string userId, string password, bool isPwExpiredCheck);

        /// <summary>
        /// Authenticates and returns true if authentication is successful
        /// </summary>
        /// <param name="param"></param>
        /// <param name="isPwExpiredCheck"></param>
        /// <returns>true if authentication is successful</returns>
        bool SetAuthenHandler(BaseSecurityParam param, bool isPwExpiredCheck);
    }
}
