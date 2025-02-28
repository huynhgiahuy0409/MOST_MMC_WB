using System;
using System.Linq;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.Codes.Item;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Mobile.Common.Constants;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item.CommonItemList;
using Tsb.Catos.Cm.Mobile.Common.Item.MobileContainerDetail;
using Tsb.Catos.Cm.Mobile.Common.Item.UpdateSeal;
using Tsb.Catos.Cm.Mobile.Common.Param.MobileContainerDetail;
using Tsb.Catos.Cm.Mobile.Service.MobileContainerDetail;
using Tsb.Catos.Cm.Mobile.Win.CommonItemList;
using Tsb.Catos.Cm.Mobile.Win.UpdateSeal;
using Tsb.Catos.Cm.Net.Constants;
using Tsb.Fontos.Core.Codes;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Resources;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Win.Menu.Action.Command;
using Tsb.Fontos.Win.MobileTemplate;

namespace Tsb.Catos.Cm.Mobile.Win.MobileContainerDetail
{
    public class MobileContainerDetailController : BaseMobileBizController
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "CTL-CT-CTMO-CTRL-MobileContainerDetailController";
        private readonly string TERMINAL_CODE_GWCT = "GWCT";

        private MobileContainerDetailInterface _formView;
        private IMobileContainerDetailService _mobileContainerDetailService;
        private MobileContainerDetailItem _detailItem;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string SelectedContainerNo { get; set; }
        public string SelectedEquipmentNo { get; set; }
        public string SelectedVesselCode { get; set; }
        public string SelectedCallYear { get; set; }
        public string SelectedCallSequence { get; set; }
        public string SelectedUserVoyage { get; set; }
        public BlockItemList BlockItemList { get; set; }
        public bool UseUserVoayage { get; set; }
        public bool UseDisableSealNo3 { get; set; }
        public string MessageCodeHead { get; set; }
        public string Module { get; set; }
        public string TerminalCode { get; set; }
        public bool VisibleSealChk { get; set; } // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public MobileContainerDetailController()
        {
            ObjectID = OBJECT_ID;
        }

        public MobileContainerDetailController(MobileContainerDetailInterface view)
        {
            ObjectID = OBJECT_ID;
            _formView = view;

            InitService();
        }

