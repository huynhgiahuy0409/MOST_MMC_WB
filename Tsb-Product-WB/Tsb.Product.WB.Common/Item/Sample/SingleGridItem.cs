/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2012 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2011.01.25  Tonny.Kim 1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Configuration.Common;
using Tsb.Fontos.Core.Item;

namespace Tsb.Product.WB.Common.Item.Sample
{
    [Serializable]
    public class SingleGridItem : BaseDataItem
    {
        #region FIELD AREA ******************************************
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
        #endregion

        #region PROPERTY AREA ***************************************
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
            set { _packingGrp = value; }
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
        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize SingleGridItem.
        /// </summary>
        public SingleGridItem()
        {
            this.ObjectID = "ITM-PT-PTWB-SPL-SingleGridItem";
        }
        #endregion

    }
}
