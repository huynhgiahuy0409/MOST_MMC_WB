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
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Codes
{
    ///// <summary>
    ///// Code data item list class
    ///// </summary>
    //[Serializable]
    [Serializable]
	public class CodeDataItemList : BaseItemsList<CodeDataItem>
    {
        #region FIELD AREA *************************************
        private string _objectID = string.Empty;
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CodeDataItemList()
            : base()
        {
            this.ObjectID = "GNR_FTCO_ITM_CodeDataItemList";
        }

        /// <summary>
        /// Initilize instance using a specified list
        /// </summary>
        /// <param name="list"></param>
        public CodeDataItemList(IList<CodeDataItem> list)
            : base(list)
        {
            this.ObjectID = "GNR_FTCO_ITM_CodeDataItemList";
        }
        #endregion
    }
}	