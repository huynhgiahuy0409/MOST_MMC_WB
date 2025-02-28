using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.WeightBridge;

namespace Tsb.Product.WB.Service.WeightBridge.Dao
{
    public interface IWeightBridgeDao
    {
        #region SEARCH AREA *****************************************
        WeightBridgeItem SelectTransactionNo(WeightBridgeParam parm);
        BaseItemsList<WeightBridgeItem> InquiryWeightBridge(WeightBridgeParam param);
        #endregion
        #region CUD AREA ********************************************
        object CreateWeightBridge(WeightBridgeItem item);
        int UpdateWeightBridge(WeightBridgeItem item);
        #endregion
    }
}
