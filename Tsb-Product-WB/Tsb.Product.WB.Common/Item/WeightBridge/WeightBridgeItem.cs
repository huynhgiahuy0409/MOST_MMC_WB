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
   
        public class WeightBridgeItem : BaseDataItem, IContextMenuParam
    {
            private decimal? _firstWgt = null;
            private decimal? _secondWgt = null;
            private decimal? _trkGrsWgt = null;
            private decimal? _trkTreWgt = null;
            private decimal? _trkNetWgt = null;
            public string TransactionNo { get; set; }
            public string GateTicketNo { get; set; }
            public DateTime? TransactionDt { get; set; }
            public string VslCallId { get; set; }
            public string VslCd { get; set; }
            public string CallYear { get; set; }
            public string CallSeq { get; set; }
            public string MfDocId { get; set; }
            public string ShipgNoteNo { get; set; }
            public string GrNo { get; set; }
            public string BlNo { get; set; }
            public string DoNo { get; set; }
            public string SdoNo { get; set; }
            public string UnitNo { get; set; }
            public string Fwrd { get; set; }
            public string Cnsne { get; set; }
            public string Shpr { get; set; }
            public string GpNo { get; set; }
            public string TsptComp { get; set; }
            public string LorryNo { get; set; }
            public string DriverId { get; set; }
            public string ChassisNo { get; set; }
            public string TrkMode { get; set; }
            public decimal? FirstWgt {
                get { return this._firstWgt; }
                set
                {
                    if (value != this._firstWgt)
                    {
                        this._firstWgt = WeightBridgeUtils.ConvertWeight(value, 1);
                        base.NotifyPropertyChanged("FirstWgt");
                    }
                }
            } 
            public decimal? SecondWgt {
                get { return this._secondWgt; }
                set
                {
                    if (value != this._secondWgt)
                    {
                        this._secondWgt = WeightBridgeUtils.ConvertWeight(value, 1); ;
                        base.NotifyPropertyChanged("SecondWgt");
                    }
                }
            }
            public decimal? BagWgt { get; set; }
            public int? PkgQty { get; set; }
            public decimal CgVol { get; set; }
            public decimal? TrkGrsWgt {
                get { return this._trkGrsWgt; }
                set
                {
                    if (value != this._trkGrsWgt)
                    {
                        this._trkGrsWgt = WeightBridgeUtils.ConvertWeight(value, 1); ;
                        base.NotifyPropertyChanged("SecondWgt");
                    }
                }
            }
            public decimal? TrkTreWgt {
                get { return this._trkTreWgt; }
                set
                {
                    if (value != this._trkTreWgt)
                    {
                        this._trkTreWgt = WeightBridgeUtils.ConvertWeight(value, 1); ;
                        base.NotifyPropertyChanged("SecondWgt");
                    }
                }
            }
            public decimal? TrkNetWgt {
                get { return this._trkNetWgt; }
                set
                {
                    if (value != this._trkNetWgt)
                    {
                        this._trkNetWgt = WeightBridgeUtils.ConvertWeight(value, 1); ;
                        base.NotifyPropertyChanged("SecondWgt");
                    }
                }
            }
            public DateTime? GateinDt { get; set; }
            public string GateinCd { get; set; }
            public DateTime? GateoutDt { get; set; }
            public string GateoutCd { get; set; }
            public string ToLocation { get; set; }
            public int PrnCnt { get; set; }
            public string Rmk { get; set; }
            public string RhdlMode { get; set; }
            public string StaffCd { get; set; }
            public DateTime? UpdateTime { get; set; }
            public DateTime? FirstWgtDt { get; set; }
            public DateTime? SecondWgtDt { get; set; }
            public string Status { get; set; }
            public string DelvTpCd { get; set; }

            public WeightBridgeItem()
            {
                this.ObjectID = "ITM-PT-PTWB-WB-SingleGridItem";
            }
    }
}
