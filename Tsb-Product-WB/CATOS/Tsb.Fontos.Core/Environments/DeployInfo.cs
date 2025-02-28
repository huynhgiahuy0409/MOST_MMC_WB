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
* 2009.08.15    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Exceptions.System;
using System.ComponentModel;
using Tsb.Fontos.Core.Constant;
using System.Windows.Forms;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Deployment information class
    /// </summary>
    public class DeployInfo : BaseEnvironmentInfo
    {
        #region FIELD AREA *************************************

        new static string ObjectID = "GNR-FTCO-ENV-DeployInfo";

        /// <summary>
        /// Deployment Mode ( PRODUCTION, TEST, DEVELOPMENT)
        /// </summary>
        public static readonly DeployModeTypes DeployMode;
        /// <summary>
        /// current mode is Run-Time value.
        /// </summary>
        private static bool _isRuntime = false;
        #endregion

        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Default Constructor
        /// </summary>
        static DeployInfo()
        {
            if (DeployInfo.IsRuntime(CallingPositionTypes.CONSTRUCTOR))
            {
                AppConfigProvider configProvider = (AppConfigProvider)ConfigContext.GetAppConfigProvider();

                string deployMode = configProvider.GetValue(ConfigConstant.APPCONFIG_KEY_DEPLOY_MODE);

                if (string.IsNullOrEmpty(deployMode))
                {
                    //MSG: Application configuration file reading error. [setting-{0}] could not found in the {1} file.
                    throw new TsbSysConfigException(DeployInfo.ObjectID, "MSG_FTCO_00008", ConfigConstant.APPCONFIG_KEY_DEPLOY_MODE, ConfigUtil.GetAppConfigPath());
                }

                try
                {
                    DeployInfo.DeployMode = (DeployModeTypes)Enum.Parse(typeof(DeployModeTypes), deployMode);
                }
                catch (ArgumentException argEx)
                {
                    //MSG:Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                    throw new TsbSysConfigException(argEx, DeployInfo.ObjectID, "MSG_FTCO_00004", ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_DEPLOY_MODE, ConfigUtil.GetAppConfigPath());
                }
            }
        }

        #endregion


        #region STATIC METHOD AREA (CHECK IS RUNTIME)***********
        /// <summary>
        /// Identifies current mode is Run-Time or not
        /// </summary>
        /// <param name="callingPositionType">Source code position type of calling this method</param>
        /// <returns>true if current mode is run-time</returns>
        public static bool IsRuntime(CallingPositionTypes callingPositionType)
        {
            try
            {
                switch (callingPositionType)
                {
                    case CallingPositionTypes.CONSTRUCTOR:
                    case CallingPositionTypes.EVENT_HANDLER:
                    case CallingPositionTypes.UI_CONTROL:
                    case CallingPositionTypes.GENERAL_METHOD:
                        {
                            if (_isRuntime == true)
                            {
                                return _isRuntime;
                            }

                            _isRuntime = !System.Diagnostics.Process.GetCurrentProcess().ProcessName.Equals("devenv");
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return _isRuntime;
        }

        #endregion
        
        #region STATIC METHOD AREA (CHECK IS DEV ENVIRONMENT)***
        /// <summary>
        /// Identifies current environment is a dev tool environment
        /// </summary>
        /// <returns>true if current mode is a dev tool environment</returns>
        public static bool IsDevToolEnvironment()
        {
            bool isDevEnv = false;

#if DEBUG
            isDevEnv = System.Diagnostics.Debugger.IsAttached;
#endif

            return isDevEnv;
        }
        #endregion
    }
}
