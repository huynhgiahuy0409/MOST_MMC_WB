using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Param;

namespace Tsb.Product.WB.Common.Param.WeightBridge
{
    public class JobParam : BaseParam
    {
        public string VslCallId { get; set; }
        public string LorryNo { get; set; }
        public string CgNo { get; set; }
        public string GateTxnNo { get; set; }
        public string BlNo { get; set; }
        public string SnNo { get; set; }
        public string GrNo { get; set; }
        public string SdoNo { get; set; }
        public string MfDocId { get; set; }
        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize SingleGridparam.
        /// </summary>
        /// <param name="paramOwner">ParamOwner</param>
        public JobParam(object paramOwner)
            : base(paramOwner)
        {
            this.ObjectID = "PAR-FT-FTSL-FTE-JobParam";
        }
        public JobParam( )
           : base()
        {
            this.ObjectID = "PAR-FT-FTSL-FTE-JobParam";
        }

        /// <summary>
        /// Initialize SingleGridparam.
        /// </summary>
        /// <param name="paramOwner">ParamOwner</param>
        /// <param name="txServiceID">TranscationID</param>
        public JobParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            this.ObjectID = "PAR-FT-FTSL-FTE-JobParam";
        }
        #endregion
    }
}
