using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation
{
    [Serializable]
    public class HatchInfoItem : BaseDataItem
    {
        #region CONST & FIELD AREA ********************************************

        public const string OBJECT_ID = "ITM-CT-CTMO-HatchInfoItem";

        private string _vesselCode = string.Empty;
        private int _hatchIdx = 0;
        private int _startBayIdx = 0;
        private int _endBayIdx = 0;
        private string _startBayNo = string.Empty;
        private string _endBayNo = string.Empty;
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string VesselCode
        {
            get { return this._vesselCode; }
            set { this._vesselCode = value; }
        }

        public int HatchIdx
        {
            get { return this._hatchIdx; }
            set { this._hatchIdx = value; }
        }

        public int StartBayIdx
        {
            get { return this._startBayIdx; }
            set { this._startBayIdx = value; }
        }

        public int EndBayIdx
        {
            get { return this._endBayIdx; }
            set { this._endBayIdx = value; }
        }

        public string StartBayNo
        {
            get { return this._startBayNo; }
            set { this._startBayNo = value; }
        }

        public string EndBayNo
        {
            get { return this._endBayNo; }
            set { this._endBayNo = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public HatchInfoItem()
        {
            this.ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************
    }
}
