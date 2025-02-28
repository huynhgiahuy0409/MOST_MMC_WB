
using System;
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
    public class WeightBridgeService : BaseService, IWeightBridgeService
    {
        public IWeightBridgeDao WeightBridgeDao { get; set; }
        public WeightBridgeService()
          : base()
        {
            this.ObjectID = "SVC-PT-PTWB-WB-WeightBridgeService";
        }
        public string GetNewTransactionNo(WeightBridgeParam param)
        {
            string result = null;
            try
            {
                WeightBridgeItem item = this.WeightBridgeDao.SelectTransactionNo(param);
                result = item.TransactionNo;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }

            return result;
        }
        public BaseResult InquiryWeightBridge(WeightBridgeParam param)
        {
            BaseResult result = null;
            try
            {
                BaseItemsList<WeightBridgeItem> items = this.WeightBridgeDao.InquiryWeightBridge(param);
                result = BaseResult.CreateOkResult(this.ObjectID, items, param);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }

            return result;
        }
        public void ProcessWeightBridge(WeightBridgeItem item)
        {
            try
            {
                if (item.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.UPDATE)
                {
                    this.WeightBridgeDao.UpdateWeightBridge(item);
                }
                else if (item.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.CREATE)
                {
                    this.WeightBridgeDao.CreateWeightBridge(item);
                }

            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), this.ObjectID, "MSG_CTCM_00008", null);
            }
            item.OpCode = Tsb.Fontos.Core.Configuration.Common.OpCodes.READ;
            item.BackupItem = item.Clone() as WeightBridgeItem;
        }
    }
}
