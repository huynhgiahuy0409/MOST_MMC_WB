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
* 2009.06.17    Jindols 1.0	First release.
* 2009.07.21    CHOI        Update (Resource Path and others)
* 2010.10.08  Tonny.Kim     Update (Multi Resource)
* 2010.10.21  Tonny.Kim     Update (ResourceKey)
* 2010.12.06  Tonny.Kim     Update (GetString)
*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Environments.Item;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Logging;
using Tsb.Fontos.Core.Message;

namespace Tsb.Fontos.Core.Resources
{
    /// <summary>
    /// XML-based resource format (.resx) file type resource Handler
    /// </summary>
    public class ResourceResx : Resource
    {
        #region FIELD AREA ***************************************
        private static ITsbLog log = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Dictionary<string, TResourceManager> _resManagers = new Dictionary<string, TResourceManager>();
        private List<ResourceManager> _defaultResMngList;
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ResourceResx()
            : base()
        {
            this.ObjectID = "GNR-FTCO-RSC-ResourceResx";

            LocalizationInfo localInfo = LocalizationInfo.GetInstance();

            foreach (ResourceDataItem resourceDataItem in localInfo.Resources)
            {
                ResourceManager predefineResMng = null;
                if (String.IsNullOrEmpty(resourceDataItem.PredefineBaseName) == false)
                {
                    predefineResMng = new ResourceManager(resourceDataItem.PredefineBaseName, Assembly.Load(resourceDataItem.ResourcAssemblyName));
                }

                TResourceManager resManager = new TResourceManager(resourceDataItem.BaseName, Assembly.Load(resourceDataItem.ResourcAssemblyName), predefineResMng);

                if (!this._resManagers.ContainsKey(resourceDataItem.Prefix))
                {
                    this._resManagers.Add(resourceDataItem.Prefix, resManager);
                }
            }

            _defaultResMngList = new List<ResourceManager>();
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Returns the value of the resource localized for the specified culture, resource section and resource's key
        /// </summary>
        /// <param name="section">Resource section type(Message, Label)</param>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <param name="culture">The CultureInfo object that represents the culture for which the resource is localized</param>
        /// <returns>The value of the resource</returns>
        public override string GetString(string section, string resourceKey, CultureInfo culture)
        {
            string rtnValue = null;

            ResourceManager resourceManager = this.GetResourceManager(resourceKey);

            if ((resourceManager == null) &&
                !string.IsNullOrEmpty(resourceKey) &&
                !resourceKey.StartsWith(DefaultMessage.NON_REG_WRD))
            {
                //MSG :	ResourceManager does not exist. Please check [LocalizationInfo.xml].
                throw new TsbSysBaseException(this.ObjectID, "MSG_FTCO_00146");
            }

            if (section.Equals(ResourceSection.LABEL))
            {
                rtnValue = this.GetString(resourceManager, resourceKey, culture);
            }
            else if (section.Equals(ResourceSection.MESSAGE))
            {
                rtnValue = this.GetString(resourceManager, resourceKey, culture);
            }
            else
            {
                //MSG :	Resource secition [{0}] is not supported. 
                throw new TsbSysBaseException(this.ObjectID, "MSG_FTCO_00012", section);
            }

            return rtnValue;
        }

        /// <summary>
        /// Returns the value of the resource localized for the specified culture, resource section and resource's key
        /// using a specified ResourceManager
        /// </summary>
        /// <param name="resourceManager">ResourceManager object reference</param>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <param name="culture">The CultureInfo object that represents the culture for which the resource is localized</param>
        /// <returns>The value of the resource</returns>
        public string GetString(ResourceManager resourceManager, string resourceKey, CultureInfo culture)
        {
            string rtnValue = null;

            if (string.IsNullOrEmpty(resourceKey))
            {
                //MSG :Null or Empty resource key is not allowed. 
                throw new TsbSysTypeException(this.ObjectID, "MSG_FTCO_00009", null);
            }

            try
            {
                if (resourceKey.StartsWith(DefaultMessage.NON_REG_WRD))
                {
                    rtnValue = resourceKey.Replace(DefaultMessage.NON_REG_WRD, string.Empty);
                }
                else
                {
                    rtnValue = resourceManager.GetString(resourceKey, culture);
                }

                if (rtnValue == null)
                {
                    //MSG:Resource key [{0}] does not exist. Please check {1}.
                    throw new TsbSysBaseException(this.ObjectID, "MSG_FTCO_00010", resourceKey, "WRD_FTCO_Resourcefile");
                }
            }
            catch (TsbBaseException ex)
            {
                ExceptionHandler.Propagate(ex, this.ObjectID);
            }
            catch (MissingManifestResourceException ex)
            {
                //MSG: Resource key [{0}] does not exist. Please check {1}.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), this.ObjectID, "MSG_FTCO_00010", resourceKey, "WRD_FTCO_Resourcefile");
            }

            return rtnValue;
        }

        /// <summary>
        /// Returns the value of the specified resource section and rosource Key
        /// </summary>
        /// <param name="section">Resource section type(Message, Label)</param>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>The value of the resource</returns>
        public override string GetString(string section, string resourceKey)
        {
            return this.GetString(section, resourceKey, LocalizationInfo.GetInstance().CultureInfo);
        }


        /// <summary>
        /// Returns the label value of the specified rosource Key
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>The value of the resource</returns>
        public override string GetLabel(string resourceKey)
        {
            return this.GetString(ResourceSection.LABEL, resourceKey, LocalizationInfo.GetInstance().CultureInfo);
        }

        /// <summary>
        /// Returns the label value of the resource localized for the specified culture and resource's key
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <param name="culture">The CultureInfo object that represents the culture for which the resource is localized</param>
        /// <returns>The value of the resource</returns>
        public override string GetLabel(string resourceKey, CultureInfo culture)
        {
            return this.GetString(ResourceSection.LABEL, resourceKey, culture);
        }

        /// <summary>
        /// Returns the message string of the specified rosource Key
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>The message string of the resource</returns>
        public override string GetMessage(string resourceKey)
        {
            return this.GetString(ResourceSection.MESSAGE, resourceKey, LocalizationInfo.GetInstance().CultureInfo);
        }

        /// <summary>
        /// Returns the message string value of the resource localized for the specified culture and resource's key
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <param name="culture">The CultureInfo object that represents the culture for which the resource is localized</param>
        /// <returns>The message string of the resource</returns>
        public override string GetMessage(string resourceKey, CultureInfo culture)
        {
            return this.GetString(ResourceSection.MESSAGE, resourceKey, culture);
        }

        /// <summary>
        /// Returns the value of the specified System.Object resource.
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>The value of the resource</returns>
        public override object GetObject(string resourceKey)
        {
            if (string.IsNullOrEmpty(resourceKey) == true)
            {
                return null;
            }

            foreach (KeyValuePair<string, TResourceManager> keyValue in _resManagers)
            {
                if (resourceKey.StartsWith(keyValue.Key))
                {
                    return keyValue.Value.GetObject(resourceKey);
                }
            }

            //GeneralLogger.Warn(string.Format("Does not exist the resource manager object.[section:{0}, resource key:{1}]", section, resourceKey));

            if (_resManagers.ContainsKey(LocalizationInfo.XML_ATT_VALUE_VOCABULARY_DEFAULT_PREFIX))
            {
                return _resManagers[LocalizationInfo.XML_ATT_VALUE_VOCABULARY_DEFAULT_PREFIX].GetObject(resourceKey);
            }

            foreach (ResourceManager resourceManager in _defaultResMngList)
            {
                object resource = resourceManager.GetObject(resourceKey);
                if (resource != null)
                {
                    return resource;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the value of the specified System.Drawing.Image resource.
        /// If the value is System.Drawing.Icon, convert to System.Drawing.Image.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public override Image GetImage(string resourceKey)
        {
            Image menuImage = null;

            try
            {
                object resoureObject = this.GetObject(resourceKey);

                if (resoureObject != null)
                {
                    if (resoureObject is Icon)
                    {
                        menuImage = (resoureObject as Icon).ToBitmap();
                    }
                    else
                    {
                        menuImage = resoureObject as Bitmap;
                    }
                }
                else
                {
                    GeneralLogger.Warn(string.Format("Does not exist the image resource.[{0}]", resourceKey));
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return menuImage;
        }

        /// <summary>
        /// Returns the value of the specified System.Drawing.Icon resource.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public override Icon GetIcon(string resourceKey)
        {
            Icon menuImage = null;

            try
            {
                object resoureObject = this.GetObject(resourceKey);

                if (resoureObject != null)
                {
                    if (resoureObject is Icon)
                    {
                        menuImage = (resoureObject as Icon);
                    }
                }
                else
                {
                    GeneralLogger.Warn(string.Format("Does not exist the icon resource.[{0}]", resourceKey));
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return menuImage;
        }

        /// <summary>
        /// Sets the default resource manager.
        /// </summary>
        /// <param name="manager"></param>
        public override void SetDefaultResourceManager(ResourceManager manager)
        {
            _defaultResMngList.Insert(0, manager);
        }

        /// <summary>
        /// Appends the default resource manager.
        /// </summary>
        /// <param name="manager"></param>
        public override void AppendDefaultResourceManager(ResourceManager manager)
        {
            _defaultResMngList.Add(manager);
        }

        /// <summary>
        /// Returns ResourceManager of the resouce key prefix.
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>ResourceManager</returns>
        public override ResourceManager GetResourceManager(string resourceKey)
        {
            ResourceManager returnResourceManager = null;

            foreach (KeyValuePair<string, TResourceManager> keyValue in _resManagers)
            {
                if (resourceKey.StartsWith(keyValue.Key))
                {
                    return keyValue.Value;
                }
            }

            //GeneralLogger.Warn(string.Format("Does not exist the resource manager object.[section:{0}, resource key:{1}]", section, resourceKey));

            if (_resManagers.ContainsKey(LocalizationInfo.XML_ATT_VALUE_VOCABULARY_DEFAULT_PREFIX))
            {
                returnResourceManager = _resManagers[LocalizationInfo.XML_ATT_VALUE_VOCABULARY_DEFAULT_PREFIX];
            }

            if (returnResourceManager == null)
            {
                foreach (ResourceManager resourceManager in _defaultResMngList)
                {
                    if (resourceManager.GetObject(resourceKey) != null)
                    {
                        returnResourceManager = resourceManager;
                        break;
                    }
                }
            }

            return returnResourceManager;
        }
        #endregion
    }
}
