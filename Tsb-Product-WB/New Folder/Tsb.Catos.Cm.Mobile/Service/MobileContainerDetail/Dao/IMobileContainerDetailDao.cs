using System.Collections.Generic;
using Tsb.Catos.Cm.Mobile.Common.Item.MobileContainerDetail;
using Tsb.Catos.Cm.Mobile.Common.Param.MobileContainerDetail;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Service.MobileContainerDetail.Dao
{
    public interface IMobileContainerDetailDao
    {
        #region METHOD AREA (SELECT) ******************************************

        BaseItemsList<ContainerInfoItem> GetContainerList(MobileContainerDetailParam param);
        List<string> GetShipPositionList(MobileContainerDetailParam param); // added by YoungOk Kim (2019.10.16) - Mantis 92189: [Tally] 컨테이너 정보에서 2 time shifting 표시

        #endregion METHOD AREA (SELECT) ***************************************
    }
}
