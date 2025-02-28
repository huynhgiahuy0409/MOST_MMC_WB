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
* 2009.06.12    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Service
{
    /// <summary>
    /// TSB Base Service Class
    /// </summary>
    public class BaseService : TsbBaseObject, ITsbService
    {
        #region FIELD AREA *************************************
        private string _name = "NOT_ASSIGNED_NAME";
        private string _className = "NOT_ASSIGNED_CLASS_NAME";
        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Gets or Sets Service Name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Gets or Sets Service Class Name
        /// </summary>
        public string ClassName
        {
            get
            {
                return _className;
            }
            set
            {
                _className = value;
            }
        }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseService() : base()
        {
            this.ObjectID = "GNR-FTCO-SVC-BaseService";
            this.ObjectType = ObjectType.SERVICE;
            this.ClassName = this.GetType().FullName;
            this.Name = this.GetType().Name;
        }
        #endregion

    }
}
