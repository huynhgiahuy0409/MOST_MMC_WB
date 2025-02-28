using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.WeightBridge;
using Tsb.Product.WB.Service.WeightBridge.Dao;

namespace Tsb.Product.WB.Service.WeightBridge
{
    class JobService : BaseService, IJobService
    {
        public IJobDao Jobdao { get; set; }
        public JobService()
         : base()
        {
            this.ObjectID = "SVC-PT-PTWB-WB-JobService";
        }

        public BaseResult InquiryJob(JobParam param)
        {
               BaseResult resultObject = null;
            try
            {
                BaseItemsList<JobItem> items = this.Jobdao.InquiryJob(param);

                //=========================================
                // Code to make the processing speed slowed.
                foreach (JobItem item in items)
                {
                    System.Threading.Thread.Sleep(10);
                }
                //=========================================

                resultObject = BaseResult.CreateOkResult(this.ObjectID, items, param);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }
            return resultObject;
        }
    }
}
