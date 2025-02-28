using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.Menu.Param;

namespace Tsb.Product.WB.Common.Item.WeightBridge
{
    public class MainSingleGridItem : BaseDataItem, IContextMenuParam
    {
        #region FIELD AREA *************************************
        private string _unid;
        private string _unno;
        private string _class;
        private string _packingGrp;
        private string _properShipNm;
        private string _subsidiaryRisk;
        private string _limitedQty;
        private string _marinePollut;
        private string _ems;
        private string _flashPoint;
        private string _extendGrp;
        private string _valueColor = "12648384";
        private string _fe;
        private int _button1;
        private int _button2;
        private int _button3;
        private int _button4;
        private bool _dummyChk;
        #endregion

        #region PROPERTY AREA *************************************
        /// <summary>
        /// Gets or sets Unid.
        /// </summary>
        public string Unid
        {
            get { return _unid; }
            set { _unid = value; }
        }

        /// <summary>
        /// Gets or sets Unno.
        /// </summary>
        public string Unno
        {
            get { return _unno; }
            set { _unno = value; }
        }

        /// <summary>
        /// Gets or sets Fe.
        /// </summary>
        public string Fe
        {
            get { return _fe; }
            set { _fe = value; }
        }

        /// <summary>
        /// Gets or sets Class.
        /// </summary>
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        /// <summary>
        /// Gets or sets PackingGrp.
        /// </summary>
        public string PackingGrp
        {
            get { return _packingGrp; }
            set
            {
                if (value != this._packingGrp)
                {
                    this._packingGrp = value;
                    base.NotifyPropertyChanged("PackingGrp");
                }
            }
        }

        /// <summary>
        /// Gets or sets PropertShipNm.
        /// </summary>
        public string ProperShipNm
        {
            get { return _properShipNm; }
            set
            {
                if (value != this._properShipNm)
                {
                    this._properShipNm = value;
                    base.NotifyPropertyChanged("ProperShipNm");
                }
            }
        }

        /// <summary>
        /// Gets or sets SubsidiaryRisk.
        /// </summary>
        public string SubsidiaryRisk
        {
            get { return _subsidiaryRisk; }
            set
            {
                if (value != this._subsidiaryRisk)
                {
                    this._subsidiaryRisk = value;
                    base.NotifyPropertyChanged("SubsidiaryRisk");
                }
            }
        }

        /// <summary>
        /// Gets or sets LimitedQty.
        /// </summary>
        public string LimitedQty
        {
            get { return _limitedQty; }
            set
            {
                if (value != this._limitedQty)
                {
                    this._limitedQty = value;
                    base.NotifyPropertyChanged("LimitedQty");
                }
            }
        }

        /// <summary>
        /// Gets or sets MarinePollut.
        /// </summary>
        public string MarinePollut
        {
            get { return _marinePollut; }
            set
            {
                if (value != this._marinePollut)
                {
                    this._marinePollut = value;
                    base.NotifyPropertyChanged("MarinePollut");
                }
            }
        }

        /// <summary>
        /// Gets or sets Ems.
        /// </summary>
        public string Ems
        {
            get { return _ems; }
            set
            {
                if (value != this._ems)
                {
                    this._ems = value;
                    base.NotifyPropertyChanged("Ems");
                }
            }
        }

        /// <summary>
        /// Gets or sets FlashPoint.
        /// </summary>
        public string FlashPoint
        {
            get { return _flashPoint; }
            set
            {
                if (value != this._flashPoint)
                {
                    this._flashPoint = value;
                    base.NotifyPropertyChanged("FlashPoint");
                }
            }
        }

        /// <summary>
        /// Gets or sets ExtendGrp.
        /// </summary>
        public string ExtendGrp
        {
            get { return _extendGrp; }
            set
            {
                if (value != this._extendGrp)
                {
                    this._extendGrp = value;
                    base.NotifyPropertyChanged("ExtendGrp");
                }
            }
        }

        /// <summary>
        /// Gets or sets ValueColor.
        /// </summary>
        public string ValueColor
        {
            get { return _valueColor; }
            set
            {
                if (value != this._valueColor)
                {
                    this._valueColor = value;
                    base.NotifyPropertyChanged("ValueColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets ReadOnlyColumn.
        /// </summary>
        public string ReadOnlyColumn
        {
            get { return "ReadOnly"; }
        }

        /// <summary>
        /// Gets or sets StaffCd.
        /// </summary>
        public string StaffCd { get; set; }

        /// <summary>
        /// Gets or sets DateTimeString.
        /// </summary>
        public string DateTimeString { get; set; }

        /// <summary>
        /// Gets or sets DoubleValue.
        /// </summary>
        public double DoubleValue { get; set; }

        /// <summary>
        /// Gets or sets Button1.
        /// </summary>
        public int Button1
        {
            get { return this._button1; }
            set
            {
                if (value != this._button1)
                {
                    this._button1 = value;
                    base.NotifyPropertyChanged("Button1");
                }
            }
        }

        /// <summary>
        /// Gets or sets Button2.
        /// </summary>
        public int Button2
        {
            get { return this._button2; }
            set
            {
                if (value != this._button2)
                {
                    this._button2 = value;
                    base.NotifyPropertyChanged("Button2");
                }
            }
        }

        /// <summary>
        /// Gets or sets Button3.
        /// </summary>
        public int Button3
        {
            get { return this._button3; }
            set
            {
                if (value != this._button3)
                {
                    this._button3 = value;
                    base.NotifyPropertyChanged("Button3");
                }
            }
        }

        /// <summary>
        /// Gets or sets Button4.
        /// </summary>
        public int Button4
        {
            get { return this._button4; }
            set
            {
                if (value != this._button4)
                {
                    this._button4 = value;
                    base.NotifyPropertyChanged("Button4");
                }
            }
        }

        public bool DummyChk
        {
            get { return this._dummyChk; }
            set
            {
                if (value != this._dummyChk)
                {
                    this._dummyChk = value;
                    base.NotifyPropertyChanged("DummyChk");
                }
            }
        }
        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize SingleGridItem.
        /// </summary>
        public MainSingleGridItem()
        {
            this.ObjectID = "ITM-FT-FTSL-FTE-SingleGridItem";
            this.DummyChk = true;
        }
        #endregion
    }
}
