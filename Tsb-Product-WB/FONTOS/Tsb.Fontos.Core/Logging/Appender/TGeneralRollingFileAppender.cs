using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using System.Windows.Forms;
using log4net;

namespace Tsb.Fontos.Core.Logging.Appender
{
    public class TGeneralRollingFileAppender : RollingFileAppender
    {
        #region FIELD AREA ***************************************
        private bool _renameToFileName;
        #endregion

        #region PROPERTY AREA ************************************
        public bool RenameToFileName
        {
            get
            {
                return _renameToFileName;
            }

            set
            {
                _renameToFileName = value;

                if (_renameToFileName == true)
                {
                    Application.ThreadExit += new EventHandler(Application_ThreadExit);
                }
                else
                {
                    Application.ThreadExit -= new EventHandler(Application_ThreadExit);
                }
            }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public TGeneralRollingFileAppender()
            : base()
        {

        }
        #endregion

        #region METHOD AREA **************************************
        private void RenameCurrentLogFile()
        {
            try
            {
                IAppender[] logAppenderList = LogManager.GetRepository().GetAppenders();

                if (logAppenderList != null)
                {
                    foreach (IAppender appender in logAppenderList)
                    {
                        RollingFileAppender rollingFileAppender = appender as RollingFileAppender;
                        if (rollingFileAppender != null)
                        {
                            FileAppender fileAppender = appender as FileAppender;
                            if (fileAppender != null)
                            {
                                String sourceFile = fileAppender.File;
                                appender.Close();

                                if (rollingFileAppender.RollingStyle == RollingFileAppender.RollingMode.Date)
                                {
                                    this.RenameCurrentLogFileByDate(sourceFile, rollingFileAppender.DatePattern);
                                }
                                else if (rollingFileAppender.RollingStyle == RollingFileAppender.RollingMode.Size)
                                {
                                    this.RenameCurrentLogFileBySize(sourceFile, rollingFileAppender.DatePattern, rollingFileAppender.MaxSizeRollBackups);
                                }
                                else if (rollingFileAppender.RollingStyle == RollingFileAppender.RollingMode.Composite)
                                {
                                    this.RenameCurrentLogFileBySize(sourceFile, rollingFileAppender.DatePattern, rollingFileAppender.MaxSizeRollBackups);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "/ * /" + e.StackTrace);
            }
        }

        private void RenameCurrentLogFileByDate(String logFileName, String dataPattern)
        {
            String sourceFile = logFileName;
            if (System.IO.File.Exists(sourceFile) == true)
            {
                String targetFile = logFileName + String.Format("{0:" + dataPattern + "}", DateTime.Now);
                System.IO.File.Move(sourceFile, this.GetRealBackupFileName(targetFile));
            }
        }

        private void RenameCurrentLogFileBySize(String logFileName, String dataPattern, int backupCount)
        {
            String sourceFile = logFileName;
            String targetFile = logFileName + String.Format("{0:" + dataPattern + "}", DateTime.Now);

            if (System.IO.File.Exists(sourceFile) == true)
            {
                System.IO.File.Move(sourceFile, targetFile);
            }

            for (int count = 1; count <= backupCount; count++)
            {
                sourceFile = logFileName + String.Format(".{0}", count);
                if (System.IO.File.Exists(sourceFile) == true)
                {
                    int lastIdx = sourceFile.LastIndexOf(".");
                    if (lastIdx > 0)
                    {
                        targetFile = logFileName.Substring(0, lastIdx) + String.Format("{0:" + dataPattern + "}.{1}", DateTime.Now, count);

                        System.IO.File.Move(sourceFile, this.GetRealBackupFileName(targetFile));
                    }
                }
            }
        }

        private string GetRealBackupFileName(String logFileName)
        {
            String targetFile = logFileName;
            int testCount = 0;
            while (testCount < 100)
            {
                bool isExist = System.IO.File.Exists(targetFile);
                if (isExist == false)
                {
                    break;
                }

                testCount++;

                targetFile = logFileName + String.Format(".{0}", testCount);
            }

            return targetFile;
        }
        #endregion

        #region EVENT HANDLER AREA *************************************
        private void Application_ThreadExit(object sender, EventArgs e)
        {
            this.RenameCurrentLogFile();
        }
        #endregion

    }
}
