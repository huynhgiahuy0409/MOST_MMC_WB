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
using System.Globalization;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Configuration;
using System.Threading;
using System.Reflection;
using Tsb.Fontos.Core.Reflection;
using Tsb.Fontos.Core.Objects;
using System.IO;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Environments.Event;
using Tsb.Fontos.Core.Environments.Item;
using Tsb.Fontos.Core.Environments.Param;
using Tsb.Fontos.Core.Configuration.Common;
using Tsb.Fontos.Core.Environments.Type;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Tsb.Fontos.Core.Util.File;
using Tsb.Fontos.Core.Item;
using System.Xml;
using System.Diagnostics;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Environments.Handler;
using Tsb.Fontos.Core.Resources;

namespace Tsb.Fontos.Core.Environments
{

    /// <summary>
    /// Encapsulates Application Localization information
    /// </summary>
    public class LocalizationInfo : BaseEnvironmentInfo
    {
        #region EVENT DECLARATION AREA**************************
        public event CultureChangedHandler CultureChanged;
        #endregion


        #region FIELD/CONST AREA *************************************
        private readonly char CULTURE_DELIM_NAME = '-';
        private readonly string FONTOS_RESOUCE_VALUE_RESOURCE_ASSEMBLY = "Tsb.Fontos.Core";
        private readonly string FONTOS_RESOUCE_VALUE_MESSAGE_BASE = "Tsb.Fontos.Core.Resources.MessageResource";
        private readonly string FONTOS_RESOUCE_VALUE_VOCABULARY_BASE = "Tsb.Fontos.Core.Resources.VocabularyResource";
        private readonly string FONTOS_RESOUCE_VALUE_MESSAGE_PREFIX = "MSG_FTCO";
        private readonly string FONTOS_RESOUCE_VALUE_VOCABULARY_PREFIX = "WRD_FTCO";

        public const string XML_ATT_VALUE_VOCABULARY_DEFAULT_PREFIX = "DEFAULT";
        public const string XML_ATT_VALUE_MESSAGE_DEFAULT_PREFIX = "DEFAULT";

        public const string XML_ATT_VALUE_POLICY_SETTING   = "Policy Setting";
        public const string XML_ATT_VALUE_RESOURCE_SETTING = "Resource Setting";
        public const string XML_ATT_VALUE_PRODUCT_COMMON_RESOURCE_SETTING = "Product Common Resource Setting";
        public const string XML_ATT_VALUE_MODULE_RESOURCE_SETTING = "Module Resource Setting";
        public const string XML_ATT_VALUE_CULTURE_SETTING  = "Culture Setting";
        public const string XML_ATT_VALUE_ADDITIONAL_RESOURCE_SETTINGS = "Additional Resource Settings";

        public const string XML_ATT_VALUE_RESOURCE_ASSEMBLY = "ResourceAssembly";
        public const string XML_ATT_VALUE_MESSAGE_BASE      = "MessageBase";
        public const string XML_ATT_VALUE_VOCABULARY_BASE   = "VocabularyBase";
        public const string XML_ATT_VALUE_MESSAGE_PREFIX    = "MessagePrefix";
        public const string XML_ATT_VALUE_VOCABULARY_PREFIX = "VocabularyPrefix";
        public const string XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY = "predefine";
        public const string XML_ATT_ADDITIONAL_RESOURCE = "additionalResource";

        public const string XML_ATT_VALUE_DATETIME_FORMAT = "Custom.DateTime.Format";
        public const string XML_ATT_VALUE_CURRENCY_FORMAT = "Custom.Currency.Format";
        public const string XML_ATT_VALUE_NUMBER_FORMAT   = "Custom.Number.Format";
        
        public const string XML_ATT_VALUE_TYPE     = "Type";
        public const string XML_ATT_VALUE_NAME     = "Name";
        
        public const string XML_ATT_VALUE_FULL_DATETIME = "FullDateTime";
        public const string XML_ATT_VALUE_LONG_DATE  = "LongDate";
        public const string XML_ATT_VALUE_SHORT_DATE = "ShortDate";
        public const string XML_ATT_VALUE_MONTH_DAY  = "MonthDay";
        public const string XML_ATT_VALUE_YEAR_MONTH = "YearMonth";
        public const string XML_ATT_VALUE_SHORT_TIME = "ShortTime";
        public const string XML_ATT_VALUE_LONG_TIME  = "LongTime";

        public const string XML_ATT_VALUE_SYMBOL  = "Symbol";
        public const string XML_ATT_VALUE_DECIMAL_DIGITS    = "DecimalDigits";
        public const string XML_ATT_VALUE_DECIMAL_SEPARATOR = "DecimalSeparator";
        public const string XML_ATT_VALUE_GROUP_SIZES       = "GroupSizes";
        public const string XML_ATT_VALUE_GROUP_SEPARATOR   = "GroupSeparator";
        public const string XML_ATT_VALUE_PERCENT_POSITIVE  = "PercentPositive";
        public const string XML_ATT_VALUE_PERCENT_NEGATIVE  = "PercentNegative";

        private static LocalizationInfo _instance = null;
        private LocalizationPolicyTypes _policyType;
        
