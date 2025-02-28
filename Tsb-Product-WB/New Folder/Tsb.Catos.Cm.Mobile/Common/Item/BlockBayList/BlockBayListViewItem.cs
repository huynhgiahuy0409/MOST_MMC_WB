using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.BlockBayList
{
    [Serializable]
    public class BlockBayListViewItem : BaseDataItem
    {
        #region FIELD AREA *************************************
        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************

        public BaseDataItem BlockAreaItem { get; set; }
        public BaseDataItem BayRowItem { get; set; }
        public String DisplayBlockAreaName { get; set; }
        public String DisplayBayRowName { get; set; }

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public BlockBayListViewItem()
        {
            this.ObjectID = "ITM-CT-CTMO-BlockBayListViewItem";
        }

        #endregion INITIALIZATION AREA *************************
    }
}
