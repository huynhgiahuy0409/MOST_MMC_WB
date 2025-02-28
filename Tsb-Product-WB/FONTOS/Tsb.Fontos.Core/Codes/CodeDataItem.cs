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
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Codes
{
    /// <summary>
    /// Code Data Item class
    /// </summary>
    [Serializable]
    public class CodeDataItem : BaseDataItem, ICodeDataItem
    {
        #region FIELD AREA *************************************
        #endregion


        #region PROPERTY AREA **********************************

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        public string Code {get; set;}

        /// <summary>
        /// Gets or Sets Code name
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        /// Gets or Sets Text Value. This property is used for only customized displaying
        /// (ex. concatenated CODE and CodeName with colon like TSB:Total Soft Bank)
        /// </summary>
        public string TextValue { get; set; }

        /// <summary>
        /// Gets or Sets Code type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets Code type name
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or Sets Default Check Y/N flag
        /// </summary>
        public string DefaultChkYN { get; set; }
        #endregion


        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Initialize instance
        /// </summary>
        public CodeDataItem()
        {
            this.ObjectID = "GNR_FTCO_ITM_CodeDataItem";
        }

        #endregion



    }
}
