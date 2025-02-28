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
* 2010.07.30  Tonny.Kim 1.0	First release.
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
using Tsb.Fontos.Core.Configuration.Common;
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
using Tsb.Fontos.Win.Layout.Form;
using Tsb.Fontos.Win.Menu.Toolbar.Attribute;
using Tsb.Fontos.Win.Menu.Toolbar.BizController;
using Tsb.Fontos.Win.Message;
using Tsb.Product.WB.Service.WeightBridge;

namespace Tsb.Product.WB.Client.Sample
{
    public class SingleGridDetailController : BaseDetailController
    {
        #region FIELD AREA ******************************************
        private string staffCd = "";
        private ISingleGridService service = null;
        #endregion

        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets or sets FormView.
        /// </summary>
        public SingleGridDetailInterface FormView { get; set; }

        /// <summary>
        /// Gets or sets MasterItem.
        /// </summary>
        public SingleGridItem MasterItem { get; set; }

        /// <summary>
        /// Name of the synchronization notified.
        /// </summary>
        public static readonly string SYNC_NOTIFY_NAME = "CTL-PT-PTWB-SPL-SingleGridDetail";
        #endregion

        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Initializes a new instance of the SingleGridDetailController class.
        /// </summary>
        public SingleGridDetailController()
            : this(null)
        { }

        /// <summary>
        /// Initializes a new instance using the specified view.
        /// </summary>
        /// <param name="view">The suggested SingleGrdiDetailInterface of this instance.</param>
        public SingleGridDetailController(SingleGridDetailInterface view)
            : base(view)
        {
            this.ObjectID = "CTL-PT-PTWB-SPL-SingleGridDetailController";
            this.FormView = view;
            this.staffCd = AuditUtil.GetStaffCd(this.FormView.FormName);
            this.AddEventHandler();
            this.InitDataSyncCallbackInfo();
        }
        #endregion

        #region BaseController Implements AREA **********************
        /// <summary>
        /// Inits Data Synchronization Callback Information
        /// </summary>
        public override void InitDataSyncCallbackInfo()
        {
            this.DataSyncAgent.AddCallbackInfo(SingleGridController.SYNC_NOTIFY_NAME, this.SingleGridDetailView_DataSyncNotified);
        }

        /// <summary>
        /// Data Synchronization Notified Callback Method
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Object of Event Arguments class that contains event data</param>
        private void SingleGridDetailView_DataSyncNotified(object sender, DataSyncNotifiedEventArgs e)
        {
            if (!FormLayoutInfo.AllowEachDetail || this.FormView.IsActiveDetail)
            {
                this.MasterItem = e.TargetDataToSync as SingleGridItem;
                this.FormView.DetailItem = this.MasterItem.Clone() as SingleGridItem;
            }
        }

        /// <summary>
        /// Adds Event Handleresult
        /// </summary>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public override void AddEventHandler()
        {
        }
        #endregion

        #region BUSINESS HELPER METHOD AREA *************************
        /// <summary>
        /// Inquiries items of SingleGrid from service 
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
        /// Save Items of booking list
        /// </summary>
        /// <returns>A saved data count</returns>
        public bool DoSaveData()
        {
            bool isSuccess = false;
            OpCodes syncOpCodes = OpCodes.NONE;
            try
            {
                SingleGridParam param = new SingleGridParam(this, ProductWBServiceSpec.SAMPLE_SINGLE_GRID_SERVICE);
                this.service = BizServiceLocator.GetService<ISingleGridService>(param);

                syncOpCodes = this.FormView.DetailItem.OpCode;
                // Dg Container Detail Master
                if ((this.FormView.DetailItem.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.CREATE) ||
                    (this.FormView.DetailItem.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.UPDATE))
                {
                    this.FormView.DetailItem.StaffCd = this.staffCd;
                    this.service.ProcessSingleGrid(this.FormView.DetailItem);
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

            if (isSuccess)
            {
                this.NotifySyncAgent(syncOpCodes);
            }

            return isSuccess;
        }

        /// <summary>
        /// Delete items of Detail from service.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        public void DoDeleteData(object sender, GridRowRemovedEventArgs e)
        {
        }

        /// <summary>
        /// Clear Detail View Data
        /// </summary>
        public void DoClearData()
        {
            SingleGridItem clearItem = new SingleGridItem();
            clearItem.OpCode = Tsb.Fontos.Core.Configuration.Common.OpCodes.CREATE;
            this.FormView.DetailItem = clearItem;
        }
        #endregion

        #region Method(DetailView/DataSync) AREA ********************
        /// <summary>
        /// Notify SyncAgent.
        /// </summary>
        public void NotifySyncAgent(OpCodes syncOpCodes)
        {
            object targetSyncData = this.FormView.DetailItem;

            if (syncOpCodes == OpCodes.CREATE)
            {
                this.DataSyncAgent.NotifyToSync(SingleGridDetailController.SYNC_NOTIFY_NAME, targetSyncData, null, syncOpCodes);
            }
            else
            {
                this.DataSyncAgent.NotifyToSync(SingleGridDetailController.SYNC_NOTIFY_NAME, targetSyncData, this.MasterItem, syncOpCodes);
            }
        }
        #endregion

        #region Toolbar Attribute METHOD AREA ***********************
        /// <summary>
        /// Menu's Common DELETE button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.DELETE)]
        public bool MenuDelete(object sender, EventArgs e)
        {
            return false;
        }

        /// <summary>
        /// Menu's Common refresh button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.REFRESH)]
        public bool MenuRefresh(object sender, EventArgs e)
        {
            return false;
        }

        // <summary>
        /// Menu's Common NEW button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.NEW)]
        public void MenuNew(object sender, EventArgs e)
        {
            this.DoClearData();
        }

        /// <summary>
        /// Menu's Common SAVE button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ControllerToolbarAttribute(ControllerToolbarAttribute.ControllerToolbarSet.SAVE)]
        public bool MenuSave(object sender, EventArgs e)
        {
            return this.DoSaveData();
        }

        /// <summary>
        /// occurs when Menu's Common REFRESH or SAVE button click event handler.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        [ValidateAttribute(ValidateAttribute.ValidateSet.DATA_CHANGE)]
        private bool DataChange()
        {
            if (this.FormView.DetailItem == null) return false;

            if ((this.FormView.DetailItem.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.CREATE) ||
                (this.FormView.DetailItem.OpCode == Tsb.Fontos.Core.Configuration.Common.OpCodes.UPDATE))
            {
                return true;
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
        #endregion
    }
}
