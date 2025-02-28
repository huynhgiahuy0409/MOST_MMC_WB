using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Win.YtListSingleTwin
{
    public interface YtListSingleTwinInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************
        string FormName { get; }
        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************
        void SetItemList(BaseItemsList<ItemListControlItem> itemList);
        void SetCPosItemList(BaseItemsList<ItemListControlItem> itemList);
        void SetSingleTwinItemList(BaseItemsList<ItemListControlItem> itemList);
        void SetSelectedJobInfo(string cntrNo, string jobCode, string ytNo, string cPos);
        void SetSingleTwinItem(string singleTwin);
        #endregion METHOD AREA ************************************************
    }
}
