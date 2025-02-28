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
* 2010.07.30  Tonny.Kim 1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.Text;
using Tsb.Catos.Cm.Core.Codes.Item;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Core.YardDefine.Param;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Tp.Common.Item.Sample;
using Tsb.Fontos.Tp.Common.Param.Sample;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Win.Grid.Event;

namespace Tsb.Product.WB.Client.Sample
{
    public interface SingleGridDetailInterface : BaseDetailViewInterface
    {
        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets FormName.
        /// </summary>
        string FormName { get; }

        /// <summary>
        /// Gets or sets SearchParam.
        /// </summary>
        SingleGridParam SearchParam { get; set; }

        /// <summary>
        /// Gets or sets DetailItem.
        /// </summary>
        SingleGridItem DetailItem { get; set; }
        #endregion

        #region METHOD AREA *****************************************
        /// <summary>
        /// Mandatory Check Function
        /// </summary>
        /// <returns>Message String</returns>
        string SaveMandatoryCheck();
        #endregion

        #region EVENT AREA ******************************************
        #endregion
    }
}
