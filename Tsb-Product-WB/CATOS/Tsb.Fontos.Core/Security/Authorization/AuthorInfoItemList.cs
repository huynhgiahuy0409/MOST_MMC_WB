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
* 2010.02.06     CHOI       1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using System.ComponentModel;
using System.Collections;

namespace Tsb.Fontos.Core.Security.Authorization
{
    /// <summary>
    /// Authorization Information Item List class
    /// </summary>
    [Serializable]
    public class AuthorInfoItemList : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************
        public IList<AuthorInfoItem> RawList { get; set; }
        #endregion
        
        
        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default constructor
        /// </summary>
        public AuthorInfoItemList()
            : base()
        {
            this.ObjectID = "ITM-FT-FTCO-SEC-AuthorInfoItemList";
            this.ObjectType = ObjectType.ITEM;
        }
        #endregion
    }
}
