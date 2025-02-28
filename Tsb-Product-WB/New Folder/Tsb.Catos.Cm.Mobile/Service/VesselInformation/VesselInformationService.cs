using System;
using Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation;
using Tsb.Catos.Cm.Mobile.Common.Param.VesselInformation;
using Tsb.Catos.Cm.Mobile.Service.VesselInformation.Dao;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;

namespace Tsb.Catos.Cm.Mobile.Service.VesselInformation
{
    public class VesselInformationService : MessgaeService, IVesselInformationService
    {
        #region CONST & FIELD AREA ********************************************

        private IVesselInformationDao _vesselInformationDao;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public IVesselInformationDao VesselInformationDao
        {
            get { return this._vesselInformationDao; }
            set { this._vesselInformationDao = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public VesselInformationService()
        {
            ObjectID = "SVC-CT-CTMO-VesselInformationService";
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA (SELECT) ******************************************

        public BaseResult GetVesselScheduleList()
        {
            BaseResult resultObject = null;
            try
            {
                BaseItemsList<BerthPlanItem> vesselScheduleList = VesselInformationDao.GetVesselScheduleList();
                resultObject = BaseResult.CreateOkResult(ObjectID, vesselScheduleList, null);
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

        public BaseResult GetHatchItemList(VesselInformationParam param)
        {
            BaseResult resultObject = null;
            try
            {
                BaseItemsList<HatchInfoItem> hatchItemList = VesselInformationDao.GetHatchItemList(param);
                resultObject = BaseResult.CreateOkResult(ObjectID, hatchItemList, param);
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

        //added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법 
        public BaseResult GetActivatedHatchItemList(ActivatedVesselInformationParam param)
        {
            BaseResult resultObject = null;
            try
            {
                BaseItemsList<ActivatedHatchInfoItem> activatedhatchItemList = VesselInformationDao.GetActivatedHatchItemList(param);
                resultObject = BaseResult.CreateOkResult(ObjectID, activatedhatchItemList, param);
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

        public BaseResult GetBayItemList(VesselInformationParam param)
        {
            BaseResult resultObject = null;
            try
            {
                BaseItemsList<VesselBayItem> hatchItemList = VesselInformationDao.GetBayItemList(param);
                resultObject = BaseResult.CreateOkResult(ObjectID, hatchItemList, param);
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

        public BaseResult GetVesselSchedule(ActivatedVesselInformationParam param)
        {
            BaseResult resultObject = null;
            try
            {
                BerthPlanItem item = VesselInformationDao.GetVesselSchedule(param);
                resultObject = BaseResult.CreateOkResult(ObjectID, item, null);
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
        #endregion METHOD AREA (SELECT) ***************************************
    }
}
