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
* 2009.08.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Security.Profile
{
    /// <summary>
    /// Base User Group Information class
    /// </summary>
    public class BaseGroupInfo : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************

        private string _groupID = null;
        /// <summary>
        /// Group ID
        /// </summary>
        public string GroupID
        {
            get { return _groupID; }
        }

        private string _useApp = null;
        /// <summary>
        /// App authorizing flag data
        /// </summary>
        public string UseApp
        {
            get { return _useApp; }
            set { this._useApp = value; }
        }
        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        private BaseGroupInfo() : base()
        {
            this.ObjectID = "GNR-FTCO-SEC-BaseGroupInfo";
            this._groupID = "DEFAULT_GROUP_ID";
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseGroupInfo(string groupID) : this()
        {
            this._groupID = groupID;
        }
    }
}
