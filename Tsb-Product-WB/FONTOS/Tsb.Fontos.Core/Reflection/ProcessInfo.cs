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
* 2009.07.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Util.File;

namespace Tsb.Fontos.Core.Reflection
{
    /// <summary>
    /// Process inforamtion Class
    /// </summary>
    public class ProcessInfo : TsbBaseObject
    {
        private const string VS_TEST_FILENAME = "QTAgent32";

        public ProcessInfo()
        {
            this.ObjectID = "GNR-FTCO-REF-ProcessInfo";
        }

        /// <summary>
        /// Gets full path to main exe file name
        /// </summary>
        /// <returns>EXE file name with full path</returns>
        public static string GetFullPathExeFileName()
        {
            string rtnFileName = Process.GetCurrentProcess().MainModule.FileName;

            if (rtnFileName.Contains(VS_TEST_FILENAME))
            {
                rtnFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                rtnFileName = rtnFileName.Replace(AppPathInfo.FILE_EXT_APPCONFIG, "");
            }
            else
            {
                rtnFileName = rtnFileName.Replace(AppPathInfo.FILE_NAME_VSHOST, "");
            }

            return rtnFileName;
        }
    }
}
