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

namespace Tsb.Fontos.Core.Message
{
    /// <summary>
    /// Constant message class which manages unexpective fatal error.
    /// </summary>
    public class DefaultMessage
    {
        public const string MSG_CODE_DEFAULT_OK           = "MSG_FTCO_00114";
        public const string MSG_CODE_NOT_EXIST            = "MSG_FTCO_00121";
        public const string MSG_CODE_NOR_EXIST_CHECK      = "MSG_FTCO_00122";
        public const string MSG_CODE_INVALID_PATH         = "MSG_FTCO_00123";
        public const string MSG_CODE_UNDEFINED            = "MSG_FTCO_00129";
        public const string MSG_CODE_NOTSUPPORTED         = "MSG_FTCO_00130";

        public const string MSG_CODE_CFGTYPE_NOT_SUPPORTED = "MSG_FTCO_00002";
        public const string MSG_CODE_CFGFILE_READ_NOTFOUND = "MSG_FTCO_00003";
        public const string MSG_CODE_CFGFILE_READ_INVALID  = "MSG_FTCO_00004";
        public const string MSG_CODE_CFGPATH_CHECK_ERROR   = "MSG_FTCO_00005";
        public const string MSG_CODE_CFGFILE_OPEN_ERROR    = "MSG_FTCO_00006";
        public const string MSG_CODE_APPCONFIG_READ_ERROR  = "MSG_FTCO_00008";
        public const string MSG_CODE_RESOURCE_KEY_NOTFOUND = "MSG_FTCO_00010";
        public const string MSG_CODE_RESOURCE_NOTFOUND     = "MSG_FTCO_00011";

        public const string MSG_STR_MSGBUILD_ERROR        = "BUIDING MESSAGE FAILED !!!";
        public const string MSG_STR_ERROR                 = "ERROR";

        public const string WRD_KEY_INFO     = "WRD_FTCO_Information";
        public const string WRD_KEY_ERROR    = "WRD_FTCO_Error";
        public const string WRD_KEY_WARN     = "WRD_FTCO_Warning";
        public const string WRD_KEY_STOP     = "WRD_FTCO_Stop";
        public const string WRD_KEY_QUESTION = "WRD_FTCO_Question";

        public const string NON_REG_WRD = "NRW_";

        /// <summary>
        /// Returns Default Message string which is not handled by resource. 
        /// </summary>
        /// <param name="msgCode">Message Code</param>
        /// <returns>Default Message string</returns>
        public static string GetDefaultMessage(string msgCode)
        {
            switch(msgCode)
            {
                case DefaultMessage.MSG_CODE_DEFAULT_OK:
                    return"The operation completed successfully.";
                case DefaultMessage.MSG_CODE_NOR_EXIST_CHECK:
                    return "{0} or [1} does not exist. Please check {2}";
                case DefaultMessage.MSG_CODE_INVALID_PATH:
                    return "The specified path is invalid (path:{0}).";
                case DefaultMessage.MSG_CODE_CFGFILE_READ_NOTFOUND:
                    return "Configuration file reading error. [section-{0}][setting-{1}] could not found in {2} file.";
                case DefaultMessage.MSG_CODE_CFGFILE_READ_INVALID:
                    return "Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.";
                case DefaultMessage.MSG_CODE_APPCONFIG_READ_ERROR:
                    return "Application configuration file reading error. [setting-{0}] could not found in the {1} file.";
                case DefaultMessage.MSG_CODE_CFGTYPE_NOT_SUPPORTED:
                    return "Other configuration source types are not supported at this time.";
                case DefaultMessage.MSG_CODE_UNDEFINED:
                    return "{0} is undefined.";
                case DefaultMessage.MSG_CODE_NOTSUPPORTED:
                    return "{0} is not supported.";
                case DefaultMessage.MSG_CODE_CFGPATH_CHECK_ERROR:
                    return "An error occurred when checking the configuration path";
                case DefaultMessage.MSG_CODE_NOT_EXIST:
                    return "{0} does not exist. Please check {1}.";
                case DefaultMessage.MSG_CODE_CFGFILE_OPEN_ERROR:
                    return "An error occurred when opening or reading the configuration file.";
                case DefaultMessage.MSG_CODE_RESOURCE_NOTFOUND:
                    return "No usable set of resources has been found.";
                case DefaultMessage.MSG_CODE_RESOURCE_KEY_NOTFOUND:
                    return "Resource key [{0}] does not exist. Please check {1}.";

                default:
                    return "Message code["+msgCode+"] can not be handled at DefaultMessage class";
            }

        }
    }
}
