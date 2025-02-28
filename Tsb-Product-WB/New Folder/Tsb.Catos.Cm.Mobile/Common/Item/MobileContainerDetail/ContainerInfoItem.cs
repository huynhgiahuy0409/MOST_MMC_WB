using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.MobileContainerDetail
{
    public class ContainerInfoItem: BaseDataItem
    {
        #region FIELD AREA *************************************

        private string _containerNo = string.Empty;
        private string _qjobType = string.Empty;
        private string _dBay = string.Empty;
        private string _dRow = string.Empty;
        private string _dTier = string.Empty;
        private string _dHoldDeck = string.Empty;
        private string _lBay = string.Empty;
        private string _lRow = string.Empty;
        private string _lTier = string.Empty;
        private string _lHoldDeck = string.Empty;

        #endregion FIELD AREA **********************************

        #region PROPERTY AREA **********************************
        
        public string ContainerNo
        {
            get { return this._containerNo; }
            set { this._containerNo = value; }
        }

        public string QJobType
        {
            get { return this._qjobType; }
            set { this._qjobType = value; }
        }

        public string DBay
        {
            get { return this._dBay; }
            set { this._dBay = value; }
        }

        public string DRow
        {
            get { return this._dRow; }
            set { this._dRow = value; }
        }

        public string DTier
        {
            get { return this._dTier; }
            set { this._dTier = value; }
        }

        public string DHoldDeck
        {
            get { return this._dHoldDeck; }
            set { this._dHoldDeck = value; }
        }

        public string LBay
        {
            get { return this._lBay; }
            set { this._lBay = value; }
        }

        public string LRow
        {
            get { return this._lRow; }
            set { this._lRow = value; }
        }

        public string LTier
        {
            get { return this._lTier; }
            set { this._lTier = value; }
        }

        public string LHoldDeck
        {
            get { return this._lHoldDeck; }
            set { this._lHoldDeck = value; }
        }

        public string ShipBay
        {
            get
            {
                string shipBay = string.Empty;
                if (string.IsNullOrEmpty(this.QJobType) == false)
                {
                    if (this.QJobType.EndsWith("L") == true)
                    {
                        shipBay = this.LBay;
                    }
                    else
                    {
                        shipBay = this.DBay;
                    }
                }

                return shipBay;
            }
        }

        public string ShipRow
        {
            get
            {
                string shipRow = string.Empty;
                if (string.IsNullOrEmpty(this.QJobType) == false)
                {
                    if (this.QJobType.EndsWith("L") == true)
                    {
                        shipRow = this.LRow;
                    }
                    else
                    {
                        shipRow = this.DRow;
                    }
                }

                return shipRow;
            }
        }

        public string ShipTier
        {
            get
            {
                string shipTier = string.Empty;
                if (string.IsNullOrEmpty(this.QJobType) == false)
                {
                    if (this.QJobType.EndsWith("L") == true)
                    {
                        shipTier = this.LTier;
                    }
                    else
                    {
                        shipTier = this.DTier;
                    }
                }

                return shipTier;
            }
        }

        public string HoldDeck
        {
            get
            {
                string holdDeck = string.Empty;
                if (string.IsNullOrEmpty(this.QJobType) == false)
                {
                    if (this.QJobType.EndsWith("L") == true)
                    {
                        holdDeck = this.LHoldDeck;
                    }
                    else
                    {
                        holdDeck = this.DHoldDeck;
                    }
                }

                return holdDeck;
            }
        }

        #endregion PROPERTY AREA *******************************

        #region INITIALIZATION AREA ****************************

        public ContainerInfoItem()
        {
            this.ObjectID = "ITM-CT-CTMO-ContainerInfoItem";
        }
        
        #endregion INITIALIZATION AREA *************************
    }
}
