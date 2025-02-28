﻿#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2015 TOTAL SOFT BANK LIMITED. All Rights
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
* 2015.07.08    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Exceptions.System
{

    /// <summary>
    /// TSB License Expired Excetpion
    /// </summary>
    [Serializable]
    public class TsbLicenseExpiredException : TsbSysBaseException
    {
        private readonly string _OBJECT_ID = "GNR-FTCO-EXP-TsbLicenseExpiredException";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TsbLicenseExpiredException()
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
        public TsbLicenseExpiredException(string objectID, string msgCode, params string[] msgArgs)
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
        public TsbLicenseExpiredException(Exception innerException, string sourceObjectID, string msgCode, params string[] msgArgs)
            : base(innerException, sourceObjectID, msgCode, msgArgs)
        {
            this.ObjectID = this._OBJECT_ID;
        }
       
    }
}

