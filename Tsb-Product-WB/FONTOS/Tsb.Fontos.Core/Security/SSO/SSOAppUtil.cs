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
using System.Diagnostics;
using Tsb.Fontos.Core.Util.File;
using System.IO;

namespace Tsb.Fontos.Core.Security.SSO
{
    public class SSOAppUtil
    {
        #region METHOD AREA **************************************
        /// <summary>
        /// Indicates whether the single sign-on agent was started.
        /// </summary>
        /// <returns>true if it was started, otherwise false.</returns>
        public static bool IsSSOAgentStarted()
        {
            bool isrunning = false;

            try
            {
                string mainModuleName = SSOConstant.SSO_MAIN_MODULE;
                isrunning = SSOAppUtil.IsAppRunning(mainModuleName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isrunning;
        }

        /// <summary>
        /// Run the single sign-on agent.
        /// if it is already running, not run the new single sign-on agent.
        /// </summary>
        public static void RunSSOAgent()
        {
            string mainModuleName = SSOConstant.SSO_MAIN_MODULE;
            string mainModuleNameExt = SSOConstant.SSO_MAIN_MODULE_EXT;
            string ssoAppName = string.Concat(mainModuleName, mainModuleNameExt);
            bool isrunning = SSOAppUtil.IsAppRunning(mainModuleName);


            if (isrunning == true)
            {
                return;
            }

            string basePath = PathUtil.GetBasePath();

            string[] filePaths = Directory.GetFiles(basePath, ssoAppName);

            if (filePaths != null && filePaths.Count() > 0)
            {
                if (isrunning == false)
                {
                    string exeFile = filePaths[0];
                    Process currentApplication = SSOAppUtil.Run(basePath, exeFile);
                }
            }
        }

        /// <summary>
        /// Runs the process resource that is specified by the parameter containing process start information 
        /// (for example, the file name of the process to start)
        /// </summary>
        /// <param name="workingDirectory">the initial directory for the process to be started.</param>
        /// <param name="executableFileName">the executable file name </param>
        /// <returns>
        ///  A new System.Diagnostics.Process component that is associated with the process
        ///  resource, or null if no process resource is started (for example, if an existing process is reused).
        /// </returns>
        public static Process Run(string workingDirectory, string executableFileName)
        {
            Process proc = null;
            try
            {
                ProcessStartInfo info = new ProcessStartInfo(executableFileName);
                info.WorkingDirectory = workingDirectory;
                info.Arguments = "External";
                proc = System.Diagnostics.Process.Start(info);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw ex;
            }

            return proc;
        }

        /// <summary>
        /// Indicates whether the specific process name was started.
        /// </summary>
        /// <param name="processName">the process name</param>
        /// <returns>true if it was started, otherwise false.</returns>
        public static bool IsAppRunning(string processName)
        {
            foreach (Process runningProcesse in Process.GetProcesses())
            {
                if (runningProcesse.ProcessName == processName)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
