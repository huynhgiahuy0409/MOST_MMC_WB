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
* DATE           AUTHOR		       REVISION    	
* 2017.12.21    JC. LIM	        First release.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using System.IO.Compression;
using System.IO;
using log4net.Util;

namespace Tsb.Fontos.Core.Logging.Appender
{
    public class RollingCompressingFileAppender : RollingFileAppender
    {
        private DateTime _fileLastWriteTime;
        private bool _rollDate = false;
        private bool _rollSize = false;

        public RollingCompressingFileAppender()
            : base()
        {
            if (RollingStyle == RollingMode.Composite)
            {
                _rollDate = true;
                _rollSize = true;
            }
            if (RollingStyle == RollingMode.Size)
            {
                _rollSize = true;
            }
            if (RollingStyle == RollingMode.Date)
            {
                _rollDate = true;
            }
        }

        public override void ActivateOptions()
        {
            _fileLastWriteTime = System.IO.File.GetLastWriteTime(File);

            base.ActivateOptions();

            CompressIfDateBoundaryCrossing();
        }

        override protected void AdjustFileBeforeAppend()
        {
            base.AdjustFileBeforeAppend();

            if (_rollDate)
            {
                CompressIfDateBoundaryCrossing();
            }
        }

        /// <summary>
        /// Initiates a compress if needed for crossing a date boundary since the last run.
        /// </summary>
        private void CompressIfDateBoundaryCrossing()
        {
            if (StaticLogFileName && _rollDate)
            {
                if (!(_fileLastWriteTime.ToString(DatePattern, System.Globalization.DateTimeFormatInfo.InvariantInfo).Equals(DateTime.Now.ToString(DatePattern, System.Globalization.DateTimeFormatInfo.InvariantInfo))))
                {
                    if (FileExists(File))
                    {
                        string searchPattern = _fileLastWriteTime.ToString(DatePattern, System.Globalization.DateTimeFormatInfo.InvariantInfo);

                        string[] targetFileNames = Directory.GetFiles(Directory.GetParent(File).FullName, "*.log" + searchPattern + "*", SearchOption.TopDirectoryOnly);
                        if (targetFileNames != null)
                        {
                            for (int i = 0; i < targetFileNames.Length; i++)
                            {
                                CompressFile(new FileInfo(targetFileNames[i]));
                                DeleteFile(targetFileNames[i]);
                            }
                        }
                        _fileLastWriteTime = System.IO.File.GetLastWriteTime(File);
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Compresses file.
        /// </summary>
        /// <param name="fileInfo"></param>
        private void CompressFile(FileInfo fileInfo)
        {
            if (fileInfo.Name.Contains(".log"))
            {
                byte[] buffer = null;

                using (FileStream inFile = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    buffer = new byte[inFile.Length];

                    int count = inFile.Read(buffer, 0, buffer.Length);

                    if (count != buffer.Length)
                    {
                        return;
                    }
                }

                using (FileStream outFile = new FileStream(fileInfo.FullName + ".zip", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    using (GZipStream compressedzipStream = new GZipStream(outFile, CompressionMode.Compress, true))
                    {
                        compressedzipStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }

            return;
        }
    }
}
