using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Configuration.Common;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Win.Menu.Param;
using Tsb.Product.WB.Common.Item.WeightBridge;

namespace Tsb.Product.WB.Common.Param.WeightBridge
{
    [Serializable]
    public class MainParam : BaseParam, IContextMenuParam
    {
        private DateTime? _weightFrom = null;
        private DateTime? _weightTo = null;
        public OpCodes OpCode { get; set; }
        public string QRCd { get; set; }
        public string Remark { get; set; }
        public string BlNo { get; set; }
        public string SdoNo { get; set; }
        public string SnNo { get; set; }
        public string GrNo { get; set; }
        public string VslCallId { get; set; }
        public string LorryNo { get; set; }
        public string SearchGridLorryNo { get; set; }
        public DateTime? WeightFrom
        {
            get { return this._weightFrom; }
            set
            {
                if (value != this._weightFrom)
                {
                    this._weightFrom = value;
                }
            }
        }
        public DateTime? WeightTo { get; set; } = null;
        public WeightInfoItem BackupItem { get; set; }

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize SingleGridparam.
        /// </summary>
        /// <param name="paramOwner">ParamOwner</param>
        public MainParam(object paramOwner)
            : base(paramOwner)
        {
            this.ObjectID = "PAR-FT-FTWB-FTE-MainParam";
        }

        /// <summary>
        /// Initialize SingleGridparam.
        /// </summary>
        /// <param name="paramOwner">ParamOwner</param>
        /// <param name="txServiceID">TranscationID</param>
        public MainParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            this.ObjectID = "PAR-FT-FTWB-FTE-MainParam";
        }
        #endregion
    }
}
