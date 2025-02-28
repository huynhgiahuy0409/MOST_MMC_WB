#region Enumveration Definitions
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
* 2009.06.26    CHOI 1.0	First release.
* 
*/
#endregion
namespace Tsb.Fontos.Core.Transaction
{
    /// <summary>
    /// Instructs the operation result type
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// The state before the transaction
        /// </summary>
        BEFORE_TX,
        
        /// <summary>
        /// Operation is OK.
        /// </summary>
        OK,

        /// <summary>
        /// Operation failed.
        /// </summary>
        ERROR,
        
        /// <summary>
        /// Operation is OK but additional information should be notified to user
        /// </summary>
        INFO,

        /// <summary>
        /// Warning operation.
        /// </summary>
        WARN
    }
}
