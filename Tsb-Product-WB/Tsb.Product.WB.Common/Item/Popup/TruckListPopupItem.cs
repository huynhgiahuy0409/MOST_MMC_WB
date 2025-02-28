using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Item;

namespace Tsb.Product.WB.Common.Item.Popup
{
    [Serializable]
    public class TruckListPopupItem : BaseDataItem
    {
        #region FIELD AREA ******************************************
        private string _lorryNo;
        private string _vslCallId;
        private string _masterBlNo;
        private string _blNo;
        private string _sdoNo;
        private string _bookingNo;
        private string _shipgNoteNo;
        private string _grNo;
        private string _qrCd;
        #endregion

        #region PROPERTY AREA ***************************************
        public string LorryNo { get => _lorryNo; set => _lorryNo = value; }
        public string VslCallId { get => _vslCallId; set => _vslCallId = value; }
        public string MasterBlNo { get => _masterBlNo; set => _masterBlNo = value; }
        public string BlNo { get => _blNo; set => _blNo = value; }
        public string SdoNo { get => _sdoNo; set => _sdoNo = value; }
        public string BookingNo { get => _bookingNo; set => _bookingNo = value; }
        public string ShipgNoteNo { get => _shipgNoteNo; set => _shipgNoteNo = value; }
        public string GrNo { get => _grNo; set => _grNo = value; }
        public string QrCd { get => _qrCd; set => _qrCd = value; }
        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize SingleGridItem.
        /// </summary>
        public TruckListPopupItem()
        {
            this.ObjectID = "ITM-PT-PTWB-POP-TruckListPopupItem";
        }


        #endregion

    }
}
