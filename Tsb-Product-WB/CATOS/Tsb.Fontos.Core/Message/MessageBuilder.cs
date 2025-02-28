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
* 2009.07.25     CHOI       1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Resources;
using Tsb.Fontos.Core.Param;
using System.Text.RegularExpressions;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Message
{
    /// <summary>
    /// Message Builder class
    /// </summary>
    public class MessageBuilder
    {
        private static readonly string ObjectID = "GNR-FTCO-MSG-MessageBuilder";

        /// <summary>
        /// Returns a Message String that is built with a specified message code and message vocabulary
        /// </summary>
        /// <param name="msgCode">Message code</param>
        /// <param name="args">Message vocabulraries</param>
        /// <returns>Message String</returns>
        public static string BuildMessage(string msgCode, params string[] args)
        {
            string rtnMessage = string.Empty;
            string[] formatArgs = null;
            Resource res = null;

            try
            {
                res = ResourceFactory.GetResource();
                rtnMessage = res.GetMessage(msgCode);

                formatArgs = MessageBuilder.ConvertArgToLabel(args);

                if (formatArgs != null && formatArgs.Length > 0)
                {
                    rtnMessage = string.Format(rtnMessage, formatArgs);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }

            return rtnMessage;
        }
        
        /// <summary>
        /// Returns a Message String that is built with a specified Tsb Exception
        /// </summary>
        /// <param name="tsbBaseEx">TSB Exception</param>
        /// <returns>Message String</returns>
        public static string BuildMessage(TsbBaseException tsbBaseEx)
        {
            return BuildMessage(tsbBaseEx.MsgCode, tsbBaseEx.MsgArgs);
        }


        /// <summary>
        /// Returns a Message String that is built with a specified Result Object
        /// </summary>
        /// <param name="resultObj">Result Object</param>
        /// <returns>Message String</returns>
        public static string BuildMessage(BaseResult resultObj)
        {
            return BuildMessage(resultObj.ResultMsgCode, resultObj.ResultMsgArgs);
        }


        /// <summary>
        /// Returns a Message String that is built with a specified message code and message vocabulary
        /// </summary>
        /// <param name="msgCode">Message code</param>
        /// <param name="args">Message vocabulraries</param>
        /// <returns>Message String</returns>
        public static string[] ConvertArgToLabel(params string[] args)
        {
            string[] formatArgs = null;
            Resource res = null;
            Regex charRegx = null;
            string tempArg = null;

            try
            {
                charRegx = new Regex(@"[\.|\,|/|%|\*]");

                if (args != null)
                {
                    res = ResourceFactory.GetResource();

                    formatArgs = new string[args.Length];

                    for (int i = 0; i < args.Length; i++)
                    {
                        try
                        {
                            if(args[i].StartsWith(DefaultMessage.NON_REG_WRD))
                            {
                                formatArgs[i] = args[i].Replace(DefaultMessage.NON_REG_WRD, "");
                            }
                            else
                            {
                                formatArgs[i] = res.GetLabel(args[i]);
                            }

                        }
                        catch (TsbBaseException ex)
                        {
                            GeneralLogger.Error(ex);

                            if (ex.MsgCode.Equals(DefaultMessage.MSG_CODE_RESOURCE_KEY_NOTFOUND))
                            {
                                tempArg = args[i];
                                double tempVal = -1;
                                tempArg = charRegx.Replace(tempArg, "");

                                if (double.TryParse(tempArg, out tempVal))
                                {
                                    formatArgs[i] = args[i];
                                }
                                else
                                {
                                    ExceptionHandler.Propagate(ex, ObjectID);
                                }

                                formatArgs[i] = args[i];
                            }
                            else
                            {
                                ExceptionHandler.Propagate(ex, ObjectID);
                            }

                        }
                    }
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }

            return formatArgs;
        }

        /// <summary>
        /// Returns Message Build Error Message
        /// </summary>
        /// <param name="capResourceKey">Resource key for Dialog catpion</param>
        /// <param name="msgResourceKey">Resource key for Dialog message</param>
        /// <param name="errorMsg">Error Message</param>
        /// <returns>Message Build Error Message</returns>
        public static string BuildMsgBuildError(string capResourceKey, string msgResourceKey, string errorMsg)
        {
            StringBuilder strBuilder = new StringBuilder();

            try
            {
                strBuilder.Append(DefaultMessage.MSG_STR_MSGBUILD_ERROR);
                strBuilder.Append(Environment.NewLine);
                strBuilder.Append(errorMsg);
                strBuilder.Append(Environment.NewLine);

                if (string.IsNullOrEmpty(capResourceKey) && string.IsNullOrEmpty(msgResourceKey) && string.IsNullOrEmpty(errorMsg))
                {
                    return strBuilder.ToString();
                }

                strBuilder.Append("[SOURCE]");

                if (!string.IsNullOrEmpty(capResourceKey))
                {
                    strBuilder.Append(" CAPTION_KEY=");
                    strBuilder.Append(capResourceKey);
                }

                if (!string.IsNullOrEmpty(msgResourceKey))
                {
                    strBuilder.Append(" MSG_KEY=");
                    strBuilder.Append(msgResourceKey);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return strBuilder.ToString();

        }
    }
}
