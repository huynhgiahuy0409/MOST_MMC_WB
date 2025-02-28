using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tsb.Fontos.Core.Configuration.Common;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Observer;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Security.Audit;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Win.Grid.Spread;
using Tsb.Fontos.Win.Message;
using Tsb.Fontos.Win.PreviewReport;
using Tsb.Product.WB.Client.ReportHandler;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.WeightBridge;
using Tsb.Product.WB.Common.ServiceSpec;
using Tsb.Product.WB.Common.Utils;
using Tsb.Product.WB.Service.WeightBridge;

namespace Tsb.Product.WB.Client.WeightBridge
{
    class MainController : BaseSingleGridController
    {
        #region FIELD/READONLY AREA *********************************
        private string staffCd = "";
        private IMainService service = null;
        private IJobService jobService = null;
        private IWeightBridgeService wbService = null;
        /// <summary>
        /// Detail View's name.
        /// </summary>
        private readonly string DETAIL_VIEW_NAME = "SingleGridDetailView";
        /// <summary>
        /// Name of the synchronization notified.
        /// </summary>
        public static readonly string SYNC_NOTIFY_NAME = "SYNC-FT-FTSL-FTE-SingleGrid";
        #endregion

        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets or sets FormView.
        /// </summary>
        public MainSingleGridInterface FormView { get; set; }
        /// <summary>
        /// Gets or sets FormView.
        /// </summary>
        /// <summary>
        /// Gets or sets DetailView.
        /// </summary>
        //public SingleGridDetailInterface DetailView { get; set; }
        #endregion
        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Initializes a new instance of the SingleGridController class.
        /// </summary>
        public MainController()
            : base()
        {
            this.ObjectID = "CTL-FT-FTSL-FTE-MainController";
        }

        /// <summary>
        /// Initializes a new instance using the specified view.
        /// </summary>
        /// <param name="view">The suggested BookingListInterface of this instance.</param>
        public MainController(MainSingleGridInterface view)
            : base(view)
        {
            this.ObjectID = "CTL-FT-FTSL-FTE-MainController";
            this.FormView = view;
            this.staffCd = AppEnv.UserInfo.StaffCD;
            this.AddEventHandler();
            this.InitDataSyncCallbackInfo();

            // DetailView Setting
            base.DetailFormName = this.DETAIL_VIEW_NAME;
        }
        #endregion
        #region BaseController Implements AREA **********************
        /// <summary>
        /// Inits Data Synchronization Callback Information
        /// </summary>
        public override void InitDataSyncCallbackInfo()
        {
            //this.DataSyncAgent.AddCallbackInfo(SingleGridDetailController.SYNC_NOTIFY_NAME, this.SingleGridView_DataSyncNotified);
        }

        /// <summary>
        /// Data Synchronization Notified Callback Method
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Object of Event Arguments class that contains event data</param>
        private void SingleGridView_DataSyncNotified(object sender, DataSyncNotifiedEventArgs e)
        {
            try
            {
                MainSingleGridItem item = e.TargetDataToSync as MainSingleGridItem;

                if ((e.AdditionInfo != null) &&
                    (e.AdditionInfo.ToString() == OpCodes.CREATE.ToString()))
                {
                    this.FormView.SourceItemList.Add(item);
                    this.FormView.BindListAsGridDataSource(this.FormView.SourceItemList, true, true);
                }

                this.FormView.Grid.Focus();
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }
        }

