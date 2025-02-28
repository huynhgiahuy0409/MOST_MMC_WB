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
* 2009.06.12    CHOI 1.0	First release.
* 
*/
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Exceptions.Formatter;
using Tsb.Fontos.Core.Objects;
using System.Diagnostics;


namespace Tsb.Fontos.Core.Exceptions
{
    /// <summary>
    /// TSB Base Excecption Class
    /// </summary>
    [Serializable]
    public class TsbBaseException : ApplicationException, ITsbBaseObject
    {
        #region FIELD AREA *************************************
        private   string   _objectID = "GNR-FTCO-EXP-TsbBaseException";
        protected string   _msgCode;
        protected string   _sourceObjectID;
        protected string[] _msgArgs;
        private Exception _originalException;
        protected IList<ExceptionInfo> _tracer;
        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Object Identification
        /// </summary>
        public string ObjectID
        {
            get{ return this._objectID; }
            set{ this._objectID = value; }
        }

        /// <summary>
        /// Object Type
        /// </summary>
        public ObjectType ObjectType
        {
            get { return ObjectType.EXCEPTION; }
        }

        /// <summary>
        /// The Object ID. This property has a operation Identifier
        /// </summary>
        public string SourceObjectID
        {
            get { return _sourceObjectID; }
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
        /// Message arguments string array to make message
        /// </summary>
        public string[] MsgArgs
        {
          get { return _msgArgs; }
          set { _msgArgs = value; }
        }

        /// <summary>
        /// Original Exception object reference
        /// </summary>
        public Exception OriginalException
        {
            get { return _originalException; }
            set { _originalException = value; }
        }

        /// <summary>
        /// Exception handling tracing informations list
        /// </summary>
        public IList<ExceptionInfo> Tracer
        {
            get { return _tracer; }
            set { _tracer = value; }
        }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TsbBaseException()
            : base()
        {
            this._sourceObjectID = "NOT_ASSIGNED_SOURCE_OBJECT_ID";
            this._msgCode = null;
            this._msgArgs = null;
            this._originalException = null;
            this.Tracer = new List<ExceptionInfo>();
        }

        
        /// <summary>
        /// Initialize a new instance with exception source object ID, message code and array of string to make message
        /// </summary>
        /// <param name="sourceObjectID">The Object ID of source object</param>
        /// <param name="msgCode">A message code to make message</param>
        /// <param name="msgArgs">An array of string to make message</param>
        public TsbBaseException(string sourceObjectID, string msgCode,params string[] msgArgs)
            : base(DefaultExceptionFormatter.GetExceptionMsgFromArgs(msgCode, sourceObjectID, msgArgs))
        {
            this._sourceObjectID = sourceObjectID;
            this._msgCode = msgCode;
            this._msgArgs = msgArgs;
            this._originalException = null;
            this.Tracer = new List<ExceptionInfo>();
        }

        
        /// <summary>
        /// Initialize a new instance with a reference to the inner exception that cause this exception,
        /// operation ID, message code and array of string to make message
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        /// <param name="sourceObjectID">The Object ID of source object</param>
        /// <param name="msgCode">A message code to make message</param>
        /// <param name="msgArgs">An array of string to make message</param>
        public TsbBaseException(Exception innerException, string sourceObjectID, string msgCode, params string[] msgArgs)
            : base(DefaultExceptionFormatter.GetExceptionMsgFromArgs(msgCode, sourceObjectID, msgArgs), innerException)
        {
            this._sourceObjectID = sourceObjectID;
            this._msgCode = msgCode;
            this._msgArgs = msgArgs;
            this._originalException = null;
            this.Tracer = new List<ExceptionInfo>();

            if (!(innerException is TsbBaseException))
            {
                this.OriginalException = innerException;
            }
            else if ( innerException is TsbBaseException && innerException.InnerException == null)
            {
                this.OriginalException = innerException;
            }

        }




        ///// <summary>
        ///// Initializes a new instance with a specified error message and a reference to the inner exception 
        ///// that is the cause of this exception.
        ///// </summary>
        ///// <param name="message">The message that describes the error.</param>
        ///// <param name="innerException">The exception that is the cause of the current exception</param>
        //protected TsbBaseException(string message, Exception innerException)
        //    : base(message, innerException)
        //{
        //    this.ObjectID = "GNR-FTCO-EXP-TsbBaseException";
        //    this._sourceObjectID = "NOT_ASSIGNED_SOURCE_OBJECT_ID";
        //    this.Tracer = new List<ExceptionInfo>();
        //    this._msgCode = null;
        //    this._msgArgs = null;
        //}

        ///// <summary>
        ///// Initializes a new instance with a specified error message,
        ///// operation ID
        ///// </summary>
        ///// <param name="message">The message that describes the error.</param>
        ///// <param name="objectID">The Object ID</param>
        //public TsbBaseException(string message, string objectID)
        //    : base(message)
        //{
        //    this.ObjectID = "GNR-FTCO-EXP-TsbBaseException";
        //    this.Tracer = new List<ExceptionInfo>();
        //    this._sourceObjectID = objectID;
        //    //--ExceptionHandler.CreateTraceInfo(this);
        //}
        #endregion
    }
}
