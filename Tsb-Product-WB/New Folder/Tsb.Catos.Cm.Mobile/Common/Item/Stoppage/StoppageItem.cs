using System;
using System.Text.RegularExpressions;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.Stoppage
{
    [Serializable]
    public class StoppageItem : BaseDataItem
    {
        #region INITIALIZATION AREA *******************************************

        public StoppageItem()
        {
            ObjectID = "ITM-CT-CTMO-StoppageItem";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region CONST & FIELD AREA ********************************************

        private string _equipmentNo = string.Empty;
        private bool _active = false;
        private string _equipmentType = string.Empty;
        private string _vesselCode = string.Empty;
        private string _callYear = string.Empty;
        private string _callSeq = string.Empty;
        private string _vvd = string.Empty;
        private string _stopReason = string.Empty;
        private string _stopReasonDesc = string.Empty;
        private string _remark = string.Empty;

        private DateTime? _stopSTime = null;
        private DateTime? _resumePTime = null;
        private DateTime? _resumeATime = null;

        private string _staffCd = string.Empty;
        private string _status = string.Empty;
        private string _mediumType = string.Empty;

        private string _poolName = string.Empty;
        private DateTime? _stopPTime = null;
        private string _vesselSchedule = string.Empty;
        private bool _isDefaultTime = false;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string Status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    base.NotifyPropertyChanged("Status");
                }
            }
        }

        public string EquipmentNo
        {
            get { return _equipmentNo; }
            set
            {
                if (value != _equipmentNo)
                {
                    _equipmentNo = value;
                    base.NotifyPropertyChanged("EquipmentNo");
                }
            }
        }
        public bool Active
        {
            get { return _active; }
            set
            {
                if (value != _active)
                {
                    _active = value;
                    base.NotifyPropertyChanged("Active");
                }
            }
        }

        public string EquipmentType
        {
            get { return _equipmentType; }
            set
            {
                if (value != _equipmentType)
                {
                    _equipmentType = value;
                    base.NotifyPropertyChanged("EquipmentType");
                }
            }
        }

        public string VesselCode
        {
            get { return _vesselCode; }
            set
            {
                if (value != _vesselCode)
                {
                    _vesselCode = value;
                    base.NotifyPropertyChanged("VesselCode");
                }
            }
        }

        public string CallYear
        {
            get { return _callYear; }
            set
            {
                if (value != _callYear)
                {
                    _callYear = value;
                    base.NotifyPropertyChanged("CallYear");
                }
            }
        }

        public string CallSeq
        {
            get { return _callSeq; }
            set
            {
                if (value != _callSeq)
                {
                    _callSeq = value;
                    base.NotifyPropertyChanged("CallSeq");
                }
            }
        }

        public string Vvd
        {
            get
            {
                return string.IsNullOrEmpty(_vvd) ? _vesselCode + "-" + _callYear + "-" + _callSeq : _vvd;
            }
            set
            {
                if (value != _vvd)
                {
                    _vvd = value;
                    base.NotifyPropertyChanged("Vvd");
                }
            }
        }

        public string StopReason
        {
            get { return _stopReason; }
            set
            {
                if (value != _stopReason)
                {
                    _stopReason = value;
                    base.NotifyPropertyChanged("StopReason");
                }
            }
        }

        public string Remark
        {
            get { return _remark; }
            set
            {
                if (value != _remark)
                {
                    _remark = value;
                    base.NotifyPropertyChanged("Remark");
                }
            }
        }

        public DateTime? StopSTime
        {
            get { return _stopSTime; }
            set
            {
                if (value != _stopSTime)
                {
                    _stopSTime = value;
                    base.NotifyPropertyChanged("StopSTime");
                }
            }
        }

        public DateTime? ResumePTime
        {
            get { return _resumePTime; }
            set
            {
                if (value != _resumePTime)
                {
                    _resumePTime = value;
                    base.NotifyPropertyChanged("ResumePTime");
                }
            }
        }

        public DateTime? ResumeATime
        {
            get { return _resumeATime; }
            set
            {
                if (value != _resumeATime)
                {
                    _resumeATime = value;
                    base.NotifyPropertyChanged("ResumeATime");
                }
            }
        }

        public string StaffCd
        {
            get { return _staffCd; }
            set { _staffCd = value; }
        }

        public string StopReasonDesc
        {
            get { return _stopReasonDesc; }
            set
            {
                if (value != _stopReasonDesc)
                {
                    _stopReasonDesc = value;
                    base.NotifyPropertyChanged("StopReasonDesc");
                }
            }
        }

        public string MediumType
        {
            get { return _mediumType; }
            set { _mediumType = value; }
        }

        public string PoolName
        {
            get { return _poolName; }
            set
            {
                if (value != _poolName)
                {
                    _poolName = value;
                    base.NotifyPropertyChanged("PoolName");
                }
            }
        }

        public DateTime? StopPTime
        {
            get { return _stopPTime; }
            set
            {
                if (value != _stopPTime)
                {
                    _stopPTime = value;
                    base.NotifyPropertyChanged("StopPTime");
                }
            }
        }

        public string VesselSchedule
        {
            get
            {
                bool isVesselCode = string.IsNullOrEmpty(_vesselCode);
                bool isCallYear = string.IsNullOrEmpty(_callYear);
                bool isCallSeq = string.IsNullOrEmpty(_callSeq);
                if (isVesselCode == false && isCallYear == false && isCallSeq == false)
                {
                    _vesselSchedule = _vesselCode + "-" + _callYear + "-" + _callSeq;
                }

                return _vesselSchedule;
            }
            set
            {
                if (value != _vesselSchedule)
                {
                    _vesselSchedule = value;
                    _vvd = value;
                    ParseVVD();
                    base.NotifyPropertyChanged("VesselSchedule");
                }
            }

        }

        public bool IsDefaultTime
        {
            get { return _isDefaultTime; }
            set { _isDefaultTime = value; }
        }

        public override string Key
        {
            get
            {
                return _equipmentNo;
            }
        }

        #endregion PROPERTY AREA **********************************************

        #region METHOD AREA ***************************************************

        public void GenerateVVD()
        {
            if (!String.IsNullOrEmpty(_vesselCode)
                && !String.IsNullOrEmpty(_callYear)
                && !String.IsNullOrEmpty(_callSeq))
                _vvd = _vesselCode + "-" + _callYear + "-" + _callSeq;
        }

        public void ParseVVD()
        {
            if (!String.IsNullOrEmpty(_vvd))
            {
                string[] vvds = Regex.Split(_vvd, @"\-");
                _vesselCode = vvds[0];
                _callYear = vvds[1];
                _callSeq = vvds[2];
            }
        }

        public void InitData(string equipmentNo, string equipmentType)
        {
            _equipmentNo = equipmentNo;
            _equipmentType = equipmentType;
        }

        #endregion METHOD AREA ************************************************
    }
}
