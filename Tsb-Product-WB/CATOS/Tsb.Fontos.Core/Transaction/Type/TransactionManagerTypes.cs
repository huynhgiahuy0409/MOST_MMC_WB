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
    /// Specifies the Transaction Manager type
    /// </summary>
    public enum TransactionManagerTypes
    {
        /// <summary>
        /// .NET TransactionScope
        /// </summary>
        TransactionScope,

        /// <summary>
        /// Internal transaction mechanism of the object relational mapping technology
        /// </summary>
        ORMTechInternal,

        /// <summary>
        /// Internal transaction mechanism of the database provider
        /// </summary>
        DBProviderInternal,

        /// <summary>
        /// Internal transaction mechanism of the DI technology
        /// </summary>
        DITechInternal
    }
}