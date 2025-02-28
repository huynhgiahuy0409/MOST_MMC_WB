using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.UpdateSeal
{
    public class UpdateSealItem : BaseDataItem
    {
        #region FIELD AREA *************************************
        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************

        public string EquipmentNo { get; set; }
        public string CntrNo { get; set; }
        public string JobCode { get; set; }
        public string VslCd { get; set; }
        public string CallYear { get; set; }
        public string CallSeq { get; set; }
        public string SealNo1 { get; set; }
        public string SealNo2 { get; set; }
        public string SealNo3 { get; set; }
        public string SealChk { get; set; } // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public UpdateSealItem()
        {
            this.ObjectID = "ITM-CT-CTMO-UpdateSealItem";
        }
        
        #endregion INITIALIZATION AREA *************************
    }
}
