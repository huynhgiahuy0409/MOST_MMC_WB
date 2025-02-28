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
* DATE           AUTHOR		       REVISION    	
* 2012.05.07  Tonny.Kim 1.0	    First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Util.Diagnostics.Type
{
    /// <summary>
    /// Performance destination Level Enumeration
    /// </summary>
    public enum PerformanceLevel
    {
        /// <summary>
        /// ALL
        /// </summary>
        All          = 1,
        /// <summary>
        /// Menu Toolbar
        /// </summary>
        MenuToolbar = 2,
        /// <summary>
        /// UI CONTROL
        /// </summary>
        UiConctrol   = 4,
        /// <summary>
        /// Business Service
        /// </summary>
        BizService  = 8,
        /// <summary>
        /// Data Access Object
        /// </summary>
        Dao         = 16
    }
}
