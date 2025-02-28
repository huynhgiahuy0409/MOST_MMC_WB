using Tsb.Catos.Cm.Mobile.Common.Param.VesselInformation;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;

namespace Tsb.Catos.Cm.Mobile.Service.VesselInformation
{
    public interface IVesselInformationService : ITsbService
    {
        #region METHOD AREA (SELECT) ******************************************

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetVesselScheduleList();

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetHatchItemList(VesselInformationParam param);

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetActivatedHatchItemList(ActivatedVesselInformationParam param);//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법 

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetBayItemList(VesselInformationParam param); // added by JH.Tak (2021.07.07) KPCT Driverless YT

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetVesselSchedule(ActivatedVesselInformationParam param); // added by JH.Tak (2021.10.01) KPCT Driverless YT
        #endregion METHOD AREA (SELECT) ***************************************
    }
}
