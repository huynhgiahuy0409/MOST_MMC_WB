using Tsb.Catos.Cm.Mobile.Common.Param.YQAssignment;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;

namespace Tsb.Catos.Cm.Mobile.Service.YQAssignment
{
    public interface IYQAssignmentService : ITsbService
    {
        #region METHOD AREA (SELECT) ******************************************

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetYQAssignment(YQAssignmentParam param);

        #endregion METHOD AREA (SELECT) ***************************************
    }
}
