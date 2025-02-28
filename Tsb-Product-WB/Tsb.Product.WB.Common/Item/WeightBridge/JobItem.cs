using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.Menu.Param;

namespace Tsb.Product.WB.Common.Item.WeightBridge
{
    public class JobItem : BaseDataItem, IContextMenuParam
    {
        public string JobTpCd { get; set; }
        public string JobPurpCd { get; set; }
    }
}
