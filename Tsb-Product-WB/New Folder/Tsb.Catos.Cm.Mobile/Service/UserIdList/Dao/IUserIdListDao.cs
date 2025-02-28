using Tsb.Catos.Cm.Mobile.Common.Item.Staff;
using Tsb.Catos.Cm.Mobile.Common.Param.Staff;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Service.UserIdList.Dao
{
    public interface IUseridListDao
    {
        BaseItemsList<StaffItem> GetUserIdList(GetStaffParam param);
    }
}
