using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item
{
    [Serializable]
    public class ItemListControlItem : BaseDataItem
    {
        #region FIELD AREA *************************************
        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************

        public object Code {get; set;}

        public string CodeName { get; set; }

        public string TextValue { get; set; }

        public object CodeItem { get; set; }

        public string CustomStyleName { get; set; }

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public ItemListControlItem()
        {
            this.ObjectID = "ITM-CT-CTMO-ItemListControlItem";
        }

        #endregion INITIALIZATION AREA *************************
    }
}
