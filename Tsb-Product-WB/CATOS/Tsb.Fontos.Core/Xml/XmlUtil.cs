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
* 2011.04.25   Tonny.Kim    GetXmlNodeList
* 
*/
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Tsb.Fontos.Core.Util;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using System.Xml.Serialization;
using System.Xml.XPath;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Log;
using System.Windows.Forms;

namespace Tsb.Fontos.Core.Xml
{
    /// <summary>
    /// Xml hanling utility class
    /// </summary>
    public class XmlUtil : BaseUtil
    {
        #region FIELD/PROPERTY AREA*****************************
        public const string OBJECT_ID = "GNR-FTCO-UTL-XmlUtil";
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public XmlUtil()
            : base()
        {
            this.ObjectID = OBJECT_ID;
        }
        #endregion


        #region METHOD AREA (Handling XML Document)*************
        /// <summary>
        /// Retrieves an XmlDocument object based on the the file. </summary>
        /// <param name="filePath">XML file path</param>
        /// <returns>XmlDocument object, which will be loaded with the file if it already exists.</returns>
        public static XmlDocument GetXmlDocument(string filePath)
        {
            XmlDocument xmlDoc = null;

            try
            {
                if (File.Exists(filePath))
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);
                    return xmlDoc;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ex is IOException)
                {
                    IOException exception = ex as IOException;

                    if (exception.Message.Contains("it is being used by another process"))
                    {
                        if (MessageBox.Show(MessageBuilder.BuildMessage("MSG_FTCO_00280", DefaultMessage.NON_REG_WRD + filePath), "Information", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning)
                            == DialogResult.Retry)
                        {
                            return GetXmlDocument(filePath);
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Retrieves an XmlDocument object which is retrieved from xml formatted string. </summary>
        /// <param name="filePath">XML formatted String</param>
        /// <returns>XmlDocument object</returns>
        public static XmlDocument GetXmlDocumentFromString(string xmlString)
        {
            XmlDocument xmlDoc = null;

            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            return xmlDoc;
        }
        #endregion


        #region METHOD AREA (GET/SET ATTRIBUTE)*****************

        /// <summary>
        /// Sets a attribute value of a specified xml file.
        /// </summary>
        /// <param name="filePath">XML file path</param>
        /// <param name="xPathExpression">XPath expression.</param>
        /// <param name="attrNameToChange">attribute name to change</param>
        /// <param name="value">string value to sets attribute value</param>
        /// <param name="elements">Element names array to reach to change target</param>
        /// <example>
        /// Sample xml file
        /// <?xml version="1.0" encoding="utf-8"?>
        /// <configuration>
        ///     <CATOS_Operation>
        ///         <Common>
        ///             <add key="DMS" value="ORACLE" />
        ///             <add key="DSN" value="" />
        ///             <add key="UID" value="PCT_DEV" />
        ///         </Common>
        ///    </CATOS_Operation>
        /// </configuration>
        /// 
        /// To set [value] attribute value of <add> element that's key is a "UID" to "TSB"
        /// using like this
        /// SetXmlAttrValue(filePath, "add[@key="UID"", "value", "TSB", "configuration", "CATOS_Operation", "Common");
        /// </example>
        public static void SetXmlAttrValue(string filePath, string xPathExpression, string attrNameToChange,
            string value, params string[] elements)
        {
            XmlUtil.SetXmlAttrValue(filePath, xPathExpression, attrNameToChange, value, null, elements);
        }

        /// <summary>
        /// Sets a attribute value of a specified xml file.
        /// </summary>
        /// <param name="filePath">XML file path</param>
        /// <param name="xPathExpression">XPath expression.</param>
        /// <param name="attrNameToChange">attribute name to change</param>
        /// <param name="value">string value to sets attribute value</param>
        /// <param name="elements">Element names array to reach to change target</param>
        /// <example>
        /// Sample xml file
        /// <?xml version="1.0" encoding="utf-8"?>
        /// <configuration>
        ///     <CATOS_Operation>
        ///         <Common>
        ///             <add key="DMS" value="ORACLE" />
        ///             <add key="DSN" value="" />
        ///             <add key="UID" value="PCT_DEV" />
        ///         </Common>
        ///    </CATOS_Operation>
        /// </configuration>
        /// 
        /// To set [value] attribute value of <add> element that's key is a "UID" to "TSB"
        /// using like this
        /// SetXmlAttrValue(filePath, "add[@key="UID"", "value", "TSB", "configuration", "CATOS_Operation", "Common");
        /// </example>
        public static void SetXmlAttrValue(string filePath, string xPathExpression, string attrNameToChange,
            string value, XPathNavigator appendXPathNavi, params string[] elements)
        {
            XmlDocument xmlDoc = null;
            XPathNavigator xmlNavi = null;
            string fullExpression = string.Empty;

            if (FileUtil.Exists(filePath) == false)
            {
                //MSG: [{0}] file does not exist. Please check this file (Path:{1})	
                throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00126", DefaultMessage.NON_REG_WRD + filePath, DefaultMessage.NON_REG_WRD + filePath);
            }

            try
            {
                xmlDoc = XmlUtil.GetXmlDocument(filePath);
                xmlNavi = xmlDoc.CreateNavigator();

                foreach (string element in elements)
                {
                    bool valid = xmlNavi.MoveToChild(element, string.Empty);

                    if (!valid &&
                        (appendXPathNavi != null))
                    {
                        xmlNavi.AppendChild("<" + element + "/>");
                        xmlNavi.MoveToChild(element, string.Empty);
                        valid = true;
                    }

                    if (!valid)
                    {
                        throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00129", DefaultMessage.NON_REG_WRD + element);
                    }
                }

                if (!XmlUtil.SetSelectXmlAttrValue(xmlNavi, xPathExpression, attrNameToChange, value))
                {
                    if (appendXPathNavi != null)
                    {
                        xmlNavi.AppendChild(appendXPathNavi.InnerXml);
                        XmlUtil.SetSelectXmlAttrValue(xmlNavi, xPathExpression, attrNameToChange, value);

                    }
                    else
                    {
                        throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00129", DefaultMessage.NON_REG_WRD + xPathExpression);
                    }
                }

                xmlDoc.Save(filePath);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                if (ex is IOException)
                {
                    IOException exception = ex as IOException;

                    if (exception.Message.Contains("it is being used by another process"))
                    {
                        if (MessageBox.Show(MessageBuilder.BuildMessage("MSG_FTCO_00280", DefaultMessage.NON_REG_WRD + filePath), "Information", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning)
                            == DialogResult.Retry)
                        {
                            SetXmlAttrValue(filePath, xPathExpression, attrNameToChange, value, appendXPathNavi, elements);
                            return;
                        }
                    }
                }

                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbBaseException), OBJECT_ID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }
            return;
        }

        /// <summary>
        /// Sets a attribute value of a specified Xml Value.
        /// </summary>
        /// <param name="xmlNavi"></param>
        /// <param name="xPathExpression"></param>
        /// <param name="attrNameToChange"></param>
        /// <param name="value"></param>
        private static bool SetSelectXmlAttrValue(XPathNavigator xmlNavi, string xPathExpression, string attrNameToChange,
                                            string value)
        {
            bool bValid = false;
            XPathNavigator selectXmlNavi = xmlNavi.SelectSingleNode(xPathExpression);

            try
            {
                if (selectXmlNavi != null)
                {
                    if (selectXmlNavi.MoveToAttribute(attrNameToChange, string.Empty))
                    {
                        value = (value == null) ? string.Empty : value;
                        selectXmlNavi.SetValue(value);
                        bValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return bValid;
        }

        /// <summary>
        /// Gets a attribute value of a specified xml file.
        /// </summary>
        /// <param name="filePath">XML file path</param>
        /// <param name="xPathExpression">XPath expression.</param>
        /// <param name="attrNameToChange">attribute name to get</param>
        /// <param name="elements">Element names array to reach to change target</param>
        /// <example>
        /// Sample xml file
        /// <?xml version="1.0" encoding="utf-8"?>
        /// <configuration>
        ///     <CATOS_Operation>
        ///         <Common>
        ///             <add key="DMS" value="ORACLE" />
        ///             <add key="DSN" value="" />
        ///             <add key="UID" value="PCT_DEV" />
        ///         </Common>
        ///    </CATOS_Operation>
        /// </configuration>
        /// 
        /// To get [value] attribute value of <add> element that's key is a "UID"
        /// using like this
        /// GetXmlAttrValue(filePath, "add[@key="UID"", "value", "configuration", "CATOS_Operation", "Common");
        /// </example>
        public static string GetXmlAttrValue(string filePath, string xPathExpression, string attrNameToGet, params string[] elements)
        {
            string rtnValue = string.Empty;
            XmlDocument xmlDoc = null;
            XPathNavigator xmlNavi = null;
            string fullExpression = string.Empty;

            if (FileUtil.Exists(filePath) == false)
            {
                //MSG: [{0}] file does not exist. Please check this file (Path:{1})	
                throw new TsbSysConfigException(OBJECT_ID, "MSG_FTCO_00126", DefaultMessage.NON_REG_WRD + filePath, DefaultMessage.NON_REG_WRD + filePath);
            }

            try
            {
                xmlDoc = XmlUtil.GetXmlDocument(filePath);
                xmlNavi = xmlDoc.CreateNavigator();

                foreach (string element in elements)
                {
                    xmlNavi.MoveToChild(element, string.Empty);
                }

                xmlNavi = xmlNavi.SelectSingleNode(xPathExpression);
                xmlNavi.MoveToAttribute(attrNameToGet, string.Empty);
                rtnValue = xmlNavi.Value;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbBaseException), OBJECT_ID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }
            return rtnValue;
        }

        /// <summary>
        /// Gets a attribute value of a specified xml file.
        /// </summary>
        /// <param name="xmlDoc">XML Document Object</param>
        /// <param name="xPathExpression">XPath expression.</param>
        /// <param name="attrNameToChange">attribute name to get</param>
        /// <param name="elements">Element names array to reach to change target</param>
        /// <example>
        /// Sample xml file
        /// <?xml version="1.0" encoding="utf-8"?>
        /// <configuration>
        ///     <CATOS_Operation>
        ///         <Common>
        ///             <add key="DMS" value="ORACLE" />
        ///             <add key="DSN" value="" />
        ///             <add key="UID" value="PCT_DEV" />
        ///         </Common>
        ///    </CATOS_Operation>
        /// </configuration>
        /// 
        /// To get [value] attribute value of <add> element that's key is a "UID"
        /// using like this
        /// GetXmlAttrValue(XmlDocument object reference, "add[@key="UID"", "value", "configuration", "CATOS_Operation", "Common");
        /// </example>
        public static string GetXmlAttrValue(XmlDocument xmlDoc, string xPathExpression, string attrNameToGet, params string[] elements)
        {
            string rtnValue = string.Empty;
            XPathNavigator xmlNavi = null;
            string fullExpression = string.Empty;

            try
            {
                xmlNavi = xmlDoc.CreateNavigator();

                foreach (string element in elements)
                {
                    xmlNavi.MoveToChild(element, string.Empty);
                }

                xmlNavi = xmlNavi.SelectSingleNode(xPathExpression);
                if (xmlNavi == null) return null;
                xmlNavi.MoveToAttribute(attrNameToGet, string.Empty);
                rtnValue = xmlNavi.Value;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbBaseException), OBJECT_ID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }
            return rtnValue;
        }
        #endregion


        #region METHOD AREA (GET/SET NODE)**********************
        /// <summary>
        /// Gets a XmlNodeList value of a specified xml file.
        /// </summary>
        /// <param name="xmlDoc">XML Document Object</param>
        /// <param name="xPathExpression">XPath expression.</param>
        public static XmlNodeList GetXmlNodeList(XmlDocument xmlDoc, string xPathExpression)
        {
            XmlNodeList xmlNodeList = null;

            try
            {
                XmlElement xmlRoot = null;
                xmlRoot = xmlDoc.DocumentElement;
                xmlNodeList = xmlRoot.SelectNodes(xPathExpression);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, OBJECT_ID);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbBaseException), OBJECT_ID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return xmlNodeList;
        }
        #endregion


        #region METHOD AREA (SERIALIZE/DESERIALIZE)*************
        /// <summary>
        /// Returns an object which is deserialized from a specified xml file
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="filePath">XML file path</param>
        /// <returns>An object which is deserialized from a specified xml file</returns>
        public static T Deserialize<T>(string filePath)
        {
            T rtnObject = default(T);
            XmlSerializer serializer = null;
            FileStream fs = null;

            try
            {
                serializer = new XmlSerializer(typeof(T));
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
                serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnreferencedObject += new UnreferencedObjectEventHandler(serializer_UnreferencedObject);

                using (fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    rtnObject = (T)serializer.Deserialize(fs);
                }

            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), OBJECT_ID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + Convert.ToString(typeof(T).Name));
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return rtnObject;
        }

        /// <summary>
        /// Returns an object which is deserialized from a specified xml file
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="stream">XML data stream</param>
        /// <returns>An object which is deserialized from a specified xml file</returns>
        public static T Deserialize<T>(Stream stream)
        {
            T rtnObject = default(T);
            XmlSerializer serializer = null;
            FileStream fs = null;

            try
            {
                serializer = new XmlSerializer(typeof(T));
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
                serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnreferencedObject += new UnreferencedObjectEventHandler(serializer_UnreferencedObject);

                rtnObject = (T)serializer.Deserialize(stream);

            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), OBJECT_ID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + Convert.ToString(typeof(T).Name));
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return rtnObject;
        }

