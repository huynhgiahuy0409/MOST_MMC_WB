#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2015 TOTAL SOFT BANK LIMITED. All Rights
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
* 2015.07.08    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Security.Authentication
{
    /// <summary>
    /// Item class of license check Information for object
    /// </summary>
    [Serializable]
    public class SiteItem : BaseDataItem
    {
        #region FIELD/PROPERTY AREA*****************************
        /// <summary>
        /// Gets or sets the ternmal code.
        /// </summary>
        public String TerminalCode { get; set; }
        /// <summary>
        /// Gets or sets the system date.
        /// </summary>
        public DateTime SysDateTime { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        public SiteItem()
            : base()
        {
            this.ObjectID = "ITM-FTCO-SEC-LicenseCheckItem";
        }
        #endregion
    }
}
