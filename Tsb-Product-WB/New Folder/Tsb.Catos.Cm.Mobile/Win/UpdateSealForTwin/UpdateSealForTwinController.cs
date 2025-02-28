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

namespace Tsb.Catos.Cm.Mobile.Win.UpdateSealForTwin
{
    public class UpdateSealForTwinController: BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-UpdateSealForTwinController";
        private UpdateSealForTwinInterface _formView;
        private IUpdateSealService _updateSealService;
        private UpdateSealItem _foreUpdateSealItem;
        private UpdateSealItem _afterUpdateSealItem;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public UpdateSealForTwinInterface FormView
        {
            get { return this._formView; }
            set { this._formView = value; }
        }
        public UpdateSealItem ForeUpdateSealItem
        {
            get { return this._foreUpdateSealItem; }
            set { this._foreUpdateSealItem = value; }
        }
        public UpdateSealItem AfterUpdateSealItem
        {
            get { return this._afterUpdateSealItem; }
            set { this._afterUpdateSealItem = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public UpdateSealForTwinController()
        {
            ObjectID = OBJECT_ID;
        }

        public UpdateSealForTwinController(UpdateSealForTwinInterface view)
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

        public void UpdateSeal(UpdateSealItem foreItem, UpdateSealItem afterItem)
        {
            try
            {
                // fore container
                ForeUpdateSealItem.SealNo1 = foreItem.SealNo1;
                ForeUpdateSealItem.SealNo2 = foreItem.SealNo2;
                ForeUpdateSealItem.SealNo3 = foreItem.SealNo3;
                if (string.IsNullOrEmpty(foreItem.SealChk) == false)
                {
                    ForeUpdateSealItem.SealChk = foreItem.SealChk; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
                }
                var foreParam = new UpdateSealParam(this);
                
                switch (ForeUpdateSealItem.JobCode)
                {
                    case CTBizConstant.QuayJobType.DISCHARGING:
                    case CTBizConstant.QuayJobType.LOADING:
                    case CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_LOADING:
                    case CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_DISCHARGING:
                    case CTBizConstant.QuayJobType.ONE_TIME_SHIFTING:
                        foreParam.MMode = C3ITInfConstant.MSG_MODE_QUAYJOB;
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
                        foreParam.MMode = C3ITInfConstant.MSG_MODE_YARDJOB;
                        break;
                    default:
                        foreParam.MMode = C3ITInfConstant.MSG_MODE_NOJOB;
                        break;
                }

                foreParam.UpdateSealItem = ForeUpdateSealItem;
                foreParam.StaffCd = AuditUtil.GetStaffCd(FormView.FormName);
                _updateSealService.UpdateSeal(foreParam);

                // after container
                AfterUpdateSealItem.SealNo1 = afterItem.SealNo1;
                AfterUpdateSealItem.SealNo2 = afterItem.SealNo2;
                AfterUpdateSealItem.SealNo3 = afterItem.SealNo3;
                if (string.IsNullOrEmpty(afterItem.SealChk) == false)
                {
                    AfterUpdateSealItem.SealChk = afterItem.SealChk; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
                }
                var afterParam = new UpdateSealParam(this);

                switch (AfterUpdateSealItem.JobCode)
                {
                    case CTBizConstant.QuayJobType.DISCHARGING:
                    case CTBizConstant.QuayJobType.LOADING:
                    case CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_LOADING:
                    case CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_DISCHARGING:
                    case CTBizConstant.QuayJobType.ONE_TIME_SHIFTING:
                        afterParam.MMode = C3ITInfConstant.MSG_MODE_QUAYJOB;
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
                        afterParam.MMode = C3ITInfConstant.MSG_MODE_YARDJOB;
                        break;
                    default:
                        afterParam.MMode = C3ITInfConstant.MSG_MODE_NOJOB;
                        break;
                }

                afterParam.UpdateSealItem = AfterUpdateSealItem;
                afterParam.StaffCd = AuditUtil.GetStaffCd(FormView.FormName);
                _updateSealService.UpdateSeal(afterParam);
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
