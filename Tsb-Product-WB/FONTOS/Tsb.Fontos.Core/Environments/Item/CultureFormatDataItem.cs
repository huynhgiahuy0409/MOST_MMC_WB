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
* 2009.07.20   CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Environments.Type;

namespace Tsb.Fontos.Core.Environments.Item
{
    /// <summary>
    /// Culture Format Data Item class
    /// </summary>
    [Serializable]
    public class CultureFormatDataItem : BaseDataItem
    {
        private CultureFormatTypes _formatType;
        private string _formatName;
        private string _settingValue;
        private string _settingKey;

        /// <summary>
        /// Culture Format Type (Currency, Number, DateTime)
        /// </summary>
        public CultureFormatTypes FormatType
        {
            get { return _formatType; }
            set { _formatType = value; }
        }
        
        /// <summary>
        /// Format Name(description)
        /// </summary>
        public string FormatName
        {
            get { return _formatName; }
            set { _formatName = value; }
        }

        /// <summary>
        /// Currentry setting value
        /// </summary>
        public string SettingValue
        {
            get { return _settingValue; }
            set { _settingValue = value; }
        }

        /// <summary>
        /// Configuration Setting Key
        /// </summary>
        public string SettingKey
        {
            get { return _settingKey; }
            set { _settingKey = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CultureFormatDataItem() : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-CultureFormatDataItem";
        }

        /// <summary>
        /// Initialize instance with specified values
        /// </summary>
        /// <param name="formatType">Culture Format Type</param>
        /// <param name="formatName">Format Name(description)</param>
        /// <param name="settingKey">Configuration Setting Key</param>
        /// <param name="settingValue">Currentry setting value</param>
        public CultureFormatDataItem(CultureFormatTypes formatType, string formatName, string settingKey, string settingValue)
            : this()
        {
            this.FormatType = formatType;
            this.FormatName = formatName;
            this.SettingKey = settingKey;
            this.SettingValue = settingValue;
        }



    }
}
