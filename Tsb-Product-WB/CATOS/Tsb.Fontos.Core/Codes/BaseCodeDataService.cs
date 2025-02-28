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
* 2009.09.05    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Codes;

namespace Tsb.Fontos.Core.Codes
{
    /// <summary>
    /// Code Item Service Base class
    /// </summary>
    public class BaseCodeDataService : BaseService, IBaseCodeDataService
    {
        #region FIELD AREA *************************************
        
        /// <summary>
        /// Data access object
        /// </summary>
        protected IBaseCodeDataDao _codeDataItemDao = null;
        
        #endregion


        #region PROPERTY AREA **********************************
        /// <summary>
        /// Gets or sets the DAO of booking-list.
        /// </summary>
        public IBaseCodeDataDao CodeItemDao
        {
            get { return _codeDataItemDao; }
            set { _codeDataItemDao = value; }
        }
        #endregion

        
        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public BaseCodeDataService() 
            : base()
        {
            this.ObjectID = "GNR_FTCO_COD-BaseCodeDataService";
        }
        #endregion
    }
}
