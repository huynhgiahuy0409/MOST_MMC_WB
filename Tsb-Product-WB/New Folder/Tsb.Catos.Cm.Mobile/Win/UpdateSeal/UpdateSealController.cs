using System;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Item.UpdateSeal;
using Tsb.Catos.Cm.Mobile.Common.Param.UpdateSeal;
using Tsb.Catos.Cm.Mobile.Service.UpdateSeal;
using Tsb.Catos.Cm.Net.Constants;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Security.Audit;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.UpdateSeal
{
    public class UpdateSealController: BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-UpdateSealController";
        private UpdateSealInterface _formView;
        private IUpdateSealService _updateSealService;
        private UpdateSealItem _updateSealItem;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public UpdateSealInterface FormView 
        {
            get { return this._formView; }
            set { this._formView = value; }
        }
        
        public UpdateSealItem UpdateSealItem
        {
            get { return this._updateSealItem; }
            set { this._updateSealItem = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public UpdateSealController()
        {
            ObjectID = OBJECT_ID;
        }

        public UpdateSealController(UpdateSealInterface view)
        {
            ObjectID = OBJECT_ID;
            _formView = view;

            InitService();
        }

        private void InitService()
        {
            try
            {
                _updateSealService = BizServiceLocator.GetService<IUpdateSealService>(ServiceConstant.UPDATE_SEAL_SERVICE);
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

        public void UpdateSeal(string sealNo1, string sealNo2, string sealNo3, string sealChk)
        {
            try
            {
                if (UpdateSealItem != null)
                {
                    UpdateSealItem.SealNo1 = sealNo1;
                    UpdateSealItem.SealNo2 = sealNo2;
                    UpdateSealItem.SealNo3 = sealNo3;
                    if (string.IsNullOrEmpty(sealChk) == false)
                    {
                        UpdateSealItem.SealChk = sealChk; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
                    }
                    var param = new UpdateSealParam(this);
                    
                    switch (UpdateSealItem.JobCode)
                    {
                        case CTBizConstant.QuayJobType.DISCHARGING:
                        case CTBizConstant.QuayJobType.LOADING:
                        case CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_LOADING:
                        case CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_DISCHARGING:
                        case CTBizConstant.QuayJobType.ONE_TIME_SHIFTING:
                            param.MMode = C3ITInfConstant.MSG_MODE_QUAYJOB;
                            break;
                        case CTBizConstant.YardJobType.LIFT_ON_FOR_LOADING:
                        case CTBizConstant.YardJobType.LIFT_FOR_DISCHARGING:
                        case CTBizConstant.YardJobType.LIFT_OFF_FOR_GATE_IN:
                        case CTBizConstant.YardJobType.LIFT_ON_FOR_GATE_OUT:
                        case CTBizConstant.YardJobType.LIFT_ON_FOR_SHIFT:
                        case CTBizConstant.YardJobType.LIFT_OFF_FOR_SHIFT:
                        case CTBizConstant.YardJobType.SHIFT_IN_ONE_BAY:
                        case CTBizConstant.YardJobType.AUTO_SHIFTING:
                        case CTBizConstant.YardJobType.AUTO_SHIFTING_2:
                        case CTBizConstant.YardJobType.AUTO_SHIFT_IN_ONE_BLOCK:
                        case CTBizConstant.YardJobType.LIFT_ON_FOR_RAIL:
                        case CTBizConstant.YardJobType.LIFT_OFF_FOR_RAIL:
                            param.MMode = C3ITInfConstant.MSG_MODE_YARDJOB;
                            break;
                        default:
                            param.MMode = C3ITInfConstant.MSG_MODE_NOJOB;
                            break;
                    }
                    
                    param.UpdateSealItem = UpdateSealItem;
                    param.StaffCd = AuditUtil.GetStaffCd(FormView.FormName);
                    _updateSealService.UpdateSeal(param);
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
        }

        #endregion METHOD AREA ************************************************
    }
}