        /// <summary>
        /// Adds Event Handleresult
        /// </summary>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public override void AddEventHandler()
        {
            //this.FormView.GridRowRemoved += new SpreadGridRowRemovedHandler(DoDeleteData);
            //base.EnableToolbar = new ToolbarEnableHandler(ToolbarEnable);
        }
        #endregion
        #region BUSINESS HELPER METHOD AREA ****************************
        public void NextValue(WeightInfoItem newItem)
        {
            this.FormView.CurWeightInfoItem = newItem;
            if (newItem != null)
            {
                newItem.MakeBackupItem();
                this.FormView.WeightInfoItem = newItem;
            }
            else
            {
                this.FormView.WeightInfoItem = new WeightInfoItem();
            }
        }
        public void ClearWeightinfo()
        {
            this.FormView.WeightInfoItem = new WeightInfoItem();
            this.FormView.CurWeightInfoItem = null;
        }
        public BaseResult DoRetrieveWeightInfo()
        {
            /* Madatory Search Textbox*/
            if (!this.FormView.SearchMandatoryCheck()) return null;
            BaseResult baseResult = null;
            WeightInfoItem resultItem;
            MainParam mainParam;
            try
            {
                mainParam = this.FormView.MainParam;
                mainParam.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_MAIN_SERVICE;
                this.service = BizServiceLocator.GetService<IMainService>(mainParam);
                /* Excute */
                baseResult = service.InquiryWeightInfo(mainParam);
                BaseItemsList<WeightInfoItem> items = baseResult.ResultObject as BaseItemsList<WeightInfoItem>;
                if (items.Count > 0)
                {
                    resultItem = items.GetItem(0) as WeightInfoItem;
                    MessageManager.Show("MSG_PTWB_Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    /* Store */
                    this.NextValue(resultItem);
                    this.FormView.CurWeightInfoCheck();
                }
                else
                {
                    MessageManager.Show("MSG_PTWB_NoRecordForSearch", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.ClearWeightinfo();
                }
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return baseResult;
        }
        public bool DoUpdateRemark()
        {
            if (!this.FormView.CurWeightInfoCheck())
            {
                return false;
            };
            bool isSuccess = false;
            try
            {
                MainParam param = new MainParam(this, ProductWBServiceSpec.WEIGHTBRIDGE_MAIN_SERVICE);
                param.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_MAIN_SERVICE;
                this.service = BizServiceLocator.GetService<IMainService>(param);
                /* Excute */
                if ((this.FormView.WeightInfoItem.OpCode != Tsb.Fontos.Core.Configuration.Common.OpCodes.READ))
                {
                    this.FormView.WeightInfoItem.StaffCd = this.staffCd;
                    this.service.UpdateRemarkWeightBridge(this.FormView.WeightInfoItem);
                    MessageManager.Show("MSG_PTWB_Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                isSuccess = true;
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return isSuccess;
        }
        public void DoConfirmWeight()
        {
            /* Madatory Search & Weight Textbox*/
            if (!this.FormView.CurWeightInfoCheck()) return;
            if (!this.FormView.WeightMandatoryCheck()) return;
            try
            {
                BaseResult jobResult = null;
                JobParam jobParam = null;
                WeightBridgeItem wbItem = null;
                WeightInfoItem curItem = this.FormView.WeightInfoItem;
                WeightBridgeParam weightBridgeParam = this.FormView.WeightBridgeParam;
                weightBridgeParam.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_WB_SERVICE;
                if (curItem != null)
                {
                    bool isFirstWeight = curItem.FirstWgt == null;
                    bool isInternal = !String.IsNullOrEmpty(curItem.TruckMode) && curItem.TruckMode.Equals("I") ? true : false;
                    bool isEmptyFirst = !String.IsNullOrEmpty(curItem.Status) && curItem.Status.Equals("E") ? true : false;
                    bool isFullFirst = !String.IsNullOrEmpty(curItem.Status) && curItem.Status.Equals("F") ? true : false;
                    bool isImport = !String.IsNullOrEmpty(curItem.BlNo);
                    bool isExport = !String.IsNullOrEmpty(curItem.ShipgNoteNo);
                    jobParam = new JobParam();
                    jobParam.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_JOB_SERVICE;
                    jobParam.VslCallId = curItem.VslCallId;
                    jobParam.LorryNo = curItem.LorryNo;
                    if (isInternal)
                    {
                        if (isImport) jobParam.BlNo = curItem.BlNo;
                        else jobParam.SnNo = curItem.ShipgNoteNo;
                    }
                    else
                    {
                        jobParam.GateTxnNo = curItem.GateTxnNo;
                        if (isImport)
                        {
                            jobParam.SdoNo = curItem.SdoNo;
                            jobParam.BlNo = curItem.BlNo;
                        }
                        if (isExport)
                        {
                            jobParam.GrNo = curItem.GrNo;
                        }
                    }
                    /* Get Jobs to check Guard*/
                    this.wbService = BizServiceLocator.GetService<IWeightBridgeService>(weightBridgeParam);
                    this.jobService = BizServiceLocator.GetService<IJobService>(jobParam);
                    jobResult = jobService.InquiryJob(jobParam);
                    IList<JobItem> jobList = jobResult.ResultObject as BaseItemsList<JobItem>;
                    IEnumerator<JobItem> jobEnumerator = jobList.GetEnumerator();
                    bool isGateIn = isInternal ? true : false;
                    bool isOperation = false;
                    bool isInternalDischarge = false;
                    bool isInternalLoading = false;
                    bool isInternalWHCheckImport = false;
                    bool isIntenralWHCheckExport = false;
                    bool isDischargeImpDirect = false;
                    bool isLoadingExportDirect = false;
                    bool isHdlImportIndirect = false;
                    bool isHdlExportIndirect = false;
                    /* Check operation */
                    while (jobEnumerator.MoveNext())
                    {
                        JobItem item = jobEnumerator.Current;
                        string jobTpCd = item.JobTpCd;
                        string jobPureCd = item.JobPurpCd;
                        if (jobTpCd == "DS" && jobPureCd == "VG")
                        {
                            isDischargeImpDirect = true;
                        }
                        else if (jobTpCd == "LD" && jobPureCd == "GV")
                        {
                            isLoadingExportDirect = true;
                        }
                        else if (jobTpCd == "LO" && jobPureCd == "WG")
                        {
                            isHdlImportIndirect = true;
                        }
                        else if ((jobTpCd == "LF" && jobPureCd == "GW"))
                        {
                            isHdlExportIndirect = true;
                        }
                        else if (jobTpCd == "DS" && jobPureCd == "VA")
                        {
                            isInternalDischarge = true;
                        }
                        else if (jobTpCd == "LD" && jobPureCd == "AV")
                        {
                            isInternalLoading = true;
                        }
                        else if (jobTpCd == "DS" && jobPureCd == "AW")
                        {
                            isInternalWHCheckImport = true;
                        }
                        else if ((jobTpCd == "LD" && jobPureCd == "WA"))
                        {
                            isIntenralWHCheckExport = true;
                        }
                        else if (item.JobTpCd == "GI" && item.JobPurpCd == "OI")
                        {
                            isGateIn = true;
                        }
                    }
                    bool isDirectOperation = false;
                    bool isImportInternalOperation = false;
                    bool isExportInternalOperation = false;
                    bool isIndirectOperation = false;
                    string status = curItem.Status;
                    if (isFirstWeight)
                    {
                        if (isInternal)
                        {
                            if((!isInternalDischarge && !isInternalWHCheckImport && isImport) || (!isIntenralWHCheckExport && !isInternalLoading && isExport))
                            {
                                status = "E";
                            }else if((isInternalDischarge && !isInternalWHCheckImport && isImport) || (isIntenralWHCheckExport && !isInternalLoading && isExport))
                            {
                                status = "F";
                            }
                        }
                        else
                        {                           
                            if ((!isDischargeImpDirect || !isHdlImportIndirect) && isImport)
                            {
                                status = "E";
                            }
                            if ((!isLoadingExportDirect || !isHdlExportIndirect) && isExport)
                            {
                                status = "F";
                            }
                            //status = (!isDischargeImpDirect || !isHdlImportIndirect) ? "E" : (!isLoadingExportDirect || !isHdlExportIndirect) ? "F" : status;
                        }
                    }
                    else
                    {
                        if (isInternal)
                        {
                            if (isInternalDischarge && !isInternalWHCheckImport && isImport)
                            {
                                if (status.Equals("E"))
                                {
                                    isImportInternalOperation = true;
                                }
                                else if (status.Equals("F"))
                                {
                                    MessageManager.Show("MSG_PTWB_ValidSecondWeight", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else if (isInternalDischarge && isInternalWHCheckImport && isImport)
                            {
                                if (status.Equals("F"))
                                {
                                    isImportInternalOperation = true;
                                }
                                else if (status.Equals("E"))
                                {
                                    MessageManager.Show("MSG_PTWB_PleaseWeightBeforeAW", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            if (isIntenralWHCheckExport && !isInternalLoading && isExport)
                            {
                                if (status.Equals("E"))
                                {
                                    isExportInternalOperation = true;
                                }
                                else if (status.Equals("F"))
                                {
                                    MessageManager.Show("MSG_PTWB_ValidSecondWeight", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            else if (isIntenralWHCheckExport && isInternalLoading && isExport)
                            {
                                if (status.Equals("F"))
                                {
                                    isExportInternalOperation = true;
                                }
                                else if (status.Equals("E"))
                                {
                                    MessageManager.Show("MSG_PTWB_PleaseWeightBeforeLD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            isDirectOperation = isDischargeImpDirect || isLoadingExportDirect;
                            isIndirectOperation = isHdlImportIndirect || isHdlExportIndirect;
                        }
                    }
                    if (isDirectOperation || isIndirectOperation || isImportInternalOperation || isExportInternalOperation) isOperation = true;
                    /* Confirm Weight Guard */
                    if (!isGateIn && !isOperation)
                    {
                        MessageManager.Show("MSG_PTWB_NotGateIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    };

                    /* Process data*/
                    String staffCd = this.FormView.StaffCd;
                    string blNo = curItem.BlNo;
                    string doNo = curItem.DoNo;
                    string sdoNo = curItem.SdoNo;
                    string snNo = curItem.ShipgNoteNo;
                    string grNo = curItem.GrNo;
                    decimal? readWeight = curItem.KG;
                    string txnNo = curItem.TransactionNo;
                    bool isExistTxnNo = !String.IsNullOrEmpty(txnNo);
                    bool isFirstWgt = !isExistTxnNo && isGateIn && !isOperation;
                    bool isSecondWgt = isExistTxnNo && isGateIn && isOperation;
                    this.wbService = BizServiceLocator.GetService<IWeightBridgeService>(weightBridgeParam);
                    if (isFirstWgt)
                    {
                        /* Get new transaction*/
                        txnNo = this.wbService.GetNewTransactionNo(weightBridgeParam);
                        wbItem = new WeightBridgeItem();
                        wbItem.LockNotifyPropertyChanged = false;
                        wbItem.OpCode = OpCodes.CREATE;
                        wbItem.Status = status;
                        wbItem.TransactionNo = txnNo;
                        wbItem.TransactionDt = DateTime.Now;
                        wbItem.UpdateTime = DateTime.Now;
                        wbItem.FirstWgtDt = DateTime.Now;
                        wbItem.StaffCd = staffCd;
                        wbItem.ShipgNoteNo = snNo;
                        wbItem.GrNo = grNo;
                        wbItem.BlNo = blNo;
                        wbItem.DoNo = doNo;
                        wbItem.SdoNo = sdoNo;
                        wbItem.GateTicketNo = curItem.GateTxnNo;
                        wbItem.Rmk = curItem.Rmk;
                        wbItem.VslCallId = curItem.VslCallId;
                        wbItem.VslCd = curItem.VslCd;
                        wbItem.CallYear = curItem.CallYear;
                        wbItem.CallSeq = curItem.CallSeq;
                        wbItem.MfDocId = curItem.MfDocId;
                        wbItem.LorryNo = curItem.LorryNo;
                        wbItem.TsptComp = curItem.TsptComp;
                        wbItem.DriverId = curItem.DriverNo;
                        wbItem.ChassisNo = curItem.ChassisNo;
                        wbItem.TrkMode = curItem.TruckMode;
                        wbItem.FirstWgt = readWeight;
                        wbItem.TrkMode = curItem.TruckMode;
                        wbItem.GateinDt = curItem.GateInDt;
                        wbItem.GateinCd = curItem.GateCd;
                        wbItem.GateoutDt = curItem.GateOutDt;
                        wbItem.PkgQty = curItem.PkgQty;
                        wbItem.CgVol = curItem.CgVol;

                        if (isImport)
                        {
                            wbItem.TrkTreWgt = readWeight;
                            wbItem.Cnsne = curItem.BlConsigneeCd;
                            wbItem.Shpr = curItem.BlShipperCd;
                        }
                        else if (isExport)
                        {
                            wbItem.TrkGrsWgt = readWeight;
                            wbItem.Cnsne = curItem.SnConsigneeCd;
                            wbItem.Shpr = curItem.SnShiperCd;
                        }

                        if (!isInternal) wbItem.DelvTpCd = curItem.DelvTpCd;
                        else wbItem.DelvTpCd = "I";

                        if (this.DoProcessConfirmWeight(wbItem, "first"))
                        {
                            //Binding First Weight To View
                            curItem.FirstWgt = WeightBridgeUtils.ConvertWeight(readWeight, 1);
                            curItem.TransactionNo = txnNo;
                            curItem.Status = status;
                        }
                    }
                    else if (isSecondWgt)
                    {
                        decimal? trkNetWgt = 0;
                        if ((isInternal && isFullFirst)|| (!isInternal && isExport))
                        {
                            trkNetWgt = curItem.FirstWgt - readWeight;
                        }
                        else if((isInternal && isEmptyFirst) || (!isInternal && isImport))
                        {
                            trkNetWgt = readWeight - curItem.FirstWgt;
                        }
                        if (trkNetWgt <= 0)
                        {
                            MessageManager.Show("MSG_PTWB_ReadWeightIsNotValid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        weightBridgeParam.TransactionNo = curItem.TransactionNo;
                        BaseItemsList<WeightBridgeItem> baseResult = this.wbService.InquiryWeightBridge(weightBridgeParam).ResultObject as BaseItemsList<WeightBridgeItem>;
                        wbItem = baseResult.GetItem(0) as WeightBridgeItem;
                        if (wbItem != null)
                        {
                            wbItem.LockNotifyPropertyChanged = false;
                            wbItem.Status = "Y";
                            wbItem.SecondWgt = readWeight;
                            wbItem.SecondWgtDt = DateTime.Now;
                            wbItem.UpdateTime = DateTime.Now;
                            wbItem.StaffCd = staffCd;
                            wbItem.Rmk = curItem.Rmk;
                            wbItem.TrkNetWgt = trkNetWgt;

                            if (isImport)
                            {
                                wbItem.TrkGrsWgt = readWeight;
                                wbItem.Cnsne = curItem.BlConsigneeCd;
                                wbItem.Shpr = curItem.BlShipperCd;
                            }
                            else if (isExport)
                            {
                                wbItem.TrkTreWgt = readWeight;
                                wbItem.Cnsne = curItem.SnConsigneeCd;
                                wbItem.Shpr = curItem.SnShiperCd;
                            }

                            if (wbItem.TrkMode.Equals("E")) wbItem.DelvTpCd = isDirectOperation ? "D" : isIndirectOperation ? "I" : "";
                            else wbItem.DelvTpCd = "I";

                            if (this.DoProcessConfirmWeight(wbItem, "second"))
                            {
                                //Binding First Weight To View
                                curItem.TransactionNo = txnNo;
                                curItem.SecondWgt = WeightBridgeUtils.ConvertWeight(readWeight, 1);
                                curItem.CargoWeight = WeightBridgeUtils.ConvertWeight(trkNetWgt, 1); ;
                            }
                        }
                    }
                    else if (isExistTxnNo && !isFirstWeight && !isOperation)
                    {
                        /* Show message when operation hasn't been taken */
                        MessageManager.Show("MSG_PTWB_ValidSecondWeight", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return;
        }
        public BaseResult DoRetrieveGridData()
        {
            BaseResult result = null;
            try
            {
                MainParam param = this.FormView.MainGridParam;
                if (param.WeightFrom == null)
                {
                    param.WeightFrom = DateTime.Now;
                }
                if (param.WeightTo == null)
                {
                    param.WeightTo = DateTime.Now;
                }
                param.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_MAIN_SERVICE;

                this.service = BizServiceLocator.GetService<IMainService>(param);
                result = this.service.InquiryWeightInfo(param);
                BaseItemsList<WeightInfoItem> resultItems = result.ResultObject as BaseItemsList<WeightInfoItem>;

                this.FormView.BindListAsGridDataSource(resultItems, true, false, true);
                (this.FormView.Grid as TSpreadGrid).ActiveSheet.RowHeader.Rows.Default.Height = 40;

            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return result;
        }
        /// <summary>
        /// Notify SyncAgent.
        /// </summary>
        public void NotifySyncAgent()
        {
            //if (this.DetailView != null && this.DetailView.IsDisposed == false)
            //{
            //    object targetSyncData = this.FormView.SourceItemList[this.FormView.ActiveRowDataIndex];
            //    this.DataSyncAgent.NotifyToSync(SingleGridController.SYNC_NOTIFY_NAME, targetSyncData);
            //}
            //return;
        }
        public bool DoUpdatePrintCount(WeightInfoItem weightInfoItem)
        {
            bool isSuccess = false;
            try
            {
                MainParam param = new MainParam(this, ProductWBServiceSpec.WEIGHTBRIDGE_MAIN_SERVICE);
                param.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_MAIN_SERVICE;
                this.service = BizServiceLocator.GetService<IMainService>(param);

                this.FormView.WeightInfoItem.StaffCd = this.staffCd;

                if (weightInfoItem.PrintCir != null)
                {
                    if (weightInfoItem.PrintCir.Equals("No"))
                    {
                        weightInfoItem.PrintCir = "Yes";
                    }

                    this.service.UpdatePrintCountWeightBridge(weightInfoItem);
                }
                //MessageManager.Show("MSG_PTWB_Success", MessageBoxButtons.OK, MessageBoxIcon.None);

                isSuccess = true;
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return isSuccess;
        }
        public void setLorryValue(WeightInfoItem weightInfoItem)
        {
            try
            {
                this.NextValue(weightInfoItem);
                this.FormView.CurWeightInfoCheck();
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }
        }
        public void DoCancelJob()
        {
            DialogResult dialogResult = MessageManager.Show("MSG_PTWB_OKCancelTransaction", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialogResult.ToString().Equals("OK"))
            {
                if (!this.FormView.CurWeightInfoCheck()) return;
                try
                {
                    BaseResult jobResult = null;
                    JobParam jobParam = null;
                    WeightInfoItem curItem = this.FormView.WeightInfoItem;
                    WeightBridgeParam weightBridgeParam = this.FormView.WeightBridgeParam;
                    weightBridgeParam.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_WB_SERVICE;
                    if ((curItem.Status != null) &&  (curItem.Status.Equals("C") ))
                    {
                        MessageManager.Show("MSG_PTWB_Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (curItem.SecondWgt != null)
                    {
                        MessageManager.Show("MSG_PTWB_RecoredSecondWeight", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (curItem != null)
                    {
                        bool isFirstWeight = curItem.FirstWgt == null;
                        bool isInternal = !String.IsNullOrEmpty(curItem.TruckMode) && curItem.TruckMode.Equals("I") ? true : false;
                        bool isEmptyFirst = !String.IsNullOrEmpty(curItem.Status) && curItem.Status.Equals("E") ? true : false;
                        bool isFullFirst = !String.IsNullOrEmpty(curItem.Status) && curItem.Status.Equals("F") ? true : false;
                        bool isImport = !String.IsNullOrEmpty(curItem.BlNo);
                        bool isExport = !String.IsNullOrEmpty(curItem.ShipgNoteNo);
                        jobParam = new JobParam();
                        jobParam.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_JOB_SERVICE;
                        jobParam.VslCallId = curItem.VslCallId;
                        jobParam.LorryNo = curItem.LorryNo;
                        if (isInternal)
                        {
                            if (isImport) jobParam.BlNo = curItem.BlNo;
                            else jobParam.SnNo = curItem.ShipgNoteNo;
                        }
                        else
                        {
                            jobParam.GateTxnNo = curItem.GateTxnNo;
                            if (isImport)
                            {
                                jobParam.SdoNo = curItem.SdoNo;
                                jobParam.BlNo = curItem.BlNo;
                            }
                            if (isExport)
                            {
                                jobParam.GrNo = curItem.GrNo;
                            }
                        }


                        /* Get Jobs to check Guard*/
                        this.wbService = BizServiceLocator.GetService<IWeightBridgeService>(weightBridgeParam);
                        this.jobService = BizServiceLocator.GetService<IJobService>(jobParam);
                        jobResult = jobService.InquiryJob(jobParam);
                        IList<JobItem> jobList = jobResult.ResultObject as BaseItemsList<JobItem>;
                        IEnumerator<JobItem> jobEnumerator = jobList.GetEnumerator();
                        bool isGateIn = isInternal ? true : false;
                        bool isOperation = false;
                        bool isInternalDischarge = false;
                        bool isInternalLoading = false;
                        bool isInternalWHCheckImport = false;
                        bool isIntenralWHCheckExport = false;
                        bool isDischargeImpDirect = false;
                        bool isLoadingExportDirect = false;
                        bool isHdlImportIndirect = false;
                        bool isHdlExportIndirect = false;


                        /* Check operation */
                        while (jobEnumerator.MoveNext())
                        {
                            JobItem item = jobEnumerator.Current;
                            string jobTpCd = item.JobTpCd;
                            string jobPureCd = item.JobPurpCd;
                            if (jobTpCd == "DS" && jobPureCd == "VG")
                            {
                                isDischargeImpDirect = true;
                            }
                            else if (jobTpCd == "LD" && jobPureCd == "GV")
                            {
                                isLoadingExportDirect = true;
                            }
                            else if (jobTpCd == "LO" && jobPureCd == "WG")
                            {
                                isHdlImportIndirect = true;
                            }
                            else if ((jobTpCd == "LF" && jobPureCd == "GW"))
                            {
                                isHdlExportIndirect = true;
                            }
                            else if (jobTpCd == "DS" && jobPureCd == "VA")
                            {
                                isInternalDischarge = true;
                            }
                            else if (jobTpCd == "LD" && jobPureCd == "AV")
                            {
                                isInternalLoading = true;
                            }
                            else if (jobTpCd == "DS" && jobPureCd == "AW")
                            {
                                isInternalWHCheckImport = true;
                            }
                            else if ((jobTpCd == "LD" && jobPureCd == "WA"))
                            {
                                isIntenralWHCheckExport = true;
                            }
                            else if (item.JobTpCd == "GI" && item.JobPurpCd == "OI")
                            {
                                isGateIn = true;
                            }
                        }
                        bool isDirectOperation = false;
                        bool isImportInternalOperation = false;
                        bool isExportInternalOperation = false;
                        bool isIndirectOperation = false;
                        string status = curItem.Status;
                        if (isFirstWeight)
                        {
                            
                        }
                        else
                        {
                            if (isInternal)
                            {
                                if (isInternalDischarge && !isInternalWHCheckImport && isImport)
                                {
                                    if (status.Equals("E"))
                                    {
                                        isImportInternalOperation = true;
                                    }
                                    else if (status.Equals("F"))
                                    {
                                        MessageManager.Show("MSG_PTWB_ValidSecondWeight", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                else if (isInternalDischarge && isInternalWHCheckImport && isImport)
                                {
                                    if (status.Equals("F"))
                                    {
                                        isImportInternalOperation = true;
                                    }
                                    else if (status.Equals("E"))
                                    {
                                        MessageManager.Show("MSG_PTWB_PleaseWeightBeforeAW", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }

                                if (isIntenralWHCheckExport && !isInternalLoading && isExport)
                                {
                                    if (status.Equals("E"))
                                    {
                                        isExportInternalOperation = true;
                                    }
                                    else if (status.Equals("F"))
                                    {
                                        MessageManager.Show("MSG_PTWB_ValidSecondWeight", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                else if (isIntenralWHCheckExport && isInternalLoading && isExport)
                                {
                                    if (status.Equals("F"))
                                    {
                                        isExportInternalOperation = true;
                                    }
                                    else if (status.Equals("E"))
                                    {
                                        MessageManager.Show("MSG_PTWB_PleaseWeightBeforeLD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                isDirectOperation = isDischargeImpDirect || isLoadingExportDirect;
                                isIndirectOperation = isHdlImportIndirect || isHdlExportIndirect;
                            }
                        }
                        if (isDirectOperation || isIndirectOperation || isImportInternalOperation || isExportInternalOperation) isOperation = true;



                        /* Confirm Weight Guard */
                        if (!isGateIn && !isOperation)
                        {
                            MessageManager.Show("MSG_PTWB_NotGateIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        };

                        /* Process data*/
                        String staffCd = this.FormView.StaffCd;
                        string blNo = curItem.BlNo;
                        string doNo = curItem.DoNo;
                        string sdoNo = curItem.SdoNo;
                        string snNo = curItem.ShipgNoteNo;
                        string grNo = curItem.GrNo;
                        decimal? readWeight = curItem.KG;
                        string txnNo = curItem.TransactionNo;
                        bool isExistTxnNo = !String.IsNullOrEmpty(txnNo);
                        bool isFirstWgt = !isExistTxnNo && isGateIn && !isOperation;
                        bool isSecondWgt = isExistTxnNo && isGateIn && isOperation;
                        this.wbService = BizServiceLocator.GetService<IWeightBridgeService>(weightBridgeParam);



                        bool isCancelJob = isExistTxnNo && (curItem.FirstWgt != null) && isGateIn && !isOperation;


                        MainParam param = new MainParam(this, ProductWBServiceSpec.WEIGHTBRIDGE_MAIN_SERVICE);
                        param.TransactionInfo.TxServiceID = ProductWBServiceSpec.WEIGHTBRIDGE_MAIN_SERVICE;
                        this.service = BizServiceLocator.GetService<IMainService>(param);

                        this.FormView.WeightInfoItem.StaffCd = this.staffCd;
                        if (isCancelJob)
                        {
                            curItem.LockNotifyPropertyChanged = false;
                            curItem.Status = "C";
                            service.upadteStatusCanCelJobWeightBridge(curItem);
                            MessageManager.Show("MSG_PTWB_Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                            this.NextValue(null);
                            IList<WeightInfoItem> datasource = this.FormView.Grid.DataSource as BaseItemsList<WeightInfoItem>;
                            if (datasource.Count > 0)
                            {
                                this.DoRetrieveGridData();
                            }
                        }
                        else
                        {
                            MessageManager.Show("MSG_PTWB_Operating", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                }
                catch (TsbBaseException tsbEx)
                {
                    MessageManager.Show(tsbEx);
                }
                catch (Exception ex)
                {
                    MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
                }
            }

        }
        #endregion
        #region METHOD( REPORT) AREA **************************
        private bool DoProcessConfirmWeight(WeightBridgeItem item, string weightTime)
        {
            DialogResult warnResult = MessageManager.Show("MSG_PTWB_WeightConfirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, DefaultMessage.NON_REG_WRD + weightTime);
            if (warnResult == DialogResult.Yes)
            {
                wbService.ProcessWeightBridge(item);
                return true;
            }
            return false;
        }
        public void DoPreviewReportForTest()
        {
            ReportSet reportSet = new ReportSet();
            BaseItemsList<ReportParam> CIRParams = new BaseItemsList<ReportParam>();
            CIRParams.Add(new ReportParam("TicketNumber_1", "TicketNumber_1", "0010040"));
            CIRParams.Add(new ReportParam("ValidDate_2", "ValidDate_2", "19-06-2024 16:16:16"));
            CIRParams.Add(new ReportParam("OrderNumber_3", "OrderNumber_3", "0010040"));
            CIRParams.Add(new ReportParam("DeliveryMode_4", "DeliveryMode_4", "direct"));
            CIRParams.Add(new ReportParam("Customer_5", "Customer_5", "Mr. Adubasa"));
            CIRParams.Add(new ReportParam("SN_BL_6", "SN_BL_6", "SN123456789123456789"));
            CIRParams.Add(new ReportParam("Voyage_7", "Voyage_7", "Voy123456789"));
            CIRParams.Add(new ReportParam("HandlingTime_8", "HandlingTime_8", "19-06-2024 16:16:16"));
            CIRParams.Add(new ReportParam("POL_POD_9", "POL_POD_9", "AABL"));
            CIRParams.Add(new ReportParam("Commodity_10", "Commodity_10", "General Cargo"));
            CIRParams.Add(new ReportParam("CargoMarks_11", "CargoMarks_11", "123456789"));
            CIRParams.Add(new ReportParam("Quantity_12", "Quantity_12", "100"));
            CIRParams.Add(new ReportParam("GrossWeight_13", "GrossWeight_13", "1000"));
            CIRParams.Add(new ReportParam("Remarks_14", "Remarks_14", "hàng bã bắp"));
            CIRParams.Add(new ReportParam("PackageType_15", "PackageType_15", "package"));
            CIRParams.Add(new ReportParam("Measure_16", "Measure_16", "kg"));
            CIRParams.Add(new ReportParam("TruckPlateNumber_17", "TruckPlateNumber_17", "PLATE123456"));
            CIRParams.Add(new ReportParam("ChassisPlateNumber_18", "ChassisPlateNumber_18", "CHS123456789"));
            CIRParams.Add(new ReportParam("GateInTime_19", "GateInTime_19", "19-06-2024 16:16:16"));
            CIRParams.Add(new ReportParam("FirstWeight_20", "FirstWeight_20", "15000"));
            CIRParams.Add(new ReportParam("SecondWeight_21", "SecondWeight_21", "16000"));
            CIRParams.Add(new ReportParam("GateOutTime_22", "GateOutTime_22", "19-06-2024 16:16:16"));
            CIRParams.Add(new ReportParam("GateController_23", "GateController_23", "Mr. Alaha"));
            CIRParams.Add(new ReportParam("GateSecurity_24", "GateSecurity_24", "Mr. Buala"));
            CIRParams.Add(new ReportParam("Customer_25", "Customer_25", "Mr. Adubasa"));

            ReportItem CIRReport = new ReportItem("CIR_Report", "CIR", CIRParams, new CIRReportHdl());

            reportSet.Reports.Add(CIRReport);

            this.ShowPreviewReport(reportSet);
        }
        public void DoPreviewReport()
        {

            WeightInfoItem weightInfoItem = (WeightInfoItem)SpreadGridUtil.GetSelectedRowDataItemObject((this.FormView.Grid as TSpreadGrid));

            if (weightInfoItem == null)
            {
                MessageManager.Show("MSG_PTWB_NoSelectedRecord", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            if (weightInfoItem.FirstWgt == null || weightInfoItem.FirstWgt == 0)
            {
                MessageManager.Show("MSG_PTWB_NoDataFirstWeight", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                if (weightInfoItem.SecondWgt == null || weightInfoItem.SecondWgt == 0)
                {
                    MessageManager.Show("MSG_PTWB_NoDataSecondWeight", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }


            string cargoWeight = String.Format("{0:#.##}", ((decimal)(weightInfoItem.SecondWgt - weightInfoItem.FirstWgt)));


            ReportSet reportSet = new ReportSet();

            BaseItemsList<ReportParam> CIRParams = new BaseItemsList<ReportParam>();
            CIRParams.Add(new ReportParam("TicketNumber_1", "TicketNumber_1", "0010040"));
            CIRParams.Add(new ReportParam("ValidDate_2", "ValidDate_2", "19-06-2024 16:16:16"));
            CIRParams.Add(new ReportParam("OrderNumber_3", "OrderNumber_3", "0010040"));
            CIRParams.Add(new ReportParam("DeliveryMode_4", "DeliveryMode_4", (weightInfoItem.TruckMode == null) ? "" : weightInfoItem.TruckMode));
            CIRParams.Add(new ReportParam("Customer_5", "Customer_5", "Mr. Adubasa"));
            CIRParams.Add(new ReportParam("SN_BL_6", "SN_BL_6", (weightInfoItem.BlNo == null ? weightInfoItem.ShipgNoteNo : weightInfoItem.BlNo)));
            CIRParams.Add(new ReportParam("Voyage_7", "Voyage_7", (weightInfoItem.VslCd == null) ? "" : weightInfoItem.VslCd));
            CIRParams.Add(new ReportParam("HandlingTime_8", "HandlingTime_8", "19-06-2024 16:16:16"));
            CIRParams.Add(new ReportParam("POL_POD_9", "POL_POD_9", "AABL"));
            CIRParams.Add(new ReportParam("Commodity_10", "Commodity_10", "General Cargo"));
            CIRParams.Add(new ReportParam("CargoMarks_11", "CargoMarks_11", "123456789"));
            CIRParams.Add(new ReportParam("Quantity_12", "Quantity_12", "100"));
            CIRParams.Add(new ReportParam("GrossWeight_13", "GrossWeight_13", (cargoWeight).ToString()));
            CIRParams.Add(new ReportParam("Remarks_14", "Remarks_14", (weightInfoItem.Rmk == null) ? "" : weightInfoItem.Rmk));
            CIRParams.Add(new ReportParam("PackageType_15", "PackageType_15", "package"));
            CIRParams.Add(new ReportParam("Measure_16", "Measure_16", "kg"));
            CIRParams.Add(new ReportParam("TruckPlateNumber_17", "TruckPlateNumber_17", (weightInfoItem.LorryNo == null) ? "" : weightInfoItem.LorryNo));
            CIRParams.Add(new ReportParam("ChassisPlateNumber_18", "ChassisPlateNumber_18", (weightInfoItem.ChassisNo == null) ? "" : weightInfoItem.ChassisNo));
            CIRParams.Add(new ReportParam("GateInTime_19", "GateInTime_19", "19-06-2024 16:16:16"));
            CIRParams.Add(new ReportParam("FirstWeight_20", "FirstWeight_20", (weightInfoItem.FirstWgt == null) ? "" : weightInfoItem.FirstWgt.ToString()));
            CIRParams.Add(new ReportParam("SecondWeight_21", "SecondWeight_21", (weightInfoItem.SecondWgt == null) ? "" : weightInfoItem.SecondWgt.ToString()));
            CIRParams.Add(new ReportParam("GateOutTime_22", "GateOutTime_22", "19-06-2024 16:16:16"));
            CIRParams.Add(new ReportParam("SecondWeight_21", "SecondWeight_21", (weightInfoItem.SecondWgt == null) ? "" : weightInfoItem.SecondWgt.ToString()));
            CIRParams.Add(new ReportParam("GateController_23", "GateController_23", (weightInfoItem.StaffCd == null) ? "" : weightInfoItem.StaffCd));
            CIRParams.Add(new ReportParam("GateSecurity_24", "GateSecurity_24", "Mr. Buala"));
            CIRParams.Add(new ReportParam("Customer_25", "Customer_25", "Mr. Adubasa"));

            ReportItem CIRReport = new ReportItem("CIR_Report", "CIR", CIRParams, new CIRReportHdl());
            reportSet.Reports.Add(CIRReport);

            this.ShowPreviewReport(reportSet);

            DoUpdatePrintCount(weightInfoItem);

        }
        public void DoSelectGridItem(WeightInfoItem sltItem)
        {
            bool isUpdatedItem = this.FormView.CurWeightInfoItem != null && this.FormView.CurWeightInfoItem.OpCode == Fontos.Core.Configuration.Common.OpCodes.UPDATE;
            if (isUpdatedItem)
            {
                DialogResult warnResult = MessageManager.Show("MSG_PTWB_SaveChangesConfirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information, null);
                if (warnResult == DialogResult.Yes)
                {
                    this.NextValue(sltItem);
                }
            }
            else
            {
                this.NextValue(sltItem);
            }
        }
        #endregion
    }
}
