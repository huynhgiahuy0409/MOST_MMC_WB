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

namespace Tsb.Fontos.Core.Exceptions.System
{
    /// <summary>
    /// TSB System Network Exception
    /// </summary>
    [Serializable]
    public class TsbSysNetException : TsbSysBaseException
    {
        private readonly string _OBJECT_ID = "GNR-FTCO-EXP-TsbSysNetException";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TsbSysNetException()
            : base()
        {
            this.ObjectID = this._OBJECT_ID;
        }

        /// <summary>
        /// Initialize a new instance with operation ID, message code and array of string to make message
        /// </summary>
        /// <param name="objectID">The Object ID</param>
        /// <param name="msgCode">A message code to make message</param>
        /// <param name="msgArgs">An array of string to make message</param>
        public TsbSysNetException(string objectID, string msgCode, params string[] msgArgs)
            : base(objectID, msgCode, msgArgs)
        {
            this.ObjectID = this._OBJECT_ID;
        }

        /// <summary>
        /// Initialize a new instance with a reference to the inner exception that cause this exception,
        /// operation ID, message code and array of string to make message
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        /// <param name="sourceObjectID">The Object ID of source object</param>
        /// <param name="msgCode">A message code to make message</param>
        /// <param name="msgArgs">An array of string to make message</param>
        public TsbSysNetException(Exception innerException, string sourceObjectID, string msgCode, params string[] msgArgs)
            : base(innerException, sourceObjectID, msgCode, msgArgs)
        {
            this.ObjectID = this._OBJECT_ID;
        }
    }
}
