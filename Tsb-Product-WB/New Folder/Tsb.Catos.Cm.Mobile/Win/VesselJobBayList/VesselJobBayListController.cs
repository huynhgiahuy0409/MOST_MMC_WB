using System;
using System.Collections.Generic;
using System.Linq;
using Tsb.Catos.Cm.Core.Codes.Item;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation;
using Tsb.Catos.Cm.Mobile.Common.Param.VesselInformation;
using Tsb.Catos.Cm.Mobile.Service.VesselInformation;
using Tsb.Fontos.Core.Codes;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Observer;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.VesselJobBayList
{
    public class VesselJobBayListController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-VesselJobBayListController";
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        private VesselJobBayListInterface FormView { get; set; }
        private IVesselInformationService VesselInformationService { get; set; }
        private DataSyncAgent DataSyncAgent { get; set; }

        public BerthPlanItem SelectedVesselItem { get; set; }
        public string SelectedJobBay { get; set; }
        public VesselJobBayListViewItem InputValue { get; set; }
        public bool UseShowAllJobBay { get; set; } // added by YoungOk Kim (2019.04.16) - Mantis 89978: [Tally] 선박의 전체 베이 표시
        public bool UseActivatedInAllBay { get; set; }//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법 
        private string _buttonListBlockBayActivated = "biz_blockbay_Activated_btn_list";//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        public string SelectedEquipmentNo { get; set; }//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        public bool UseShowAllVessel { get; set; } // added by BE.Ahn (2020. 07. 08) - Mnatis 0107443: Remove vessel without operations

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public VesselJobBayListController()
        {
            ObjectID = OBJECT_ID;
        }

        public VesselJobBayListController(VesselJobBayListInterface view)
        {
            ObjectID = OBJECT_ID;
            FormView = view;
            this.DataSyncAgent = new DataSyncAgent();

            InitService();
        }

        private void InitService()
        {
            try
            {
                VesselInformationService = BizServiceLocator.GetService<IVesselInformationService>(ServiceConstant.VESSEL_INFORMATION_SERVICE);
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

        #region BASE IMPLEMENTATION AREA **************************************

        public void NotifySyncAgent()
        {
            this.InputValue = GetInputValue();

            object targetSyncData = this.InputValue as VesselJobBayListViewItem;

            this.DataSyncAgent.NotifyToSync(DataSyncConstant.SYNC_NOTIFY_VESSEL_JOBBAY_LIST, targetSyncData);
        }

        #endregion BASE IMPLEMENTATION AREA ***********************************

        #region METHOD AREA ***************************************************

        public BaseItemsList<ItemListControlItem> DoRetrieveData()
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                itemList = GetBerthPlanList();
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }

            return itemList;
        }

        public BaseItemsList<ItemListControlItem> GetBerthPlanList()
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                var result = VesselInformationService.GetVesselScheduleList();
                var vesselScheduleList = result.ResultObject as BaseItemsList<BerthPlanItem>;

                if (vesselScheduleList != null && vesselScheduleList.Any())
                {
                    itemList = new BaseItemsList<ItemListControlItem>();

                    foreach (var codeItem in vesselScheduleList)
                    {
                        bool isBarge = string.IsNullOrEmpty(codeItem.BargeChk) == false && codeItem.BargeChk.Equals(CTBizConstant.YesNo.YES);
                        if (isBarge == false)
                        {
                            // added by BE.Ahn (2020. 07. 08) - Mnatis 0107443: Remove vessel without operations
                            if (UseShowAllVessel == false)
                            {
                                var isEmpty = true;
                                IList<CodeGeneralItem> codeItemList = CodeManager.GetCodes<CodeGeneralItem>(CTBizConstant.CodeType.VVD_JOB_BAY, new string[] { codeItem.VslCd, codeItem.CallYear, codeItem.CallSeq });

                                if (codeItemList != null && codeItemList.Any())
                                {
                                    foreach (var Item in codeItemList)
                                    {
                                        if (string.IsNullOrEmpty(Item.Code) == false)
                                        {
                                            isEmpty = false;
                                        }
                                    } 
                                    if (isEmpty == false)
                                    {
                                        ItemListControlItem item = new ItemListControlItem();
                                        item.Code = codeItem.Key;
                                        item.CodeName = codeItem.VslCd;
                                        item.CodeItem = codeItem;
                                        if (FormView.UseUserVoyage)
                                        {
                                            item.TextValue = codeItem.UserVoy;
                                        }
                                        else if (FormView.UseVesselName)
                                        {
                                            item.TextValue = codeItem.VslName;
                                        }
                                        else if (FormView.UsePortVoy) // added by BG.Kim (2022.11.29) [WHL_UP] 0134776: Show VRN(PORT_VOY) in Location
                                        {
                                            item.TextValue = codeItem.PortVoy;
                                        }
                                        else
                                        {
                                            item.TextValue = codeItem.VslCd + "-" + codeItem.CallSeq;
                                        }
                                        itemList.Add(item);
                                    }
                                }
                                else
                                {
                                    //nothing
                                }
                            }
                            else
                            {
                                ItemListControlItem item = new ItemListControlItem();
                                item.Code = codeItem.Key;
                                item.CodeName = codeItem.VslCd;
                                item.CodeItem = codeItem;
                                if (FormView.UseUserVoyage)
                                {
                                    item.TextValue = codeItem.UserVoy;
                                    if (FormView.UsePortVoy) // added by BG.Kim (2022.11.29) [WHL_UP] 0134776: Show VRN(PORT_VOY) in Location
                                    {
                                        item.TextValue = codeItem.PortVoy;
                                    }
                                }
                                else if (FormView.UseVesselName)
                                {
                                    item.TextValue = codeItem.VslName;
                                }
                                else if (FormView.UsePortVoy) // added by BG.Kim (2022.11.29) [WHL_UP] 0134776: Show VRN(PORT_VOY) in Location
                                {
                                    item.TextValue = codeItem.PortVoy;
                                }
                                else
                                {
                                    item.TextValue = codeItem.VslCd + "-" + codeItem.CallSeq;
                                }
                                itemList.Add(item);
                            }
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

            return itemList;
        }

        public BaseItemsList<ItemListControlItem> GetJobSeqList(string vslCd, string callYear, string callSeq, bool isShowAll)
        {
            BaseItemsList<ItemListControlItem> itemList = null;

            try
            {
                // added by YoungOk Kim (2019.04.16) - Mantis 89978: [Tally] 선박의 전체 베이 표시
                if (this.UseShowAllJobBay == true)
                {
                    isShowAll = true;
                }

                if (isShowAll)
                {
                    // modified by JH.Tak (2021.07.07) KPCT Driverless YT
                    if (this.FormView.UseBayInsteadHatch)
                    {
                        VesselInformationParam param = new VesselInformationParam(this);
                        param.VesselCode = vslCd;
                        var result = VesselInformationService.GetBayItemList(param);
                        var bayItemList = result.ResultObject as BaseItemsList<VesselBayItem>;
                        BaseItemsList<ItemListControlItem> jobBayItemList = new BaseItemsList<ItemListControlItem>();
                        
                        ItemListControlItem noneItem = new ItemListControlItem();
                        noneItem.Code = "";
                        noneItem.CodeName = "";
                        noneItem.TextValue = "[None]";
                        jobBayItemList.Add(noneItem);

                        foreach (VesselBayItem bayItem in bayItemList)
                        {
                            ItemListControlItem jobBayItem = new ItemListControlItem();
                            jobBayItem.Code = bayItem.NO;
                            jobBayItem.CodeName = bayItem.NO;
                            jobBayItem.TextValue = bayItem.NO;
                            jobBayItemList.Add(jobBayItem);
                        }

                        itemList = jobBayItemList;
                    }
                    else
                    {
                        VesselInformationParam param = new VesselInformationParam(this);
                        param.VesselCode = vslCd;

                        var result = VesselInformationService.GetHatchItemList(param);
                        var hatchInfoItemList = result.ResultObject as BaseItemsList<HatchInfoItem>;

                        BaseItemsList<ItemListControlItem> jobBayItemList = new BaseItemsList<ItemListControlItem>();

                        if (this.UseActivatedInAllBay == true && SelectedEquipmentNo != null)//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법 
                        {
                            this.FormView.UseActivatedInAllBay = true;

                            ActivatedVesselInformationParam activatedParam = new ActivatedVesselInformationParam(this);
                            activatedParam.VesselCode = vslCd;
                            activatedParam.CallYear = callYear;
                            activatedParam.CallSequence = callSeq;
                            activatedParam.EquNo = SelectedEquipmentNo;

                            var activatedresult = VesselInformationService.GetActivatedHatchItemList(activatedParam);
                            var activatedHatchInfoItemList = activatedresult.ResultObject as BaseItemsList<ActivatedHatchInfoItem>;

                            if (activatedHatchInfoItemList != null)
                            {
                                foreach (HatchInfoItem hatchItem in hatchInfoItemList)
                                {
                                    string jobBay = hatchItem.StartBayIdx == hatchItem.EndBayIdx
                              ? hatchItem.StartBayNo : hatchItem.StartBayNo + " " + (Int32.Parse(hatchItem.EndBayNo) - 1).ToString("D2") + "/" + hatchItem.EndBayNo;

                                    ItemListControlItem item = new ItemListControlItem();
                                    item.Code = hatchItem.StartBayNo;
                                    item.CodeName = jobBay;
                                    item.TextValue = jobBay;
                                    jobBayItemList.Add(item);

                                    int compareHatchIdx = (Int32.Parse(hatchItem.StartBayNo) + Int32.Parse(hatchItem.EndBayNo)) / 2;
                                    foreach (var activatedItem in activatedHatchInfoItemList)
                                    {
                                        if (compareHatchIdx != null && compareHatchIdx != 0)
                                        {
                                            if (compareHatchIdx == Convert.ToInt32(activatedItem.HatchIdx))
                                            {
                                                item.CustomStyleName = _buttonListBlockBayActivated;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (HatchInfoItem hatchItem in hatchInfoItemList)
                            {
                                string jobBay = hatchItem.StartBayIdx == hatchItem.EndBayIdx
                                    ? hatchItem.StartBayNo : hatchItem.StartBayNo + " " + (Int32.Parse(hatchItem.EndBayNo) - 1).ToString("D2") + "/" + hatchItem.EndBayNo;

                                ItemListControlItem jobBayItem = new ItemListControlItem();
                                jobBayItem.Code = hatchItem.StartBayNo;
                                jobBayItem.CodeName = jobBay;
                                jobBayItem.TextValue = jobBay;
                                jobBayItemList.Add(jobBayItem);
                            }
                        }
                        itemList = jobBayItemList;
                    }
                }
                else
                {
                    IList<CodeGeneralItem> codeItemList = CodeManager.GetCodes<CodeGeneralItem>(CTBizConstant.CodeType.VVD_JOB_BAY, new string[] { vslCd, callYear, callSeq });
                    if (codeItemList != null)
                    {
                        itemList = new BaseItemsList<ItemListControlItem>();

                        foreach (var codeItem in codeItemList)
                        {
                            if (string.IsNullOrEmpty(codeItem.Code) == false)
                            {
                                ItemListControlItem item = new ItemListControlItem();
                                item.Code = codeItem.Code;
                                item.CodeName = codeItem.CodeName;
                                item.TextValue = codeItem.CodeName;
                                itemList.Add(item);
                            }
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

            return itemList;
        }

        public VesselJobBayListViewItem GetInputValue()
        {
            VesselJobBayListViewItem returnValue = null;

            try
            {
                returnValue = new VesselJobBayListViewItem();
                returnValue.VesselScheduleItem = this.SelectedVesselItem;
                returnValue.JobBay = this.SelectedJobBay;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }

            return returnValue;
        }

        #endregion METHOD AREA ************************************************
    }
}
