#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2013 TOTAL SOFT BANK LIMITED. All Rights
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
* 2013.11.25   Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Security.SSO
{
    public static class SSOConstant
    {
        #region CONST AREA *************************************
        public const string PGM_USER_LOC = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\SSO";
        public const string USER_ID = "User ID";
        public const string USER_PASSWORD = "Password";
        public const string AGENT_ENABLED = "AgentEnabled";
        public const string IPC_BASE_URL = "ipc://";
        public const string IPC_SEPERATOR = "/";
        public const string IPC1 = "CATOS-SSO-";
        public const string DEFALUT_PGMCODE = "TOS-CM";

        public const string SSO_MAIN_MODULE = "CatosSSOAgent";
        public const string SSO_MAIN_MODULE_EXT = ".exe";
        public const string SSOLOGIN_CLASS_TYPENAME = "Tsb.Fontos.Core.Security.SSO.SSOLogin";
        public const string SSOLOGIN_PORT_NAME = "SSOLogin";
        public const string SSOLOGIN_PREFIX_OBJECT_URI = "SSOLogin";
        #endregion
    }
}
