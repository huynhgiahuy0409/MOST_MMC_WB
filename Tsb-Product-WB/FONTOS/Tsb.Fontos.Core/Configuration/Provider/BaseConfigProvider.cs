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
using Tsb.Fontos.Core.Configuration.Event;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Configuration.Provider
{
    public abstract class BaseConfigProvider : TsbBaseObject, IConfigProvider
    {
        protected string _sourceName;

        public string SourceName
        {
            get { return _sourceName; }
        }

        #region IConfigProvider Members

        /// <summary>
        ///   Sets the value for an setting inside a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting where the value will be set. </param>
        /// <param name="value">The value to set. If it's null, the setting should be removed. </param>
        /// <remarks>This method needs to be implemented by derived classes.
        /// It should also raise the "Changed" event after the value is set. 
        /// </remarks>
        public abstract void SetValue(string section, string setting, object value);


        /// <summary>
        /// Retrieves the value of an setting inside a section.
        /// </summary>
        /// <param name="section">The name of the section that holds the setting with the value.</param>
        /// <param name="setting">The name of the setting where the value is stored.</param>
        /// <returns>setting's value, or null if the setting does not exist.</returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract object GetValue(string section, string setting);

        /// <summary>
        /// Retrieves the attribute value of an setting inside a section.
        /// </summary>
        /// <param name="section">The name of the section that holds the setting with the value.</param>
        /// <param name="setting">The name of the setting that holds the setting with the value.</param>
        /// <param name="attributeName">The attribute name of the setting where the value is stored.</param>
        /// <returns>setting's value, or null if the setting does not exist.</returns>
        public abstract string GetAttributeValue(string section, string setting, string attributeName);

        /// <summary>
        ///   Retrieves the value of an setting inside a section, or a default value if the setting does not exist. </summary>
        /// <param name="section">The name of the section that holds the setting with the value. </param>
        /// <param name="setting">The name of the setting where the value is stored. </param>
        /// <param name="defaultValue">The value to return if the setting (or section) does not exist. </param>
        /// <returns>
        /// This method calls GetValue(section, entry) of the derived class.
        /// The setting's value converted to a string, or the given default value if the setting does not exist.
        /// </returns>
        public virtual string GetValue(string section, string setting, string defaultValue)
        {
            object value = GetValue(section, setting);
            return (value == null ? defaultValue : value.ToString());
        }

        /// <summary>
        ///   Retrieves the value of an setting inside a section, or a default value if the setting does not exist. </summary>
        /// <param name="section">The name of the section that holds the setting with the value. </param>
        /// <param name="setting">The name of the setting where the value is stored. </param>
        /// <param name="defaultValue">The value to return if the setting (or section) does not exist. </param>
        /// <returns>
        /// This method calls GetValue(section, entry) of the derived class.  
        /// The return value should be the setting's value converted to an integer.  If the value
        /// cannot be converted, the return value should be 0.  If the setting does not exist, the
        /// given default value should be returned. 
        /// </returns>
        public virtual int GetValue(string section, string setting, int defaultValue)
        {
            object value = GetValue(section, setting);
            if (value == null)
                return defaultValue;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///   Retrieves the value of an setting inside a section, or a default value if the setting does not exist. </summary>
        /// <param name="section">The name of the section that holds the setting with the value. </param>
        /// <param name="setting">The name of the setting where the value is stored. </param>
        /// <param name="defaultValue">The value to return if the setting (or section) does not exist. </param>
        /// <returns>
        /// This method calls GetValue(section, entry) of the derived class.
        /// The return value should be the setting's value converted to a double.  If the value
        /// cannot be converted, the return value should be 0.  If the setting does not exist, the
        /// given default value should be returned. 
        /// </returns>
        public virtual double GetValue(string section, string setting, double defaultValue)
        {
            object value = GetValue(section, setting);
            if (value == null)
                return defaultValue;

            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        ///   Retrieves the value of an setting inside a section, or a default value if the setting does not exist. </summary>
        /// <param name="section">The name of the section that holds the setting with the value. </param>
        /// <param name="setting">The name of the setting where the value is stored. </param>
        /// <param name="defaultValue">The value to return if the setting (or section) does not exist. </param>
        /// <returns>
        /// This method calls GetValue(section, entry) of the derived class.
        /// The return value should be the setting's value converted to a bool.  If the value
        /// cannot be converted, the return value should be false.  If the setting does not exist, the
        /// given default value should be returned.
        /// </returns>
        public virtual bool GetValue(string section, string setting, bool defaultValue)
        {
            object value = GetValue(section, setting);

            if (value == null)
                return defaultValue;

            try
            {
                return Convert.ToBoolean(setting);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a section.
        /// </summary>
        /// <param name="section"> The name of the section to remove. </param>
        /// <remarks>
        /// This method needs to be implemented by derived classes.
        /// It should raise the "Changed" event after the section is removed.
        /// </remarks>
        public abstract void RemoveSection(string section);

        /// <summary>
        /// Creates a section.
        /// </summary>
        /// <param name="section"> The name of the section to create. </param>
        /// <remarks>
        /// This method needs to be implemented by derived classes.
        /// It should raise the "Changed" event after the section is created.
        /// </remarks>
        public abstract void CreateSection(string section);


        /// <summary>
        /// Removes an setting from a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting to remove. </param>
        /// <remarks>
        /// This method needs to be implemented by derived classes.
        /// It should raise the "Changed" event after the setting is removed.
        /// </remarks>
        public abstract void RemoveSetting(string section, string setting);

        /// <summary>
        /// Create an setting on a section. </summary>
        /// <param name="section">The name of the section to create.</param>
        /// <param name="setting">The name of the setting to create. </param>
        /// <param name="value">The value of new setting</param>
        /// <remarks>
        /// This method needs to be implemented by derived classes.
        /// It should raise the "Changed" event after the setting is created.
        /// </remarks>
        public abstract void CreateSetting(string section, string setting, object value);

        /// <summary>
        /// Determines if a section exists. </summary>
        /// <param name="section">The name of the section to be checked for existence. </param>
        /// <returns>If the section exists, the return value should be true; otherwise false. </returns>
        public bool HasSection(string section)
        {
            bool hasSection = false;

            try
            {
                string[] sectionNames = this.GetSectionNames();

                if (sectionNames == null)
                {
                    hasSection = false;
                }
                else if (Array.IndexOf(sectionNames, section) >= 0)
                {
                    hasSection = true;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }


            return hasSection;

        }

        /// <summary>
        /// Determines if an setting exists inside a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting to be checked for existence. </param>
        /// <returns>
        /// If the setting exists inside the section, the return value should be true; otherwise false.
        /// </returns>
        public virtual bool HasSetting(string section, string setting)
        {
            bool hasSetting = false;

            try
            {
                string[] settingNames = this.GetSettingNames(section);

                if (settingNames == null)
                {
                    hasSetting = false;
                }
                else if (Array.IndexOf(settingNames, setting) >= 0)
                {
                    hasSetting = true;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return hasSetting;
        }


        /// <summary>
        /// Retrieves the names of all the sections. </summary>
        /// <returns>The return value should be an array with the names of all the sections. </returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract string[] GetSectionNames();

        /// <summary>
        /// Retrieves the names of all the entries inside a section. </summary>
        /// <param name="section">The name of the section holding the entries. </param>
        /// <returns>
        /// If the section exists, the return value should be an array with the names of its entries; 
        /// otherwise it should be null. 
        /// </returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract string[] GetSettingNames(string section);

        public event ConfigChangedHandler Changed;

        #endregion

        /// <summary>
        /// Fire Changed event.
        /// </summary>
        /// <param name="changeType">The type of change being made.</param>
        /// <param name="section">The name of the section that was involved in the change</param>
        /// <param name="setting">The name of the setting that was involved in the change</param>
        /// <param name="value">The value that was changed</param>
        protected void FireConfigChangedEvent(ConfigChangedType changeType, string section, string setting, object value)
        {
            if (Changed != null)
            {
                Changed(this, new ConfigChangedEventArgs(changeType, section, setting, value));
            }
            return;
        }
    }
}
