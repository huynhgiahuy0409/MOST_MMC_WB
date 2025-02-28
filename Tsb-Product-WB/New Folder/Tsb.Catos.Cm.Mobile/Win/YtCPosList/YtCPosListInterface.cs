using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Win.YtCPosList
{
    public interface YtCPosListInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************
        string FormName { get; }
        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************
        void SetYtItemList(BaseItemsList<ItemListControlItem> itemList);
        void SetCPosItemList(BaseItemsList<ItemListControlItem> itemList);
        void SetSelectedJobInfo(string cntrNo, string jobCode, string ytNo, string cPos);
        #endregion METHOD AREA ************************************************
    }
}
