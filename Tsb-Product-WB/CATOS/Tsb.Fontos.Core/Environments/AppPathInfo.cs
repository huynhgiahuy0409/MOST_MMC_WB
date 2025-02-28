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
* 2009.07.15    CHOI 1.0	First release.
* 2011.04.25  Tonny.Kim      Multiple DB Connection
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Util.File;
using System.IO;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Configuration.Provider;
using System.ComponentModel;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Application Directory Information class
    /// </summary>
    public class AppPathInfo : BaseEnvironmentInfo
    {
        new static string ObjectID = "GNR-FTCO-ENV-AppPathInfo";
        private static AppPathInfo _instance = new AppPathInfo();

        public static string PATH_APP_BASE;
        public static string PATH_USER_BASE;

        public const string FILE_EXT_APPCONFIG = ".config";
        public const string FILE_NAME_VSHOST = ".vshost";
        public const string FILE_NAME_APP_CONFIG = "App.config";

        //ENVIRONMENT RELATED AREA
        public static string PATH_ROOT_ENVIRONMENT;
        public static string PATH_ROOT_USER_ENVIRONMENT;
        public static string PATH_APP_ENVIRONMENT;
        public static string PATH_APP_USER_ENVIRONMENT;
        public static string FILE_NAME_ARCHITECTURE_INFO;
        public static string FILE_NAME_LOCALIZATION_INFO;
        public static string FILE_NAME_MENU_ITEM;
        public static string FILE_NAME_SHORTCUTKEY_INFO;
        public static string FILE_NAME_CONTEXT_MENU_ITEM;
        public static string FILE_NAME_MSGPOLICY_INFO;
        public static string FILE_NAME_DIAGNOSTICS_INFO;
        public static string FILE_NAME_MODULE_INFO;
        public static string FILE_NAME_RUNENV_INFO;
        public static string FILE_NAME_SECPOLICY_INFO;
        public static string FILE_NAME_SITE_INFO;
        public static string FILE_NAME_UISTYLE_INFO;
        public static string FILE_NAME_CUSTOM_UISTYLE_INFO;
        public static string FILE_NAME_VERSION_INFO;
        public static string FILE_NAME_GRIDPOLICY_INFO;
        public static string FILE_NAME_WEB_REST_SERVER;
        public static string FILE_NAME_LOGPOLICY_INFO;
        

        //CONTEXT RELATED AREA
        public static string PATH_ROOT_CONTEXT;
        public static string PATH_APP_CONTEXT;

        //LIBRARY RELATED AREA
        public static string PATH_ROOT_LIB;
        public const string PATH_SUB_LIB_ODP_NET = "ODP.Net";
        public const string PATH_SUB_LIB_IBATIS_NET = "IBatis.Net";
        public const string PATH_SUB_LIB_SPRING_NET = "Spring.Net";
        public const string PATH_SUB_LIB_LOG4_NET = "Log4.Net";
        public const string PATH_SUB_LIB_C1 = "C1";
        public const string PATH_SUB_LIB_FAR_POINT = "FarPoint";
        public const string PATH_SUB_LIB_FONTOS = "Fontos";


        //PERSISTENCE RELATED AREA
        public static string PATH_ROOT_PERSISTENCE;
        public static string FILE_NAME_PERSISTENCE_INFO;
        public static string FILE_NAME_SQLMAP_CONFIG;
        internal static string[] FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS;
        internal static string[] FILE_NAME_ADDITIONAL_MODULE_NAMES;
        public static string FILE_NAME_DB_PROVIDER_ASSEMBLY;

        //GRID RELATED ARA
        public static string PATH_ROOT_GRID;
        public static string PATH_APP_GRID;
        public static string PATH_SUB_SDK_CELLSTYLES;
        public static string PATH_SUB_FONTOS_CELL_STYLES;

        //BIZ CONFIG RELAGED AREA
        public static string PATH_ROOT_BIZCONFIG;
        public static string PATH_APP_BIZCONFIG;

        //MESSAGE SCHEMA AREA
        public static string PATH_ROOT_MESSAGE_SCHEMA;

        //HELP RELATED AREA
        public static string PATH_ROOT_HELP;
        public static string PATH_APP_HELP;
        public static string FILE_NAME_HELP_CHM;

        //LOG RELATED AREA
        public static string PATH_ROOT_LOG;
        public static string PATH_APP_LOG;


        //REPORT DOCUMENT RELATED ARA
        public static string PATH_ROOT_REPORT;
        public static string PATH_APP_REPORT;

        public static string PATH_APP_WEB_REST;

        /// <summary>
        /// Default Constructor
        /// </summary>
        static AppPathInfo()
        {
            InitializeAppPathInfo();
        }

        /// <summary>
        /// Verify Path
        /// </summary>
        /// <param name="path">PATH to verify</param>
        /// <param name="dirCheckRequired">whether dir check is required or not</param>
        /// <returns>Valid Path string</returns>
        private static string GetValidPath(string path, bool dirCheckRequired)
        {
            if (dirCheckRequired && !PathUtil.Exists(path))
            {
                throw new TsbSysConfigException(AppPathInfo.ObjectID, "MSG_FTCO_00123", DefaultMessage.NON_REG_WRD + path);
            }

            return path;
        }


        /// <summary>
        /// Returns configruation setting value from App config file
        /// </summary>
        /// <param name="configProvider">App Configuration Provider object reference</param>
        /// <param name="setting">setting key</param>
        /// <param name="dirCheckRequired">whether dir check is required or not</param>
        /// <returns>onfigruation setting value </returns>
        private static string GetValue(ref AppConfigProvider configProvider, string setting, bool dirCheckRequired)
        {
            string rtnValue = null;

            try
            {
                rtnValue = configProvider.GetValue(setting);

                if (dirCheckRequired && string.IsNullOrEmpty(rtnValue))
                {
                    //MSG: Application configuration file reading error. [setting-{0}] could not found in the {1} file.
                    throw new TsbSysConfigException(AppPathInfo.ObjectID, "MSG_FTCO_00008",
                        DefaultMessage.NON_REG_WRD + setting,
                        DefaultMessage.NON_REG_WRD + ConfigUtil.GetAppConfigPath());
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnValue == null ? string.Empty : rtnValue;
        }

        public static void InitializeAppPathInfo()
        {
            string moduleName = string.Empty;
            string tempValue = null;

            try
            {
                //DESIGN MODE CHECK
                if (DeployInfo.IsRuntime(CallingPositionTypes.CONSTRUCTOR))
                {
                    bool dirCheckRequired = ModuleInfo.CheckConfigDir;
                    bool useMultiDataSource = ModuleInfo.UseMultiDataSource;
                    AppConfigProvider configProvider = (AppConfigProvider)ConfigContext.GetAppConfigProvider();
                    moduleName = ModuleInfo.ModuleName;

                    PATH_APP_BASE = PathUtil.GetBasePath();
                    PATH_USER_BASE = PathUtil.GetBasePath(); //TODO: Set user roaming home directory

                    //ENVIRONMENT RELATED AREA----------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_ENVIRONMENT, dirCheckRequired);
                    PATH_ROOT_ENVIRONMENT = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), dirCheckRequired);
                    PATH_ROOT_USER_ENVIRONMENT = GetValidPath(Path.Combine(PATH_USER_BASE, tempValue), dirCheckRequired);
                    PATH_APP_ENVIRONMENT = GetValidPath(Path.Combine(PATH_ROOT_ENVIRONMENT, moduleName), dirCheckRequired);
                    PATH_APP_USER_ENVIRONMENT = GetValidPath(Path.Combine(PATH_ROOT_USER_ENVIRONMENT, moduleName), dirCheckRequired);
                    FILE_NAME_ARCHITECTURE_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_ARCHITECTURE_INFO, dirCheckRequired);
                    FILE_NAME_LOCALIZATION_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_LOCALIZATION_INFO, dirCheckRequired);
                    FILE_NAME_MENU_ITEM = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_MENU_ITEM, dirCheckRequired);
                    FILE_NAME_SHORTCUTKEY_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_SHORTCUTKEY_INFO, false);
                    FILE_NAME_MSGPOLICY_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_MSGPOLICY_INFO, dirCheckRequired);
                    FILE_NAME_MODULE_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_MODULE_INFO, dirCheckRequired);
                    FILE_NAME_SECPOLICY_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_SECPOLICY_INFO, dirCheckRequired);
                    FILE_NAME_SITE_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_SITE_INFO, dirCheckRequired);
                    FILE_NAME_RUNENV_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_RUNENV_INFO, dirCheckRequired);
                    FILE_NAME_VERSION_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_VERSION_INFO, dirCheckRequired);
                    FILE_NAME_UISTYLE_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_UISTYLE_INFO, dirCheckRequired);
                    FILE_NAME_CUSTOM_UISTYLE_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_CUSTOM_UISTYLE_INFO, false);
                    FILE_NAME_CONTEXT_MENU_ITEM = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_CONTEXT_MENU_ITEM, false);
                    FILE_NAME_GRIDPOLICY_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_GRIDPOLICY_INFO, false);

                    FILE_NAME_LOGPOLICY_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_LOGPOLICY_INFO, false);

                    


