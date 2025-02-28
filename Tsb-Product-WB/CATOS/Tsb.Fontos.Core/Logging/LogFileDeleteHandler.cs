#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2022 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		    REVISION    	
* 2022.08.10     Jindols 1.0    	First Release
* 
*/
#endregion


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Logging
{
    public class LogFileDeleteHandler
    {
        private LogPolicyInfo _config;
        public LogFileDeleteHandler(LogPolicyInfo config)
        {
            _config = config;
        }

        public void DeleteLogFile()
        {
            if (_config.DeleteEnable == false)
            {
                GeneralLogger.Info("The log deletion function is disabled.");
                return;
            }

            if (_config.IsExistLogPolicyInfoFile == false)
            {
                GeneralLogger.Warn("The LogPolicyInfo.xml does not exist.");
                return;
            }


            if(string.IsNullOrEmpty(_config.LogFolder) == true)
            {
                GeneralLogger.Warn("The logFolder property value is empty at LogPolicyInfo.xml");
                return;
            }

            //삭제할 파일들이 있는 경로 설정

            //string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            string rootPath = AppPathInfo.PATH_APP_BASE;
            string logDirPath = Path.Combine(rootPath, _config.LogFolder);
            int keepDays = _config.DeleteCheckNumber;
            string logFileSearchKey = _config.LogFileSearchKey;

            GeneralLogger.Info("Log folder path : " + logDirPath);

            if (keepDays <= 0)
            {
                keepDays = 1;
            }

            //if (string.IsNullOrWhiteSpace(_config.LogFolder) == true)
            if (string.IsNullOrEmpty(_config.LogFolder) == true || string.IsNullOrEmpty(_config.LogFolder.Trim()) == true)
            {
                GeneralLogger.Info("The log deletion target LogFolder does not exist.(LogFolder at LogPolicyInfo.xml)");
                return;
            }


            if (Directory.Exists(logDirPath) == false)
            {
                //삭제할 경로가 존재하지 않으면
                GeneralLogger.Info(string.Format("The log folder does not exist. : {0}  [{1}]", logDirPath));
                return;
            }

            DirectoryInfo di = new DirectoryInfo(logDirPath);

            //if (string.IsNullOrWhiteSpace(_config.LogFileSearchKey) == true)
            if (string.IsNullOrEmpty(_config.LogFileSearchKey) == true || string.IsNullOrEmpty(_config.LogFileSearchKey.Trim()) == true)
            {
                GeneralLogger.Info("The log deletion target file search key does not exist.(LogFileSearchKey at LogPolicyInfo.xml)");
                return;
            }

            //삭제할 경로가 존재하면
            if (di.Exists)
            {
                FileInfo[] files = di.GetFiles();

                List<FileInfo> filtedFileList = new List<FileInfo>();

                foreach (FileInfo fileInfo in files)
                {
                    //확장자가 .log인 파일들 지워라
                    if (System.Text.RegularExpressions.Regex.IsMatch(fileInfo.Name, logFileSearchKey))
                    {
                        filtedFileList.Add(fileInfo);
                    }
                }

                if(_config.DeleteCheckType == DeleteCheckStyle.Day)
                {
                    this.DeleteLogFlieByDay(filtedFileList);

                }else if (_config.DeleteCheckType == DeleteCheckStyle.File)
                {
                    this.DeleteLogFlieByFile(filtedFileList);
                }
                else
                {
                    throw new NotSupportedException("Log Deleteed DeleteCheckType");
                }
            }
        }


        private void DeleteLogFlieByDay(List<FileInfo> filtedFileList)
        {
            if(filtedFileList == null)
            {
                return;
            }

            int keepDays = _config.DeleteCheckNumber;
            string logFileSearchKey = _config.LogFileSearchKey;
            string rootPath = AppPathInfo.PATH_APP_BASE;
            string logDirPath = Path.Combine(rootPath, _config.LogFolder);

            DateTime today = DateTime.Today;

            foreach (FileInfo file in filtedFileList)
            {
                //파일의 마지막 쓰여진 시간과 date 날짜와 비교
                //TimeSpan diffDates = today.Subtract(file.LastWriteTime);
                DateTime tempToday = new DateTime(today.Year, today.Month, today.Day);
                DateTime tempLastWroteTime = new DateTime(file.LastWriteTime.Year, file.LastWriteTime.Month, file.LastWriteTime.Day);

                TimeSpan diffDates = tempToday.Subtract(tempLastWroteTime);

                //Console.WriteLine(" --> {0} [{1}]", file.Name, diffDates.Days);

                if (diffDates.Days > keepDays)
                {
                    //Console.WriteLine("DiffDate : " + diffDates.Days);

                    //확장자가 .log인 파일들 지워라
                    if (System.Text.RegularExpressions.Regex.IsMatch(file.Name, logFileSearchKey))
                    {

                        string deleteFilePath = Path.Combine(logDirPath, file.Name);

                        try
                        {
                            if (File.Exists(deleteFilePath))
                            {
                               File.Delete(deleteFilePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            GeneralLogger.Error(ex);
                            MessageBox.Show(ex.ToString());
                        }


                        GeneralLogger.Info(string.Format("DELETE LOG FILE : {0}  [{1}]", file.Name, diffDates.Days));

                        //Console.WriteLine("{0}  [{1}] 파일 삭제 완료 ", file.Name, diffDates.Days);

                    }
                }
            }
        }

        private void DeleteLogFlieByFile(List<FileInfo> filtedFileList)
        {
            if (filtedFileList == null)
            {
                return;
            }

            int keepCount = _config.DeleteCheckNumber;
            string logFileSearchKey = _config.LogFileSearchKey;
            string rootPath = AppPathInfo.PATH_APP_BASE;
            string logDirPath = Path.Combine(rootPath, _config.LogFolder);

            List<FileInfo> sortedFileList = filtedFileList.OrderByDescending(c => c.LastWriteTime).ToList();
            int checkCount = 0;

            foreach (FileInfo file in sortedFileList)
            {

                //Console.WriteLine("DiffDate : " + diffDates.Days);

                //확장자가 .log인 파일들 지워라
                if (System.Text.RegularExpressions.Regex.IsMatch(file.Name, logFileSearchKey))
                {
                    if(checkCount> keepCount)
                    {
                        string deleteFilePath = Path.Combine(logDirPath, file.Name);

                        try
                        {
                            if (File.Exists(deleteFilePath))
                            {
                                File.Delete(deleteFilePath);
                            }

                            GeneralLogger.Info(string.Format("DELETE LOG FILE : {0}  [{1}]", file.Name, checkCount));
                        }
                        catch (Exception ex)
                        {
                            GeneralLogger.Error(ex);
                            MessageBox.Show(ex.ToString());
                        }
                    }

                    checkCount++;


                    

                    //Console.WriteLine("{0}  [{1}] 파일 삭제 완료 ", file.Name, diffDates.Days);

                }
            }
        }
    }
}