        private string[] _positivePercentPatterns = new string[] { @"n%", @"n%", @"%n", @"% n" };
        private string[] _negativePercentPatterns = new string[] { @"-n %", @"-n%", @"-%n", @"%-n", @"%n-", @"n-%", @"n%", @"-% n", @"n %-", @"% n-", @"% -n", @"n- %" };

        private List<CultureInfo> _supportedCultures = null;

        private List<ResourceDataItem> _resources = new List<ResourceDataItem>();

        private string defaultResouceAssemblyName = "";

        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Localization Policy Type (Basic, OS, Custom)
        /// </summary>
        public LocalizationPolicyTypes PolicyType
        {
            get { return _policyType; }
        }

        /// <summary>
        /// Currently used culture name
        /// </summary>
        public string CultureName
        {
            get { return Thread.CurrentThread.CurrentCulture.Name; }
        }

        /// <summary>
        /// Currently setted Culture Information object
        /// </summary>
        public CultureInfo CultureInfo
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        /// <summary>
        /// Currently setted TwoLetterISOLanguageName
        /// </summary>
        public string TwoLetterISOLanguageName
        {
            get { return this.CultureInfo.TwoLetterISOLanguageName; }
        }

        /// <summary>
        /// DateTimeFormatInfo that defines the culturally appropriate format of displaying dates and times.
        /// </summary>
        public DateTimeFormatInfo DateTimeFormat
        {
            get { return Thread.CurrentThread.CurrentCulture.DateTimeFormat; }
        }

        /// <summary>
        ///NumberFormatInfo that defines the culturally appropriate format of displaying numbers, currency, and percentage.
        /// </summary>
        public NumberFormatInfo NumberFormat
        {
            get { return Thread.CurrentThread.CurrentCulture.NumberFormat; }
        }

        /// <summary>
        /// List of supported CultureInfo object by system
        /// </summary>
        public List<CultureInfo> SupportedCultures
        {
            get { return this._supportedCultures; }

        }

