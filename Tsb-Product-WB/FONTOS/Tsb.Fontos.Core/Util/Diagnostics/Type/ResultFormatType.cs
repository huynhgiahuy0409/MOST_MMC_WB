#region Enum Definitions
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
* 2011.03.22    CHOI 1.0	First release.
* 
*/
#endregion


namespace Tsb.Fontos.Core.Util.Diagnostics.Type
{
    /// <summary>
    /// Output result format type Enumeration
    /// </summary>
    public enum ResultFormatType
    {
        /// <summary>
        /// Output result is displayed with tick count
        /// </summary>
        TICK,

        /// <summary>
        /// Output result is displayed with elased milli-second
        /// </summary>
        MILLI_SECOND,

        /// <summary>
        /// Output result is displayed with date time format
        /// </summary>
        DATETIME,
    }
}
