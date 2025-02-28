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
* 2009.08.10    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Exceptions.System;

namespace Tsb.Fontos.Core.Util.File
{
    /// <summary>
    /// File Utility Class
    /// </summary>
    public class FileUtil: BaseUtil
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileUtil()
            : base()
        {
            this.ObjectID = "GNR_FTCO_UTL_FileUtil";
        }

        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <returns>The file path to check.</returns>
        public static bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }

        /// <summary>
        /// Write text in the file's path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="text"></param>
        public static void Write(string filePath, string text)
        {
            try
            {
                if (Exists(filePath))
                {
                    System.IO.File.AppendAllText(filePath, text);
                }
                else
                {
                    System.IO.File.WriteAllText(filePath, text);
                }
            }
            catch (Exception ex)
            {
                //MSG:While writing {0} xml file, a serialization operation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), "GNR_FTCO_UTL_FileUtil", "MSG_FTCO_00037", DefaultMessage.NON_REG_WRD + filePath);
            }
            
            return;
        }
    }
}
