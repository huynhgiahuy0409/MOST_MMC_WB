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
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;
using System.ComponentModel;
using Tsb.Fontos.Core.Util.Type;

namespace Tsb.Fontos.Core.Security.Authentication
{
    /// <summary>
    /// Base User Expiration Policy information Item class
    /// </summary>
    [Serializable]
    public class BaseExpirePolicyItem : BaseDataItem
    {
        #region FIELD/PROPERTY AREA*****************************
        private int _expireLogin;
        /// <summary>
        /// Gets or Sets limit days of no-login
        /// </summary>
        public int ExpireLogin
        {
            get { return this._expireLogin; }
            set { _expireLogin = value; }
        }

        private int _expireTerm;
        /// <summary>
        /// Gets or Sets limit days of password change
        /// </summary>
        public int ExpireTerm
        {
          get { return _expireTerm; }
          set { _expireTerm = value; }
        }


        private int _expireRetry;
        /// <summary>
        /// Gets or Sets limit count of retry login with a wrong password
        /// </summary>
        public int ExpireRetry
        {
          get { return _expireRetry; }
          set { _expireRetry = value; }
        }

        private int _warningTerm;
        /// <summary>
        /// Gets or Sets password expiration remaining days to pre-notify (default value is 7)
        /// </summary>
        public int WarningTerm
        {
          get { return _warningTerm; }
          set { _warningTerm = value; }
        }

        private int _autoLogoutInterval;
        /// <summary>
        /// Gets or Sets auto logout interval.
        /// </summary>
        public int AutoLogoutInterval
        {
            get { return _autoLogoutInterval; }
            set { _autoLogoutInterval = value; }
        }
        #endregion


        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseExpirePolicyItem()
        {
            this.ObjectID = "ITM-FT-FTCO-SEC-BaseExpirePolicyItem";
        }
        #endregion

    }
}
