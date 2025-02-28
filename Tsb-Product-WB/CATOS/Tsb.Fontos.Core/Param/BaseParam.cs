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
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.BgWorker;

namespace Tsb.Fontos.Core.Param
{
    /// <summary>
    /// TSB Base Parameter class
    /// </summary>
    [Serializable]
    public class BaseParam : TsbBaseObject, ITsbDto
    {
        #region FIELD AREA *************************************
        private TransactionInfo _transactionInfo = null;
        private BaseDataItem _paramDataItem = null;
        private object _paramItemList =null ;
        private object _sender = null;
        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Transaction information object
        /// </summary>
        public TransactionInfo TransactionInfo
        {
            get { return _transactionInfo; }
            set { _transactionInfo = value; }
        }

        /// <summary>
        /// DataItem Parameter
        /// </summary>
        public BaseDataItem ParamDataItem
        {
            get { return _paramDataItem; }
            set { _paramDataItem = value; }
        }

        /// <summary>
        /// Data Item List Parameter
        /// </summary>
        public object ParamItemList
        {
            get { return _paramItemList; }
            set { _paramItemList = value; }
        }

        /// <summary>
        /// Sender which is a origin of request
        /// </summary>
        public object Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        /// <summary>
        /// Gets or Sets User Information object reference
        /// </summary>
        public BaseUserInfo UserInfo { get; set; }
        /// <summary>
        /// 백그라운드 작업을 수행하는 객체정보를 가져오거나 설정합니다.
        /// </summary>
        public BaseAsyncAgent AsyncAgent { get; set; }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseParam()
            : base()
        {
            this.ObjectID = "GNR-FTCO-PAR-BaseParam";
            this.ObjectType = ObjectType.PARAM;
            this.TransactionInfo = new TransactionInfo();
            this.ParamDataItem = null;
            this.ParamItemList = null;
            this.Sender = null;
        }
        

        /// <summary>
        /// Initialize instance with specified parameter owner(sender) reference
        /// </summary>
        public BaseParam(object paramOwner)
            : this()
        {
            this.Sender = paramOwner;
        }


        /// <summary>
        /// Initialize instance with specified transaction ID and parameter owner(sender) reference
        /// </summary>
        /// <param name="paramOwner">Parameter Owner(Creator or sender)</param>
        /// <param name="txServiceID">Transaction ID</param>
        public BaseParam(object paramOwner, string txServiceID)
            : this(paramOwner)
        {
            this.TransactionInfo.TxServiceID = txServiceID;
        }
        #endregion


    }
}
