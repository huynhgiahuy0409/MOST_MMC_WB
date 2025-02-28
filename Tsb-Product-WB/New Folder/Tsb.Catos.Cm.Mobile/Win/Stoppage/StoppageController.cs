using System;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.Stoppage;
using Tsb.Catos.Cm.Mobile.Common.Param;
using Tsb.Catos.Cm.Mobile.Common.Param.Stoppage;
using Tsb.Catos.Cm.Mobile.Service.Stoppage;
using Tsb.Catos.Cm.Net.Constants;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.Stoppage
{
    public class StoppageController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-StoppageController";
        private IStoppageService _stoppageService;
        private StoppageInterface _formView;
        private StoppageReasonItemList _stoppageItemList;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public EquipmentItem SelectedEquipmentItem { get; set; }
        public StoppageReasonItem SelectedStoppageReasonItem { get; set; }
        public string SelectedVesselCode { get; set; }
        public string SelectedCallYear { get; set; }
        public string SelectedCallSeq { get; set; }
        public string DriverID { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public StoppageController()
        {
            ObjectID = OBJECT_ID;
            InitService();
        }

        public StoppageController(StoppageInterface view)
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
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
            }
        }

        public void DoRetrieveData()
        {
            BaseResult tempBaseResult = null;

            try
            {
                if (_stoppageService == null) return;

                // Modified by BG.Kim (2023.01.10)	[PCT] Not show vessel stoppages on Tally VMT
                //tempBaseResult = _stoppageService.GetEquipmentStoppageReasonAll(new StoppageReasonParam(this));
                StoppageReasonParam stoppageRsnParam = new StoppageReasonParam(this);
                stoppageRsnParam.IsExcludeVesselStoppage = _formView.ExcludeVesselStoppage;
                tempBaseResult = _stoppageService.GetEquipmentStoppageReasonAll(stoppageRsnParam);
                StoppageReasonItemList stoppageReasonItemList = tempBaseResult.ResultObject as StoppageReasonItemList;

                _stoppageItemList = new StoppageReasonItemList();
                if (stoppageReasonItemList != null)
                {
                    foreach (StoppageReasonItem item in stoppageReasonItemList)
                    {
                        if (this.SelectedEquipmentItem.EquType.Equals(CTBizConstant.EquipmentType.REACH_STACKER)
                            || this.SelectedEquipmentItem.EquType.Equals(CTBizConstant.EquipmentType.FORK_LIFT)
                            || this.SelectedEquipmentItem.EquType.Equals(CTBizConstant.EquipmentType.STRADDLE_CARRIER)
                            || this.SelectedEquipmentItem.EquType.Equals(CTBizConstant.EquipmentType.RTG))
                        {
                            if (item.UseYc.Equals(MOCommonConstants.StoppageAuthority.RDT)
                                || item.UseYc.Equals(MOCommonConstants.StoppageAuthority.ALL))
                            {
                                _stoppageItemList.Add(item);
                            }
                        }
                        else if (this.SelectedEquipmentItem.EquType.Equals(CTBizConstant.EquipmentType.CRANE))
                        {
                            if (item.UseGc.Equals(MOCommonConstants.StoppageAuthority.RDT)
                                || item.UseGc.Equals(MOCommonConstants.StoppageAuthority.ALL))
                            {
                                _stoppageItemList.Add(item);
                            }
                        }
                    }
                }

                if (_stoppageItemList != null)
                {
                    BaseItemsList<ItemListControlItem> itemList = new BaseItemsList<ItemListControlItem>();
                    foreach (StoppageReasonItem item in _stoppageItemList)
                    {
                        ItemListControlItem displayItem = new ItemListControlItem();
                        displayItem.Code = item.StopCode;
                        displayItem.CodeName = item.StopDesc;
                        displayItem.TextValue = item.StopDesc;
                        itemList.Add(displayItem);
                    }
                    _formView.SetItemList(itemList);
                    _formView.SetEquipmentNo(this.SelectedEquipmentItem.Name);
                }

            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
            }
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA ***************************************************

        public BaseResult InsertStopReason(string stoppageCode)
        {
            BaseResult resultObject = null;

            try
            {
                if (_stoppageService != null)
                {
                    StoppageItem item = new StoppageItem();
                    item.EquipmentNo = this.SelectedEquipmentItem.Name;
                    item.Status = MOCommonConstants.EquipmentStatus.RUN;
                    item.StopReason = stoppageCode;
                    item.Remark = stoppageCode;
                    item.VesselCode = SelectedVesselCode;
                    item.CallYear = SelectedCallYear;
                    item.CallSeq = SelectedCallSeq;
                    item.Active = false;
                    string driverID = string.IsNullOrEmpty(DriverID) == true ? AppEnv.UserInfo.UserID : DriverID; // added by YoungOk Kim (2019.08.23) - Mantis 92633: ESM many records its TGDVID is C3IT
                    item.StaffCd = AppEnv.UserInfo.UserID + C3ITInfConstant.CHAR_DTL_PLUS + driverID;

                    CudServiceParam param = new CudServiceParam();
                    param.ParamDataItem = item;
                    resultObject = _stoppageService.UpdateStoppageInfoOfEquipment(param);
                    if (resultObject != null)
                    {
                        if (_stoppageItemList != null)
                        {
                            this.SelectedStoppageReasonItem = _stoppageItemList.GetItem(stoppageCode);
                        }
                    }
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
