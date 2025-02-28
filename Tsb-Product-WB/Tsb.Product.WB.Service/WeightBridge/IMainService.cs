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
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.Sample;
using Tsb.Product.WB.Common.Param.WeightBridge;

namespace Tsb.Product.WB.Service.WeightBridge
{
    public interface IMainService : ITsbService
    {
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryWeightInfo(MainParam param);

        [TransactionOption(TransactionScopeTypes.Required)]
        void UpdateRemarkWeightBridge(WeightInfoItem item);
        [TransactionOption(TransactionScopeTypes.Required)]
        void UpdatePrintCountWeightBridge(WeightInfoItem item);
        [TransactionOption(TransactionScopeTypes.Required)]
        void upadteStatusCanCelJobWeightBridge(WeightInfoItem item);
    }
}
