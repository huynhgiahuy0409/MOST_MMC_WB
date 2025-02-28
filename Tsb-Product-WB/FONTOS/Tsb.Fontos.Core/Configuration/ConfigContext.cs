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
using Tsb.Fontos.Core.Configuration.Provider;
using System.Collections;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Context;
using Tsb.Fontos.Core.Util.File;
using System.IO;
using System.Reflection;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions.System;
using System.Diagnostics;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Configuration.Types;
using Tsb.Fontos.Core.Constant;

namespace Tsb.Fontos.Core.Configuration
{
    /// <summary>
    /// Configuration Context class
    /// </summary>
    public class ConfigContext : TsbBaseObject
    {
        private static volatile ConfigContext _instance;
        private static object syncRoot = new Object();
        private Hashtable _providerCache;

        private static readonly string _objectID = "GNR-FTCO-CFG-ConfigContext";

        /// <summary>
        /// Default Constructor
        /// </summary>
        private ConfigContext()
        {
           _providerCache = new Hashtable();
           this.ObjectID = ConfigContext._objectID;

        }

        /// <summary>
        /// Returns a reference to the current ConfigContext object for the application
        /// </summary>
        /// <returns>A reference to the current ConfigContext object</returns>
        public static ConfigContext GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigContext();
                    }
                }
            }
            
            return _instance;
        }

        /// <summary>
        /// Returns a Boolean value indicating whether config file provider is cached
        /// </summary>
        /// <param name="configProviderKey">Configation providers key</param>
        /// <returns>true if the config file provider is cached; otherwise, false</returns>
        public bool IsCached(string configProviderKey)
        {
            return this._providerCache.Contains(configProviderKey);
        }

        /// <summary>
        /// Add a configuratin provider to cache
        /// </summary>
        /// <param name="configProviderKey">Configation providers key</param>
        /// <param name="provider">configuration object</param>
        public void AddProviderToCache(string configProviderKey, IConfigProvider provider)
        {
            this._providerCache.Add(configProviderKey, provider);
        }


        /// <summary>
        /// Creates Configuration provider
        /// </summary>
        /// <param name="sourceType">Configuration source type like AppConfig, XML, Registry, INI file</param>
        /// <param name="configSourceName">Configuration source name like configuration file name</param>
        /// <param name="sectionIndicator">Indicator of section area like AppSetting in App.config file</param>
        /// <param name="settingIndicator">Indicator of setting area</param>
        /// <returns>Configuration provider object reference</returns>
        private IConfigProvider CreateProvider(ConfigSourceType sourceType, string configSourceName, string sectionIndicator, string settingIndicator)
        {
            IConfigProvider configProvider = null;
             
            switch(sourceType)
            {
                case ConfigSourceType.APPCONFIG:
                    configProvider = new AppConfigProvider(configSourceName, sectionIndicator, settingIndicator);
                    break;
                case ConfigSourceType.XML:
                    configProvider = new XmlConfigProvider(configSourceName, sectionIndicator, settingIndicator);
                    break;
                default:
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00002", null);
            }

            this.AddProviderToCache(configSourceName, configProvider);
            return configProvider;
        }

        /// <summary>
        /// Returns cached configuration provider object reference
        /// </summary>
        /// <param name="configProviderKey">Configation providers key</param>
        /// <returns>cached configuration provider object reference</returns>
        public IConfigProvider GetProviderFromCache(string configProviderKey)
        {
            return (IConfigProvider)this._providerCache[configProviderKey];
        }

        /// <summary>
        /// Returns Configuration provider object reference
        /// </summary>
        /// <param name="sourceType">Configuration source type like AppConfig, XML, Registry, INI file</param>
        /// <param name="configType">Configuration type like AppConfig, BizConfig, Persistence,Context</param>
        /// <param name="configSourceName">Configuration source name like configuration file name. If you want to use specific file, you shoud set absolute path</param>
        /// <param name="sectionXmlElement">XML Element string of section area like AppSetting in App.config file</param>
        /// <param name="settingXmlElement">XML Element string of setting area</param>
        /// <returns>Configuration provider object reference</returns>
        public static IConfigProvider GetProvider(ConfigSourceType sourceType, ConfigType configType, string configSourceName, string sectionXmlElement, string settingXmlElement)
        {
            string sourceNameWithFullPath = string.Empty;
            IConfigProvider rtnProvider = null;

            if (sourceType == ConfigSourceType.APPCONFIG || sourceType == ConfigSourceType.XML)
            {
                if (PathUtil.isAbsolutePath(configSourceName))
                    sourceNameWithFullPath = configSourceName;
                else
                    sourceNameWithFullPath = GetConfigPath(sourceType, configType);
            }
            else
            {
                throw new TsbSysConfigException(ConfigContext._objectID, "MSG_FTCO_00002", null);
            }

            if (ConfigContext.GetInstance().IsCached(sourceNameWithFullPath))
            {
                rtnProvider = ConfigContext.GetInstance().GetProviderFromCache(sourceNameWithFullPath);
            }
            else
            {
                rtnProvider = ConfigContext.GetInstance().CreateProvider(sourceType, sourceNameWithFullPath, sectionXmlElement, settingXmlElement);
            }

            return rtnProvider;
        }

        /// <summary>
        /// Returns App.Config configuration provider. Using this, you can access
        /// App.Config file which xml structure is pre-reserved file format.
        /// </summary>
        /// <returns>Configuration provider object reference</returns>
        public static IConfigProvider GetAppConfigProvider()
        {
            IConfigProvider rtnProvider = null;
            string fileName = string.Empty;

            //fileName = ProcessInfo.GetFullPathExeFileName()+ConfigDefaultConstant.FILE_EXT_APPCONFIG;
            fileName = ConfigUtil.GetAppConfigPath();

            if (ConfigContext.GetInstance().IsCached(fileName))
            {
                rtnProvider = ConfigContext.GetInstance().GetProviderFromCache(fileName);
            }
            else
            {
                rtnProvider = ConfigContext.GetInstance().CreateProvider(ConfigSourceType.APPCONFIG, fileName, ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION, ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SETTING);
            }

            return rtnProvider;
        }


        /// <summary>
        /// Returns XML file format configuration provider. If you want to use this, XML file should be existed in the reserved path
        /// </summary>
        /// <param name="fileNameWithBaseRelativePath">A relative file path on application base path like BizConfig/Managment/something.xml</param>
        /// <returns>Configuration provider object reference</returns>
        public static IConfigProvider GetXmlConfigProvider(string fileNameWithBaseRelativePath)
        {
            IConfigProvider rtnProvider = null;
            string fileName = string.Empty;

            if (PathUtil.isAbsolutePath(fileNameWithBaseRelativePath))
            {
                fileName = fileNameWithBaseRelativePath;
            }
            else
            {
                fileName = Path.Combine(PathUtil.GetBasePath(), fileNameWithBaseRelativePath);
            }

            if (ConfigContext.GetInstance().IsCached(fileName))
            {
                rtnProvider = ConfigContext.GetInstance().GetProviderFromCache(fileName);
            }
            else
            {
                rtnProvider = ConfigContext.GetInstance().CreateProvider(ConfigSourceType.XML, fileName, ConfigConstant.XML_ELE_XMLCONFIG_DEFAULT_SECTION, ConfigConstant.XML_ELE_XMLCONFIG_DEFAULT_SETTING);
            }

            return rtnProvider;
        }
        
        /// <summary>
        /// Returns Config Path string based on Module inforamtion
        /// </summary>
        /// <param name="sourceType">Configuration source type like AppConfig, XML, Registry, INI file</param>
        /// <param name="configType">Configuration type like AppConfig, BizConfig, Persistence,Context</param>
        /// <returns>Configuration Path string</returns>
        public static string GetConfigPath(ConfigSourceType sourceType, ConfigType configType)
        {
            string rtnPath  = string.Empty;

            if (sourceType == ConfigSourceType.APPCONFIG || sourceType == ConfigSourceType.XML)
            {
                switch (configType)
                {
                    case ConfigType.AppConfig:
                        rtnPath = AppPathInfo.PATH_APP_BASE;
                        break;
                    case ConfigType.BizConfig:
                        rtnPath = AppPathInfo.PATH_APP_BIZCONFIG;
                        break;
                    case ConfigType.Context:
                        rtnPath = AppPathInfo.PATH_APP_CONTEXT;
                        break;
                    //case ConfigType.Persistence:
                    //    rtnPath = AppPathInfo.PATH_APP_PERSISTENCE;
                    //    break;
                    case ConfigType.Spread:
                        rtnPath = AppPathInfo.PATH_APP_GRID;
                        break;
                    default:
                        throw new TsbSysConfigException(ConfigContext.GetInstance().ObjectID,"MSG_FTCO_00129","WRD_FTCO_ConfigurationType");
                }
            }
            else
            {
                throw new TsbSysConfigException(ConfigContext.GetInstance().ObjectID,"MSG_FTCO_00130","WRD_FTCO_Otherconfigurationtype");
            }

            return rtnPath;            
        }

    }
}
