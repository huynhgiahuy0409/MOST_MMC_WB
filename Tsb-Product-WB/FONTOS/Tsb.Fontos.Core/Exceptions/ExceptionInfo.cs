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
* 2009.07.11    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Exceptions
{
    /// <summary>
    /// Exception Information class. This class will be used for Exception trace
    /// </summary>
    [Serializable]
    public sealed class ExceptionInfo : TsbBaseObject
    {
        #region FIELD AREA *************************************
        private string _exceptionName;
        private DateTime _occurDate;
        private string _methodName;
        private string[] _msgArgs;
        private string _msgCode;
        private string _sourceObjectID;
        #endregion

        /// <summary>
        /// Exception Name
        /// </summary>
        public string ExceptionName
        {
            get { return _exceptionName; }
            set { _exceptionName = value; }
        }

        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime OccurDate
        {
            get { return _occurDate; }
        }

        /// <summary>
        /// Exception occured method name
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        /// <summary>
        /// Message arguments string array to make message
        /// </summary>
        public string[] MsgArgs
        {
            get { return _msgArgs; }
            set { _msgArgs = value; }
        }

        /// <summary>
        /// Message code to make message
        /// </summary>
        public string MsgCode
        {
            get { return _msgCode; }
            set { _msgCode = value; }
        }

        /// <summary>
        /// Function ID. This property has a operation Identifier
        /// </summary>
        public string SourceObjectID
        {
            get { return _sourceObjectID; }
            set { _sourceObjectID = value; }
        }
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ExceptionInfo()
        {
            this.ObjectID = "GNR-FTCO-EXP-ExceptionInfo";

            this.ExceptionName = "NOT_ASSIGNED_EXCEPTION_NAME";
            this._occurDate = DateTime.Now.ToLocalTime();
            this.MethodName  = "NOT_ASSIGNED_METHOD_NAME";
            this.MsgCode = "NOT_ASSIGNED_MESSAGE_CODE";
            this.MsgArgs = null;
            this.SourceObjectID = "NOT_ASSIGNED_SOURCE_OBJECT_ID";
        }


        
    }
}
