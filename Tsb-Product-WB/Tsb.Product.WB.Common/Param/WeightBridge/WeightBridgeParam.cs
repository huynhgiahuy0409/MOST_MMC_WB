using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Param;

namespace Tsb.Product.WB.Common.Param.WeightBridge
{
    public class WeightBridgeParam : BaseParam
    {
        public string TransactionNo { get; set; }
        public string LaneNo { get; set; }

        public WeightBridgeParam(object paramOwner)
            : base(paramOwner)
        {
            this.ObjectID = "PAR-FT-FTWB-FTE-WeightBridgeParam";
        }
        public WeightBridgeParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            this.ObjectID = "PAR-FT-FTWB-FTE-WeightBridgeParam";
        }
    }
}
