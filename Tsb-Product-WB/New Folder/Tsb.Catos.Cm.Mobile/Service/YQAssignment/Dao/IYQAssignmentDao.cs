using Tsb.Catos.Cm.Mobile.Common.Item.YQAssignment;
using Tsb.Catos.Cm.Mobile.Common.Param.YQAssignment;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Service.YQAssignment.Dao
{
    public interface IYQAssignmentDao
    {
        #region METHOD AREA (SELECT) ******************************************

        BaseItemsList<EquipmentCoverageItem> GetYQCoverageList(YQAssignmentParam param);

        #endregion METHOD AREA (SELECT) ***************************************
    }
}