        /// <summary>
        /// List of supported ResourceDataItem object by system
        /// </summary>
        public List<ResourceDataItem> Resources
        {
            get { return this._resources; }

        }
        #endregion

        

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="id">The ID of the Module</param>
        /// <param name="name">The Name of the Module</param>
        private LocalizationInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-LocalizationInfo";

            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.SystemLocaleChangedHandler);
        }


        /// <summary>
        /// Gets Localization information object reference.
        /// </summary>
        /// <returns>Localization information object reference</returns>
        public static LocalizationInfo GetInstance()
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new LocalizationInfo();

                    if(DeployInfo.IsRuntime(CallingPositionTypes.GENERAL_METHOD))
                    {
                        _instance.LoadEnvironmentInfo();

                        _instance._supportedCultures = _instance.GetSupportedCultureList();
                    }
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, "GNR-FTCO-ENV-LocalizationInfo");
            }

            return _instance;
        }


        /// <summary>
        /// Load Localization info
        /// </summary>
        private void LoadEnvironmentInfo()
        {
            string policyTypeValue   = string.Empty;
            
            try
            {
                this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_USER_ENVIRONMENT, AppPathInfo.FILE_NAME_LOCALIZATION_INFO);
            }
            catch (System.TypeInitializationException initEx)
            {
                if (initEx.InnerException is TsbBaseException)
                {
                    TsbBaseException tsbEx = initEx.InnerException as TsbBaseException;
                    ExceptionHandler.Replace(initEx, initEx.InnerException.GetType(), tsbEx.SourceObjectID, tsbEx.MsgCode, tsbEx.MsgArgs);
                }
                else
                {
                    //MSG:An error occurred when checking the configuration path
                    ExceptionHandler.Wrap(initEx, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00005", null);
                }
            }

            if (string.IsNullOrEmpty(this._sourcePath))
            {
                //MSG:{0} does not exist. Please check {1}.
                throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00121",
                     this.SourcePath,
                    "WRD_FTCO_thisfile"
                    );
            }

            try
            {
                policyTypeValue = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_POLICY_SETTING, LocalizationInfo.XML_ATT_VALUE_TYPE);
                this._policyType = ConfigUtil.GetValidValueFromXml<LocalizationPolicyTypes>(policyTypeValue
                    , LocalizationInfo.XML_ATT_VALUE_POLICY_SETTING
                    , LocalizationInfo.XML_ATT_VALUE_TYPE
                    , this.SourcePath
                    );

                // Sets resource data.
                this.SetResouceConfig();

                switch (this._policyType)
                {
                    case LocalizationPolicyTypes.OS:
                        this.SetCultureWithSystemSetting();
                        break;
                    case LocalizationPolicyTypes.Basic:
                        this.SetCultureWithBasicPolicy();
                        break;
                    case LocalizationPolicyTypes.Custom:
                        this.SetCultureWithCustomPolicy();
                        break;
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            return;
        }

        /// <summary>
        /// Sets the resouce data.
        /// </summary>
        private void SetResouceConfig()
        {
            List<ResourceDataItem> resourceDataItem = null;

            // Fontos Core Resource File
            _resources.Add(new ResourceDataItem(ResourceSection.MESSAGE, FONTOS_RESOUCE_VALUE_MESSAGE_PREFIX, FONTOS_RESOUCE_VALUE_RESOURCE_ASSEMBLY, FONTOS_RESOUCE_VALUE_MESSAGE_BASE, null));
            _resources.Add(new ResourceDataItem(ResourceSection.LABEL, FONTOS_RESOUCE_VALUE_VOCABULARY_PREFIX, FONTOS_RESOUCE_VALUE_RESOURCE_ASSEMBLY, FONTOS_RESOUCE_VALUE_VOCABULARY_BASE, null));

            // Product Resource - XML Setting
            resourceDataItem = this.GetResouceDataItem(LocalizationInfo.XML_ATT_VALUE_RESOURCE_SETTING);
            if (resourceDataItem != null)
            {
                _resources.AddRange(resourceDataItem);
            }
            else
            { 
                // Module Resource - XML Setting
                resourceDataItem = this.GetResouceDataItem(LocalizationInfo.XML_ATT_VALUE_MODULE_RESOURCE_SETTING);
                if (resourceDataItem != null)
                {
                    _resources.AddRange(resourceDataItem);
                }
            }

            // Product Common Resource - XML Setting
            resourceDataItem = this.GetResouceDataItem(LocalizationInfo.XML_ATT_VALUE_PRODUCT_COMMON_RESOURCE_SETTING);
            if (resourceDataItem != null)
            {
                _resources.AddRange(resourceDataItem);
            }

            // Sets simple resource data
            ResourceConfigHandler resConfigHdl = new ResourceConfigHandler();
            resourceDataItem = resConfigHdl.GetResourceConfig(this.SourcePath);
            if (resourceDataItem != null && resourceDataItem.Count > 0)
            {
                _resources.AddRange(resourceDataItem);
            }

            resourceDataItem = GetAdditionalResourceDataItems();
            if (resourceDataItem != null)
            {
                _resources.AddRange(resourceDataItem);
            }
        }

        /// <summary>
        /// Gets Assembly Name in Resouce
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="settingName"></param>
        /// <returns></returns>
        private string GetValueFromXml(string sectionName, string settingName)
        {
            string rtnValue = null;

            try
            {
                rtnValue = ConfigUtil.GetValidValueFromXmlExceptionNull(this.SourcePath, sectionName, settingName);
            }
            catch (Exception)
            {
                rtnValue = null;
            }

            return rtnValue;
        }

        private List<ResourceDataItem> GetAdditionalResourceDataItems()
        {
            List<ResourceDataItem> resourceDataItems = new List<ResourceDataItem>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.SourcePath);

            XmlNodeList elemList = xmlDoc.GetElementsByTagName(XML_ATT_ADDITIONAL_RESOURCE);
            
            if (elemList != null && elemList.Count > 0 && elemList[0].ParentNode.Attributes["name"].Value == XML_ATT_VALUE_ADDITIONAL_RESOURCE_SETTINGS)
            {
                foreach (XmlNode node in elemList)
                {
                    string msgPrefix = string.Empty, vocaPrefix = string.Empty, resourceAssemblyName = string.Empty, msgFileName = string.Empty, vocaFileName = string.Empty, predefineMsgFileName = string.Empty, predefineVocaFileName = string.Empty;

                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Attributes["name"].Value == XML_ATT_VALUE_MESSAGE_PREFIX)
                        {
                            msgPrefix = childNode.FirstChild.Value;
                        }
                        else if (childNode.Attributes["name"].Value == XML_ATT_VALUE_VOCABULARY_PREFIX)
                        {
                            vocaPrefix = childNode.FirstChild.Value;
                        }
                        else if (childNode.Attributes["name"].Value == XML_ATT_VALUE_RESOURCE_ASSEMBLY)
                        {
                            resourceAssemblyName = childNode.FirstChild.Value;
                        }
                        else if (childNode.Attributes["name"].Value == XML_ATT_VALUE_MESSAGE_BASE)
                        {
                            msgFileName = childNode.FirstChild.Value;

                            if (childNode.Attributes[XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY] != null && string.IsNullOrEmpty(childNode.Attributes[XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY].Value) == false)
                            {
                                predefineMsgFileName = childNode.Attributes[XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY].Value;
                            }
                        }
                        else if (childNode.Attributes["name"].Value == XML_ATT_VALUE_VOCABULARY_BASE)
                        {
                            vocaFileName = childNode.FirstChild.Value;

                            if (childNode.Attributes[XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY] != null && string.IsNullOrEmpty(childNode.Attributes[XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY].Value) == false)
                            {
                                predefineVocaFileName = childNode.Attributes[XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY].Value;
                            }
                        }
                    }

                    resourceDataItems.Add(new ResourceDataItem(ResourceSection.MESSAGE, msgPrefix, resourceAssemblyName, msgFileName, predefineMsgFileName));
                    resourceDataItems.Add(new ResourceDataItem(ResourceSection.MESSAGE, vocaPrefix, resourceAssemblyName, vocaFileName, predefineVocaFileName));
                }
            }

            return resourceDataItems;
        }

        /// <summary>
        /// Gets ResourceDataItem 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        private List<ResourceDataItem> GetResouceDataItem(string sectionName)
        {
            List<ResourceDataItem> resourceDataItems = null;
            string resourcAssemblyName = this.GetValueFromXml(sectionName, LocalizationInfo.XML_ATT_VALUE_RESOURCE_ASSEMBLY);

            try
            {
                if (!string.IsNullOrEmpty(resourcAssemblyName))
                {
                    string messageBase = ConfigUtil.GetValidValueFromXml(this.SourcePath, sectionName, LocalizationInfo.XML_ATT_VALUE_MESSAGE_BASE);
                    string vocabularyBase = ConfigUtil.GetValidValueFromXml(this.SourcePath, sectionName, LocalizationInfo.XML_ATT_VALUE_VOCABULARY_BASE);

                    string predefineMessageBase = ConfigUtil.GetValidValueFromXmlExceptionNull(this.SourcePath, sectionName, XML_ATT_VALUE_MESSAGE_BASE, XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY);
                    string predefineVocabularyBase = ConfigUtil.GetValidValueFromXmlExceptionNull(this.SourcePath, sectionName, XML_ATT_VALUE_VOCABULARY_BASE, XML_ATT_PREDEFINE_NAME_RESOURCE_ASSEMBLY);

                    string messagePrefix = null;
                    string vocabularyPrefix = null;

                    if (sectionName == LocalizationInfo.XML_ATT_VALUE_RESOURCE_SETTING ||
                        sectionName == LocalizationInfo.XML_ATT_VALUE_MODULE_RESOURCE_SETTING)
                    { // None Exception - PRODUCT Module
                        this.defaultResouceAssemblyName = resourcAssemblyName;          // Use function of GetSupportedCultureList()
                        messagePrefix = this.GetValueFromXml(sectionName, LocalizationInfo.XML_ATT_VALUE_MESSAGE_PREFIX);
                        vocabularyPrefix = this.GetValueFromXml(sectionName, LocalizationInfo.XML_ATT_VALUE_VOCABULARY_PREFIX);
                    }
                    else
                    { // Throw Exception if [XML attribute] doesn't exist in XML File.
                        messagePrefix = ConfigUtil.GetValidValueFromXml(this.SourcePath, sectionName, LocalizationInfo.XML_ATT_VALUE_MESSAGE_PREFIX);
                        vocabularyPrefix = ConfigUtil.GetValidValueFromXml(this.SourcePath, sectionName, LocalizationInfo.XML_ATT_VALUE_VOCABULARY_PREFIX);
                    }

                    if (string.IsNullOrEmpty(messagePrefix))
                    {
                        messagePrefix = LocalizationInfo.XML_ATT_VALUE_MESSAGE_DEFAULT_PREFIX;
                    }

                    if (string.IsNullOrEmpty(vocabularyPrefix))
                    {
                        vocabularyPrefix = LocalizationInfo.XML_ATT_VALUE_VOCABULARY_DEFAULT_PREFIX;
                    }

                    resourceDataItems = new List<ResourceDataItem>();
                    resourceDataItems.Add(new ResourceDataItem(ResourceSection.MESSAGE, messagePrefix, resourcAssemblyName, messageBase, predefineMessageBase));
                    resourceDataItems.Add(new ResourceDataItem(ResourceSection.LABEL, vocabularyPrefix, resourcAssemblyName, vocabularyBase, predefineVocabularyBase));

                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return resourceDataItems;
        }

        /// <summary>
        ///  Removes the ResourceDataItem with the specified resourcePrefix from the resouce list.
        /// </summary>
        /// <param name="resourcePrefix">The prefix key of resource name.</param>
        /// <returns>
        /// Returns is null, if does not exist.
        /// </returns>
        public ResourceDataItem GetResouceDataItemByNamePrefix(String resourcePrefix)
        {
            if (resourcePrefix == null)
                throw new ArgumentNullException("resourcePrefix");

            ResourceDataItem resDataItem = null;
            resDataItem = _resources.Find(item => item.Prefix.Equals(resourcePrefix));

            return resDataItem;
        }
        #endregion


        #region METHOD AREA (Set Culture)***********************
        /// <summary>
        /// Sets Current Thread Culture with specified CultureInfo object
        /// </summary>
        /// <param name="ci">CultureInfo object reference</param>
        /// <param name="changingType">Localization policy type</param>
        private void SetCurrentCulture(CultureInfo ci, LocalizationPolicyTypes policyType)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            XmlConfigProvider configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this.SourcePath);

            try
            {
                if (ConfigUtil.IsNewXmlValue(configProvider, LocalizationInfo.XML_ATT_VALUE_POLICY_SETTING, LocalizationInfo.XML_ATT_VALUE_TYPE, policyType.ToString()))
                {
                    this._policyType = policyType;  // (LocalizationPolicyTypes)Enum.Parse(typeof(LocalizationPolicyTypes), policyType.ToString());
                    configProvider.SetValue(LocalizationInfo.XML_ATT_VALUE_POLICY_SETTING, LocalizationInfo.XML_ATT_VALUE_TYPE, policyType.ToString());
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }         

            return;
        }

        /// <summary>
        /// Set Current Thread Culture with default culture of client system. 
        /// This setting is specified in Regional and Language Options of Control Panel.
        /// </summary>
        public void SetCultureWithSystemSetting()
        {
            CultureInfo ci = null;
            int systemLCID = -1;

            systemLCID = GetUserDefaultLCID();

            ci = new CultureInfo(systemLCID);

            this.SetCurrentCulture(ci, LocalizationPolicyTypes.OS);
            return;
        }

        /// <summary>
        /// Set Current Thread Culture using setting value which is stored in LocalizationInfo.xml file
        /// </summary>
        public void SetCultureWithPolicy(LocalizationPolicyTypes policyType)
        {
            string cultureName = string.Empty;

            try
            {
                cultureName = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME);
                this.SetCultureWithName(cultureName, policyType);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        /// <summary>
        /// Set Current Thread Culture using setting value which is stored in LocalizationInfo.xml file
        /// </summary>
        public void SetCultureWithBasicPolicy()
        {
            string cultureName = string.Empty;

            try
            {
                cultureName = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME);
                this.SetCultureWithName(cultureName, LocalizationPolicyTypes.Basic);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        /// <summary>
        /// Set Current Thread Culture using culture name(like 'en-US')
        /// /// </summary>
        /// <param name="cultureName">The name of culture like 'en-US', ko-KR</param>
        public void SetCultureWithName(string cultureName, LocalizationPolicyTypes policyType)
        {
            CultureInfo ci = null;

            try
            {
                ci = new CultureInfo(cultureName);
                this.SetCurrentCulture(ci, policyType);
            }
            catch (ArgumentException argEx)
            {
                //MSG:Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                ExceptionHandler.Wrap(argEx, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00004", LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        /// <summary>
        /// Set Current Thread Culture using culture name(like 'en-US')
        /// /// </summary>
        /// <param name="cultureName">The name of culture like 'en-US', ko-KR</param>
        public void SetCultureWithName(string cultureName)
        {
            CultureInfo ci = null;

            try
            {
                ci = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
                XmlConfigProvider configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this.SourcePath);

                if (ConfigUtil.IsNewXmlValue(configProvider, LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME, cultureName))
                {
                    configProvider.SetValue(LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME, cultureName);
                }   
            }
            catch (ArgumentException argEx)
            {
                //MSG:Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                ExceptionHandler.Wrap(argEx, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00004", LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }


        /// <summary>
        /// Set Current Thread Culture with custom setting values which are stored in LocalizationInfo.xml file
        /// </summary>
        public void SetCultureWithCustomPolicy()
        {
            CultureInfo ci = null;

            string cultureName  = string.Empty;
            string fullDateTimeVal = string.Empty;
            string longDateVal = string.Empty;
            string shortDateVal = string.Empty;
            string monthDayVal = string.Empty;
            string yearMonthVal = string.Empty;
            string shortTimeVal = string.Empty;
            string longTimeVal = string.Empty;
            string currencySymbol = string.Empty;
            string currencyDecimalDigits = string.Empty;
            string currencycultureName = string.Empty;

            try
            {
                cultureName = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME);

                ci = new CultureInfo(cultureName, true);

                ci.DateTimeFormat.FullDateTimePattern = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_DATETIME_FORMAT, LocalizationInfo.XML_ATT_VALUE_FULL_DATETIME);
                ci.DateTimeFormat.LongDatePattern     = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_DATETIME_FORMAT, LocalizationInfo.XML_ATT_VALUE_LONG_DATE);
                ci.DateTimeFormat.ShortDatePattern    = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_DATETIME_FORMAT, LocalizationInfo.XML_ATT_VALUE_SHORT_DATE);
                ci.DateTimeFormat.MonthDayPattern     = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_DATETIME_FORMAT, LocalizationInfo.XML_ATT_VALUE_MONTH_DAY);
                ci.DateTimeFormat.YearMonthPattern    = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_DATETIME_FORMAT, LocalizationInfo.XML_ATT_VALUE_YEAR_MONTH);
                ci.DateTimeFormat.ShortTimePattern    = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_DATETIME_FORMAT, LocalizationInfo.XML_ATT_VALUE_SHORT_TIME);
                ci.DateTimeFormat.LongTimePattern     = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_DATETIME_FORMAT, LocalizationInfo.XML_ATT_VALUE_LONG_TIME);

                ci.NumberFormat.CurrencySymbol           = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_CURRENCY_FORMAT, LocalizationInfo.XML_ATT_VALUE_SYMBOL);
                ci.NumberFormat.CurrencyDecimalDigits    = Convert.ToInt32(ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_CURRENCY_FORMAT, LocalizationInfo.XML_ATT_VALUE_DECIMAL_DIGITS));
                ci.NumberFormat.CurrencyDecimalSeparator = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_CURRENCY_FORMAT, LocalizationInfo.XML_ATT_VALUE_DECIMAL_SEPARATOR);
                ci.NumberFormat.CurrencyGroupSizes       = new int[]{Convert.ToInt32(ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_CURRENCY_FORMAT, LocalizationInfo.XML_ATT_VALUE_GROUP_SIZES))};
                ci.NumberFormat.CurrencyDecimalSeparator = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_CURRENCY_FORMAT, LocalizationInfo.XML_ATT_VALUE_GROUP_SEPARATOR);

                ci.NumberFormat.NumberDecimalDigits    = Convert.ToInt32(ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, LocalizationInfo.XML_ATT_VALUE_DECIMAL_DIGITS));
                ci.NumberFormat.NumberDecimalSeparator = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, LocalizationInfo.XML_ATT_VALUE_DECIMAL_SEPARATOR);
                ci.NumberFormat.NumberGroupSizes       = new int[]{Convert.ToInt32(ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, LocalizationInfo.XML_ATT_VALUE_GROUP_SIZES))};
                ci.NumberFormat.NumberGroupSeparator   = ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, LocalizationInfo.XML_ATT_VALUE_GROUP_SEPARATOR);

                ci.NumberFormat.PercentPositivePattern = Convert.ToInt32(ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, LocalizationInfo.XML_ATT_VALUE_PERCENT_POSITIVE));
                ci.NumberFormat.PercentNegativePattern = Convert.ToInt32(ConfigUtil.GetValidValueFromXml(this.SourcePath, LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, LocalizationInfo.XML_ATT_VALUE_PERCENT_NEGATIVE));

                this.SetCurrentCulture(ci, LocalizationPolicyTypes.Custom);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }
        #endregion


        #region METHOD AREA (Save Localization Config)**********
        /// <summary>
        /// Save Localization inforamtin
        /// </summary>
        /// <param name="param">Localization setting values parameter</param>
        /// <returns>whether localization information is changed or not</returns>
        public bool SaveLocalizationSetting(LocalizationSettingParam param)
        {
            XmlConfigProvider configProvider = null;
            bool isModified = false;

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this.SourcePath);

                if (ConfigUtil.IsNewXmlValue(configProvider, LocalizationInfo.XML_ATT_VALUE_POLICY_SETTING, LocalizationInfo.XML_ATT_VALUE_TYPE, param.PolicyType))
                {
                    isModified = true;
                    this._policyType = (LocalizationPolicyTypes)Enum.Parse(typeof(LocalizationPolicyTypes), param.PolicyType);
                    configProvider.SetValue(LocalizationInfo.XML_ATT_VALUE_POLICY_SETTING, LocalizationInfo.XML_ATT_VALUE_TYPE, param.PolicyType);
                }

                if (ConfigUtil.IsNewXmlValue(configProvider, LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME, param.CultureName))
                {
                    isModified = true;
                    configProvider.SetValue(LocalizationInfo.XML_ATT_VALUE_CULTURE_SETTING, LocalizationInfo.XML_ATT_VALUE_NAME, param.CultureName);
                }

                foreach (CultureFormatDataItem item in param.CurrencyFormatList)
                {
                    if (item.OpCode != OpCodes.READ)
                    {
                        isModified = true;
                        configProvider.SetValue(LocalizationInfo.XML_ATT_VALUE_CURRENCY_FORMAT, item.SettingKey, item.SettingValue);
                    }
                }

                foreach (CultureFormatDataItem item in param.NumberFormatList)
                {
                    if (item.OpCode != OpCodes.READ)
                    {
                        isModified = true;
                        configProvider.SetValue(LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, item.SettingKey, item.SettingValue);
                    }
                }

                foreach (CultureFormatDataItem item in param.DateTimeFormatList)
                {
                    if (item.OpCode != OpCodes.READ)
                    {
                        isModified = true;
                        configProvider.SetValue(LocalizationInfo.XML_ATT_VALUE_DATETIME_FORMAT, item.SettingKey, item.SettingValue);
                    }
                }

                //Culture Changed Event Fire
                if (isModified)
                {
                    this.AfterReloadFireCultureChangedEvent();
                }
                    

            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            return isModified;
        }
        #endregion


        #region EVENT HANDLER AREA *****************************
        /// <summary>
        /// Culture Changed Event Fire
        /// </summary>
        public void  AfterReloadFireCultureChangedEvent()
        {
            this.LoadEnvironmentInfo();
            this.FireCultureChangedEvent();
        }

        /// <summary>
        /// OS Locale Changed Event Handler. 
        /// This event handler method will be called when user change locale on Regional and Language option in Control Panel
        /// </summary>
        /// <param name="sender">Culture Changed Event source</param>
        /// <param name="e">CultureChangedEvent argument</param>
        public void SystemLocaleChangedHandler(object sender, UserPreferenceChangedEventArgs args)
        {
            if (this.PolicyType == LocalizationPolicyTypes.OS && args.Category.Equals(UserPreferenceCategory.Locale))
            {
                Control.CheckForIllegalCrossThreadCalls = false;

                this.LoadEnvironmentInfo();

                this.FireCultureChangedEvent();
            }
            return;
        }

        /// <summary>
        /// Fires CultureChanged Event 
        /// </summary>
        private void FireCultureChangedEvent()
        {
            try
            {
                if (this.CultureChanged != null)
                {
                    this.CultureChanged(this, new CultureChangedEventArgs(this._policyType));
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
            return;
        }
        #endregion


        #region METHOD AREA (Get Win32 System LCID)*************
        /// <summary>
        /// Get current system region and language setting value through WIN32 API call
        /// </summary>
        /// <returns>current LCID value</returns>
        [DllImport("kernel32.dll")]
        static extern int GetUserDefaultLCID();
        #endregion


        #region METHOD AREA (Get Format Item List)**************
        /// <summary>
        /// Returns CurrencyFormatItemList
        /// </summary>
        /// <returns>CurrencyFormatItemList</returns>
		public BaseItemsList<CultureFormatDataItem> GetCurrencyFormatItemList()
        {
			BaseItemsList<CultureFormatDataItem> itemList = null;

            try
            {
				itemList = new BaseItemsList<CultureFormatDataItem>();

                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Currency, "WRD_FTCO_CurrencySymbol", LocalizationInfo.XML_ATT_VALUE_SYMBOL, Convert.ToString(this.NumberFormat.CurrencySymbol)));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Currency, "WRD_FTCO_CurrencyDecimalDigits",LocalizationInfo.XML_ATT_VALUE_DECIMAL_DIGITS, Convert.ToString(this.NumberFormat.CurrencyDecimalDigits)));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Currency, "WRD_FTCO_CurrencyDecimalSeparator",LocalizationInfo.XML_ATT_VALUE_DECIMAL_SEPARATOR, this.NumberFormat.CurrencyDecimalSeparator.Trim()));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Currency, "WRD_FTCO_CurrencyGroupSizes", LocalizationInfo.XML_ATT_VALUE_GROUP_SIZES, Convert.ToString(this.NumberFormat.CurrencyGroupSizes.GetValue(0))));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Currency, "WRD_FTCO_CurrencyGroupSeparator", LocalizationInfo.XML_ATT_VALUE_GROUP_SEPARATOR, this.NumberFormat.CurrencyGroupSeparator.Trim()));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            return itemList;
        }

        /// <summary>
        /// Returns NumberFormatitemLists
        /// </summary>
        /// <returns>NumberFormatItemLists</returns>
		public BaseItemsList<CultureFormatDataItem> GetNumberFormatItemList()
        {
			BaseItemsList<CultureFormatDataItem> itemList = null;

            try
            {
				itemList = new BaseItemsList<CultureFormatDataItem>();
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Number, "WRD_FTCO_NumberDecimalDigits", LocalizationInfo.XML_ATT_VALUE_DECIMAL_DIGITS, Convert.ToString(this.NumberFormat.NumberDecimalDigits)));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Number, "WRD_FTCO_NumberDecimalSeparator", LocalizationInfo.XML_ATT_VALUE_DECIMAL_SEPARATOR, this.NumberFormat.NumberDecimalSeparator.Trim()));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Number, "WRD_FTCO_NumberGroupSizes", LocalizationInfo.XML_ATT_VALUE_GROUP_SIZES, Convert.ToString(this.NumberFormat.NumberGroupSizes.GetValue(0))));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Number, "WRD_FTCO_NumberGroupSeparator", LocalizationInfo.XML_ATT_VALUE_GROUP_SEPARATOR, this.NumberFormat.NumberGroupSeparator.Trim()));

                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Number, "WRD_FTCO_PercentPositivePattern",LocalizationInfo.XML_ATT_VALUE_PERCENT_POSITIVE, this.ConvertPercentPatternToString(true, this.NumberFormat.PercentPositivePattern)));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.Number, "WRD_FTCO_PercentNegativePattern",LocalizationInfo.XML_ATT_VALUE_PERCENT_NEGATIVE, this.ConvertPercentPatternToString(false, this.NumberFormat.PercentNegativePattern)));

            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            return itemList;
        }


        /// <summary>
        /// Returns DateTimeFormatItemList
        /// </summary>
        /// <returns>DateTimeFormatItemList</returns>
		public BaseItemsList<CultureFormatDataItem> GetDateTimeFormatItemList()
        {
			BaseItemsList<CultureFormatDataItem> itemList = null;

            try
            {
				itemList = new BaseItemsList<CultureFormatDataItem>();

                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.DateTime, "WRD_FTCO_FullDateTimePattern", LocalizationInfo.XML_ATT_VALUE_FULL_DATETIME, this.DateTimeFormat.FullDateTimePattern));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.DateTime, "WRD_FTCO_LongDatePattern", LocalizationInfo.XML_ATT_VALUE_LONG_DATE, this.DateTimeFormat.LongDatePattern));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.DateTime, "WRD_FTCO_ShortDatePattern", LocalizationInfo.XML_ATT_VALUE_SHORT_DATE, this.DateTimeFormat.ShortDatePattern));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.DateTime, "WRD_FTCO_MonthDayPattern", LocalizationInfo.XML_ATT_VALUE_MONTH_DAY, this.DateTimeFormat.MonthDayPattern));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.DateTime, "WRD_FTCO_YearMonthPattern", LocalizationInfo.XML_ATT_VALUE_YEAR_MONTH, this.DateTimeFormat.YearMonthPattern));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.DateTime, "WRD_FTCO_ShortTimePattern", LocalizationInfo.XML_ATT_VALUE_SHORT_TIME, this.DateTimeFormat.ShortTimePattern));
                itemList.Add(new CultureFormatDataItem(CultureFormatTypes.DateTime, "WRD_FTCO_LongTimePattern", LocalizationInfo.XML_ATT_VALUE_LONG_TIME, this.DateTimeFormat.LongTimePattern));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            return itemList;
        }
        #endregion


        #region METHOD AREA (Convert Method)********************
        /// <summary>
        /// Returns converted pattern string from percent pattern value
        /// </summary>
        /// <param name="isPositiveType">true, if target to converted is positive pattern</param>
        /// <param name="patternType">The integer value of percent pattern</param>
        /// <returns>Converted string value</returns>
        public string ConvertPercentPatternToString(bool isPositiveType, int patternType)
        {
            string rtnPattern = null;

            if(isPositiveType)
            {
                rtnPattern = (string)this._positivePercentPatterns.GetValue(patternType);

                if (string.IsNullOrEmpty(rtnPattern))
                {
                    //MSG: Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00004", LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, LocalizationInfo.XML_ATT_VALUE_PERCENT_POSITIVE);
                }
            }
            else
            {
                rtnPattern = (string)this._negativePercentPatterns.GetValue(patternType);

                if (string.IsNullOrEmpty(rtnPattern))
                {
                    //MSG: Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00004", LocalizationInfo.XML_ATT_VALUE_NUMBER_FORMAT, LocalizationInfo.XML_ATT_VALUE_PERCENT_NEGATIVE);
                }
            }

            return rtnPattern;
        }

        /// <summary>
        /// Returns converted pattern string to percent pattern value
        /// </summary>
        /// <param name="isPositiveType">true, if target to converted is positive pattern</param>
        /// <param name="pattern">The string value of percent pattern</param>
        /// <returns>Converted integer pattern value</returns>
        public int ConvertStringToPattern(bool isPositiveType, string pattern)
        {
            int nPattern = -1;

            try
            {
                if (isPositiveType)
                {
                    for (int i = 0; i < this._positivePercentPatterns.Length; i++)
                    {
                        if (this._positivePercentPatterns[i].Equals(pattern))
                        {
                            nPattern = i;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < this._negativePercentPatterns.Length; i++)
                    {
                        if (this._negativePercentPatterns[i].Equals(pattern))
                        {
                            nPattern = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
            if(nPattern ==-1)
            {
                string patternName = isPositiveType ? "WRD_FTCO_PercentPositivePattern" : "WRD_FTCO_PercentNegativePattern";

                //MSG:Inputed {0} value({1}) is not supported. 
                throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00124", patternName, pattern);
            }

            return nPattern;
        }
        #endregion


        #region METHOD AREA (Supported Culture)*****************
        /// <summary>
        /// Returns list of CultureInfo objects that are supported culture by system
        /// </summary>
        /// <returns>List of CultureInfo objects</returns>
        private List<CultureInfo> GetSupportedCultureList()
        {
            List<CultureInfo> rtnCultureInfos = null;

            try
            {
                rtnCultureInfos = new List<CultureInfo>();

                Assembly assembly = Assembly.Load(this.defaultResouceAssemblyName);
                DirectoryInfo dirInfo = new DirectoryInfo(PathUtil.GetBasePath());

                if (assembly == null)
                {
                    return rtnCultureInfos;
                }

                //string assemblyFullPath;
                //string assemblyDir;
                //string assemblyName;
                string[] dirNames;

                foreach (DirectoryInfo subDir in dirInfo.GetDirectories())
                {
                    try
                    {

                        if (subDir == null || string.IsNullOrEmpty(subDir.Name) == true)
                        {
                            continue;
                        }

                        dirNames = subDir.Name.Split(this.CULTURE_DELIM_NAME);

                        if (dirNames.Length == 2)
                        {
                            //실제 리소스 파일의 존재 여부를 체크한다.
                            if (this.IsExistsResource(assembly, subDir.Name) == true)
                            {
                                this.AddCultureInfo(rtnCultureInfos, assembly, subDir.Name);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        GeneralLogger.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnCultureInfos;
        }

        /// <summary>
        /// Determines whether the specified resource file exists.
        /// </summary>
        /// <param name="assembly">The assembly to check local resource.</param>
        /// <param name="name">
        /// A predefined System.Globalization.CultureInfo name, System.Globalization.CultureInfo.Name
        /// of an existing System.Globalization.CultureInfo, or Windows-only culture name.
        /// </param>
        /// <returns>
        /// true if the assembly has resource file about culture name; otherwise, false
        /// </returns>
        private bool IsExistsResource(Assembly assembly, string name)
        {
            bool returValue = false;

            string assemblyFullPath;
            string assemblyDir;
            string assemblyName;

            try
            {
                //Assembly 파일.
                assemblyFullPath = assembly.Location;
                //실행파일.
                assemblyDir = PathUtil.GetBasePath();
                //확장자를 제외한 Assembly 파일 이름.
                assemblyName = Path.GetFileNameWithoutExtension(assemblyFullPath);

                //특정 언어에 대한 지원 가능한 리소스 파일
                string resourceFullPath = string.Format(CultureInfo.InvariantCulture,
                   @"{0}\{1}\{2}.resources.dll",
                   assemblyDir,
                   name,
                   assemblyName);

                //실제 리소스 파일의 존재 여부를 체크한다.
                if (File.Exists(resourceFullPath) == true)
                {
                    returValue = true;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                returValue = false;
            }

            return returValue;
        }

        /// <summary>
        /// Adds culture info.
        /// </summary>
        /// <param name="rtnCultureInfos"></param>
        /// <param name="assembly"></param>
        /// <param name="folderNames"></param>
        private void AddCultureInfo(List<CultureInfo> rtnCultureInfos, Assembly assembly, string folderName)
        {
            CultureInfo ci = null;
            Assembly satellite = null;

            try
            {
                try
                {
                    ci = new CultureInfo(folderName);
                    satellite = assembly.GetSatelliteAssembly(ci);
                    rtnCultureInfos.Add(ci);
                }
                catch (ArgumentException)
                {
                    // Swallow this exception, it means no such resources exist for the given language
                }
                catch (FileNotFoundException)
                {
                    // Swallow this exception, it means no such resources exist for the given language
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion


        
    }
}
