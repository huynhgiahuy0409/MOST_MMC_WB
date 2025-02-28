using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.Stoppage
{
    public interface StoppageInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************
        string FormName { get; }
        bool ExcludeVesselStoppage { get; set; }
        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************
        void SetEquipmentNo(string equipmentNo);
        void SetItemList(BaseItemsList<ItemListControlItem> itemList);
        #endregion METHOD AREA ************************************************
    }
}
