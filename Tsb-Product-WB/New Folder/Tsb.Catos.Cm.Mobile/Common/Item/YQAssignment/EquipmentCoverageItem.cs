using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.YQAssignment
{
    [Serializable]
    public class EquipmentCoverageItem : BaseDataItem
    {
        #region INITIALIZATION AREA *******************************************

        public EquipmentCoverageItem()
        {
            this.ObjectID = "ITM-CT-CTMO-EquipmentCoverageItem";
        }

        public EquipmentCoverageItem(String block, int fromBayIndex, int toBayIndex)
            : base()
        {
            this._block = block;
            this._fromBayIndex = fromBayIndex;
            this._toBayIndex = toBayIndex;
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _equNo = string.Empty;
        private string _block = string.Empty;
        private int _fromBayIndex = 0;
        private int _toBayIndex = 0;
        private string _staffCd = string.Empty;
        private DateTime _updateTime;
        private string _assignType = string.Empty;

        #endregion CONST & FIELD AREA *****************************************


        #region PROPERTY AREA *************************************************

        public string EquNo
        {
            get
            {
                return this._equNo;
            }
            set
            {
                if (!value.Equals(this._equNo))
                {
                    this._equNo = value;
                    base.NotifyPropertyChanged("EquNo");
                }
            }
        }

        public string Block
        {
            get
            {
                return this._block;
            }
            set
            {
                if (!value.Equals(this._block))
                {
                    this._block = value;
                    base.NotifyPropertyChanged("Block");
                }
            }
        }

        public int FromBayIndex
        {
            get
            {
                return this._fromBayIndex;
            }
            set
            {
                if (value != this._fromBayIndex)
                {
                    this._fromBayIndex = value;
                    base.NotifyPropertyChanged("FromBayIndex");
                }
            }
        }

        public int ToBayIndex
        {
            get
            {
                return this._toBayIndex;
            }
            set
            {
                if (value != this._toBayIndex)
                {
                    this._toBayIndex = value;
                    base.NotifyPropertyChanged("ToBayIndex");
                }
            }
        }

        public string StaffCd
        {
            get
            {
                return this._staffCd;
            }
            set
            {
                if (!value.Equals(this._staffCd))
                {
                    this._staffCd = value.ToString();
                    base.NotifyPropertyChanged("StaffCd");

                }
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return _updateTime;
            }
            set
            {
                if (!value.Equals(_updateTime))
                {
                    _updateTime = value;
                    base.NotifyPropertyChanged("UpdateTime");

                }
            }
        }

        public string AssignType
        {
            get { return _assignType; }
            set
            {
                if (!value.Equals(this._assignType))
                {
                    this._assignType = value.ToString();
                    base.NotifyPropertyChanged("AssignType");

                }
            }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
