using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.CommonItemList
{
    public interface CommonItemListInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************

        string FormName { get; }

        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************

        void SetItemList(BaseItemsList<ItemListControlItem> itemList, string title);
        void SetMaxRowCount(int count);

        #endregion METHOD AREA ************************************************
    }
}
