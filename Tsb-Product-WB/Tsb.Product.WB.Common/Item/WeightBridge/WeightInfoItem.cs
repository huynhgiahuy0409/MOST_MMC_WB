using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.Menu.Param;
using Tsb.Product.WB.Common.Utils;

namespace Tsb.Product.WB.Common.Item.WeightBridge
{
    public class WeightInfoItem : BaseDataItem, IContextMenuParam
    {
        private string _rmk;
        private decimal? _firstWgt = null;
        private decimal? _secondWgt = null;
        private decimal? _cargoWgt = null;
        public string StaffCd { get; set; }
        public string _status;
        public string GatePoint { get; set; }
        public string GateLane { get; set; }
        public string ReadWeight { get; set; }
        public decimal? KG { get; set; } = null;
        public string GateTxnNo { get; set; }
        public string TransactionNo { get; set; }
        public DateTime TransactionDt { get; set; }
        #region  --- NM & CD ---
        public string BlConsigneeNm { get; set; }
        public string BlCShipperNm { get; set; }
        public string SnConsigneeNm { get; set; }
        public string SnShiperNm { get; set; }
        public string TransporterNm { get; set; }
        public string CnsnShprNm { get; set; }
        public string BlConsigneeCd { get; set; }
        public string BlShipperCd { get; set; }
        public string SnConsigneeCd { get; set; }
        public string SnShiperCd { get; set; }
        public string DelvTpCd { get; set; }
        #endregion
        #region  --- VSL ---
        public string VslCallId { get; set; }
        public string VslCd { get; set; }
        public string CallYear { get; set; }
        public string CallSeq { get; set; }
        #endregion
        #region  --- LORRY ---
        public string LorryNo { get; set; }
        public string DriverNo { get; set; }
        public string ChassisNo { get; set; }
        public string TruckMode { get; set; }
        public string TsptComp { get; set; }
        #endregion
        #region  --- DOC ---
        public string BlNo { get; set; }
        public string DoNo { get; set; }
        public string SdoNo { get; set; }
        public string ShipgNoteNo { get; set; }
        public string BlOrSn { get; set; }
        public string SdoOrGr { get; set; }
        public string GrNo { get; set; }
        public string MfDocId { get; set; }
        public string Category { get; set; }
        #endregion
        #region  --- GT ---
        public DateTime? GateInDt { get; set; }
        public string GateCd { get; set; }
        public DateTime? GateOutDt { get; set; }
        public string GateCdOut { get; set; }
        public int PkgQty { get; set; }
        public decimal CgVol { get; set; }
        public string Rmk
        {
            get { return _rmk; }
            set
            {
                if (value != this._rmk)
                {
                    this._rmk = value;
                    base.NotifyPropertyChanged("Rmk");
                }
            }
        }
        public string CgNo { get; set; }
        #endregion
        #region  --- WB ---
        public decimal? FirstWgt {
            get { return this._firstWgt; }
            set
            {
                if (value != this._firstWgt)
                {
                    this._firstWgt = WeightBridgeUtils.ConvertWeight(value, -1);
                    base.NotifyPropertyChanged("FirstWgt");
                }
            }
        }
        public decimal? SecondWgt
        {
            get { return this._secondWgt; }
            set
            {
                if (value != this._secondWgt)
                {
                    this._secondWgt = WeightBridgeUtils.ConvertWeight(value, -1);
                    Console.WriteLine(this._secondWgt);
                    base.NotifyPropertyChanged("SecondWgt");
                }
            }
        }
        public decimal? CargoWeight
        {
            get { return this._cargoWgt; }
            set
            {
                if (value != this._cargoWgt)
                {
                    this._cargoWgt = WeightBridgeUtils.ConvertWeight(value, -1);
                    base.NotifyPropertyChanged("CargoWeight");
                }
            }
        }
        public string _printCir;
        #endregion
        #region  --- WB ---
        public string CmdtDesc { get; set; }
        #endregion

        public string QrCd { get; set; }
        public string Status
        {
            get { return this._status; }
            set
            {
                if (value != this._status)
                {
                    this._status = value;
                    base.NotifyPropertyChanged("Status");
                }
            }
        }
        public string PrintCir
        {
            get { return this._printCir; }
            set
            {
                if (!value.Equals(this._printCir))
                {
                    this._printCir = value;
                    base.NotifyPropertyChanged("PrintCir");
                }
                
            }
        }
        public WeightInfoItem()
        {
            this.ObjectID = "ITM-PT-PTWB-WB-WeightInfoItem";
        }
    }
}
