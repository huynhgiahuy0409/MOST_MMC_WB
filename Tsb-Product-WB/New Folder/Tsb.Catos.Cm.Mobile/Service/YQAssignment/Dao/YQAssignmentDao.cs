using System;
using Tsb.Catos.Cm.Mobile.Common.Item.YQAssignment;
using Tsb.Catos.Cm.Mobile.Common.Param.YQAssignment;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Service.YQAssignment.Dao
{
    public class YQAssignmentDao: SqlMapDaoSupport, IYQAssignmentDao
    {
        #region INITIALIZATION AREA *******************************************

        private const string MAP_PACKAGE_STR = "Tsb.Catos.Cm.Mobile.Service.YQAssignment.Map.";

        public YQAssignmentDao()
            : base()
        {
            this.ObjectID = "DAO-CT-CTMO-YQAssignmentDao";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************

        public BaseItemsList<EquipmentCoverageItem> GetYQCoverageList(YQAssignmentParam param)
        {
            BaseItemsList<EquipmentCoverageItem> yqAssignmentItemList = null;

            try
            {
                yqAssignmentItemList = new BaseItemsList<EquipmentCoverageItem>(this.QueryForList<EquipmentCoverageItem>(MAP_PACKAGE_STR + "YQAssignmentMap.select-yqCoverageList", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return yqAssignmentItemList;
        }

        #endregion METHOD AREA (SELECT) ***************************************
    }
}