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
* 2011.04.22  Tonny.Kim     Modify to function of CreateSetting()
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Tsb.Fontos.Core.Xml;
using System.IO;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Configuration.Event;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Configuration.Provider
{
    /// <summary>
    /// Configuration Provider class that utilizes an XML file to retrieve and save its configuration setting.
    /// </summary>
    public class XmlConfigProvider : BaseConfigProvider
    {

        private XmlDocument _xmlDoc = null;

        private string _sectionElement;
        private string _settingElement;
        private string _nameAttribute;


        public string SectionElement
        {
            get { return _sectionElement; }
        }

        public string SettingElement
        {
            get { return _settingElement; }
        }

        public string NameAttribute
        {
            get { return _nameAttribute; }
        }



        /// <summary>
        /// Initializes a new instance of the  Xml Configuation Provider class
        /// </summary>
        private XmlConfigProvider()
            : base()
        {
            this._sectionElement = ConfigConstant.XML_ELE_XMLCONFIG_DEFAULT_SECTION;
            this._settingElement = ConfigConstant.XML_ELE_XMLCONFIG_DEFAULT_SETTING;
            this._nameAttribute = ConfigConstant.XML_ATT_XMLCONFIG_DEFAULT_NAME;
            this.ObjectID = "GNR-FTCO-CFG-XmlConfigProvider";
        }


        /// <summary>
        /// Initializes a new instance of the  Xml Configuation Provider class using the given file name.
        /// </summary>
        /// <param name="fileName">The name of the XML file to initialize</param>
        public XmlConfigProvider(string fileName)
            : this()
        {
            this._sourceName = PathUtil.GetFileFullPath(fileName);
            this._xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);

            if (this._xmlDoc == null)
            {
                ///TODO:throw new TsbSysConfigException(new FileNotFoundException("Configuration file does not exist", this.SourceName));
            }
        }

        /// <summary>
        /// Initializes a new instance of the  Xml Configuation Provider class using the given file name, section element, setting element.
        /// This class is used for custom defiend configuration xml
        /// </summary>
        /// <param name="fileName">The name of the XML file to initialize</param>
        /// <param name="sectionXmlElement">String value of section element name</param>
        /// <param name="settingXmlElement">String value of setting element name</param>
        public XmlConfigProvider(string fileName, string sectionXmlElement, string settingXmlElement)
            : this(fileName)
        {
            this._sectionElement = sectionXmlElement;
            this._settingElement = settingXmlElement;
        }


        /// <summary>
        /// Retrieves the XPath string used for retrieving a section from the XML file.
        /// </summary>
        /// <returns>XPath string.</returns>
        private string GetSectionsPath(string section)
        {
            string rtnPath = this.SectionElement + "[@" + this.NameAttribute + "=\"" + section + "\"]";
            return rtnPath;
        }

        /// <summary>
        /// Retrieves the XPath string used for retrieving an setting from the XML file.
        /// </summary>
        /// <returns>An XPath string.</returns>
        private string GetSettingPath(string setting)
        {
            string rtnPath = this.SettingElement + "[@" + this.NameAttribute + "=\"" + setting + "\"]";
            return rtnPath;
        }

        /// <summary>
        /// Sets the value for an setting inside a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting where the value will be set. </param>
        /// <param name="value">The value to set. If it's null, the setting is removed. </param>
        public override void SetValue(string section, string setting, object value)
        {
            XmlElement xmlRoot = null;
            //XmlNode sectionNode = null;
            XmlNode settingNode = null;

            try
            {
                if (value == null)
                {
                    this.RemoveSetting(section, setting);
                    return;
                }

                xmlRoot = this._xmlDoc.DocumentElement;
                settingNode = xmlRoot.SelectSingleNode(this.GetSectionsPath(section) + "/" + this.GetSettingPath(setting));

                if (settingNode == null)
                {
                    this.CreateSetting(section, setting, value);
                }
                else
                {
                    settingNode.InnerText = value.ToString();

                    this._xmlDoc.Save(this.SourceName);

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
            XmlElement xmlRoot = null;
            XmlNode settingNode = null;
            string rtnValue = null;

            try
            {
                if (this._xmlDoc == null) return rtnValue;

                xmlRoot = this._xmlDoc.DocumentElement;
                settingNode = xmlRoot.SelectSingleNode(this.GetSectionsPath(section) + "/" + this.GetSettingPath(setting));

                if (settingNode != null)
                {
                    rtnValue = settingNode.InnerText;
                }
                else
                {
                    rtnValue = null;
                }
            }
            catch
            {
                rtnValue = null;
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
            XmlElement xmlRoot = null;
            XmlNode settingNode = null;
            string rtnValue = null;

            try
            {
                if (this._xmlDoc == null) return rtnValue;

                xmlRoot = this._xmlDoc.DocumentElement;
                settingNode = xmlRoot.SelectSingleNode(this.GetSectionsPath(section) + "/" + this.GetSettingPath(setting));

                if (settingNode != null)
                {
                    XmlAttribute xmlAttribute = settingNode.Attributes[attributeName];
                    if(xmlAttribute != null)
                    {
                        rtnValue = xmlAttribute.InnerText;
                    }
                    
                }
                else
                {
                    rtnValue = null;
                }
            }
            catch
            {
                rtnValue = null;
            }
            return rtnValue;
        }

        /// <summary>
        /// Removes a section.
        /// </summary>
        /// <param name="section"> The name of the section to remove. </param>
        public override void RemoveSection(string section)
        {
            XmlElement xmlRoot = null;
            XmlNode sectionNode = null;

            try
            {
                xmlRoot = this._xmlDoc.DocumentElement;

                if (xmlRoot == null)
                    return;

                sectionNode = xmlRoot.SelectSingleNode(this.GetSectionsPath(section));

                if (sectionNode == null)
                    return;

                xmlRoot.RemoveChild(sectionNode);

                this._xmlDoc.Save(this.SourceName);

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
        /// <param name="section"> The name of the section to create. </param>
        public override void CreateSection(string section)
        {
            XmlElement xmlRoot = null;
            XmlNode sectionNode = null;

            try
            {
                xmlRoot = this._xmlDoc.DocumentElement;

                if (xmlRoot == null)
                    return;

                sectionNode = this._xmlDoc.CreateNode(XmlNodeType.Element, this.SectionElement, null);
                sectionNode.Attributes.Append(this._xmlDoc.CreateAttribute(this.NameAttribute));
                sectionNode.Attributes[this.NameAttribute].Value = section;
                xmlRoot.AppendChild(sectionNode);

                this._xmlDoc.Save(this.SourceName);
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
            XmlElement xmlRoot = null;
            XmlNode settingNode = null;

            try
            {
                xmlRoot = this._xmlDoc.DocumentElement;
                settingNode = xmlRoot.SelectSingleNode(this.GetSectionsPath(section) + "/" + this.GetSettingPath(setting));

                if (settingNode == null)
                    return;

                settingNode.ParentNode.RemoveChild(settingNode);

                this._xmlDoc.Save(this.SourceName);

                FireConfigChangedEvent(ConfigChangedType.SettingRemoved, section, setting, null);
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
            XmlElement xmlRoot = null;
            XmlNode sectionNode = null;
            XmlNode settingNode = null;

            try
            {
                xmlRoot = this._xmlDoc.DocumentElement;

                sectionNode = xmlRoot.SelectSingleNode(this.GetSectionsPath(section));

                if (sectionNode == null)
                    this.CreateSection(section);

                sectionNode = xmlRoot.SelectSingleNode(this.GetSectionsPath(section) + "/" + this.GetSettingPath(setting));

                this._xmlDoc = XmlUtil.GetXmlDocument(this.SourceName);
                xmlRoot = this._xmlDoc.DocumentElement;
                sectionNode = xmlRoot.SelectSingleNode(this.GetSectionsPath(section));

                settingNode = this._xmlDoc.CreateNode(XmlNodeType.Element, this.SettingElement, null);
                settingNode.Attributes.Append(this._xmlDoc.CreateAttribute(this._nameAttribute));
                settingNode.Attributes[this._nameAttribute].Value = setting;
                settingNode.InnerText = value.ToString();

                sectionNode.AppendChild(settingNode);

                this._xmlDoc.Save(this.SourceName);

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
            XmlElement xmlRoot = null;
            XmlNodeList sectionNodes = null;
            string[] secitonNames = null;

            try
            {
                xmlRoot = _xmlDoc.DocumentElement;
                if (xmlRoot == null)
                    return null;

                sectionNodes = xmlRoot.SelectNodes(this.SectionElement + "[@" + this.NameAttribute + "]");

                if (sectionNodes == null)
                    return null;

                secitonNames = new string[sectionNodes.Count];

                int i = 0;
                foreach (XmlNode node in sectionNodes)
                    secitonNames[i++] = node.Attributes[this.NameAttribute].Value;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return secitonNames;
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
            XmlElement xmlRoot = null;
            XmlNodeList settingNodes = null;
            string[] settingNames = null;

            try
            {
                if (!HasSection(section))
                    return null;

                xmlRoot = this._xmlDoc.DocumentElement;

                //settingNodes = xmlRoot.SelectNodes(this.GetSectionsPath(section) + "/"+this.SettingElement+"[@"+this.NameAttribute+"]");
                settingNodes = xmlRoot.SelectSingleNode(this.GetSectionsPath(section)).ChildNodes;

                if (settingNodes == null)
                    return null;

                settingNames = new string[settingNodes.Count];
                int i = 0;

                foreach (XmlNode node in settingNodes)
                    settingNames[i++] = node.Attributes[this.NameAttribute].Value;

            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return settingNames;
        }


    }
}
