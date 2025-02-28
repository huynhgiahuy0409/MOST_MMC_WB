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
* 2009.06.26    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Transaction
{
    /// <summary>
    /// Transaction Information Value Object class
    /// </summary>
    [Serializable]
    public class TransactionInfo : TsbBaseObject
    {
        #region FIELD AREA *************************************
        private string _txID = string.Empty;
        private string _txServiceID = string.Empty;

        private DateTime _txStartTime;
        private DateTime _txEndTime;
        
        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Transaction ID. This property is initialized with cuttent time.
        /// </summary>
        public string TxID
        {
            get { return _txID; }
            set { _txID = value; }
        }

        /// <summary>
        /// Transaction Service ID
        /// </summary>
        public string TxServiceID
        {
            get { return _txServiceID; }
            set { _txServiceID = value; }
        }

        /// <summary>
        /// Transaction start time
        /// </summary>
        public DateTime TxStartTime
        {
            get { return _txStartTime; }
            set { _txStartTime = value; }
        }

        /// <summary>
        /// Transaction end time
        /// </summary>
        public DateTime TxEndTime
        {
            get { return _txEndTime; }
            set { _txEndTime = value; }
        }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Costructor
        /// </summary>
        public TransactionInfo()
        {
            this.ObjectID = "GNR-FTCO-TRS-TransactionInfo";
            this.TxID = "TXID_" + Convert.ToString(DateTime.UtcNow.Ticks);
            this.TxServiceID = "NOT_ASSIGNED_SERVICE_ID";
            this.TxStartTime = System.DateTime.Now;
            this.TxEndTime   = System.DateTime.Now;
        }
        #endregion


    }
}
