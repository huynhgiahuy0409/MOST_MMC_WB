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
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Message;

namespace Tsb.Fontos.Core.Param
{
    /// <summary>
    /// TSB Base Result class
    /// </summary>
    [Serializable]
    public class BaseResult : TsbBaseObject, ITsbDto
    {
        #region FIELD AREA *************************************
        private TransactionInfo _transactionInfo;
        
        private ResultDataType _resultDataType;
        private object _resultObject;

        private ResultType _resultType;
        private string _resultMsgCode;
        private string[] _resultMsgArgs;
        private string senderID;
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
        /// Data type of result
        /// </summary>
        public ResultDataType ResultDataType
        {
            get { return _resultDataType; }
            set { _resultDataType = value; }
        }

        /// <summary>
        /// General Result Object
        /// </summary>
        public object ResultObject
        {
            get { return _resultObject; }
            set { _resultObject = value; }
        }

        /// <summary>
        /// Type of result (BEFORE_TX, OK, ERROR, INFO, WARN)
        /// </summary>
        public ResultType ResultType
        {
            get { return _resultType; }
            set { _resultType = value; }
        }

        /// <summary>
        /// Message code of result
        /// </summary>
        public string ResultMsgCode
        {
            get { return _resultMsgCode; }
            set { _resultMsgCode = value; }
        }

        /// <summary>
        /// Message arguments of result
        /// </summary>
        public string[] ResultMsgArgs
        {
            get { return _resultMsgArgs; }
            set { _resultMsgArgs = value; }
        }

        /// <summary>
        /// Object ID of sender
        /// </summary>
        public string SenderID
        {
            get { return senderID; }
            set { senderID = value; }
        }

        public Exception Exception { get; set; }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseResult()
            : base()
        {
            this.ObjectID = "GNR-FTCO-PAR-BaseResult";
            this.ObjectType = ObjectType.RESULT;

            this.ResultDataType = ResultDataType.UNDEFINED;
            this.TransactionInfo= null;
            this.ResultType = ResultType.BEFORE_TX;
            this.ResultMsgCode = null;
            this.ResultMsgArgs = null;
            this.SenderID = null;
        }
        #endregion


        #region INSTNACE METHOD AREA ***************************
        /// <summary>
        /// Creates a OK(SUCCESS) Result Object with specified
        /// Result Type, Sender ID, Returning Object, passed Parameter object, Message Code, and Message Vocabulary arguments.
        /// In this method, result message code will be set as default successful message like (The operation completed successfully.)
        /// </summary>
        /// <param name="senderObjectID">Sender(Service)'s object ID</param>
        /// <param name="objToReturn">Object to return (DataItem, ItemList, Rows count or others</param>
        /// <param name="param">Paramter object</param>
        /// <returns>Base result object</returns>
        public static BaseResult CreateOkResult(string senderObjectID, object objToReturn, BaseParam param)
        {
            return BaseResult.CreateOkResult(senderObjectID, objToReturn, param, DefaultMessage.MSG_CODE_DEFAULT_OK, null);
        }

        /// <summary>
        /// Creates a OK(SUCCESS) Result Object, with specified
        /// Result Type, Sender ID, Returning Object, passed Parameter object, Message Code, and Message Vocabulary arguments
        /// </summary>
        /// <param name="senderObjectID">Sender(Service)'s object ID</param>
        /// <param name="objToReturn">Object to return (DataItem, ItemList, Rows count or others</param>
        /// <param name="param">Paramter object</param>
        /// <param name="msgCode">Message Code</param>
        /// <param name="msgArgs">Message vocabulary arguments</param>
        /// <returns>Base result object</returns>
        public static BaseResult CreateOkResult(string senderObjectID, object objToReturn, BaseParam param, string msgCode, params string[] msgArgs)
        {
            return BaseResult.CreateResult(ResultType.OK, senderObjectID, objToReturn, param, msgCode, msgArgs);
        }

        /// <summary>
        /// Creates a ERROR Result Object with specified
        /// Result Type, Sender ID, Returning Object, passed Parameter object, Message Code, and Message Vocabulary arguments
        /// </summary>
        /// <param name="senderObjectID">Sender(Service)'s object ID</param>
        /// <param name="objToReturn">Object to return (DataItem, ItemList, Rows count or others</param>
        /// <param name="param">Paramter object</param>
        /// <param name="msgCode">Message Code</param>
        /// <param name="msgArgs">Message vocabulary arguments</param>
        /// <returns>Base result object</returns>
        public static BaseResult CreateErrorResult(string senderObjectID, object objToReturn, BaseParam param, string msgCode, params string[] msgArgs)
        {
            return BaseResult.CreateResult(ResultType.ERROR, senderObjectID, objToReturn, param, msgCode, msgArgs);
        }

