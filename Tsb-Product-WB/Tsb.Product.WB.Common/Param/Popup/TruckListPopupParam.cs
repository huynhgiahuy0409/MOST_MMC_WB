/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2012 TOTAL SOFT BANK LIMITED. All Rights
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
* 2011.01.25  Tonny.Kim 1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Param;

namespace Tsb.Product.WB.Common.Param.Popup
{
    [Serializable]
    public class TruckListPopupParam : BaseParam
    {


        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets or sets LorryNo.
        /// </summary>
        public string LorryNo { get; set; }

        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize TruckListPopupParam.
        /// </summary>
        /// <param name="paramOwner">ParamOwner</param>
        public TruckListPopupParam(object paramOwner)
            : base(paramOwner)
        {
            this.ObjectID = "PAR-PT-PTWB-POP-TruckListPopupParam";
        }

        /// <summary>
        /// Initialize TruckListPopupParam.
        /// </summary>
        /// <param name="paramOwner">ParamOwner</param>
        /// <param name="txServiceID">TranscationID</param>
        public TruckListPopupParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            this.ObjectID = "PAR-PT-PTWB-POP-TruckListPopupParam";
        }
        #endregion
    }
}
