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
* DATE           AUTHOR		      REVISION    	
* 2012.04.26  Tonny.Kim 1.0   	First Release
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Data.Information
{
    public class DatabaseChange
    {
        #region PROPERTY AREA **********************************
        public static DatabaseAvailabilityChangedEventHandler DatabaseAvailabilityChanged { get; set; }
        #endregion
    }
}
