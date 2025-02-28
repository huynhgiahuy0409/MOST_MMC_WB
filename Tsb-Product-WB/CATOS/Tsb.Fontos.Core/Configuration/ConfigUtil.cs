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
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Util;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Environments;
using System.Xml;

namespace Tsb.Fontos.Core.Configuration
{
    /// <summary>
    /// Configuration Utility class
    /// </summary>
    public class ConfigUtil : BaseUtil
    {

        public const string OBJECT_ID = "GNR-FTCO-CFG-ConfigUtil";
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfigUtil()
            : base()
        {
            this.ObjectID = ObjectID;
        }

        /// <summary>
        /// Returns valid configuration setting value
        /// </summary>
        /// <param name="sourcePath">Config source file's path</param>
        /// <param name="sectionName">Section name value to read</param>
        /// <param name="settingName">Setting name value to read</param>
        /// <returns>A valid configuration setting value</returns>
        public static string GetValidValueFromXml(string sourcePath, string sectionName, string settingName)
        {
            string rtnValue = string.Empty;
            object rtnObject = null;
            XmlConfigProvider configProvider = null;

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(sourcePath);

                rtnObject = configProvider.GetValue(sectionName, settingName);

                if (rtnObject != null)
                {
                    rtnValue = rtnObject.ToString().Trim();
                }

                if (string.IsNullOrEmpty(rtnValue))
                {
                    //MSG:Configuration file reading error. [section-{0}][setting-{1}] could not found in {2} file.
                    throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00003", sectionName, settingName, sourcePath);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG:An error occurred when opening or reading the configuration file.
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), OBJECT_ID, "MSG_FTCO_00006", null);
            }

            return rtnValue;
        }

        /// <summary>
        /// Returns valid configuration setting attribute value
        /// </summary>
        /// <param name="sourcePath">Config source file's path</param>
        /// <param name="sectionName">Section name value to read</param>
        /// <param name="settingName">Setting name value to read</param>
        /// <param name="attributeName">Attribute name value to read</param>
        /// <returns>A valid configuration setting value</returns>
        public static string GetValidValueFromXmlExceptionNull(string sourcePath, string sectionName, string settingName, string attributeName)
        {
            string rtnValue = string.Empty;
            object rtnObject = null;
            XmlConfigProvider configProvider = null;

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(sourcePath);

                rtnObject = configProvider.GetAttributeValue(sectionName, settingName, attributeName);

                if (rtnObject != null)
                {
                    rtnValue = rtnObject.ToString().Trim();
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG:An error occurred when opening or reading the configuration file.
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), OBJECT_ID, "MSG_FTCO_00006", null);
            }

            return rtnValue;
        }

        /// <summary>
        /// Returns valid configuration setting value
        /// </summary>
        /// <param name="sourcePath">Config source file's path</param>
        /// <param name="sectionName">Section name value to read</param>
        /// <param name="settingName">Setting name value to read</param>
        /// <returns>A valid configuration setting value</returns>
        public static string GetValidValueFromXmlExceptionNull(string sourcePath, string sectionName, string settingName)
        {
            string rtnValue = string.Empty;
            object rtnObject = null;
            XmlConfigProvider configProvider = null;

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(sourcePath);

                rtnObject = configProvider.GetValue(sectionName, settingName);

                if (rtnObject != null)
                {
                    rtnValue = rtnObject.ToString().Trim();
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG:An error occurred when opening or reading the configuration file.
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), OBJECT_ID, "MSG_FTCO_00006", null);
            }

            return rtnValue;
        }

        /// <summary>
        /// Returns valid type which is converted suitable architecture type
        /// </summary>
        /// <typeparam name="T">type to convert</typeparam>
        /// <param name="settingValue">setting value string</param>
        /// <param name="sectionName">section name</param>
        /// <param name="settingName">setting name</param>
        /// <returns>converted valid type</returns>
        public static T GetValidValueFromXml<T>(string settingValue, string sectionName, string settingName, string fileName)
        {
            T rtnEnumType;

            try
            {
                rtnEnumType = (T)Enum.Parse(typeof(T), settingValue);
            }
            catch (ArgumentException argEx)
            {
                //MSG:Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                throw new TsbSysConfigException(argEx, OBJECT_ID, "MSG_FTCO_00004", sectionName, settingName, fileName);
            }


            return rtnEnumType;
        }


        /// <summary>
        /// Returns whether a specified new value is diffrent from a current value or not
        /// </summary>
        /// <param name="sourcePath">Config source file's path</param>
        /// <param name="sectionName">Section name value to read</param>
        /// <param name="settingName">Setting name value to read</param>
        /// <param name="newValue">New value to specified setting name</param>
        /// <returns>A valid configuration setting value</returns>
        public static bool IsNewXmlValue(string sourcePath, string sectionName, string settingName, string newValue)
        {
            bool isNew = false;
            string currValue = string.Empty;
            XmlConfigProvider configProvider = null;

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(sourcePath);
                isNew = ConfigUtil.IsNewXmlValue(configProvider, sectionName, settingName, newValue);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG:An error occurred when opening or reading the configuration file.
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), OBJECT_ID, "MSG_FTCO_00006", null);
            }

            return isNew;
        }


        /// <summary>
        /// Returns whether a specified new value is diffrent from a current value or not
        /// </summary>
        /// <param name="provider">XmlConfigProvider object reference</param>
        /// <param name="sectionName">Section name value to read</param>
        /// <param name="settingName">Setting name value to read</param>
        /// <param name="newValue">New value to specified setting name</param>
        /// <returns>A valid configuration setting value</returns>
        public static bool IsNewXmlValue(XmlConfigProvider configProvider, string sectionName, string settingName, string newValue)
        {
            bool isNew = false;
            string currValue = string.Empty;

            try
            {
                currValue = configProvider.GetValue(sectionName, settingName).ToString().Trim();

                if (!currValue.Equals(newValue))
                {
                    isNew = true;
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG:An error occurred when opening or reading the configuration file.
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), OBJECT_ID, "MSG_FTCO_00006", null);
            }

            return isNew;
        }


        /// <summary>
        /// Returns Exe File's App.Config path
        /// </summary>
        /// <returns>Exe File's App.Config path</returns>
        public static string GetAppConfigPath()
        {
            return ProcessInfo.GetFullPathExeFileName() + AppPathInfo.FILE_EXT_APPCONFIG;
        }

    }
}
