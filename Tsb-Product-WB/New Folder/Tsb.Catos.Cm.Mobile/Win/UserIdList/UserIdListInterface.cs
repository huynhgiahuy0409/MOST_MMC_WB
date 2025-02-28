using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Core.Item;
using Tsb.Catos.Cm.Mobile.Common.Item;

namespace Tsb.Catos.Cm.Mobile.Win.UserIdList
{
    public interface UserIdListInterface : BaseMobileViewInterface
    {
        #region PROPERTY AREA *************************************************
        string FormName { get; }
        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************
        void SetItemList(BaseItemsList<ItemListControlItem> itemList);
        #endregion METHOD AREA ************************************************
    }
}
