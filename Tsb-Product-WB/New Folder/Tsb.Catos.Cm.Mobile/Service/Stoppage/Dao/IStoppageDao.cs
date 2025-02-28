using Tsb.Catos.Cm.Mobile.Common.Item.Stoppage;
using Tsb.Catos.Cm.Mobile.Common.Param.Stoppage;

namespace Tsb.Catos.Cm.Mobile.Service.Stoppage.Dao
{
    public interface IStoppageDao
    {
        #region METHOD AREA (SELECT) ******************************************

        StoppageReasonItemList GetEquipmentStoppageReasonAll(StoppageReasonParam param);
        StoppageReasonItemList GetEquipmentStoppageReason(StoppageReasonParam param);

        #endregion METHOD AREA (SELECT) ***************************************
    }
}
