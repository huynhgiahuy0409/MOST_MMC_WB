using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.WeightBridge;

namespace Tsb.Product.WB.Service.WeightBridge.Dao
{
    class JobDao : SqlMapDaoSupport, IJobDao
    {
        #region INITIALIZE AREA *************************************
        public JobDao()
            : base()
        {
            this.ObjectID = "DAO-PT-PTWB-WB-JobDao";
        }
        #endregion
        #region IJobDao Members ******************************
        public BaseItemsList<JobItem> InquiryJob(JobParam param)
        {
            BaseItemsList<JobItem> items = null;

            try
            {
                items = new BaseItemsList<JobItem>(this.QueryForList<JobItem>("Tsb.Product.WB.Service.WeightBridge.Map.JobMap.select-job", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return items;
        }
        #endregion
    }
}
