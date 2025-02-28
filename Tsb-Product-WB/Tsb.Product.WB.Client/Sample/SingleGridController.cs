/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2012 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2011.01.25  Tonny.Kim 1.0	First release.
* 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.Codes.Item;
using Tsb.Catos.Cm.Core.Common;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Core.YardDefine.Param;
using Tsb.Catos.Cm.Core.YardDefine.Service;
using Tsb.Catos.Cm.Win.Menu.Toolbar.Attribute;
using Tsb.Fontos.Core.Configuration.Common;
using Tsb.Fontos.Core.Context;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Observer;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Security.Audit;
using Tsb.Fontos.Core.Service;
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Param.Sample;
using Tsb.Product.WB.Common.ServiceSpec;
using Tsb.Fontos.Win.ExceptionReport.Core;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Win.Grid.Event;
using Tsb.Fontos.Win.Grid.Spread;
using Tsb.Fontos.Win.Menu.Action.Command;
using Tsb.Fontos.Win.Menu.Action.Event;
using Tsb.Fontos.Win.Menu.Event;
using Tsb.Fontos.Win.Menu.Toolbar.Attribute;
using Tsb.Fontos.Win.Menu.Toolbar.BizController;
using Tsb.Fontos.Win.Message;
using Tsb.Product.WB.Service.WeightBridge;

namespace Tsb.Product.WB.Client.Sample
{
    public class SingleGridController : BaseSingleGridController
    {
        #region FIELD/READONLY AREA *********************************
        private string staffCd = "";
        private ISingleGridService service = null;

        /// <summary>
        /// Detail View's name.
        /// </summary>
        private readonly string DETAIL_VIEW_NAME = "SingleGridDetailView";

        /// <summary>
        /// Name of the synchronization notified.
        /// </summary>
        public static readonly string SYNC_NOTIFY_NAME = "SYNC-PT-PTWB-SPL-SingleGrid";
        #endregion

        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets or sets FormView.
        /// </summary>
        public SingleGridInterface FormView { get; set; }

        /// <summary>
        /// Gets or sets DetailView.
        /// </summary>
        public SingleGridDetailInterface DetailView { get; set; }
        #endregion

        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Initializes a new instance of the SingleGridController class.
        /// </summary>
        public SingleGridController()
            : base()
        {
            this.ObjectID = "CTL-PT-PTWB-SPL-SingleGridController";
        }

        /// <summary>
        /// Initializes a new instance using the specified view.
        /// </summary>
        /// <param name="view">The suggested BookingListInterface of this instance.</param>
        public SingleGridController(SingleGridInterface view)
            : base(view)
        {
            this.ObjectID = "CTL-PT-PTWB-SPL-SingleGridController";
            this.FormView = view;
            this.staffCd = AuditUtil.GetStaffCd(this.FormView.FormName);
            this.AddEventHandler();
            this.InitDataSyncCallbackInfo();

            // DetailView Setting
            base.DetailFormName = this.DETAIL_VIEW_NAME;
        }
        #endregion

        #region BaseController Implements AREA **********************
        /// <summary>
        /// Inits Data Synchronization Callback Information.
        /// </summary>
        public override void InitDataSyncCallbackInfo()
        {
            this.DataSyncAgent.AddCallbackInfo(SingleGridDetailController.SYNC_NOTIFY_NAME, this.SingleGridView_DataSyncNotified);
        }

