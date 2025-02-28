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
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Param.Sample;

namespace Tsb.Product.WB.Service.WeightBridge
{
    public interface ISingleGridService : ITsbService
    {
        #region SEARCH AREA *****************************************
        /// <summary>
        /// Inquire the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SinlgeGridItem</param>
        /// <returns>the rows inquired</returns>      
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquirySingleGrid(SingleGridParam param);
        #endregion

        #region CUD AREA ********************************************
        /// <summary>
        /// Insert or Update a row into the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SingleGridItem</param>
        [TransactionOption(TransactionScopeTypes.Required)]
        void ProcessSingleGrid(SingleGridItem item);

        /// <summary>
        /// Deletes a row into the SingleGrid's table with the specified item.
        /// </summary>
        /// <param name="item">SinlgeGridItem</param>
        [TransactionOption(TransactionScopeTypes.Required)]
        void DeleteSingleGrid(SingleGridItem item);
        #endregion

    }
}
