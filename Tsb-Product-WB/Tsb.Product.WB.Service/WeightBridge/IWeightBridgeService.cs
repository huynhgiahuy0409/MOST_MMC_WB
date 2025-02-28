using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.WeightBridge;

namespace Tsb.Product.WB.Service.WeightBridge
{
    public interface IWeightBridgeService : ITsbService
    {
        [TransactionOption(TransactionScopeTypes.Support)]
        string GetNewTransactionNo(WeightBridgeParam param);

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryWeightBridge(WeightBridgeParam param);

        [TransactionOption(TransactionScopeTypes.Required)]
        void ProcessWeightBridge(WeightBridgeItem  item);
    }
}
