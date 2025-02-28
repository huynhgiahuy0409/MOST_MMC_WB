using Tsb.Catos.Cm.Mobile.Common.Item.MobileContainerDetail;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.MobileContainerDetail
{
    public interface MobileContainerDetailInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************
        string FormName { get; }
        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************
        void SetDetailLayout(MobileContainerDetailItem detailItem);
        void SetContainerNo(string containerNo);
        #endregion METHOD AREA ************************************************
    }
}