        private void InitService()
        {
            try
            {
                _mobileContainerDetailService = BizServiceLocator.GetService<IMobileContainerDetailService>(ServiceConstant.MOBILE_CONTAINER_DETAIL_SERVICE);
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

        public void DoRetrieveData()
        {
            MobileContainerDetailItem detailItem = null;

            try
            {                
                if (string.IsNullOrEmpty(this.SelectedContainerNo) == false)
                {
                    var param = new MobileContainerDetailParam(this);
                    param.MMode = C3ITInfConstant.MSG_MODE_RC;
                    param.EquipmentNo = this.SelectedEquipmentNo;
                    param.ContainerNo = this.SelectedContainerNo;

                    if (MessageCodeHead.Equals("Y"))
                    {
                        var result = _mobileContainerDetailService.GetYardContainerDetailItem(param);
                        detailItem = result.ResultObject as MobileContainerDetailItem;
                    }
                    else if (MessageCodeHead.Equals("Q"))
                    {
                        // added by YoungOk Kim (2019.10.15) - Mantis 92188: [Tally] 컨테이너 정보에서 컨테이너 번호 뒷자리 4자리로 조회
                        string containerNo = SelectedContainerNo;
                        if (TerminalCode.Equals(TERMINAL_CODE_GWCT) == true)
                        {
                            if (SelectedContainerNo.Length < 11)
                            {
                                MobileContainerDetailParam mobileCntrDetailParam = new MobileContainerDetailParam(this);
                                mobileCntrDetailParam.ContainerNo = this.SelectedContainerNo;
                                mobileCntrDetailParam.VesselCode = this.SelectedVesselCode;
                                mobileCntrDetailParam.CallYear = this.SelectedCallYear;
                                mobileCntrDetailParam.CallSequence = this.SelectedCallSequence;

                                var containerList = _mobileContainerDetailService.GetContainerList(mobileCntrDetailParam).ResultObject as BaseItemsList<ContainerInfoItem>;
                                if (containerList != null && containerList.Any())
                                {
                                    if (containerList.Count > 1)
                                    {
                                        BaseItemsList<CommonItemListItem> itemList = new BaseItemsList<CommonItemListItem>();

                                        foreach (var cntrItem in containerList)
                                        {
                                            var sameCntrItem = itemList.Where(p => p.Code.Equals(cntrItem.ContainerNo)).FirstOrDefault();
                                            if (sameCntrItem != null)
                                            {
                                                continue;
                                            }

                                            CommonItemListItem item = new CommonItemListItem();
                                            item.Code = cntrItem.ContainerNo;
                                            item.CodeName = cntrItem.ContainerNo;
                                            item.DisplayValue = cntrItem.ContainerNo + Environment.NewLine + cntrItem.ShipBay + cntrItem.HoldDeck + "-" + cntrItem.ShipRow + "-" + cntrItem.ShipTier;
                                            itemList.Add(item);
                                        }

                                        if (itemList != null)
                                        {
                                            if (itemList.Count > 1)
                                            {
                                                var title = ResourceFactory.GetResource().GetLabel("WRD_CTMO_ContainerNo");
                                                var commonItemListItem = ShowItemListView(itemList, title);
                                                if (commonItemListItem != null)
                                                {
                                                    containerNo = commonItemListItem.Code;
                                                }
                                                else
                                                {
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                var item = itemList.FirstOrDefault();
                                                if (item != null)
                                                {
                                                    containerNo = item.Code;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var cntrItem = containerList.FirstOrDefault();
                                        if (cntrItem != null)
                                        {
                                            containerNo = cntrItem.ContainerNo;
                                        }
                                    }
                                }
                                _formView.SetContainerNo(containerNo);
                            }
                        }
                        else
                        {
                            // nothing
                        }
                        param.ContainerNo = containerNo;

                        param.VesselCode = this.SelectedVesselCode;
                        param.CallYear = this.SelectedCallYear;
                        param.CallSequence = this.SelectedCallSequence;

                        var result = _mobileContainerDetailService.GetQuayContainerDetailItem(param);
                        detailItem = result.ResultObject as MobileContainerDetailItem;
                    }
                    else
                    {
                        // nothing
                    }

                    if (detailItem != null)
                    {
                        if (UseUserVoayage)
                        {
                            detailItem.Vvd = string.IsNullOrEmpty(detailItem.UserVoy) ? this.SelectedUserVoyage : detailItem.UserVoy;
                        }
                        DoDecorate(detailItem);
                    }
                    this._detailItem = detailItem;
                }
                else
                {
                    this._detailItem = null;
                }
                //_formView.SetDetailLayout(detailItem); // deleted by YoungOk Kim (2019.03.05) - Mantis 88953: Tally module keep dis/load information for ROB container
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
            finally
            {
                _formView.SetDetailLayout(detailItem); // added by YoungOk Kim (2019.03.05) - Mantis 88953: Tally module keep dis/load information for ROB container
            }
        }

        private void DoDecorate(MobileContainerDetailItem item)
        {
            try
            {
                if (this.BlockItemList != null)
                {
                    if (this.BlockItemList.GetBlock(item.Block) == null)
                    {
                        item.YardPosition = item.Block;
                    }
                    else
                    {
                        RowItem rowItem = string.IsNullOrEmpty(item.Row) ? null : this.BlockItemList.GetBlock(item.Block).GetRow(Convert.ToInt16(item.Row));
                        string row = rowItem == null ? string.Empty : rowItem.Name;
                        string bay = string.Empty;
                        if (string.IsNullOrEmpty(item.Bay) == false)
                        {
                            BayItem bayItem = this.BlockItemList.GetBlock(item.Block).GetBay(Convert.ToInt16(item.Bay));
                            if (bayItem != null)
                            {
                                if (item.SzTp2.IndexOf('2') == 0)
                                {
                                    bay = bayItem.Name2;
                                }
                                else
                                {
                                    bay = bayItem.Name4;
                                }
                            }
                        }

                        if (this.BlockItemList.GetBlock(item.Block).Facility.Equals(CTBizConstant.BlockType.SC))
                        {
                            if (string.IsNullOrEmpty(row))
                            {
                                item.YardPosition = item.Block;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(item.Tier))
                                {
                                    item.YardPosition = item.Block + "-" + row + "-" + bay;
                                }
                                else
                                {
                                    item.YardPosition = item.Block + "-" + row + "-" + bay + "-" + CvtTier2Idx(item.Tier).ToString() + "(SC BLOCK)";
                                }
                            }
                        }
                        else if (this.BlockItemList.GetBlock(item.Block).Facility.Equals(CTBizConstant.BlockType.FL))
                        {
                            if (string.IsNullOrEmpty(row))
                            {
                                item.YardPosition = item.Block;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(item.Tier))
                                {
                                    item.YardPosition = item.Block + "-" + bay + "-" + row;
                                }
                                else
                                {
                                    item.YardPosition = item.Block + "-" + bay + "-" + row + "-" + CvtTier2Idx(item.Tier).ToString() + "(FL BLOCK)";
                                }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(bay))
                            {
                                item.YardPosition = item.Block;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(item.Tier))
                                {
                                    item.YardPosition = item.Block + "-" + bay + "-" + row;
                                }
                                else
                                {
                                    item.YardPosition = item.Block + "-" + bay + "-" + row + "-" + CvtTier2Idx(item.Tier).ToString() + "(TC BLOCK)";
                                }
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(item.CustomsHold) == false)
                {
                    item.CustomsHold = item.CustomsHold.Equals(CTBizConstant.YesNo.YES) ? item.CustomsHold : string.Empty;
                }

                if (string.IsNullOrEmpty(item.TerminalHold) == false)
                {
                    item.TerminalHold = item.TerminalHold.Equals(CTBizConstant.YesNo.YES) ? item.TerminalHold : string.Empty;
                }

                if (string.IsNullOrEmpty(item.InspectCheck) == false)
                {
                    item.InspectCheck = item.InspectCheck.Equals(CTBizConstant.YesNo.YES) ? item.InspectCheck : string.Empty;
                }

                if (TerminalCode.Equals(TERMINAL_CODE_GWCT) == true)
                {
                    if (string.IsNullOrEmpty(item.DamageCondition) == false)
                    {
                        switch (item.DamageCondition)
                        {
                            case CTBizConstant.DamageCondition.SOUND:
                                item.DamageCondition = "양호";
                                break;

                            case CTBizConstant.DamageCondition.HEAVY:
                            case CTBizConstant.DamageCondition.MINOR:
                            case CTBizConstant.DamageCondition.NEED_TO_RE_CHECK:
                            case CTBizConstant.DamageCondition.TOTAL_LOSS:
                                item.DamageCondition = "손상";
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        item.DamageCondition = "양호";
                    }

                    // added by YoungOk Kim (2019.10.16) - Mantis 92189: [Tally] 컨테이너 정보에서 2 time shifting 표시
                    item.DisShipPosition = item.ShipPosition; // default
                    if (string.IsNullOrEmpty(item.JobCode) == false)
                    {
                        if (item.JobCode.Equals(CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_DISCHARGING) == true)
                        {
                            MobileContainerDetailParam param = new MobileContainerDetailParam(this);
                            param.ContainerNo = item.CntrNo;
                            param.VesselCode = item.VslCd;
                            param.CallYear = item.CallYear;
                            param.CallSequence = item.CallSeq;
                            param.QJobType = CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_LOADING;

                            var shipPosition = _mobileContainerDetailService.GetShipPosition(param).ResultObject as string;
                            if (string.IsNullOrEmpty(shipPosition) == false)
                            {
                                item.LoadShipPosition = shipPosition;
                            }
                        }
                        else if (item.JobCode.Equals(CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_LOADING) == true)
                        {
                            item.LoadShipPosition = item.ShipPosition;

                            MobileContainerDetailParam param = new MobileContainerDetailParam(this);
                            param.ContainerNo = item.CntrNo;
                            param.VesselCode = item.VslCd;
                            param.CallYear = item.CallYear;
                            param.CallSequence = item.CallSeq;
                            param.QJobType = CTBizConstant.QuayJobType.TWO_TIME_SHIFTING_DISCHARGING;

                            var shipPosition = _mobileContainerDetailService.GetShipPosition(param).ResultObject as string;
                            if (string.IsNullOrEmpty(shipPosition) == false)
                            {
                                item.DisShipPosition = shipPosition;
                            }
                        }
                        else
                        {
                            // nothing
                        }
                    }
                }

                // added by YoungOk Kim (2020.04.08) - Mantis 105287: Show Special Color for Reservation Contaioners: TM /YQ/C3IT part
                if (string.IsNullOrEmpty(item.PuPlanDate2) == false)
                {
                    var pickupDate = DateTimeUtil.ToDateTimeFromyyyyMMddHHmmss(item.PuPlanDate2);
                    if (pickupDate != null)
                    {
                        item.PuPlanDate2 = DateTimeUtil.ToStringyyyyMMddWithSeparator((DateTime)pickupDate, "-");
                    }
                }

                // added by JH.Tak (2022.11.04)
                if (string.IsNullOrEmpty(item.TimeSlotDate2) == false)
                {
                    var timeSlotDate = DateTimeUtil.ToDateTimeFromyyyyMMddHHmmss(item.TimeSlotDate2);
                    if (timeSlotDate != null)
                    {
                        item.TimeSlotDate2 = DateTimeUtil.ToStringyyyyMMddWithSeparator((DateTime)timeSlotDate, "-");
                    }
                }

                // added by YoungOk Kim (2020.04.08) - Mantis 105287: Show Special Color for Reservation Contaioners: TM /YQ/C3IT part
                if (string.IsNullOrEmpty(item.TimeSlotNo2) == false) 
                {
                    var timeSlotList = CodeManager.GetCodes<CodeGeneralItem>(CTBizConstant.CodeType.TIME_SLOT);
                    if (timeSlotList != null && timeSlotList.Any())
                    {
                        var timeSlotItem = timeSlotList.Where(p => p.Code.Equals(item.TimeSlotNo2)).FirstOrDefault();
                        if (timeSlotItem != null)
                        {
                            item.TimeSlotNo2 = timeSlotItem.CodeName;
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
        }

        public int CvtTier2Idx(string cData)
        {
            int tierIdx = 0;
            try
            {
                switch (cData)
                {
                    case "+":
                        tierIdx = -1;
                        break;
                    case "-":
                        tierIdx = -2;
                        break;
                    case "*":
                        tierIdx = -3;
                        break;
                    case "/":
                        tierIdx = -4;
                        break;
                    default:
                        tierIdx = string.IsNullOrEmpty(cData) ? 0 : Convert.ToInt32(cData);
                        break;
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
            return tierIdx;
        }

        public void ShowUpdateSealView() // added by SH.Nam (2018.06.08) WHL Gap ID : CR-K-031
        {
            try
            {
                if (this._detailItem != null)
                {
                    UpdateSealView updateSealView = new UpdateSealView();
                    updateSealView.StartPosition = FormStartPosition.CenterScreen;
                    updateSealView.UseDisableSealNo3 = UseDisableSealNo3;
                    updateSealView.VisibleSealChk = VisibleSealChk; // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT

                    var controller = updateSealView.Controller as UpdateSealController;
                    if (controller != null)
                    {
                        UpdateSealItem updateSealItem = new UpdateSealItem() 
                        {
                            EquipmentNo = SelectedEquipmentNo,
                            CntrNo = _detailItem.CntrNo,
                            JobCode = _detailItem.JobCode,
                            VslCd = _detailItem.VslCd,
                            CallYear = _detailItem.CallYear,
                            CallSeq = _detailItem.CallSeq,
                            SealNo1 = _detailItem.SealNo1,
                            SealNo2 = _detailItem.SealNo2,
                            SealChk = _detailItem.SealChk // added by JH.Tak (2021.10.25) 0118003: Adding Seal Presence function to Tally VMT
                        };
                        controller.UpdateSealItem = updateSealItem;
                        DialogResult result = updateSealView.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            DoRetrieveData();
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
        }

        private CommonItemListItem ShowItemListView(BaseItemsList<CommonItemListItem> itemList, string viewTitle)
        {
            CommonItemListItem selectedItem = null;
            try
            {
                var commonItemListView = MobileMenuController.GetInstance().MenuClick("CommonItemListView") as CommonItemListView;
                if (commonItemListView == null)
                {
                    ErrorMessageHandler.WriteLog4Debug("ShowItemListView() : cannot find CommonItemListView.");
                    return null;
                }
                commonItemListView.StartPosition = FormStartPosition.CenterScreen;

                var controller = commonItemListView.Controller as CommonItemListController;
                if (controller != null)
                {
                    if (itemList != null && itemList.Count > 0)
                    {
                        controller.ItemList = itemList;
                        controller.ViewTitle = viewTitle;
                        controller.SelectedItem = null;

                        DialogResult result = commonItemListView.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            selectedItem = controller.SelectedItem;
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

            return selectedItem;
        }

        #endregion METHOD AREA ************************************************
    }
}
