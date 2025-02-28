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
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Environments.Item;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Environments.Param
{
    /// <summary>
    /// Localization Setting Parameter class
    /// </summary>
    public class LocalizationSettingParam : BaseParam
    {
        private string _policyType;
        private string _cultureName;
        private BaseItemsList<CultureFormatDataItem> _currencyFormatList;
		private BaseItemsList<CultureFormatDataItem> _numberFormatList;
		private BaseItemsList<CultureFormatDataItem> _dateTimeFormatList;

        /// <summary>
        /// Localization Policy Type
        /// </summary>
        public string PolicyType
        {
            get { return _policyType; }
            set { _policyType = value; }
        }

        /// <summary>
        /// Culture Name (like en-US)
        /// </summary>
        public string CultureName
        {
            get { return _cultureName; }
            set { _cultureName = value; }
        }
        
        /// <summary>
        /// List value of currency format items
        /// </summary>
		public BaseItemsList<CultureFormatDataItem> CurrencyFormatList
        {
            get { return _currencyFormatList; }
            set { _currencyFormatList = value; }
        }

        /// <summary>
        /// List value of number format items
        /// </summary>
		public BaseItemsList<CultureFormatDataItem> NumberFormatList
        {
            get { return _numberFormatList; }
            set { _numberFormatList = value; }
        }

        /// <summary>
        /// List value of date/time format items
        /// </summary>
		public BaseItemsList<CultureFormatDataItem> DateTimeFormatList
        {
            get { return _dateTimeFormatList; }
            set { _dateTimeFormatList = value; }
        }

        public LocalizationSettingParam()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-LocalizationSettingParam";
        }

    }
}
