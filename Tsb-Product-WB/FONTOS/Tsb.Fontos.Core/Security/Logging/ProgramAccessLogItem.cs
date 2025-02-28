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
* 2020.10.28    LIM JC 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Security.Logging
{
    [Serializable]
    public class ProgramAccessLogItem : BaseDataItem
    {
        /// <summary>
        /// Gets or Sets program code
        /// </summary>
        public string PgmCode { get; set; }

        /// <summary>
        /// Gets or Sets Staff's IP Address
        /// </summary>
        public string IpAddress { get; set; }
        #region INITIALIZATION AREA ****************************
        public ProgramAccessLogItem()
            : base()
        {
            this.ObjectID = "ITM-FTCO-SEC-ProgramAccessLogItem";
        }
        #endregion
    }
}
