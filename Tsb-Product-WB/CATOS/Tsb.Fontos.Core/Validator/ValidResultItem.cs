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

namespace Tsb.Fontos.Core.Validator
{
    /// <summary>
    /// Validator Result Item class
    /// </summary>
    [Serializable]
    public class ValidResultItem : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************
        private ResultType _resultType;
        /// <summary>
        /// Type of result (OK, ERROR, INFO, WARN)
        /// </summary>
        public ResultType ResultType
        {
            get { return _resultType; }
            set { _resultType = value; }
        }

        private string _resultMsg;
        /// <summary>
        /// Message of result
        /// </summary>
        public string ResultMsg
        {
            get { return _resultMsg; }
            set { _resultMsg = value; }
        }

        private string _validatorID;
        /// <summary>
        /// Object ID of validator
        /// </summary>
        public string ValidatorID
        {
            get { return _validatorID; }
            set { _validatorID = value; }
        }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initializes a new instance of the ValidatorResultItem class with an result type, result message and validator ID
        /// </summary>
        /// <param name="resultType">ResultType enum value</param>
        /// <param name="resultMsg">Result Message</param>
        /// <param name="validatorID">Validator's ID</param>
        public ValidResultItem(ResultType resultType, string resultMsg, string validatorID)
            : base()
        {
            this.ObjectID = "GNR-FTCO-VAL-ValidResultItem";
            this.ObjectType = ObjectType.RESULT;

            this.ResultType = resultType;
            this.ResultMsg  = resultMsg;
            this.ValidatorID = validatorID;
        }
        #endregion
    }
}
