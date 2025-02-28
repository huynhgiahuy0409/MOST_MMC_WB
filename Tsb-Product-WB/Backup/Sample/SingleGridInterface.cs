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
using System.Drawing;
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
    public interface SingleGridInterface : BaseSingleGridViewInterface
    {
        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets FormsName.
        /// </summary>
        string FormName { get; }

        /// <summary>
        /// Gets or sets GridForeColor.
        /// </summary>
        Color GridForeColor { get; set; }

        /// <summary>
        /// Gets or sets GridBackColor.
        /// </summary>
        Color GridBackColor { get; set; }

        /// <summary>
        /// Gets or sets SearchParam.
        /// </summary>
        SingleGridParam SearchParam { get; set; }

        /// <summary>
        /// Gets or sets ActiveItem.
        /// </summary>
        SingleGridItem ActiveItem { get; set; }
        #endregion

        #region METHOD AREA *****************************************
        /// <summary>
        /// Mandatory Check Function
        /// </summary>
        /// <returns>Message String</returns>
        string SaveMandatoryCheck();

        /// <summary>
        /// Before Search, Mandatory Check.
        /// </summary>
        /// <returns>True or fase after Manadatory Check</returns>
        bool SearchMandatoryCheck();

        /// <summary>
        /// Refresh SpreadGrid
        /// </summary>
        void SpreadRefresh();
        #endregion

        #region EVENT AREA ******************************************
        /// <summary>
        /// Occurs before GridRow removed.
        /// </summary>
        event SpreadGridRowRemovedHandler GridRowRemoved;
        #endregion
    }
}
