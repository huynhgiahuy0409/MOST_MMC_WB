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
* 2009.08.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Version Info Class
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// The product assembly version associated with this application.
        /// </summary>
        public static readonly string ASSEMBLY_VERSION = Application.ProductVersion;

        /// <summary>
        /// The Execution assembly build time
        /// </summary>
        public static readonly string BUILD_TIME = new DateTime(2000, 1, 1).AddDays(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build).ToShortDateString();

        /// <summary>
        /// The operating system version.
        /// </summary>
        public static readonly string OS_VERSION = Environment.OSVersion.ToString();

        /// <summary>
        /// The operating system version.
        /// </summary>
        public static readonly string OS_VERSION_FULL_TEXT = Environment.OSVersion.VersionString;

        /// <summary>
        /// The operating system platform
        /// </summary>
        public static readonly string OS_PLATFORM = Environment.OSVersion.Platform.ToString();

        /// <summary>
        /// The operating system service pack version
        /// </summary>
        public static readonly string OS_SERVICEPACK_VER = Environment.OSVersion.ServicePack;

        /// <summary>
        /// The CLR Version
        /// </summary>
        public static readonly string OS_CLR_VER = Environment.Version.ToString();

    }
}
