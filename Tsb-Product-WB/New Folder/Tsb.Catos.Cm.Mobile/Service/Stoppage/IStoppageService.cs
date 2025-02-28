using Tsb.Catos.Cm.Mobile.Common.Param;
using Tsb.Catos.Cm.Mobile.Common.Param.Stoppage;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;

namespace Tsb.Catos.Cm.Mobile.Service.Stoppage
{
    public interface IStoppageService : ITsbService
    {
        #region METHOD AREA (SELECT) ******************************************

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetEquipmentStoppageReasonAll(StoppageReasonParam param);

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetEquipmentStoppageReason(StoppageReasonParam param);

        #endregion METHOD AREA (SELECT) ***************************************

        #region METHOD AREA (CUD) *********************************************

        [TransactionOption(TransactionScopeTypes.Required)]
        BaseResult UpdateStoppageInfoOfEquipment(CudServiceParam param);

        #endregion METHOD AREA (CUD) ******************************************
    }
}
