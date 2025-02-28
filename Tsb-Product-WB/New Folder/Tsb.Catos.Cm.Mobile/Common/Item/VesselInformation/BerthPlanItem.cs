using System;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation
{
    [Serializable]
    public class BerthPlanItem : BaseDataItem
    {
        #region CONST & FIELD AREA ********************************************

        private const string OBJECT_ID = "ITM-CT-CTMO-BerthPlanItem";
        private string _vslCd = string.Empty;
        private string _callYear = string.Empty;
        private string _callSeq = string.Empty;
        private string _userVoy = string.Empty;
        private string _partnerCode = string.Empty;
        private string _berthNo = string.Empty;
        private string _aSide = string.Empty;
        private string _dPos = string.Empty;
        private Nullable<DateTime> _eta = null;
        private Nullable<DateTime> _etb = null;
        private Nullable<DateTime> _etw = null;
        private Nullable<DateTime> _etc = null;
        private Nullable<DateTime> _etd = null;
        private Nullable<DateTime> _ata = null;
        private Nullable<DateTime> _atb = null;
        private Nullable<DateTime> _atw = null;
        private Nullable<DateTime> _atc = null;
        private Nullable<DateTime> _atd = null;
        private string _vslType = string.Empty;
        private string _inLane = string.Empty;
        private string _outLane = string.Empty;
        private string _disChk = string.Empty;
        private string _bargeChk = string.Empty;
        private string _vslName = string.Empty;
        private string _portVoy = string.Empty; // added by BG.Kim (2022.11.29) [WHL_UP] 0134776: Show VRN(PORT_VOY) in Location

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public override string Key
        {
            get { return VslCd; }
        }

        public string VslCd//Modified by JH.Tak(2017.01.12)
        {
            get { return _vslCd; }
            set
            {
                if (value != this._vslCd)
                {
                    this._vslCd = value;
                    base.NotifyPropertyChanged("VslCd");
                }
            }
        }

        public string CallYear//Modified by JH.Tak(2017.01.12)
        {
            get { return _callYear; }
            set
            {
                if (value != this._callYear)
                {
                    this._callYear = value;
                    base.NotifyPropertyChanged("CallYear");
                }
            }
        }

        public string CallSeq//Modified by JH.Tak(2017.01.12)
        {
            get { return _callSeq; }
            set
            {
                if (value != this._callSeq)
                {
                    this._callSeq = value;
                    base.NotifyPropertyChanged("CallSeq");
                }
            }
        }

        public string UserVoy//Modified by JH.Tak(2017.01.12)
        {
            get { return _userVoy; }
            set
            {
                if (value != this._userVoy)
                {
                    this._userVoy = value;
                    base.NotifyPropertyChanged("UserVoy");
                }
            }
        }

        public string PartnerCode
        {
            get { return this._partnerCode; }
            set
            {
                if (value != this._partnerCode)
                {
                    this._partnerCode = value;
                    base.NotifyPropertyChanged("PartnerCode");
                }
            }
        }

        public string BerthNo //Modified by JH.Tak(2017.01.12)
        {
            get { return _berthNo; }
            set
            {
                if (value != this._berthNo)
                {
                    this._berthNo = value;
                    base.NotifyPropertyChanged("BerthNo");
                }
            }
        }

        public string ASide //Modified by JH.Tak(2017.01.12)
        {
            get { return _aSide; }
            set
            {
                if (value != this._aSide)
                {
                    this._aSide = value;
                    base.NotifyPropertyChanged("ASide");
                }
            }
        }

        public string DPos //Modified by JH.Tak(2017.01.12)
        {
            get { return _dPos; }
            set
            {
                if (value != this._dPos)
                {
                    this._dPos = value;
                    base.NotifyPropertyChanged("DPos");
                }
            }
        }


        public Nullable<DateTime> Eta//Modified by JH.Tak(2017.01.12)
        {
            get { return _eta; }
            set
            {
                if (value != this._eta)
                {
                    this._eta = value;
                    base.NotifyPropertyChanged("Eta");
                }
            }
        }

        public Nullable<DateTime> Etb//Modified by JH.Tak(2017.01.12)
        {
            get { return _etb; }
            set
            {
                if (value != this._etb)
                {
                    this._etb = value;
                    base.NotifyPropertyChanged("Etb");
                }
            }
        }

        public Nullable<DateTime> Etw//Modified by JH.Tak(2017.01.12)
        {
            get { return _etw; }
            set
            {
                if (value != this._etw)
                {
                    this._etw = value;
                    base.NotifyPropertyChanged("Etw");
                }
            }
        }

        public Nullable<DateTime> Etc//Modified by JH.Tak(2017.01.12)
        {
            get { return _etc; }
            set
            {
                if (value != this._etc)
                {
                    this._etc = value;
                    base.NotifyPropertyChanged("Etc");
                }
            }
        }

        public Nullable<DateTime> Etd//Modified by JH.Tak(2017.01.12)
        {
            get { return _etd; }
            set
            {
                if (value != this._etd)
                {
                    this._etd = value;
                    base.NotifyPropertyChanged("Etd");
                }
            }
        }

        public Nullable<DateTime> Ata//Modified by JH.Tak(2017.01.12)
        {
            get { return _ata; }
            set
            {
                if (value != this._ata)
                {
                    this._ata = value;
                    base.NotifyPropertyChanged("Ata");
                }
            }
        }

        public Nullable<DateTime> Atb//Modified by JH.Tak(2017.01.12)
        {
            get { return _atb; }
            set
            {
                if (value != this._atb)
                {
                    this._atb = value;
                    base.NotifyPropertyChanged("Atb");
                }
            }
        }

        public Nullable<DateTime> Atw//Modified by JH.Tak(2017.01.12)
        {
            get { return _atw; }
            set
            {
                if (value != this._atw)
                {
                    this._atw = value;
                    base.NotifyPropertyChanged("Atw");
                }
            }
        }

        public Nullable<DateTime> Atc//Modified by JH.Tak(2017.01.12)
        {
            get { return _atc; }
            set
            {
                if (value != this._atc)
                {
                    this._atc = value;
                    base.NotifyPropertyChanged("Atc");
                }
            }

        }

        public Nullable<DateTime> Atd//Modified by JH.Tak(2017.01.12)
        {
            get { return _atd; }
            set
            {
                if (value != this._atd)
                {
                    this._atd = value;
                    base.NotifyPropertyChanged("Atd");
                }
            }
        }

        public string VslType //Modified by JH.Tak(2017.01.12)
        {
            get { return _vslType; }
            set
            {
                if (value != this._vslType)
                {
                    this._vslType = value;
                    base.NotifyPropertyChanged("VslType");
                }
            }
        }

        public string InLane//Modified by JH.Tak(2017.01.12)
        {
            get { return _inLane; }
            set
            {
                if (value != this._inLane)
                {
                    this._inLane = value;
                    base.NotifyPropertyChanged("InLane");
                }
            }
        }

        public string OutLane//Modified by JH.Tak(2017.01.12)
        {
            get { return _outLane; }
            set
            {
                if (value != this._outLane)
                {
                    this._outLane = value;
                    base.NotifyPropertyChanged("OutLane");
                }
            }
        }

        public string DisChk//Modified by JH.Tak(2017.01.12)
        {
            get { return _disChk; }
            set
            {
                if (value != this._disChk)
                {
                    this._disChk = value;
                    base.NotifyPropertyChanged("DisChk");
                }
            }
        }

        public string BargeChk//Modified by JH.Tak(2017.01.12)
        {
            get { return _bargeChk; }
            set
            {
                if (value != this._bargeChk)
                {
                    this._bargeChk = value;
                    base.NotifyPropertyChanged("BargeChk");
                }
            }
        }

        public string VslName//Modified by JH.Tak(2017.01.12)
        {
            get { return _vslName; }
            set
            {
                if (value != this._vslName)
                {
                    this._vslName = value;
                    base.NotifyPropertyChanged("VslName");
                }
            }
        }

        public string VesselSchedule
        {
            get { return this.VslCd + "-" + this.CallYear + "-" + this.CallSeq; }
        }

        public string PortVoy // added by BG.Kim (2022.11.29) [WHL_UP] 0134776: Show VRN(PORT_VOY) in Location
        {
            get { return _portVoy; }
            set
            {
                if (value != this._portVoy)
                {
                    this._portVoy = value;
                    base.NotifyPropertyChanged("PortVoy");
                }
            }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public BerthPlanItem()
        {
            this.ObjectID = OBJECT_ID;
        }

        #endregion INITIALIZATION AREA ****************************************
    }
}
