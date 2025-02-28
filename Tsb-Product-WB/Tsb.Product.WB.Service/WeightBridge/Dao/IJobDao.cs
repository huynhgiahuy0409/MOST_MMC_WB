using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.WeightBridge;

namespace Tsb.Product.WB.Service.WeightBridge.Dao
{
    public interface IJobDao
    {
        #region SEARCH AREA *****************************************
        BaseItemsList<JobItem> InquiryJob(JobParam param);
        #endregion
        #region CUD AREA ********************************************
        #endregion
    }
}
