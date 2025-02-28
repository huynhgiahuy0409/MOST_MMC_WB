using System;
using Tsb.Catos.Cm.Mobile.Common.Item.Stoppage;
using Tsb.Catos.Cm.Mobile.Common.Param.Stoppage;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;

namespace Tsb.Catos.Cm.Mobile.Service.Stoppage.Dao
{
    public class StoppageDao : SqlMapDaoSupport, IStoppageDao
    {
        #region INITIALIZATION AREA *******************************************

        private const string MAP_PACKAGE_STR = "Tsb.Catos.Cm.Mobile.Service.Stoppage.Map.";

        public StoppageDao()
            : base()
        {
            this.ObjectID = "DAO-CT-CTMO-StoppageDao";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************

        public StoppageReasonItemList GetEquipmentStoppageReasonAll(StoppageReasonParam param)
        {
            StoppageReasonItemList stoppageReasonItemList = null;

            try
            {
                stoppageReasonItemList = new StoppageReasonItemList(this.QueryForList<StoppageReasonItem>(MAP_PACKAGE_STR + "StoppageMap.select-EquipmentStoppageReasonAll", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return stoppageReasonItemList;
        }

        public StoppageReasonItemList GetEquipmentStoppageReason(StoppageReasonParam param)
        {
            StoppageReasonItemList stoppageReasonItemList = null;

            try
            {
                stoppageReasonItemList = new StoppageReasonItemList(this.QueryForList<StoppageReasonItem>(MAP_PACKAGE_STR + "StoppageMap.select-EquipmentStoppageReason", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return stoppageReasonItemList;
        }

        #endregion METHOD AREA (SELECT) ***************************************
    }
}
