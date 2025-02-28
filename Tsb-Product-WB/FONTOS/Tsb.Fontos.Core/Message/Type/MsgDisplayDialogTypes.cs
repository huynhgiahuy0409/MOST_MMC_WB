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
* 2009.06.17     CHOI   1.0	First release.
*
*/
#endregion

namespace Tsb.Fontos.Core.Message.Type
{
    /// <summary>
    /// Specifies the message display dialog type
    /// </summary>
    public enum MsgDialogTypes
    {
        /// <summary>
        /// Exception Report dialog
        /// </summary>
        ExceptionReport,

        /// <summary>
        /// General Message Box
        /// </summary>
        MessageBox,

        /// <summary>
        /// Legacy Message Box
        /// </summary>
        MessageBoxLegacy
    }
}