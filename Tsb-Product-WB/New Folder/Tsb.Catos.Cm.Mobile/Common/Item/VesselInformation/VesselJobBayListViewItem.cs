using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation
{
    [Serializable]
    public class VesselJobBayListViewItem : BaseDataItem
    {
        #region FIELD AREA *************************************
        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************

        public BerthPlanItem VesselScheduleItem { get; set; }
        public string JobBay { get; set; }

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public VesselJobBayListViewItem()
        {
            this.ObjectID = "ITM-CT-CTMO-VesselJobBayListViewItem";
        }

        #endregion INITIALIZATION AREA *************************
    }
}
