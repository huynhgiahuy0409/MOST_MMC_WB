#region Class Definitions
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
* DATE            AUTHOR		   REVISION    	
* 2010.04.28   Tonny Kim 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Param;

namespace Tsb.Fontos.Core.Common.Param
{
    public class BaseDataChangeParam : BaseParam
    {
        #region PROPERTY AREA ****************************
        public Enum RuleType { get; set; }
        public string[] PropertyNames { get; set; }
        public object TargetObject { get; set; }
        public bool ThrowException { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initialize Instance using  a specified ITsbGrid implements class object reference
        /// </summary>
        /// <param name="grid"> A ITsbGrid implements class object reference</param>
        public BaseDataChangeParam()
            : base()
        {
            this.ObjectID = "PAR-FTCO-CMN-BaseDataChangeParam";
            this.ThrowException = true;
        }
        #endregion
    }
}
