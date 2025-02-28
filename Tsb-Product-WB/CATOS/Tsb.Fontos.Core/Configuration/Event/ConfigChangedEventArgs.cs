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
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Configuration.Event
{
    /// <summary>
    /// EventArgs class to be passed as the second parameter of a ConfigChangedHandler event handler. </summary>
    /// <remarks>
    public class ConfigChangedEventArgs : EventArgs, ITsbBaseObject
    {
        private ConfigChangedType _changedType;
        private string _section;
        private string _setting;
        private object _value;
        private string _objectID;

        public ConfigChangedType ChangedType
        {
            get { return _changedType; }
        }

        public string Section
        {
            get { return _section; }
        }

        public string Setting
        {
            get { return _setting; }
        }

        public object Value
        {
            get { return _value; }
        }

        /// <summary>
		/// Initializes a new instance of the ProfileChangedArgs class
        /// </summary>
		/// <param name="changeType">The type of change made to the profile.</param>
		/// <param name="section"> The name of the section involved in the change</param>
		/// <param name="setting">The name of the setting involved in the change</param>
		/// <param name="value">The new value for the setting</param>
        public ConfigChangedEventArgs(ConfigChangedType changeType, string section, string setting, object value) 
		{
            this._changedType = changeType;
            this._section     = section;
            this._setting     = setting;
            this._value       = value;
		}


        #region ITsbBaseObject Members

        public string ObjectID
        {
            get
            {
                return this._objectID;
            }
            set
            {
                this._objectID = "GNR-FTCO-CFG-ConfigChangedEventArgs";
            }
        }

        public ObjectType ObjectType
        {
            get { return ObjectType.DEFAULT; }
        }

        #endregion

    }
}
