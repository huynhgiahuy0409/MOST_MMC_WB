using System;
using Tsb.Catos.Cm.Mobile.Common.Item.Staff;
using Tsb.Catos.Cm.Mobile.Common.Param.Staff;
using Tsb.Fontos.Core.Data.IBatis;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;

namespace Tsb.Catos.Cm.Mobile.Service.UserIdList.Dao
{
    public class UserIdListDao : SqlMapDaoSupport, IUseridListDao
    {
        #region CONST & FIELD AREA ********************************************

        private const string MAP_PACKAGE_STR = "Tsb.Catos.Cm.Mobile.Service.UserIdList.Map.";

        #endregion CONST & FIELD AREA *****************************************

        #region INITIALIZATION AREA *******************************************

        public UserIdListDao()
            : base()
        {
            this.ObjectID = "DAO-CT-CTMO-UserIdListDao";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************

        public BaseItemsList<StaffItem> GetUserIdList(GetStaffParam param)
        {
            BaseItemsList<StaffItem> itemList = null;

            try
            {
                itemList = new BaseItemsList<StaffItem>(this.QueryForList<StaffItem>(MAP_PACKAGE_STR + "UserIdListMap.select-UserIdList", param));
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizDAOException), this.ObjectID, "MSG_CTCM_00003", null);
            }
            return itemList;
        }

        #endregion METHOD AREA (SELECT) ***************************************
    }
}

