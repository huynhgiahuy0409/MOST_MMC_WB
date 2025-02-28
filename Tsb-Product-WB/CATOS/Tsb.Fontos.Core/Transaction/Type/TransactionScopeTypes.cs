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
    /// Specifies the scope transaction.
    /// </summary>
    public enum TransactionScopeTypes
    {
        /// <summary>
        /// The transaction scope must be associated with a transaction. If we are in a transaction scope join it. If we aren't, create a new one. 
        /// </summary>
        Required,
        
        /// <summary>
        /// Always creates a new transaction scope. 
        /// </summary>
        RequiresNew,

        /// <summary>
        /// Don't need a transaction scope, but if we are in a transaction scope then join it. 
        /// </summary>
        Support,

        /// <summary>
        /// Means that cannot cannot be associated with a transaction scope. 
        /// </summary>
        NotSupport
    }
}