        /// <summary>
        /// Returns an object which is deserialized from a specified xml file
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="filePath">XML file path</param>
        /// <returns>An object which is deserialized from a specified xml file</returns>
        public static T Deserialize<T>(String assemblyString, string name)
        {
            T rtnObject = default(T);
            XmlSerializer serializer = null;
            //FileStream fs = null;
            System.IO.Stream fs = null;

            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(assemblyString);

                //System.IO.Stream fileStream = assembly.GetManifestResourceStream(name);

                serializer = new XmlSerializer(typeof(T));
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
                serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnreferencedObject += new UnreferencedObjectEventHandler(serializer_UnreferencedObject);

                //using (fs = new FileStream(filePath, FileMode.Open))
                using (fs = assembly.GetManifestResourceStream(name))
                {
                    rtnObject = (T)serializer.Deserialize(fs);
                }

            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), OBJECT_ID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + Convert.ToString(typeof(T).Name));
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return rtnObject;
        }

        /// <summary>
        /// Returns an object which is deserialized from a specified xml string
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="xmlString">XML formatted stringfile path</param>
        /// <returns>An object which is deserialized from a specified xml formatted string</returns>
        public static T DeserializeFromString<T>(string xmlString)
        {
            T rtnObject = default(T);
            XmlSerializer serializer = null;

            try
            {
                using (StringReader stringReader = new StringReader(xmlString))
                {
                    using (XmlTextReader xmlReader = new XmlTextReader(stringReader))
                    {
                        serializer = new XmlSerializer(typeof(T));
                        serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
                        serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);
                        serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                        serializer.UnreferencedObject += new UnreferencedObjectEventHandler(serializer_UnreferencedObject);

                        rtnObject = (T)serializer.Deserialize(xmlReader);
                        xmlReader.Close();
                    }
                    stringReader.Close();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), OBJECT_ID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + Convert.ToString(typeof(T).Name));
            }
            return rtnObject;
        }

        /// <summary>
        /// Returns an object which is deserialized from a specified xml string
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="xmlString">XML formatted stringfile path</param>
        /// <returns>An object which is deserialized from a specified xml formatted string</returns>
        public static object DeserializeFromString(string xmlString, Type type)
        {
            object rtnObject = default(object);
            XmlSerializer serializer = null;

            try
            {
                using (StringReader stringReader = new StringReader(xmlString))
                {
                    using (XmlTextReader xmlReader = new XmlTextReader(stringReader))
                    {
                        serializer = new XmlSerializer(type);
                        serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
                        serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);
                        serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                        serializer.UnreferencedObject += new UnreferencedObjectEventHandler(serializer_UnreferencedObject);

                        rtnObject = serializer.Deserialize(xmlReader);
                        xmlReader.Close();
                    }
                    stringReader.Close();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), OBJECT_ID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + Convert.ToString(type.Name));
            }
            return rtnObject;
        }


        /// <summary>
        /// Returns an object which is deserialized from a specified xml file using uncheck charater validation.
        /// Use this method in case xml file has invalid character(like 0x12...)
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="filePath">XML file path</param>
        /// <returns>An object which is deserialized from a specified xml file</returns>
        public static T DeserializeWithUncheckOption<T>(string filePath)
        {
            T rtnObject = default(T);
            XmlSerializer serializer = null;

            try
            {
                serializer = new XmlSerializer(typeof(T));
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
                serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnreferencedObject += new UnreferencedObjectEventHandler(serializer_UnreferencedObject);

                var settings = new XmlReaderSettings { CheckCharacters = false };

                using (var sr = new System.IO.StringReader(XmlUtil.GetXmlDocument(filePath).InnerXml))
                using (var reader = XmlReader.Create(sr, settings))
                {
                    rtnObject = (T)serializer.Deserialize(reader);
                }

            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), OBJECT_ID, "MSG_FTCO_00007", DefaultMessage.NON_REG_WRD + Convert.ToString(typeof(T).Name));
            }

            return rtnObject;
        }

        /// <summary>
        /// Serializes the specified Object and writes the XML document to a specified file
        /// </summary>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        /// <param name="filePath">XML file path</param>
        /// <param name="objectToWrite"> Object to write xml file</param>
        public static void Serialize<T>(string filePath, object objectToWrite)
        {
            XmlSerializer serializer = null;
            FileStream fs = null;

            try
            {
                serializer = new XmlSerializer(typeof(T));
                using (fs = new FileStream(filePath, FileMode.Create))
                {

                    serializer.Serialize(fs, objectToWrite);
                }
            }
            catch (Exception ex)
            {
                //MSG:While writing {0} xml file, a serialization operation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), OBJECT_ID, "MSG_FTCO_00037", DefaultMessage.NON_REG_WRD + filePath);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return;
        }


        /// <summary>
        /// Returns xml formatted string which is serialized from a specified object
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="xmlString">XML formatted stringfile path</param>
        /// <returns>An object which is deserialized from a specified xml formatted string</returns>
        public static string SerializeToXmlString<T>(object objectToSerialize)
        {
            string xmlString = null;
            XmlSerializer serializer = null;
            XmlSerializerNamespaces xmlSerNamespaces = null;

            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (XmlTextWriter xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8))
                    {
                        xmlSerNamespaces = new XmlSerializerNamespaces();
                        xmlSerNamespaces.Add(string.Empty, string.Empty);

                        xmlWriter.Formatting = Formatting.Indented;

                        serializer = new XmlSerializer(typeof(T));
                        serializer.Serialize(xmlWriter, objectToSerialize, xmlSerNamespaces);

                        xmlWriter.Close();
                    }
                    memStream.Close();

                    xmlString = Encoding.UTF8.GetString(memStream.GetBuffer());
                    xmlString = xmlString.Substring(xmlString.IndexOf(Convert.ToChar(60)));
                    xmlString = xmlString.Substring(0, (xmlString.LastIndexOf(Convert.ToChar(62)) + 1));
                }
            }
            catch (Exception ex)
            {
                //MSG:While writing {0} xml file, a serialization operation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysXmlException), OBJECT_ID, "MSG_FTCO_00037", DefaultMessage.NON_REG_WRD + string.Empty);
            }
            return xmlString;
        }


        static void serializer_UnreferencedObject(object sender, UnreferencedObjectEventArgs e)
        {
            throw new NotImplementedException();
        }

        static void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            throw new NotImplementedException();
        }

        static void serializer_UnknownElement(object sender, XmlElementEventArgs e)
        {
            throw new NotImplementedException();
        }

        static void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
