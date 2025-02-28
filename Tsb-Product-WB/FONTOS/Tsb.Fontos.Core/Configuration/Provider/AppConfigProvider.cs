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
using System.Configuration;
using System.Xml;
using Tsb.Fontos.Core.Xml;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Configuration.Event;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Configuration.Provider
{
    /// <summary>
    /// Configuration Provider class that utilizes an XML-formatted .config file to retrieve and save its data.
    /// </summary>
    public class AppConfigProvider : BaseConfigProvider
    {
        private System.Configuration.Configuration _configuration;

        private string _sectionElement;
        private string _settingElement;
        private string _keyAttribute;
        private string _valueAttribute;


        public string SectionElement
        {
            get { return _sectionElement; }
        }

        public string SettingElement
        {
            get { return _settingElement; }
        }

        public string KeyAttribute
        {
            get { return _keyAttribute; }
        }

        public string ValueAttribute
        {
            get { return _valueAttribute; }
        }

        /// <summary>
        /// Initializes a new instance of the Config class
        /// </summary>
        private AppConfigProvider()
            : base()
        {
            this._sectionElement = ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SECTION;
            this._settingElement = ConfigConstant.XML_ELE_APPCONFIG_DEFAULT_SETTING;
            this._keyAttribute = ConfigConstant.XML_ATT_APPCONFIG_DEFAULT_KEY;
            this._valueAttribute = ConfigConstant.XML_ATT_APPCONFIG_DEFAULT_VALUE;
            this.ObjectID = "GNR-FTCO-CFG-AppConfigProvider";
        }

        /// <summary>
        /// Initializes a new instance of the Config class by the given file name.
        /// </summary>
        /// <param name="fileName">The name of the Config file</param>
        public AppConfigProvider(string fileName)
            : this()
        {
            ExeConfigurationFileMap fileMap = null;

            try
            {
                if (fileName.ToLower().Contains(AppPathInfo.FILE_EXT_APPCONFIG))
                {
                    this._sourceName = PathUtil.GetFileFullPath(fileName);
                    fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = this.SourceName;

                    this._configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                }
                else
                {
                    ///TODO:throw new TsbSysConfigException(new InvalidOperationException("Application Configuration file should end with .config extention."));
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Initializes a new instance of the  App Configuation Provider class using the given file name, section element, setting element.
        /// This class is used for custom defiend configuration xml
        /// </summary>
        /// <param name="fileName">The name of the config file to initialize</param>
        /// <param name="sectionXmlElement">String value of section element name</param>
        /// <param name="settingXmlElement">String value of setting element name</param>
        public AppConfigProvider(string fileName, string sectionXmlElement, string settingXmlElement)
            : this(fileName)
        {
            this._sectionElement = sectionXmlElement;
            this._settingElement = settingXmlElement;
        }


        /// <summary>
        /// Sets the value for an setting inside a section.</summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting where the value will be set. </param>
        /// <param name="value">The value to set. If it's null, the setting is removed. </param>
        public override void SetValue(string section, string setting, object value)
        {
            XmlDocument xmlDoc = null;
            XmlElement xmlRoot = null;
            XmlNodeList settingNodes = null;

            try
            {
                if (String.IsNullOrEmpty(section))
                    section = this.SectionElement;

                if (value == null)
                {
                    this.RemoveSetting(section, setting);
                    return;
                }

                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;

                settingNodes = xmlRoot.SelectNodes(section + "/" + this.SettingElement);

                if (settingNodes.Count == 0)
                {
                    this.CreateSetting(null, setting, value);
                }
                else
                {
                    foreach (XmlNode childNode in settingNodes)
                    {
                        if (childNode.Attributes[this.KeyAttribute].Value.Equals(setting))
                            childNode.Attributes[this.ValueAttribute].Value = value.ToString();
                    }

                    xmlDoc.Save(this.SourceName);

                    this._configuration.Save(ConfigurationSaveMode.Full);
                    ConfigurationManager.RefreshSection(section);

                    FireConfigChangedEvent(ConfigChangedType.ValueChanged, section, setting, value);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }

        /// <summary>
        /// Retrieves the value of an setting inside a section.
        /// </summary>
        /// <param name="section">The name of the section that holds the setting with the value.</param>
        /// <param name="setting">The name of the setting where the value is stored.</param>
        /// <returns>setting's value, or null if the setting does not exist.</returns>
        public override object GetValue(string section, string setting)
        {
            XmlDocument xmlDoc = null;
            XmlElement xmlRoot = null;
            XmlNodeList settingNodes = null;

            string rtnValue = null;

            try
            {
                if (string.IsNullOrEmpty(section))
                    section = this.SectionElement;

                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;

                settingNodes = xmlRoot.SelectNodes(section + "/" + this.SettingElement);

                foreach (XmlNode childNode in settingNodes)
                {
                    if (childNode.Attributes[this.KeyAttribute].Value.Equals(setting))
                    {
                        rtnValue = childNode.Attributes[this.ValueAttribute].Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnValue;
        }

        /// <summary>
        /// Retrieves the attribute value of an setting inside a section.
        /// </summary>
        /// <param name="section">The name of the section that holds the setting with the value.</param>
        /// <param name="setting">The name of the setting that holds the setting with the value.</param>
        /// <param name="attributeName">The attribute name of the setting where the value is stored.</param>
        /// <returns>setting's value, or null if the setting does not exist.</returns>
        public override string GetAttributeValue(string section, string setting, string attributeName)
        {
            XmlDocument xmlDoc = null;
            XmlElement xmlRoot = null;
            XmlNodeList settingNodes = null;

            string rtnValue = null;

            try
            {
                if (string.IsNullOrEmpty(section))
                    section = this.SectionElement;

                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;

                settingNodes = xmlRoot.SelectNodes(section + "/" + this.SettingElement);

                foreach (XmlNode childNode in settingNodes)
                {
                    if (childNode.Attributes[this.KeyAttribute].Value.Equals(setting))
                    {
                        rtnValue = childNode.Attributes[attributeName].Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnValue;
        }

        /// <summary>
        /// Retrieves the value 
        /// </summary>
        /// <param name="setting">The name of the setting where the value is stored.</param>
        /// <returns>setting's value, or null if the setting does not exist.</returns>
        public string GetValue(string setting)
        {
            string rtnValue = null;
            KeyValueConfigurationElement configElement = null;

            try
            {
                configElement = this._configuration.AppSettings.Settings[setting];

                if (configElement != null)
                {
                    rtnValue = configElement.Value;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnValue;
        }

        /// <summary>
        /// Removes a section.
        /// </summary>
        /// <param name="section"> The name of the section to remove. </param>
        public override void RemoveSection(string section)
        {
            XmlDocument xmlDoc = null;
            XmlElement xmlRoot = null;
            XmlNode sectionNode = null;

            try
            {
                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;

                if (xmlRoot == null)
                    return;

                if (string.IsNullOrEmpty(section))
                    section = this.SectionElement;

                sectionNode = xmlRoot.SelectSingleNode(section);

                if (sectionNode == null)
                    return;

                xmlRoot.RemoveChild(sectionNode);

                xmlDoc.Save(this.SourceName);
                this._configuration.Save(ConfigurationSaveMode.Full);

                FireConfigChangedEvent(ConfigChangedType.SectionRemoved, section, null, null);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }

        /// <summary>
        /// Create a section.
        /// </summary>
        public override void CreateSection(string section)
        {
            XmlDocument xmlDoc = null;
            XmlElement xmlRoot = null;
            XmlNode sectionNode = null;

            try
            {
                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;

                if (xmlRoot == null)
                    return;

                sectionNode = xmlDoc.CreateNode(XmlNodeType.Element, this.SectionElement, null);
                xmlRoot.AppendChild(sectionNode);

                xmlDoc.Save(this.SourceName);
                this._configuration.Save(ConfigurationSaveMode.Full);

                FireConfigChangedEvent(ConfigChangedType.SectionCreated, this.SectionElement, null, null);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }


        /// <summary>
        /// Removes an setting from a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting to remove. </param>
        public override void RemoveSetting(string section, string setting)
        {
            XmlDocument xmlDoc = null;
            XmlElement xmlRoot = null;
            XmlNode sectionNode = null;
            bool isModified = false;

            try
            {
                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;

                if (String.IsNullOrEmpty(section))
                    section = this.SectionElement;

                sectionNode = xmlRoot.SelectSingleNode(section);

                if (sectionNode == null)
                    return;

                foreach (XmlNode childNode in sectionNode.ChildNodes)
                {
                    if (childNode.Attributes[this.KeyAttribute].Value.Equals(setting))
                    {
                        sectionNode.RemoveChild(childNode);
                        isModified = true;
                    }
                }

                if (isModified)
                {
                    xmlDoc.Save(this.SourceName);
                    this._configuration.Save(ConfigurationSaveMode.Full);
                    ConfigurationManager.RefreshSection(section);

                    FireConfigChangedEvent(ConfigChangedType.SettingRemoved, section, setting, null);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }

        /// <summary>
        /// Create an setting on a section. </summary>
        /// <param name="setting">The name of the setting to create. </param>
        /// <param name="value">The value of new setting</param>
        public override void CreateSetting(string section, string setting, object value)
        {
            XmlDocument xmlDoc = null;
            XmlElement xmlRoot = null;
            XmlNode sectionNode = null;
            XmlNode settingNode = null;

            try
            {
                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;

                sectionNode = xmlRoot.SelectSingleNode(this.SectionElement);

                if (sectionNode == null)
                    this.CreateSection(null);

                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;
                sectionNode = xmlRoot.SelectSingleNode(this.SectionElement);

                settingNode = xmlDoc.CreateNode(XmlNodeType.Element, this.SettingElement, null);
                settingNode.Attributes.Append(xmlDoc.CreateAttribute(this.KeyAttribute));
                settingNode.Attributes.Append(xmlDoc.CreateAttribute(this.ValueAttribute));
                settingNode.Attributes[this.KeyAttribute].Value = setting;
                settingNode.Attributes[this.ValueAttribute].Value = value.ToString();

                sectionNode.AppendChild(settingNode);

                xmlDoc.Save(this.SourceName);
                this._configuration.Save(ConfigurationSaveMode.Full);

                FireConfigChangedEvent(ConfigChangedType.SettingCreated, this.SectionElement, setting, value);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }

        /// <summary>
        /// Retrieves the names of all the sections. </summary>
        /// <returns>The return value should be an array with the names of all the sections. </returns>
        public override string[] GetSectionNames()
        {
            string[] rtnNames = null;

            try
            {
                ConfigurationSectionCollection configCollection = this._configuration.Sections;

                rtnNames = new string[configCollection.Count];

                int i = 0;
                foreach (ConfigurationSection configSection in configCollection)
                {
                    rtnNames[i++] = configSection.SectionInformation.Name;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnNames;
        }

        /// <summary>
        /// Retrieves the names of all the entries inside a section. </summary>
        /// <param name="section">The name of the section holding the entries. </param>
        /// <returns>
        /// If the section exists, the return value should be an array with the names of its entries; 
        /// otherwise it should be null. 
        /// </returns>
        public override string[] GetSettingNames(string section)
        {
            XmlDocument xmlDoc = null;
            XmlElement xmlRoot = null;
            XmlNodeList settingNodes = null;

            string[] settingNames = null;

            try
            {
                if (string.IsNullOrEmpty(section))
                    section = this.SectionElement;

                if (!HasSection(section))
                    return null;

                xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = xmlDoc.DocumentElement;

                settingNodes = xmlRoot.SelectNodes(section + "/" + this.SettingElement);

                if (settingNodes == null)
                    return null;

                settingNames = new string[settingNodes.Count];
                int i = 0;

                foreach (XmlNode node in settingNodes)
                    settingNames[i++] = node.Attributes[this.KeyAttribute].Value;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return settingNames;
        }
    }
}
