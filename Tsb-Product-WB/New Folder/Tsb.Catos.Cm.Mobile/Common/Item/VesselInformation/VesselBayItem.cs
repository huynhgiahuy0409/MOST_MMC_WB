using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation
{
    [Serializable]
    public class VesselBayItem : BaseDataItem
    {
        #region CONST & FIELD AREA ********************************************

        public const string OBJECT_ID = "ITM-CT-CTMO-VesselBayItem";

        public int BayIndex { get; set; }
        public string NO { get; set; }
        public string UserNO { get; set; }
        public int HatchIndex { get; set; }
        public int HatchCoverNo { get; set; }
        public double DeckLCG { get; set; }
        public double HoldLCG { get; set; }
        public int HoldTopTierIndex { get; set; }
        public int HoldNo { get; set; }
        public string ChkCrane { get; set; }
        public bool NotLoaded40Cntr { get; set; }
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************


        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public VesselBayItem()
        {
            this.ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************
    }
}
