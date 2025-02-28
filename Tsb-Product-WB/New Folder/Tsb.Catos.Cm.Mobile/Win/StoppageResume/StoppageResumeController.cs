using System;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Item.Stoppage;
using Tsb.Catos.Cm.Mobile.Common.Param;
using Tsb.Catos.Cm.Mobile.Service.Stoppage;
using Tsb.Catos.Cm.Net.Constants;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.StoppageResume
{
    public class StoppageResumeController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************
        
        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-StoppageResumeController";
        private StoppageResumeInterface _formView;
        private IStoppageService _stoppageService;
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string SelectedEquipmentNo { get; set; }
        public StoppageReasonItem SelectedStoppageReasonItem { get; set; }
        public string SelectedVesselCode { get; set; }
        public string SelectedCallYear { get; set; }
        public string SelectedCallSeq { get; set; }
        public string DriverID { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public StoppageResumeController()
        {
            ObjectID = OBJECT_ID;
            InitService();
        }

        public StoppageResumeController(StoppageResumeInterface view)
        {
            ObjectID = OBJECT_ID;
            _formView = view;
            InitService();
        }

        private void InitService()
        {
            try
            {
                _stoppageService = BizServiceLocator.GetService<IStoppageService>(ServiceConstant.STOPPAGE_SERVICE);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA ***************************************************

        public BaseResult UpdateStopReason(string stopReason)
        {
            BaseResult resultObject = null;

            try
            {
                if (_stoppageService != null)
                {
                    StoppageItem item = new StoppageItem();
                    item.EquipmentNo = this.SelectedEquipmentNo;
                    item.Status = MOCommonConstants.EquipmentStatus.STOPPAGE;
                    item.StopReason = stopReason;
                    item.Remark = SelectedStoppageReasonItem.StopCode;
                    item.VesselCode = SelectedVesselCode;
                    item.CallYear = SelectedCallYear;
                    item.CallSeq = SelectedCallSeq;
                    item.Active = true;
                    string driverID = string.IsNullOrEmpty(DriverID) == true ? AppEnv.UserInfo.UserID : DriverID; // added by YoungOk Kim (2019.08.23) - Mantis 92633: ESM many records its TGDVID is C3IT
                    item.StaffCd = AppEnv.UserInfo.UserID + C3ITInfConstant.CHAR_DTL_PLUS + driverID;

                    CudServiceParam param = new CudServiceParam();
                    param.ParamDataItem = item;
                    resultObject = _stoppageService.UpdateStoppageInfoOfEquipment(param);
                    if (resultObject != null) this.SelectedStoppageReasonItem = null;
                }

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

        #endregion METHOD AREA ************************************************
    }
}