#if DEBUG
                    FILE_NAME_DIAGNOSTICS_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_DIAGNOSTICS_INFO, false);
#endif
                    //CONTEXT RELATED AREA--------------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_CONTEXT, dirCheckRequired);
                    PATH_ROOT_CONTEXT = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);
                    PATH_APP_CONTEXT = GetValidPath(Path.Combine(PATH_ROOT_CONTEXT, moduleName), false);


                    //LIBRARY RELATED AREA--------------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_LIB, dirCheckRequired);
                    PATH_ROOT_LIB = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);


                    //PERSISTENCE RELATED AREA ---------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_PERSISTENCE, dirCheckRequired);
                    PATH_ROOT_PERSISTENCE = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), dirCheckRequired);
                    FILE_NAME_PERSISTENCE_INFO = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_PERSISTENCE_INFO, dirCheckRequired);

                    PersistenceInfo.DatabaseInfo databaseInfo = PersistenceInfo.GetInstance().GetDatabaseInfo(ConfigConstant.SERVER_ROLE_MAIN_DATABASE);
                    FILE_NAME_SQLMAP_CONFIG = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_SQLMAP_CONFIG, dirCheckRequired);
                    
                    // Not Use Multi DataSource
                    if (!useMultiDataSource)
                    {
                        if (databaseInfo != null)
                        {
                            databaseInfo.SqlMapConfig = FILE_NAME_SQLMAP_CONFIG;
                        }
                    }

                    string additionalSqlFileNames = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_ADDITIONAL_SQLMAP_CONFIGS, false);

                    FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS = additionalSqlFileNames.Trim().Split(',');
                    FILE_NAME_ADDITIONAL_MODULE_NAMES = new string[FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS.Length];

                    if (FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS != null)
                    {
                        for (int i = 0; i < FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS.Length; i++)
                        {
                            if (FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS[i].Trim().Length > 6)
                            {
                                FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS[i] = FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS[i].Trim();
                                string additionalModuleName = FILE_NAME_ADDITIONAL_SQLMAP_CONFIGS[i].Remove(0, 7);

                                if (additionalModuleName.Length > 6)
                                {
                                    FILE_NAME_ADDITIONAL_MODULE_NAMES[i] = additionalModuleName.Remove(additionalModuleName.Length - 7, 7);  
                                }
                            }
                        }
                    }

                    if (databaseInfo != null &&
                        dirCheckRequired &&
                        databaseInfo.DBProduct == DBProductTypes.ORACLE)
                    {
                        FILE_NAME_DB_PROVIDER_ASSEMBLY = "Oracle.DataAccess.dll";
                    }
                    else
                    {
                        FILE_NAME_DB_PROVIDER_ASSEMBLY = string.Empty;
                    }


                    //GRID RELATED ARA-------------------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_GRID, false);
                    PATH_ROOT_GRID = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);
                    PATH_APP_GRID = GetValidPath(Path.Combine(PATH_ROOT_GRID, moduleName), false);
                    PATH_SUB_SDK_CELLSTYLES = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_GRID_SUB_CELLSTYLES_TSBSDK, false);
                    PATH_SUB_FONTOS_CELL_STYLES = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_GRID_SUB_CELLSTYLES_FONTOS, false);

                    //BIZ CONFIG RELAGED AREA------------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_BIZCONFIG, dirCheckRequired);
                    PATH_ROOT_BIZCONFIG = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);
                    PATH_APP_BIZCONFIG = GetValidPath(Path.Combine(PATH_ROOT_BIZCONFIG, moduleName), false);

                    //MESSAGE SCHEMA RELAGED AREA------------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_MESSAGE_SCHEMA, false);
                    PATH_ROOT_MESSAGE_SCHEMA = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);

                    //HELP RELATED AREA------------------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_HELP, dirCheckRequired);
                    PATH_ROOT_HELP = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);
                    PATH_APP_HELP = GetValidPath(Path.Combine(PATH_ROOT_HELP, moduleName), false);
                    FILE_NAME_HELP_CHM = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_HELP_CHM, false);


                    //LOG RELATED AREA-------------------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_LOG, dirCheckRequired);
                    PATH_ROOT_LOG = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);
                    PATH_APP_LOG = PATH_ROOT_LOG;

                    //REPORT RELATED ARA-------------------------------------------------------------------------------------------
                    tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_REPORT, false);
                    if(String.IsNullOrEmpty(tempValue) == true)
                    {
                        tempValue = "ReportDocument";
                    }
                    PATH_ROOT_REPORT = GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);
                    PATH_APP_REPORT = GetValidPath(Path.Combine(PATH_ROOT_REPORT, moduleName), false);

                    FILE_NAME_WEB_REST_SERVER = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_FILE_WEB_REST_SERVER, false);
                    //tempValue = GetValue(ref configProvider, ConfigConstant.APPCONFIG_KEY_PATH_WEB_REST, false);
                    PATH_APP_WEB_REST = PATH_APP_ENVIRONMENT; // GetValidPath(Path.Combine(PATH_APP_BASE, tempValue), false);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
    }
}
