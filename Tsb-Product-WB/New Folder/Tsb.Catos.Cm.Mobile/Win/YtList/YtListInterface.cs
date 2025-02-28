using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Win.YtList
{
    public interface YtListInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************
        string FormName { get; }
        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************
        void SetItemList(BaseItemsList<ItemListControlItem> itemList);
        void SetSelectedJobInfo(string cntrNo, string jobCode, string ytNo);
        #endregion METHOD AREA ************************************************
    }
}
