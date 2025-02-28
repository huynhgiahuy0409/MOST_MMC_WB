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
    /// Base Password Change information Item class
    /// </summary>
    [Serializable]
    public class BasePwdChangeItem : BaseDataItem
    {
        #region FIELD/PROPERTY AREA*****************************
        /// <summary>
        /// Gets or Sets User ID
        /// </summary>
        public string userID { get; set; } 

        /// <summary>
        /// Gets or Sets Old Password
        /// </summary>
        public string OldPwd { get; set; } 

        /// <summary>
        /// Gets or Sets New Password
        /// </summary>
        public string NewPwd { get; set; } 

        /// <summary>
        /// Gets or Sets Confirm New Password
        /// </summary>
        public string ConfirmNewPwd { get; set; } 

        #endregion


        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BasePwdChangeItem()
        {
            this.ObjectID = "ITM-FT-FTCO-SEC-BasePwdChangeItem";
        }
        #endregion

    }
}