        /// <summary>
        /// Data Synchronization Notified Callback Method.
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Object of Event Arguments class that contains event data</param>
        private void SingleGridView_DataSyncNotified(object sender, DataSyncNotifiedEventArgs e)
        {
            try
            {
                SingleGridItem item = e.TargetDataToSync as SingleGridItem;

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
        /// Adds Event Handleresult.
        /// </summary>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public override void AddEventHandler()
        {
            this.FormView.GridRowRemoved += new SpreadGridRowRemovedHandler(DoDeleteData);
            base.EnableToolbar = new ToolbarEnableHandler(ToolbarEnable);
        }
        #endregion

        #region BUSINESS HELPER METHOD AREA ****************************
        /// <summary>
        /// Inquiries items of SingleGrid from service.
        /// </summary>
        public BaseResult DoRetrieveData()
        {
            BaseResult result = null;
            BaseItemsList<SingleGridItem> resultItems = null;

            try
            {
                SingleGridParam param = this.FormView.SearchParam;
                param.TransactionInfo.TxServiceID = ProductWBServiceSpec.SAMPLE_SINGLE_GRID_SERVICE;

                this.service = BizServiceLocator.GetService<ISingleGridService>(param);
                result = service.InquirySingleGrid(this.FormView.SearchParam);
                resultItems = result.ResultObject as BaseItemsList<SingleGridItem>;

                if (resultItems.Count > 0)
                {
                    this.FormView.ActiveItem = resultItems[0];
                }

                // Input for data at SpreadList.
                this.FormView.BindListAsGridDataSource(resultItems, true);
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
        /// Save Items of booking list.
        /// </summary>
        /// <returns>A saved data count</returns>
        public void DoSaveData()
        {
            SingleGridParam param = null;
            param = new SingleGridParam(this, ProductWBServiceSpec.SAMPLE_SINGLE_GRID_SERVICE);

            this.service = BizServiceLocator.GetService<ISingleGridService>(param);
            this.DoMsgHandlingOnScreen = true;

            BaseItemsList<SingleGridItem> singleGridItems = this.FormView.SourceItemList as BaseItemsList<SingleGridItem>;
            bool isSuccess = false;

            if (singleGridItems != null)
            {
                // ProgressBar Code =======================================
                this.ProgressBarManager.BeginManualProgress(singleGridItems.Count);
                //==========================================================
                foreach (SingleGridItem item in singleGridItems)
                {
                    isSuccess = true;

                    try
                    {
                        if (item.OpCode != Tsb.Fontos.Core.Configuration.Common.OpCodes.READ)
                        {
                            item.StaffCd = this.staffCd;
                            this.service.ProcessSingleGrid(item);
                        }
                    }
                    catch (TsbBaseException tsbEx)
                    {
                        isSuccess = false;
                        MessageManager.Show(tsbEx);
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                        MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
                    }

                    //ProgressBar Code==========================================
                    this.ProgressBarManager.PerformStep();
                }

                //ProgressBar Code==========================================
                this.ProgressBarManager.EndProgress();

                if (isSuccess)
                {
                    MessageManager.Show("MSG_CTCM_00001", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            this.FormView.BindListAsGridDataSource(singleGridItems, true);
        }

        /// <summary>
        /// Delete items of Detail from service.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        public void DoDeleteData(object sender, GridRowRemovedEventArgs e)
        {
            IList<int> deleteKeys = new List<int>();
            SingleGridParam param = new SingleGridParam(this, ProductWBServiceSpec.SAMPLE_SINGLE_GRID_SERVICE);
            this.service = BizServiceLocator.GetService<ISingleGridService>(param);

            BaseItemsList<SingleGridItem> singleGridItems = this.FormView.SourceItemList as BaseItemsList<SingleGridItem>;

            if (singleGridItems != null)
            {
                bool isSuccess = false;

                // ProgressBar Code =======================================
                this.ProgressBarManager.BeginManualProgress(singleGridItems.Count);

                for (int i = 0; i < singleGridItems.Count; i++)
                {
                    isSuccess = true;

                    SingleGridItem item = singleGridItems[i];
                    try
                    {
                        if (item.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.DELETE)
                        {
                            item.UserID = this.staffCd;
                            this.service.DeleteSingleGrid(item);
                            deleteKeys.Add(i);
                        }
                    }
                    catch (TsbBaseException tsbEx)
                    {
                        isSuccess = false;
                        MessageManager.Show(tsbEx);
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                        MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
                    }
                    finally
                    {
                        if (item.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.DELETE)
                        {
                            item.OpCode = Tsb.Fontos.Core.Configuration.Common.OpCodes.READ; // 지우지 않은 상태로 만들어 준다.
                        }
                    }

                    //ProgressBar Code==========================================
                    this.ProgressBarManager.PerformStep();
                }
                //ProgressBar Code==========================================
                this.ProgressBarManager.EndProgress();

                // About the exception not happening from SpreadGrid does elimination.
                for (int i = deleteKeys.Count - 1; i >= 0; i--)
                {
                    int removeIdx = Int32.Parse(deleteKeys[i].ToString());
                    this.FormView.Grid.DeleteViewRowWithDataRow(removeIdx);
                }

                if (isSuccess)
                {
                    MessageManager.Show("MSG_CTCM_00001", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        #region Method(DetailView/DataSync) AREA ********************
        /// <summary>
        /// Show Detail View.
        /// </summary>
        /// <param name="formId">form's id</param>
        /// <param name="bClear">bClear</param>
        public void ShowDetailView(string formId, bool bClear)
        {
            if (this.FormView.SourceItemList != null && this.FormView.ActiveRowDataIndex > -1)
            {
                this.DetailView = this.FormView.ShowDetailView() as SingleGridDetailInterface;

                if (bClear)
                {
                    if (this.DetailView.Controller != null)
                    {
                        (this.DetailView.Controller as SingleGridDetailController).DoClearData();
                    }
                }
                else
                {
                    // Data Notify
                    this.NotifySyncAgent();
                }
            }
        }

        /// <summary>
        /// Notify SyncAgent.
        /// </summary>
        public void NotifySyncAgent()
        {
            if (this.DetailView != null && this.DetailView.IsDisposed == false)
            {
                object targetSyncData = this.FormView.SourceItemList[this.FormView.ActiveRowDataIndex];
                this.DataSyncAgent.NotifyToSync(SingleGridController.SYNC_NOTIFY_NAME, targetSyncData);
            }
            return;
        }
        #endregion

        #region Toolbar Attribute METHOD AREA ***********************
        /// <summary>
        /// Menu's Common refresh button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.REFRESH)]
        public bool MenuRefresh(object sender, EventArgs e)
        {
            if (!this.FormView.SearchMandatoryCheck()) return false;

            if (!ObserverController.GetInstance().SaveClick(sender, e))
            {
                object obj = this.DoRetrieveData();

                if (obj != null) return true;  // Success message showing.
                else return false;             // Success message does not show.
            }
            else
            {
                return false; // Success message does not show.
            }
        }

        /// <summary>
        /// Menu's Common DETAIL button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.DETAIL)]
        public void MenuDetail(object sender, EventArgs e)
        {
            this.ShowDetailView(this.DETAIL_VIEW_NAME, false);
        }

        /// <summary>
        /// Menu's Common DELETE button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.DELETE)]
        public bool MenuDelete(object sender, EventArgs e)
        {
            if (this.FormView.Grid.DataSource != null)
            {
                this.FormView.Grid.DeleteRow();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Menu's Common NEW button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.NEW)]
        public void MenuNew(object sender, EventArgs e)
        {
            this.ShowDetailView(this.DETAIL_VIEW_NAME, true);
        }

        /// <summary>
        /// Menu's Common SAVE button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.SAVE)]
        public void MenuSave(object sender, EventArgs e)
        {
            this.DoSaveData();
        }

        /// <summary>
        /// occurs when Menu's Common REFRESH or SAVE button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ValidateAttribute(ValidateAttribute.ValidateSet.DATA_CHANGE)]
        private bool DataChange()
        {
            this.FormView.Grid.Focus();
            BaseItemsList<SingleGridItem> singleGridItems = this.FormView.SourceItemList as BaseItemsList<SingleGridItem>;

            if (singleGridItems != null)
            {
                List<SingleGridItem> modifiedList = singleGridItems.Where(p => p.OpCode.Equals(OpCodes.CREATE) || p.OpCode.Equals(OpCodes.UPDATE)).ToList();

                if (modifiedList != null && modifiedList.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// occurs when Menu's Common SAVE button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ValidateAttribute(ValidateAttribute.ValidateSet.DATA_VALIDATION)]
        private string DataValidation()
        {
            return this.FormView.SaveMandatoryCheck();
        }

        /// <summary>
        /// occurs when Menu's Common ForeColor button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.FORE_COLOR)]
        public void MenuForeColor(object sender, ColorPickerEventArgs e)
        {
            // Change SpreadGrid's forecolor
        }

        /// <summary>
        /// occurs when Menu's Common BackColor button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.BACK_COLOR)]
        public void MenuBackColor(object sender, ColorPickerEventArgs e)
        {
            // Change SpreadGrid's backcolor
        }

        /// <summary>
        /// occurs when Menu's Common business dialog button click handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        /// <returns>Class String</returns>
        [BizDialogToolbarAttribute(BizDialogToolbarAttribute.BizDialogToolbarSet.CNTR_DETAIL)]
        [CTBizDialogToolbarAttribute(CTBizDialogToolbarAttribute.CTBizDialogToolbarSet.CatosCntrDetail)]
        public string GetCntrNo(object sender, EventArgs e)
        {
            SingleGridItem activeItem = this.FormView.ActiveItem;

            if (activeItem != null)
            {
                return activeItem.Class;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region METHOD(ToolbarEnable) AREA **************************
        /// <summary>
        /// Make Toolbar's buttons enabled or not.
        /// </summary>
        public void ToolbarEnable()
        {
            if ((this.FormView.SourceItemList != null) &&
                (this.FormView.SourceItemList.Count > 0))
            {
                ObserverController.GetInstance().SetEnableToolbar(
                    ControllerToolbarAttribute.ControllerToolbarSet.SAVE |
                    ControllerToolbarAttribute.ControllerToolbarSet.NEW,
                    true);
            }
            else
            {
                ObserverController.GetInstance().SetEnableToolbar(
                    ControllerToolbarAttribute.ControllerToolbarSet.SAVE |
                    ControllerToolbarAttribute.ControllerToolbarSet.NEW,
                    false);
            }
        }
        #endregion
    }
}
