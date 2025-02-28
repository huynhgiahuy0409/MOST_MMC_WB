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

namespace Tsb.Product.WB.Common.Param.Sample
{
    [Serializable]
    public class SingleGridParam : BaseParam
    {
        #region ENUM AREA *******************************************
        /// <summary>
        /// Enumerates the types of ImdgUnno.
        /// </summary>
        public enum ImdgUnnoType { NONE, IMDG, UNNO }
        #endregion

        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets or sets ImdgClass.
        /// </summary>
        public string ImdgClass { get; set; }

        /// <summary>
        /// Gets or sets Unno.
        /// </summary>
        public string Unno { get; set; }

        /// <summary>
        /// Gets or sets Unid.
        /// </summary>
        public string Unid { get; set; }
        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize SingleGridparam.
        /// </summary>
        /// <param name="paramOwner">ParamOwner</param>
        public SingleGridParam(object paramOwner)
            : base(paramOwner)
        {
            this.ObjectID = "PAR-PT-PTWB-SPL-SingleGridParam";
        }

        /// <summary>
        /// Initialize SingleGridparam.
        /// </summary>
        /// <param name="paramOwner">ParamOwner</param>
        /// <param name="txServiceID">TranscationID</param>
        public SingleGridParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            this.ObjectID = "PAR-PT-PTWB-SPL-SingleGridParam";
        }
        #endregion
    }
}
