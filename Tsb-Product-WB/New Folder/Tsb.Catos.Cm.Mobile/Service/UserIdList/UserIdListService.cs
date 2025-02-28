using System;
using Tsb.Catos.Cm.Mobile.Common.Param.Staff;
using Tsb.Catos.Cm.Mobile.Service.UserIdList.Dao;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Service.UserIdList
{
    public class UserIdListService : MessgaeService, IUserIdListService
    {
        #region CONST & FIELD AREA ********************************************

        private IUseridListDao _userIdListDao = null;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public IUseridListDao UserIdListDao
        {
            get { return this._userIdListDao; }
            set { this._userIdListDao = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public UserIdListService()
        {
            this.ObjectID = "SVC-CT-CTMO-UserIdListService";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region INTERFACE IMPLEMENTATION AREA *********************************

        public BaseResult GetUserIdList(GetStaffParam param)
        {
            BaseResult resultObject = null;
            try
            {
                var list = UserIdListDao.GetUserIdList(param);
                resultObject = BaseResult.CreateOkResult(ObjectID, list, null);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
            return resultObject;
        }

        #endregion INTERFACE IMPLEMENTATION AREA ******************************

        #region METHOD AREA ***************************************************

        
        
        #endregion METHOD AREA ************************************************
    }
}
