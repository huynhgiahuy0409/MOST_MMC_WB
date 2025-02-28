﻿#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2010 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR 		REVISION    	
* 2011.07.20  Tonny Kim 1.0   First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Common.Param;

namespace Tsb.Fontos.Core.Common.DataValidate
{
    public abstract class BaseDataValidateManager : TsbBaseObject
    {
        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initilize instance
        /// </summary>
        public BaseDataValidateManager()
            : base()
        {
            this.ObjectID = "GNR-FTCO-CMN-BaseDataValidateManager";
        }
        #endregion

        #region abstract METHOD AREA ********************************
        public abstract bool IsDataValidValue(BaseDataValidateParam dataValidateParam);
        public abstract string GetValidValue(BaseDataValidateParam dataValidateParam);
        #endregion
    }
}
