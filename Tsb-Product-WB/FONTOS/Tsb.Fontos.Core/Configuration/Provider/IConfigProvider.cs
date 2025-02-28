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

namespace Tsb.Fontos.Core.Configuration.Provider
{
    /// <summary>
    /// Base interface for all Config Provider classes
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        /// Sets the value for an setting inside a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting where the value will be set. </param>
        /// <param name="value">The value to set. If it's null, the setting should be removed. </param>
        /// <remarks>It should also raise the "Changed" event before and after the value is set. </remarks>
        void SetValue(string section, string setting, object value);

        /// <summary>
        /// Retrieves the value of an setting inside a section.
        /// </summary>
        /// <param name="section">The name of the section that holds the setting with the value.</param>
        /// <param name="setting">The name of the setting where the value is stored.</param>
        /// <returns>setting's value, or null if the setting does not exist.</returns>
        object GetValue(string section, string setting);


        /// <summary>
        /// Retrieves the value of an setting inside a section, or a default value if the setting does not exist. </summary>
        /// <param name="section">The name of the section that holds the setting with the value. </param>
        /// <param name="setting">The name of the setting where the value is stored. </param>
        /// <param name="defaultValue">The value to return if the setting (or section) does not exist. </param>
        /// <returns>The setting's value converted to a string, or the given default value if the setting does not exist. </returns>
        string GetValue(string section, string setting, string defaultValue);

        /// <summary>
        /// Retrieves the attribute value of an setting inside a section.
        /// </summary>
        /// <param name="section">The name of the section that holds the setting with the value.</param>
        /// <param name="setting">The name of the setting that holds the setting with the value.</param>
        /// <param name="attributeName">The attribute name of the setting where the value is stored.</param>
        /// <returns>setting's value, or null if the setting does not exist.</returns>
        string GetAttributeValue(string section, string setting, string attributeName);

        /// <summary>
        /// Retrieves the value of an setting inside a section, or a default value if the setting does not exist. </summary>
        /// <param name="section">The name of the section that holds the setting with the value. </param>
        /// <param name="setting">The name of the setting where the value is stored. </param>
        /// <param name="defaultValue">The value to return if the setting (or section) does not exist. </param>
        /// <returns>
        /// The return value should be the setting's value converted to an integer.  If the value
        /// cannot be converted, the return value should be 0.  If the setting does not exist, the
        /// given default value should be returned.
        /// </returns>
        int GetValue(string section, string setting, int defaultValue);

        /// <summary>
        /// Retrieves the value of an setting inside a section, or a default value if the setting does not exist. </summary>
        /// <param name="section">The name of the section that holds the setting with the value. </param>
        /// <param name="setting">The name of the setting where the value is stored. </param>
        /// <param name="defaultValue">The value to return if the setting (or section) does not exist. </param>
        /// <returns>
        /// The return value should be the setting's value converted to a double.  If the value
        /// cannot be converted, the return value should be 0.  If the setting does not exist, the
        /// given default value should be returned.
        /// </returns>
        double GetValue(string section, string setting, double defaultValue);

        /// <summary>
        /// Retrieves the value of an setting inside a section, or a default value if the setting does not exist. </summary>
        /// <param name="section">The name of the section that holds the setting with the value. </param>
        /// <param name="setting">The name of the setting where the value is stored. </param>
        /// <param name="defaultValue">The value to return if the setting (or section) does not exist. </param>
        /// <returns>
        /// The return value should be the setting's value converted to a bool.  If the value
        /// cannot be converted, the return value should be false.  If the setting does not exist, the
        /// given default value should be returned.
        /// </returns>
        bool GetValue(string section, string setting, bool defaultValue);

        /// <summary>
        /// Removes a section. </summary>
        /// <param name="section"> The name of the section to remove. </param>
        /// <remarks>It should raise the "Changed" event after the section is removed.</remarks>
        void RemoveSection(string section);


        /// <summary>
        /// Creates a section. </summary>
        /// <param name="section"> The name of the section to create. </param>
        /// <remarks>It should raise the "Changed" event after the section is created.</remarks>
        void CreateSection(string section);

        /// <summary>
        /// Removes an setting from a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting to remove. </param>
        /// <remarks>It should raise the "Changed" event after the setting is removed.</remarks>
        void RemoveSetting(string section, string setting);

        /// <summary>
        /// Create an setting on a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting to create. </param>
        /// <param name="value">The value of new setting</param>
        /// <remarks>It should raise the "Changed" event after the setting is removed.</remarks>
        void CreateSetting(string section, string setting, object value);

        /// <summary>
        /// Determines if a section exists. </summary>
        /// <param name="section">The name of the section to be checked for existence. </param>
        /// <returns>If the section exists, the return value should be true; otherwise false. </returns>
        bool HasSection(string section);

        /// <summary>
        /// Determines if an setting exists inside a section. </summary>
        /// <param name="section">The name of the section that holds the setting. </param>
        /// <param name="setting">The name of the setting to be checked for existence. </param>
        /// <returns>If the setting exists inside the section, the return value should be true; otherwise false.</returns>
        bool HasSetting(string section, string setting);

        /// <summary>
        /// Retrieves the names of all the sections. </summary>
        /// <returns>The return value should be an array with the names of all the sections. </returns>
        string[] GetSectionNames();

        /// <summary>
        /// Retrieves the names of all the settings inside a section. </summary>
        /// <param name="section">The name of the section holding the entries. </param>
        /// <returns>
        /// If the section exists, the return value should be an array with the names of its entries; 
        /// otherwise it should be null. 
        /// </returns>
        string[] GetSettingNames(string section);

        /// <summary>
        ///   Event that should be raised right after the profile has been changed. </summary>
        /// <seealso cref="Changing" />
        event ConfigChangedHandler Changed;		

    }
}
