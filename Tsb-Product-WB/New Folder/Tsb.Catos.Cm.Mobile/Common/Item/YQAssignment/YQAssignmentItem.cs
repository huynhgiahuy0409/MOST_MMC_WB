using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.YQAssignment
{
    [Serializable]
    public class YQAssignmentItem : BaseDataItem
    {
        #region INITIALIZATION AREA *******************************************

        public YQAssignmentItem()
        {
            this.ObjectID = "ITM-CT-CTMO-YQAssignmentItem";
        }

        #endregion

        #region CONST & FIELD AREA ********************************************

        private string _equNo = string.Empty;
        private int _workAssign = 0;
        private BaseItemsList<EquipmentCoverageItem> _equCoverages = null;
        private string _lastPosition = string.Empty;
        private string _block = string.Empty;
        private string _bay = string.Empty;
        private bool _isUpdateEquLocation = false;
        private string _staffCd = string.Empty;
        private DateTime _updateTime;
        private string _equType = string.Empty;

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

        public int WorkAssign
        {
            get
            {
                return this._workAssign;
            }
            set
            {
                if (value != this._workAssign)
                {
                    this._workAssign = value;
                    base.NotifyPropertyChanged("WorkAssign");
                }
            }
        }

        public BaseItemsList<EquipmentCoverageItem> EquipmentCoverages
        {
            get
            {
                return this._equCoverages;
            }
            set
            {
                if (!value.Equals(this._equCoverages))
                {
                    this._equCoverages = value;
                    base.NotifyPropertyChanged("EquipmentCoverages");
                }
            }
        }

        public BaseItemsList<EquipmentCoverageItem> SortedEquipmentCoverages()
        {
            BaseItemsList<EquipmentCoverageItem> sortedList = _equCoverages;

            if (sortedList != null)
            {
                for (int i = 1; i < sortedList.Count; i++)
                {
                    for (int j = i; j > 0; j--)
                    {
                        if (sortedList[j].Block.CompareTo(sortedList[j - 1].Block) < 0)
                        {
                            EquipmentCoverageItem tmp;
                            tmp = sortedList[j - 1];
                            sortedList[j - 1] = sortedList[j];
                            sortedList[j] = tmp;
                        }
                        else break;
                    }
                }
            }
            return sortedList;
        }

        public string LastPosition
        {
            get
            {
                return this._lastPosition;
            }
            set
            {
                if (!value.Equals(this._lastPosition))
                {
                    this._lastPosition = value;
                    base.NotifyPropertyChanged("LastPosition");
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

        public string Bay
        {
            get
            {
                return this._bay;
            }
            set
            {
                if (!value.Equals(this._bay))
                {
                    this._bay = value;
                    base.NotifyPropertyChanged("Bay");
                }
            }
        }

        public bool IsUpdateEquLocation
        {
            get
            {
                return this._isUpdateEquLocation;
            }
            set
            {
                if (!value.Equals(this._isUpdateEquLocation))
                {
                    this._isUpdateEquLocation = value;
                    base.NotifyPropertyChanged("IsUpdateEquLocation");
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

        public string EquType
        {
            get
            {
                return this._equType;
            }
            set
            {
                if (!value.Equals(this._equType))
                {
                    this._equType = value;
                    base.NotifyPropertyChanged("EquType");
                }
            }
        }

        #endregion PROPERTY AREA **********************************************
    }
}
