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
* 2009.07.20    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using System.Globalization;
using Tsb.Fontos.Core.Event;
using Tsb.Fontos.Core.Environments.Type;

namespace Tsb.Fontos.Core.Environments.Event
{
    /// <summary>
    /// EventArgs class to be passed as the second parameter of a CultureChangedHandler event handler. </summary>
    /// <remarks>
    public class CultureChangedEventArgs : BaseEventArgs
    {
        private LocalizationPolicyTypes _changedType;

        /// <summary>
        /// Changed Localization policy(Basic/OS/Custom) type
        /// </summary>
        public LocalizationPolicyTypes ChangedType
        {
            get { return _changedType; }
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CultureChangedEventArgs() : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-CultureChangedEventArgs";
        }

        /// <summary>
        /// Initializes a new instance of the CultureChangedEventArgs class
        /// </summary>
		/// <param name="changeType">The type of change made to localization policy.</param>
		public CultureChangedEventArgs(LocalizationPolicyTypes changeType) : this()
		{
            this._changedType = changeType;
		}

    }
}
