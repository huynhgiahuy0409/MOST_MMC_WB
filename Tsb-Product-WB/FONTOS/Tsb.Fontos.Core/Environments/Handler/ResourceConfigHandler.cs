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
* 2013.04.24    Jindols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Xml;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using Tsb.Fontos.Core.Environments.Item;
using Tsb.Fontos.Core.Resources;

namespace Tsb.Fontos.Core.Environments.Handler
{
    internal class ResourceConfigHandler : TsbBaseObject
    {
        #region CONST AREA *************************************
        private const string XML_ATT_VALUE_RESOURCE_SETTING = "Custom Resource Setting";
        private const string XML_ATT_VALUE_RESOURCE = "Resource";
        private const string XML_ATT_VALUE_RESOURCE_TYPE = "Type";
        private const string XML_ATT_VALUE_RESOURCE_PREFIX = "Prefix";
        private const string XML_ATT_VALUE_RESOURCE_ASSEMBLY = "Assembly";
        private const string XML_ATT_VALUE_RESOURCE_FILE = "BaseName";
        #endregion

        #region FIELD AREA ***************************************
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ResourceConfigHandler()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-ResoureceConfigHandler";

        }
        #endregion

        #region METHOD AREA **************************************
        public List<ResourceDataItem> GetResourceConfig(string filePath)
        {
            List<ResourceDataItem> resConfigList = new List<ResourceDataItem>();

            try
            {
                XmlDocument xmlRoot = XmlUtil.GetXmlDocument(filePath);
                string searchKey = this.GetXpath("section", "name", XML_ATT_VALUE_RESOURCE_SETTING); //"descendant::section[@name=\"Resource Setting\"]";
                XmlNode settingNode = xmlRoot.SelectSingleNode(searchKey);

                if (settingNode == null)
                {
                    return resConfigList;
                }

                XmlNodeList nodeList = settingNode.ChildNodes;


                string nodeName = Tsb.Fontos.Core.Constant.ConfigConstant.XML_ELE_XMLCONFIG_DEFAULT_SETTING;
                string attrubeteName = Tsb.Fontos.Core.Constant.ConfigConstant.XML_ATT_XMLCONFIG_DEFAULT_NAME;

                foreach (XmlNode item in nodeList)
                {
                    //XmlNode nameNode = item.SelectSingleNode("descendant::section[@name=\"Resource\"]/setting[@name=\"Prefix\"]");
                    if (item != null && string.IsNullOrEmpty(item.InnerXml) == false)
                    {
                        string typeName = GetValue(item, nodeName, attrubeteName, XML_ATT_VALUE_RESOURCE_TYPE);
                        string prefix = GetValue(item, nodeName, attrubeteName, XML_ATT_VALUE_RESOURCE_PREFIX);
                        string assemblyName = GetValue(item, nodeName, attrubeteName, XML_ATT_VALUE_RESOURCE_ASSEMBLY);
                        string fileName = GetValue(item, nodeName, attrubeteName, XML_ATT_VALUE_RESOURCE_FILE);
                        string predefineFileName = GetAttributeValue(item, nodeName, attrubeteName, XML_ATT_VALUE_RESOURCE_FILE, LocalizationInfo.XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY);

                        if (string.IsNullOrEmpty(prefix) == false && string.IsNullOrEmpty(assemblyName) == false && string.IsNullOrEmpty(fileName) == false)
                        {
                            resConfigList.Add(new ResourceDataItem(typeName, prefix, assemblyName, fileName, predefineFileName));
                        }
                    }
                }
                
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG:An error occurred when opening or reading the configuration file.
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00006", null);
            }

            return resConfigList;
        }

        private string GetValue(XmlNode item , string nodeName, string attrubeteName, string attrubeteValue)
        {
            string returnValue = string.Empty;

            string xPath = this.GetXpath(nodeName, attrubeteName, attrubeteValue);

            XmlNode xmlNode = item.SelectSingleNode(xPath);
            returnValue = GetNodeInnerText(xmlNode);

            return returnValue;
        }

        private string GetAttributeValue(XmlNode item, string nodeName, string attrubeteName, string attrubeteValue, string searchAtbName)
        {
            string returnValue = string.Empty;

            string xPath = this.GetXpath(nodeName, attrubeteName, attrubeteValue);

            XmlNode xmlNode = item.SelectSingleNode(xPath);
            if (xmlNode != null)
            {
                XmlAttribute attribute = xmlNode.Attributes[searchAtbName];
                if (attribute != null)
                {
                    returnValue = attribute.InnerText;
                }
            }

            return returnValue;
        }

        private string GetXpath(string nodeName, string attrubeteName, string attrubeteValue)
        {
            string xPathFormat = "descendant::{0}[@{1}=\"{2}\"]";

            return string.Format(xPathFormat, nodeName, attrubeteName, attrubeteValue);
        }

        private string GetNodeInnerText(XmlNode node)
        {
            string returnValue = string.Empty;
            try
            {
                if (node != null)
                {
                    returnValue = node.InnerText;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return returnValue;
        }
        #endregion
    }
}
