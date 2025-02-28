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
* 2009.09.05    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Codes.Type;

namespace Tsb.Fontos.Core.Codes
{
    /// <summary>
    /// Code Item parameter class
    /// </summary>
    [Serializable]
    public class CodeDataParam : BaseParam
    {
        #region PROPERTY AREA **********************************

        /// <summary>
        /// Gets or Sets CodeGroupType Type of code data
        /// </summary>
        public CodeGroupTypes CodeGroupType { get; set; }

        /// <summary>
        /// Gets or Sets Type of code data
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets Type Name of code data
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or Sets Key string of code data
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// General arguments array to use general purpose (like query option)
        /// </summary>
        public string[] Args { get; set; }

        /// <summary>
        /// Gets or Sets Previous Value (Displayed Value)
        /// </summary>
        public string PrevCodeValue { get; set; }

        /// <summary>
        /// Gets or Sets Previous Data (original data)
        /// </summary>
        public string PrevCodeData { get; set; }

        #endregion


        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Initialize instance with a specified parameter owner(sender) reference
        /// </summary>
        /// <param name="paramOwner">Parameter Owner(Creator or sender)</param>
        public CodeDataParam(object paramOwner)
            : base(paramOwner)
        {
            this.ObjectID = "GNR_FTCO_ITM_CodeDataParam";
            this.CodeGroupType = default(CodeGroupTypes);
            this.Type = null;
            this.Key = null;
            this.Args = null;
            this.PrevCodeValue = null;
            this.PrevCodeData = null;
        }

        /// <summary>
        /// Initialize instance with specified transaction ID and parameter owner(sender) reference
        /// </summary>
        /// <param name="paramOwner">Parameter Owner(Creator or sender)</param>
        /// <param name="txServiceID">Transaction ID</param>
        public CodeDataParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            this.ObjectID = "GNR_FTCO_ITM_CodeDataParam";
            this.CodeGroupType = default(CodeGroupTypes);
            this.Type = null;
            this.Key = null;
            this.Args = null;
            this.PrevCodeValue = null;
            this.PrevCodeData = null;
        }
        #endregion
    }
}
