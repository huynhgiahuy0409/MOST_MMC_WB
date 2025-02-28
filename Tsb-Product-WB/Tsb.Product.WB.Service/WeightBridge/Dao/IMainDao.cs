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
using Tsb.Fontos.Core.Item;
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.Sample;
using Tsb.Product.WB.Common.Param.WeightBridge;

namespace Tsb.Product.WB.Service.WeightBridge.Dao
{
    public interface IMainDao
    {
        #region SEARCH AREA *****************************************
        /// <summary>
        /// Inquire the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SinlgeGridItem</param>
        /// <returns>the rows inquired</returns>
        BaseItemsList<WeightInfoItem> InquiryWeightInfo(MainParam param);
        #endregion

        #region CUD AREA ********************************************
        int UpdateWeightBridge(WeightInfoItem param);
        int UpdatePrintCountWeightBridge(WeightInfoItem param);
        int upadteStatusCanCelJobWeightBridge(WeightInfoItem item);
        #endregion
    }
}
