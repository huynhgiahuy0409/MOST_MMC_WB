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
* DATE           AUTHOR		      REVISION    	
* 2012.04.26  Tonny.Kim 1.0   	First Release
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Event;

namespace Tsb.Fontos.Core.Data.Information
{
    /// <summary>
    /// Database Availability Changed event handler delegator
    /// </summary>
    /// <param name="sender">Object that raised the event</param>
    /// <param name="e">Object of Event Arguments class that contains event data</param>
    public delegate void DatabaseAvailabilityChangedEventHandler(object sender, DatabaseAvailabilityChangedEventArgs e);
    
    /// <summary>
    /// Database Availability Changed event argument class
    /// </summary>
    public class DatabaseAvailabilityChangedEventArgs : BaseEventArgs
    {
        #region PROPERTY/FIDELDS AREA **************************
        private bool isAvaiable = false;
        
        // Summary:
        //     Gets the current status of the database connection.
        //
        // Returns:
        //     true if the database is available; otherwise, false.
        public bool IsAvailable { get {return this.isAvaiable;} }
        #endregion

        #region INITIALIZE AREA ********************************
        /// <summary>
        /// Initialize Instance
        /// </summary>
        public DatabaseAvailabilityChangedEventArgs(bool isAvaiable)
        {
            this.isAvaiable = isAvaiable;
        }
        #endregion
    }
}
