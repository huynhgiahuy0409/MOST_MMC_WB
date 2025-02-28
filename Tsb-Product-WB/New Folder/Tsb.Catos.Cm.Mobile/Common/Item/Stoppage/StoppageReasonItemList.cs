using System.Collections.Generic;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.Stoppage
{
    public class StoppageReasonItemList : BaseItemsList<StoppageReasonItem>, IDataItemsList
    {
        #region INITIALIZATION AREA *******************************************

        public StoppageReasonItemList()
            : this(null)
        {
            this.ObjectID = "ITM-CT-CTMO-StoppageReasonItemList";
        }

        public StoppageReasonItemList(IList<StoppageReasonItem> list)
            : base(list, false) // backup false
        {
            this.ObjectID = "ITM-CT-CTMO-StoppageReasonItemList";
        }

        #endregion INITIALIZATION AREA ****************************************
    }
}
