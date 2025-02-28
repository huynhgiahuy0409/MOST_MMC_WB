using Tsb.Catos.Cm.Mobile.Common.Param.UpdateSeal;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;

namespace Tsb.Catos.Cm.Mobile.Service.UpdateSeal
{
    public interface IUpdateSealService : ITsbService
    {
        #region METHOD AREA (CUD) *********************************************

        [TransactionOption(TransactionScopeTypes.Required)]
        BaseResult UpdateSeal(UpdateSealParam param);

        #endregion METHOD AREA (CUD) ******************************************
    }
}
