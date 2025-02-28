using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.Stoppage
{
    [Serializable]
    public class StoppageReasonItem : BaseDataItem
    {

        #region INITIALIZATION AREA *******************************************

        public StoppageReasonItem()
        {
            this.ObjectID = "ITM-CT-CTMO-StoppageReasonItem";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _stopCode = string.Empty;
        private string _stopDesc = string.Empty;
        private string _useGc = string.Empty;
        private string _useYc = string.Empty;
        private string _useYt = string.Empty;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string StopCode
        {
            get { return _stopCode; }
            set
            {
                _stopCode = value;
                base.Key = StopCode;
            }
        }

        public string StopDesc
        {
            get { return _stopDesc; }
            set { _stopDesc = value; }
        }

        public string UseGc
        {
            get { return _useGc; }
            set { _useGc = value; }
        }

        public string UseYc
        {
            get { return _useYc; }
            set { _useYc = value; }
        }

        public string UseYt
        {
            get { return _useYt; }
            set { _useYt = value; }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
