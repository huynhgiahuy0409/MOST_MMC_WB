using Tsb.Catos.Cm.Mobile.Common.Param.MobileContainerDetail;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;

namespace Tsb.Catos.Cm.Mobile.Service.MobileContainerDetail
{
    public interface IMobileContainerDetailService : ITsbService
    {
        #region METHOD AREA (SELECT) ******************************************

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetYardContainerDetailItem(MobileContainerDetailParam param);

        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetQuayContainerDetailItem(MobileContainerDetailParam param);

        // added by YoungOk Kim (2019.10.15) - Mantis 92188: [Tally] 컨테이너 정보에서 컨테이너 번호 뒷자리 4자리로 조회
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetContainerList(MobileContainerDetailParam param);

        // added by YoungOk Kim (2019.10.16) - Mantis 92189: [Tally] 컨테이너 정보에서 2 time shifting 표시
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult GetShipPosition(MobileContainerDetailParam param);

        #endregion METHOD AREA (SELECT) ***************************************
    }
}
