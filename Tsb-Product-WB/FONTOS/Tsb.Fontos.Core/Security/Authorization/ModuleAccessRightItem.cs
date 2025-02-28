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
* 2010.02.05     CHOI       1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;
using System.ComponentModel;
using Tsb.Fontos.Core.Util.Type;

namespace Tsb.Fontos.Core.Security.Authorization
{
    /// <summary>
    /// Base Application access right information Item class
    /// </summary>
    [Serializable]
    public class ModuleAccessRightItem : BaseDataItem
    {
        #region FIELD/PROPERTY AREA*****************************

        private string _userType;
        /// <summary>
        /// Gets or Sets user type string
        /// </summary>
        public string UserType
        {
            get { return this._userType; }
            set { _userType = value; }
        }

        private string _pgmCode;
        /// <summary>
        /// Gets or Sets program code string
        /// </summary>
        public string PgmCode
        {
          get { return _pgmCode; }
          set { _pgmCode = value; }
        }

        private string _useChkYN;
        /// <summary>
        /// Gets or Sets Use Check 
        /// </summary>
        public string UseCheckYN
        {
          get { return _useChkYN; }
          set { _useChkYN = value; }
        }
        #endregion


        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ModuleAccessRightItem()
        {
            this.ObjectID = "ITM-FT-FTCO-SEC-ModuleAccessRightItem";
        }
        #endregion

    }
}
