using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.BlockList
{
    [Serializable]
    public class BlockListViewItem : BaseDataItem
    {
        #region FIELD AREA *************************************
        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************

        public BaseDataItem BlockAreaItem { get; set; }
        public String DisplayBlockAreaName { get; set; }

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public BlockListViewItem()
        {
            this.ObjectID = "ITM-CT-CTMO-BlockListViewItem";
        }

        #endregion INITIALIZATION AREA *************************
    }
}
