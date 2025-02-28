using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.MobileContainerDetail
{
    [Serializable]
    public class MobileContainerDetailItem : BaseDataItem
    {
        #region FIELD AREA *************************************
        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************
        
        public string CntrNo { get; set; }
        public string Vvd { get; set; }
        public string VslNm { get; set; }
        public string YardPosition { get; set; }
        public string ShipPosition { get; set; }
        public string Block { get; set; }
        public string Bay { get; set; }
        public string Row { get; set; }
        public string Tier { get; set; }
        public string SzTp { get; set; }
        public string SzTp2 { get; set; }
        public string IMDG { get; set; }
        public string UNNo { get; set; }
        public string Weight { get; set; }
        public string Pod { get; set; }
        public string PtnrCode { get; set; }
        public string FE { get; set; }
        public string CargoType { get; set; }
        public string Owner { get; set; }
        public string SealNo1 { get; set; }
        public string SealNo2 { get; set; }
        public string BundleList { get; set; }
        public string Remark { get; set; }
        public string UserVoy { get; set; }
        public string JobCode { get; set; }
        public string VslCd { get; set; }
        public string CallYear { get; set; }
        public string CallSeq { get; set; }
        public string SetTemp { get; set; }
        public string OverDimension { get; set; }
        public string Fdest { get; set; }
        public string InspectCheck { get; set; }
        public string TerminalHold { get; set; }
        public string CustomsHold { get; set; }
        public string DamageCondition { get; set; }
        public string ContainerCondition { get; set; }
        public string DisShipPosition { get; set; }
        public string LoadShipPosition { get; set; }
        public string PuPlanDate2 { get; set; } // added by YoungOk Kim (2020.04.08) - Mantis 105287: Show Special Color for Reservation Contaioners: TM /YQ/C3IT part
        public string TimeSlotNo2 { get; set; } // added by YoungOk Kim (2020.04.08) - Mantis 105287: Show Special Color for Reservation Contaioners: TM /YQ/C3IT part
        public string SealChk { get; set; } // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
        public string TimeSlotDate2 { get; set; } // added by JH.Tak (2022.09.05) WHL_UP OM-K-009
        public Boolean SealChkToBoolean
        {
            get { return this.SealChk.Equals("Y"); }
        }
        public string TrainVoyage { get; set; } // added by Ron (2023.08.21) [ADP-RT] 0151467: [Container Information] Add field Train Voyage

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public MobileContainerDetailItem()
        {
            this.ObjectID = "ITM-CT-CTMO-MobileContainerDetailItem";
        }
        
        #endregion INITIALIZATION AREA *************************
    }
}
