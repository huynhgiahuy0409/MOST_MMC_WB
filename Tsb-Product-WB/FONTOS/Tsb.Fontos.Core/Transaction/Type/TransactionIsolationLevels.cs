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

namespace Tsb.Fontos.Core.Transaction.Type
{
    /// <summary>
    /// Specifies the isolation level of a transaction. 
    /// </summary>
    public enum TransactionIsolationLevels
    {
        /// <summary>
        /// Volatile data can be read but not modified, and no new data can be added during the transaction.  
        /// </summary>
        Serializable,
 
        /// <summary>
        /// Volatile data can be read but not modified during the transaction. New data may be added during the transaction.  
        /// </summary>
        RepeatableRead,

        /// <summary>
        /// Volatile data cannot be read during the transaction, but can be modified.  
        /// </summary>
        ReadCommitted,
 
        /// <summary>
        /// Volatile data can be read and modified during the transaction.  
        /// </summary>
        ReadUncommitted, 

        /// <summary>
        /// Volatile data can be read but not modified, and no new data can be added during the transaction.  
        /// </summary>
        Unspecified 

    }
}