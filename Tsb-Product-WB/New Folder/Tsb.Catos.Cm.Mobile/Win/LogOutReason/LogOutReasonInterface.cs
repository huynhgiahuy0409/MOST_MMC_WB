using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.LogOutReason
{
    public interface LogOutReasonInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************
        string FormName { get; }
        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************
        void SetItemList(BaseItemsList<ItemListControlItem> itemList);
        #endregion METHOD AREA ************************************************
    }
}