        /// <summary>
        /// Creates a INFO Result Object with specified
        /// Result Type, Sender ID, Returning Object, passed Parameter object, Message Code, and Message Vocabulary arguments
        /// </summary>
        /// <param name="senderObjectID">Sender(Service)'s object ID</param>
        /// <param name="objToReturn">Object to return (DataItem, ItemList, Rows count or others</param>
        /// <param name="param">Paramter object</param>
        /// <param name="msgCode">Message Code</param>
        /// <param name="msgArgs">Message vocabulary arguments</param>
        /// <returns>Base result object</returns>
        public static BaseResult CreateInfoResult(string senderObjectID, object objToReturn, BaseParam param, string msgCode, params string[] msgArgs)
        {
            return BaseResult.CreateResult(ResultType.INFO, senderObjectID, objToReturn, param, msgCode, msgArgs);
        }

        /// <summary>
        /// Creates a WARN Result Object with specified
        /// Result Type, Sender ID, Returning Object, passed Parameter object, Message Code, and Message Vocabulary arguments
        /// </summary>
        /// <param name="senderObjectID">Sender(Service)'s object ID</param>
        /// <param name="objToReturn">Object to return (DataItem, ItemList, Rows count or others</param>
        /// <param name="param">Paramter object</param>
        /// <param name="msgCode">Message Code</param>
        /// <param name="msgArgs">Message vocabulary arguments</param>
        /// <returns>Base result object</returns>
        public static BaseResult CreateWarnResult(string senderObjectID, object objToReturn, BaseParam param, string msgCode, params string[] msgArgs)
        {
            return BaseResult.CreateResult(ResultType.WARN, senderObjectID, objToReturn, param, msgCode, msgArgs);
        }

        /// <summary>
        /// Creates a Result Object with specified
        /// Result Type, Sender ID, Returning Object, passed Parameter object, Message Code, and Message Vocabulary arguments
        /// </summary>
        /// <param name="resultType">Type of result</param>
        /// <param name="senderObjectID">Sender(Service)'s object ID</param>
        /// <param name="objToReturn">Object to return (DataItem, ItemList, Rows count or others</param>
        /// <param name="param">Paramter object</param>
        /// <param name="msgCode">Message Code</param>
        /// <param name="msgArgs">Message vocabulary arguments</param>
        /// <returns>Base result object</returns>
        public static BaseResult CreateResult(ResultType resultType, string senderObjectID, object objToReturn, BaseParam param, string msgCode, params string[] msgArgs)
        {
            BaseResult resultObject = null;

            resultObject = new BaseResult();

            resultObject.ResultType = resultType;
            resultObject.SenderID = senderObjectID;
            BaseResult.LocateResultObject(ref resultObject, objToReturn);
            resultObject.ResultMsgCode = msgCode;
            resultObject.ResultMsgArgs = msgArgs;

            if (param == null || param.TransactionInfo == null)
            {
                resultObject.TransactionInfo = new TransactionInfo();
            }
            else
            {
                resultObject.TransactionInfo = param.TransactionInfo;
            }

            resultObject.TransactionInfo.TxEndTime = DateTime.Now;

            return resultObject;
        }



        /// <summary>
        /// Locate object to suitable result object's variable
        /// </summary>
        /// <param name="resultObject">Result Object</param>
        /// <param name="objToReturn">Object to return</param>
        private static void LocateResultObject(ref BaseResult resultObject, object objToReturn)
        {
            if (objToReturn == null)
            {
                return;
            }
            else
            {
                resultObject.ResultObject = objToReturn;
            }


			if (objToReturn.GetType().BaseType.Name.Equals(typeof(BaseItemsList<>).Name))
            {
                resultObject.ResultDataType = ResultDataType.ITEMS_LIST;
            }
            else if (objToReturn is BaseDataItem)
            {
                resultObject.ResultDataType = ResultDataType.ITEM;
            }
            else if (objToReturn is Int32)
            {
                resultObject.ResultDataType = ResultDataType.ROW_COUNT;
            }
            else if (objToReturn is Boolean)
            {
                resultObject.ResultDataType = ResultDataType.BOOLEAN;
            }
            else
            {
                resultObject.ResultDataType = ResultDataType.GENERAL_OBJECT;
            }

            return;
        }

        #endregion
    }
}
