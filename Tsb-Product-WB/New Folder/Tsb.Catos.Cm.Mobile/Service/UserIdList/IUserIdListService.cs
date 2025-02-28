using Tsb.Catos.Cm.Mobile.Common.Param.Staff;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;

namespace Tsb.Catos.Cm.Mobile.Service.UserIdList
{
    public interface IUserIdListService : ITsbService
    {
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetUserIdList(GetStaffParam param);
    }
}
