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
* 2009.07.09    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Exceptions.Formatter
{
    /// <summary>
    /// Default Exception Formatter class
    /// </summary>
    public class DefaultExceptionFormatter : TsbBaseObject
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DefaultExceptionFormatter()
        {
            this.ObjectID = "GNR-FTCO-EXP-DefaultExceptionFormatter";
        }

        /// <summary>
        /// Returns excpeiton message using args
        /// </summary>
        /// <param name="msgCode">Message code</param>
        /// <param name="operationID">Exception originater's id</param>
        /// <param name="msgArgs">Message arguments</param>
        /// <returns>Formatted excpeiton message</returns>
        public static string GetExceptionMsgFromArgs(string msgCode, string operationID, params string[] msgArgs)
        {
            string rtnMsg = null;

            StringBuilder builder = new StringBuilder();

            ///ex) MESSAGE_CODE(TSB-CM-99999) OBJECT_ID(FTS-EX-0001) PARAMS(Invalied operation)
            builder.Append("MESSAGE_CODE("+msgCode+") ");
            builder.Append("OBJECT_ID(" + operationID + ") ");
            builder.Append("PARAMS(");
            
            for (int i = 0; msgArgs != null && i < msgArgs.Length; i++)
            {
                if(i!=0)
                    builder.Append(",");

                builder.Append(msgArgs[i]);
            }
            builder.Append(")");

            rtnMsg = builder.ToString();
            builder = null;
            return rtnMsg;
        }
    }
}
