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
    public class WeightBridgeDao : SqlMapDaoSupport, IWeightBridgeDao
    {
        #region INITIALIZE AREA *************************************
        public WeightBridgeDao()
         : base()
        {
            this.ObjectID = "DAO-PT-PTWB-WB-WeightBridgeDao";
        }
        #endregion
        #region IWeightBridgeDao Members ******************************
        public WeightBridgeItem SelectTransactionNo(WeightBridgeParam parm)
        {
            WeightBridgeItem result = null;
            try
            {
                result = this.QueryForObject<WeightBridgeItem>("Tsb.Product.WB.Service.WeightBridge.Map.WeightBridgeMap.select-transactionNo", parm);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizBaseException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return result;
        }
      
        public BaseItemsList<WeightBridgeItem> InquiryWeightBridge(WeightBridgeParam param)
        {
            BaseItemsList<WeightBridgeItem> items = null;
            try
            {
                items = new BaseItemsList<WeightBridgeItem>(this.QueryForList<WeightBridgeItem>("Tsb.Product.WB.Service.WeightBridge.Map.WeightBridgeMap.select-weightBridge", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizBaseException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return items;
        }
        public object CreateWeightBridge(WeightBridgeItem item)
        {
            object rtnObj = null;

            try
            {
                rtnObj = this.InsertItem("Tsb.Product.WB.Service.WeightBridge.Map.WeightBridgeMap.insert-weightBridge", item);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return rtnObj;
        }

        public int UpdateWeightBridge(WeightBridgeItem item)
        {
            int result = -1;
            try
            {
                result = this.Update("Tsb.Product.WB.Service.WeightBridge.Map.WeightBridgeMap.update-weightBridge", item);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizBaseException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return result;
        }
        #endregion
    }
}
