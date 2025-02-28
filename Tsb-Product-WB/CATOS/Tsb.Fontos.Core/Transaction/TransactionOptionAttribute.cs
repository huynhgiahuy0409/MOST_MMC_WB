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
* 2010.02.12     CHOI       1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.Text;
using Tsb.Fontos.Core.Transaction.Type;


namespace Tsb.Fontos.Core.Transaction
{
    /// <summary>
    /// TransactionOption Attribute Class
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TransactionOptionAttribute : Attribute
    {
        #region FIELD/PROPERTY AREA*****************************
        /// <summary>
        /// Gets or Sets Transaction Scope Type
        /// </summary>
        public TransactionScopeTypes TRScopeType { get; set;}

        
        private TransactionIsolationLevels _trIsolationLevel = TransactionIsolationLevels.ReadCommitted;
        /// <summary>
        /// Gets or Sets Transaction Isolation Level. Default value is TransactionIsolationLevels.ReadCommitted
        /// </summary>
        public TransactionIsolationLevels TRIsolationLevel
        {
            get { return this._trIsolationLevel; }
            set { this._trIsolationLevel = value; }
        }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initialize Instance using a specified TransactionScope
        /// </summary>
        /// <param name="trScopeType">Transaction scope type</param>
        public TransactionOptionAttribute(TransactionScopeTypes trScopeType)
        {
            this.TRScopeType = trScopeType;
        }

        /// <summary>
        /// Initialize Instance using specified TransactionScope and Isolation Level
        /// </summary>
        /// <param name="trScopeType">Transaction scope type</param>
        /// <param name="trIsoLevel">Transaction Isolation level</param>
        public TransactionOptionAttribute(TransactionScopeTypes trScopeType, TransactionIsolationLevels trIsoLevel)
        {
            this.TRScopeType = trScopeType;
            this._trIsolationLevel = trIsoLevel;
        }
        #endregion
    }
}
