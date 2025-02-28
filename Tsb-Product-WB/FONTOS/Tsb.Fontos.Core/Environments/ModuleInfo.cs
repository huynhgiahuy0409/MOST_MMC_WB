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
* 2009.07.08    CHOI 1.0	First release.
* 2011.04.22  Tonny.Kim     UseMultiDataSource
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Context;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Util.Type;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Encapsulates information about a specific business module
    /// </summary>
    public class ModuleInfo : BaseEnvironmentInfo
    {
        #region FIELD/PROPERTY AREA*****************************

        private const string OBJECT_ID = "GNR-FTCO-CFG-ModuleInfo";

        /// <summary>
        /// Module's Production ID (like CT, FT, CA)
        /// </summary>
        public static string ProductID = null;

        /// <summary>
        /// Module's Production Name (like CATOS, FONTOS, CASP)
        /// </summary>
        public static string ProductName = null;

        /// <summary>
        /// The ID of the Module like CTBP (CATOS Birth planning), CTSP, BP...)
        /// </summary>
        public static string ModuleID = null;

        /// <summary>
        /// The Name of the Module (like Rail, ShipPlanning...)
        /// </summary>
        public static string ModuleName = null;

        /// <summary>
        /// The title of Module(Operation Management, Ship Planning...)
        /// </summary>
        public static string ModuleTitle = null;

        /// <summary>
        /// The Code of Program(OM/DG/SP)
        /// </summary>
        public static string  PgmCode= null;

        /// <summary>
        /// Whether configuration check is required or not
        /// </summary>
        public static bool  CheckConfigDir = false;

        /// <summary>
        /// Whether using Multi DataSource or not
        /// </summary>
        public static bool UseMultiDataSource = false;

		/// <summary>
		/// Whether using catos config or not
		/// </summary>
		public static bool UseCatosConfig = false;

        /// <summary>
        /// Whether using print preview show type is modal or Modeless 
        /// </summary>
        public static bool PrintPreviewShowModal = true;

        /// <summary>
        /// Whether using report preview show type is modal or Modeless 
        /// </summary>
        public static bool ReportPreviewShowModal = true;

        /// <summary>
        /// Whether using Performance Log write
        /// </summary>
        public static bool UsePerformanceLog = false;

        /// <summary>
        /// Reference mode of rule set in the Container Editor.
        /// </summary>
        public static string ContainerEditorRuleSetRefMode = null;

        /// <summary>
        /// Whether using usage data collect
        /// </summary>
        public static bool UsageDataCollection = false;
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Static Constructor
        /// </summary>
        static ModuleInfo()
        {
            BaseConfigProvider configProvider = null;
            string strCheckDirRequired  = string.Empty;
            string checkTarget = string.Empty;

            //DESIGN MODE CHECK
            if (DeployInfo.IsRuntime(CallingPositionTypes.GENERAL_METHOD))
            {
                configProvider = (AppConfigProvider)ConfigContext.GetAppConfigProvider();

                try
                {
                    checkTarget = ConfigConstant.APPCONFIG_KEY_MODULE_ID;
                    ModuleInfo.ModuleID = configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_MODULE_ID).ToString();
                    if (string.IsNullOrEmpty(ModuleInfo.ModuleID))
                    {
                        //MSG:Application config file reading error. [section-{0}][key-{1}] could not found in {2} file.	
                        throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00073",
                            DefaultMessage.NON_REG_WRD + ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION,
                            DefaultMessage.NON_REG_WRD + checkTarget,
                            "WRD_FTCO_Applicationcontextfile"
                            );
                    }

                    checkTarget = ConfigConstant.APPCONFIG_KEY_MODULE_NAME;
                    ModuleInfo.ModuleName = configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_MODULE_NAME).ToString();
                    if (string.IsNullOrEmpty(ModuleInfo.ModuleName))
                    {
                        //MSG:Application config file reading error. [section-{0}][key-{1}] could not found in {2} file.	
                        throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00073",
                            DefaultMessage.NON_REG_WRD + ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION,
                            DefaultMessage.NON_REG_WRD + checkTarget,
                            "WRD_FTCO_Applicationcontextfile"
                            );
                    }

                    checkTarget = ConfigConstant.APPCONFIG_KEY_PROD_ID;
                    ModuleInfo.ProductID = configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_PROD_ID).ToString();
                    if (string.IsNullOrEmpty(ModuleInfo.ProductID))
                    {
                        //MSG:Application config file reading error. [section-{0}][key-{1}] could not found in {2} file.	
                        throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00073",
                            DefaultMessage.NON_REG_WRD + ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION,
                            DefaultMessage.NON_REG_WRD + checkTarget,
                            "WRD_FTCO_Applicationcontextfile"
                            );
                    }

                    checkTarget = ConfigConstant.APPCONFIG_KEY_PROD_NAME;
                    ModuleInfo.ProductName = configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_PROD_NAME).ToString();
                    if (string.IsNullOrEmpty(ModuleInfo.ProductName))
                    {
                        //MSG:Application config file reading error. [section-{0}][key-{1}] could not found in {2} file.	
                        throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00073",
                            DefaultMessage.NON_REG_WRD + ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION,
                            DefaultMessage.NON_REG_WRD + checkTarget,
                            "WRD_FTCO_Applicationcontextfile"
                            );
                    }

                }
                catch (Exception ex)
                {
                    //MSG:Application config file reading error. [section-{0}][key-{1}] could not found in {2} file.	
                    ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException),OBJECT_ID, "MSG_FTCO_00073",
                        DefaultMessage.NON_REG_WRD + ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION,
                        DefaultMessage.NON_REG_WRD + checkTarget,
                        "WRD_FTCO_Applicationcontextfile"
                        );
                }


                try
                {
                    strCheckDirRequired = configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_CHECK_CONFIG_DIR).ToString();
                }
                catch (Exception ex)
                {
                    //MSG:Application config file reading error. [section-{0}][key-{1}] could not found in {2} file.	
                    ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), OBJECT_ID, "MSG_FTCO_00073",
                        DefaultMessage.NON_REG_WRD + ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION,
                        DefaultMessage.NON_REG_WRD + ConfigConstant.APPCONFIG_KEY_CHECK_CONFIG_DIR,
                        "WRD_FTCO_Applicationcontextfile"
                        );
                }


                if (!string.IsNullOrEmpty(strCheckDirRequired))
                {
                    try
                    {
                        ModuleInfo.CheckConfigDir = Boolean.Parse(strCheckDirRequired);
                    }
                    catch (Exception ex)
                    {
                        //MSG:Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                        ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), OBJECT_ID,
                            "MSG_FTCO_00004",
                            ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION,
                            ConfigConstant.APPCONFIG_KEY_CHECK_CONFIG_DIR,
                            ConfigUtil.GetAppConfigPath()
                            );
                    }
                }

                //ModuleTitle Setting
                try
                {
                    ModuleInfo.ModuleTitle = configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_MODULE_TITLE).ToString();
                    if (string.IsNullOrEmpty(ModuleInfo.ModuleTitle))
                    {
                        ModuleInfo.ModuleTitle = typeof(ModuleInfo).Assembly.GetName().ToString();
                    }
                }
                catch (Exception)
                {
                    ModuleInfo.ModuleTitle = typeof(ModuleInfo).Assembly.GetName().Name.ToString();
                }

                //PgmCode Setting
                try
                {
                    ModuleInfo.PgmCode = configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_PGM_CODE).ToString();
                    if (string.IsNullOrEmpty(ModuleInfo.PgmCode))
                    {
                        ModuleInfo.PgmCode = ModuleInfo.ModuleID.Substring(2, 2);
                    }
                }
                catch (Exception)
                {
                    ModuleInfo.PgmCode = ModuleInfo.ModuleID.Substring(2, 2);
                }

				try
				{
                    string strAppCfgValue = Convert.ToString(configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_ENV_USE_CATOS_CONFIG));

					if (string.IsNullOrEmpty(strAppCfgValue))
					{
						UseCatosConfig = false;
					}
					else
					{
						UseCatosConfig = ConvertUtil.ToBoolean(strAppCfgValue);
					}
				}
				catch (Exception)
				{
					UseCatosConfig = false;
				}

                try
                {
                    string strAppCfgValue = 
                        Convert.ToString(configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_ENV_USE_MULTI_DATASOURCE));

                    if (string.IsNullOrEmpty(strAppCfgValue))
                    {
                        UseMultiDataSource = false;
                    }
                    else
                    {
                        UseMultiDataSource = ConvertUtil.ToBoolean(strAppCfgValue);
                    }
                }
                catch (Exception)
                {
                    UseCatosConfig = false;
                }

                try
                {
                    string printPreviewDisplay =
                        Convert.ToString(configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_ENV_PRINT_PREVIEW_SHOW_MODAL));

                    if (string.IsNullOrEmpty(printPreviewDisplay))
                    {
                        PrintPreviewShowModal = true;
                    }
                    else
                    {
                        PrintPreviewShowModal = ConvertUtil.ToBoolean(printPreviewDisplay);
                    }
                }
                catch (Exception)
                {
                    PrintPreviewShowModal = true;
                }


                try
                {
                    string reportPreviewDisplay =
                        Convert.ToString(configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_ENV_REPORT_PREVIEW_SHOW_MODAL));

                    if (string.IsNullOrEmpty(reportPreviewDisplay))
                    {
                        ReportPreviewShowModal = true;
                    }
                    else
                    {
                        ReportPreviewShowModal = ConvertUtil.ToBoolean(reportPreviewDisplay);
                    }
                }
                catch (Exception)
                {
                    PrintPreviewShowModal = true;
                }

                try
                {
                    string usageDataCollection =
                           Convert.ToString(configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_ENV_USAGE_DATA_COLLECTION));

                    if (string.IsNullOrEmpty(usageDataCollection))
                    {
                        UsageDataCollection = false;
                    }
                    else
                    {
                        UsageDataCollection = ConvertUtil.ToBoolean(usageDataCollection);
                    }
                }
                catch (Exception)
                {
                    UsageDataCollection = false;
                }

#if DEBUG
                try
            {
                    string perforamnceLogValue =
                        Convert.ToString(configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_ENV_USE_PERFORMANCE_LOG));

                    if (string.IsNullOrEmpty(perforamnceLogValue))
                    {
                        UsePerformanceLog = false;
                    }
                    else
                    {
                        UsePerformanceLog = ConvertUtil.ToBoolean(perforamnceLogValue);
                    }
                }
                catch (Exception)
                {
                    UsePerformanceLog = false;
                }
#endif

                try
                {
                    object value = configProvider.GetValue(ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.APPCONFIG_KEY_ENV_CONTAINER_EDITOR_RULESET_REF_MODE);
                    
                    if (value != null)
                    {
                        ContainerEditorRuleSetRefMode = value.ToString();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="id">The ID of the Module</param>
        /// <param name="name">The Name of the Module</param>
        public ModuleInfo() : base()
        {
            this.ObjectID = OBJECT_ID;
        }
        #endregion
    }
}
