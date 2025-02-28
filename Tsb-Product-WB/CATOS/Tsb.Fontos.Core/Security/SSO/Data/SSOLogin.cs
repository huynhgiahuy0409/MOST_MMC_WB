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
    /// Implementation of ISSOLogin which provides user information
    /// </summary>
    public class SSOLogin : MarshalByRefObject, ISSOLogin
    {
        #region FIELDS AREA ************************************
        private static string _userID;
        private static string _userPassword;
        private static bool _enabled = true;
        #endregion


        #region CONSTRUCTOR AREA *********************************
        ///// <summary>
        ///// Default Constructor
        ///// </summary>
        //public SSOLogin()
        //{
        //}
        /// <summary>
        /// static initialize
        /// </summary>
        static SSOLogin()
        {
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Gets the user ID.
        /// </summary>
        /// <returns></returns>
        public string GetUserID()
        {
            if (this.GetEnabled() == false)
            {
                return string.Empty;
            }

            return _userID;
        }
        /// <summary>
        /// Sets the user ID.
        /// </summary>
        /// <param name="userID"></param>
        public void SetUserID(string userID)
        {
            _userID = userID;
        }
        /// <summary>
        /// Gets the user password.
        /// </summary>
        /// <returns></returns>
        public string GetUserPassword()
        {
            if (this.GetEnabled() == false)
            {
                return string.Empty;
            }

            return _userPassword;
        }
        /// <summary>
        /// Sets the user password.
        /// </summary>
        /// <param name="userPassword"></param>
        public void SetUserPassword(string userPassword)
        {
            _userPassword = userPassword;
        }
        /// <summary>
        /// Gets the value whether indicating the single sign-on function is enabled.
        /// </summary>
        /// <returns></returns>
        public bool GetEnabled()
        {
            return _enabled;
        }
        /// <summary>
        /// Sets the value whether indicating the single sign-on function is enabled.
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public void CheckIfAvailable()
        {
        }
        #endregion        
    }
}
