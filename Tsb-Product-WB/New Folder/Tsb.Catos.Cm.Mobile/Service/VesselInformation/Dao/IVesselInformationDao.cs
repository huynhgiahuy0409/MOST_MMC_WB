using Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation;
using Tsb.Catos.Cm.Mobile.Common.Param.VesselInformation;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Service.VesselInformation.Dao
{
    public interface IVesselInformationDao
    {
        #region METHOD AREA (SELECT) ******************************************

        BaseItemsList<BerthPlanItem> GetVesselScheduleList();
        BaseItemsList<HatchInfoItem> GetHatchItemList(VesselInformationParam param);
        BaseItemsList<ActivatedHatchInfoItem> GetActivatedHatchItemList(ActivatedVesselInformationParam param);//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법 
        BaseItemsList<VesselBayItem> GetBayItemList(VesselInformationParam param); // added by JH.Tak (2021.07.07) KPCT Driverless YT
        BerthPlanItem GetVesselSchedule(ActivatedVesselInformationParam param); // added by JH.Tak (2021.10.01) KPCT Driverless YT
        #endregion METHOD AREA (SELECT) ***************************************
    }
}
