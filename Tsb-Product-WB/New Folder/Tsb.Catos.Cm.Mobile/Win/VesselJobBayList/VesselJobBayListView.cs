using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.VesselInformation;
using Tsb.Fontos.Core.BgWorker;
using Tsb.Fontos.Core.BgWorker.Event;
using Tsb.Fontos.Core.BgWorker.Info;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.VesselJobBayList
{
    public partial class VesselJobBayListView : BaseMobileBizView, VesselJobBayListInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-VesselJobBayListView";
        private bool _showHoldDeckSelection = false;//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        private string _selectedHoldDeck = null;//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        private string _approachModule = null;//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        private VesselJobBayListController ThisController
        {
            get { return this.Controller as VesselJobBayListController; }
        }

        public AsyncAgent AsyncAgent { get; set; }
        public bool UseUserVoyage { get; set; }
        public bool UseEnableVesselChangeAfterLogin { get; set; }
        public bool UseActivatedInAllBay { get; set; }//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
        public bool UseVesselName { get; set; } // added by JH.Tak (2020.09.23) 0109492: QC driver- login screen - select vessel display vessel name instead of vsl code
        public int VslColumnCount { get; set; } // added by JH.Tak (2020.09.23) 0109492: QC driver- login screen - select vessel display vessel name instead of vsl code
        public bool UseBayInsteadHatch { get; set; } // added by JH.Tak (2021.07.07) KPCT Driverless YT
        public bool UsePortVoy { get; set; } // added by BG.Kim (2022.11.29) [WHL_UP] 0134776: Show VRN(PORT_VOY) in Location

        public bool ShowHoldDeckSelection//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        {
            get
            {
                return _showHoldDeckSelection;
            }
            set
            {
                _showHoldDeckSelection = value;
            }
        }

        public string SelectedHoldDeck//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        {
            get { return _selectedHoldDeck; }
            set { _selectedHoldDeck = value; }
        }

        public string ApproachModule//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        {
            get
            {
                return _approachModule;
            }
            set 
            { 
                _approachModule = value;
            }
        }
 
        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public VesselJobBayListView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();

            this.AsyncAgent = new AsyncAgent(null, true, false);
            this.Controller = new VesselJobBayListController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.vesselList.ItemButton_Clicked += new ItemButtonClickedEventHandler(VesselItemButton_Clicked);
            this.jobBayList.ItemButton_Clicked += new ItemButtonClickedEventHandler(JobBayItemButton_Clicked);

            this.AsyncAgent.CallBackResult += new ResultEventHandler(AsyncAgent_CallBackResult);
            this.AsyncAgent.CallBackError += new FaultEventHandler(AsyncAgent_CallBackError);

            this.rbtnHold.CheckedChanged += new EventHandler(rbtnHold_CheckedChanged);
            this.rbtnDeck.CheckedChanged +=new EventHandler(rbtnDeck_CheckedChanged);
        }

        private void InitControls()
        {
            try
            {
                FormDraggingHandler _formDraggingHdl = new FormDraggingHandler(this.lblTitle, this); // added by YoungOk Kim (2019.05.16) - Mantis 89476: [Tally] 창 이동 기능
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion INITIALIZATION AREA ****************************************

        #region METHOD AREA ***************************************************

        public void SetItemList(BaseItemsList<ItemListControlItem> itemList)
        {
            try
            {
                this.vesselList.ItemList = itemList;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void GetVesselScheduleList()
        {
            try
            {
                Expression<Func<VesselJobBayListController, object>> expression = o => o.DoRetrieveData();
                this.AsyncAgent.RunWorkerAsync(new AsyncFuncWorker<VesselJobBayListController>("Vessel Job Bay List View", ThisController, expression));
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void InitHoldDeckRadioButton()//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedHoldDeck))
                {
                    if (_showHoldDeckSelection == false)
                    {
                        rbtnHold.Visible = false;
                        rbtnDeck.Visible = false;
                    }
                    else
                    {
                        rbtnHold.Checked = true;
                        rbtnDeck.Checked = false;
                    }
                }
                else
                {
                    if (_selectedHoldDeck == "H")
                    {
                        rbtnHold.Checked = true;
                        rbtnDeck.Checked = false;
                    }
                    else
                    {
                        rbtnHold.Checked = false;
                        rbtnDeck.Checked = true;
                    }
                }
                this.Invalidate();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private string ConvertHoldDeck(string text)//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        {
            string convertHoldDeck = null;
            try
            {
                switch (text)
                {
                    case "Hold":
                        convertHoldDeck = "H";
                        break;
                    case "Deck":
                        convertHoldDeck = "D";
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }

            return convertHoldDeck;
        }
        #endregion METHOD AREA ************************************************

        #region EVENTHANDLER AREA *********************************************

        protected override void OnShown(EventArgs e)
        {
            try
            {
                this.InitHoldDeckRadioButton(); //added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT

                if (string.IsNullOrEmpty(_approachModule) == false) //added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
                {
                    switch (_approachModule)
                    {
                        case "Qc":
                            this.btnOk.Enabled = false;
                            break;
                    }
                }

                if (this.vesselList.ItemList == null)
                {
                    GetVesselScheduleList();
                    //if (UseUserVoyage) deleted by SH.Nam (2018.12.07) due to overflowing text.
                    {
                        if (VslColumnCount <= 0)
                        {
                            VslColumnCount = 2;
                        }
                        this.vesselList.MaxColumnCount = VslColumnCount;
                    }
                }

                if (ThisController.SelectedVesselItem != null)
                {
                    this.vesselList.SelectItem(ThisController.SelectedVesselItem.Key);

                    // modified by SH.Nam (2018.12.18) 88105: Tally 모듈에서 로그아웃 없이 모선 변경
                    if (UseEnableVesselChangeAfterLogin == false)
                    {
                        this.vesselList.Enabled = false;
                    }
                    else
                    {
                        ThisController.SelectedJobBay = string.Empty;
                    }

                    if (string.IsNullOrEmpty(ThisController.SelectedJobBay) == true)
                    {
                        this.btnOk.Enabled = false;
                    }

                    if (this.jobBayList.ItemList == null)
                    {
                        var vesselScheduleItem = ThisController.SelectedVesselItem;
                        this.jobBayList.ItemList = ThisController.GetJobSeqList(vesselScheduleItem.VslCd, vesselScheduleItem.CallYear, vesselScheduleItem.CallSeq, true);
                        this.jobBayList.SelectItem(ThisController.SelectedJobBay);
                    }

                    if (this.UseActivatedInAllBay == true)//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
                    {
                        var vesselScheduleItem = ThisController.SelectedVesselItem;
                        this.jobBayList.ItemList = ThisController.GetJobSeqList(vesselScheduleItem.VslCd, vesselScheduleItem.CallYear, vesselScheduleItem.CallSeq, true);
                        this.jobBayList.UseActivatedInAllBay = true;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;

                ThisController.NotifySyncAgent();

                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        private void VesselItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                BerthPlanItem item = e.SelectedItem is BerthPlanItem ? e.SelectedItem as BerthPlanItem : null;
                if (item != null)
                {
                    ThisController.SelectedVesselItem = item;
                    this.jobBayList.ItemList = ThisController.GetJobSeqList(item.VslCd, item.CallYear, item.CallSeq, false);
                    if (UseEnableVesselChangeAfterLogin)    // added by SH.Nam (2018.12.18) 88105: Tally 모듈에서 로그아웃 없이 모선 변경
                    {
                        this.btnOk.Enabled = false;
                    }

                    if (this.UseActivatedInAllBay == true)//added by YoungHwan Choi (2020.06.30) -Mantis 107546 GCT Activated 작업이 있는 베이를 구분할 수 있는 방법
                    {
                        this.jobBayList.UseActivatedInAllBay = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        private void JobBayItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                ThisController.SelectedJobBay = e.SelectedItemCode.ToString();
                this.btnOk.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        private void AsyncAgent_CallBackResult(object sender, AsyncResultEventArgs e)
        {
            try
            {
                BaseItemsList<ItemListControlItem> itemList = e.ResultObject is BaseItemsList<ItemListControlItem>  ? e.ResultObject as BaseItemsList<ItemListControlItem> : null;
                if (itemList != null)
                {
                    SetItemList(itemList);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        private void AsyncAgent_CallBackError(object sender, AsyncFaultEventArgs e)
        {
            ErrorMessageHandler.ErrorLog(e.Error);
        }

        private void rbtnHold_CheckedChanged(object sender, EventArgs e)//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        {
            try
            {
                if (rbtnHold.Checked)
                {
                    _selectedHoldDeck = this.ConvertHoldDeck(rbtnHold.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }


        private void rbtnDeck_CheckedChanged(object sender, EventArgs e)//added by YoungHwan Choi(2020.12.01) HJNC CTR-207 : STS Cabin VMT
        {
            try
            {
                if (rbtnDeck.Checked)
                {
                    _selectedHoldDeck = this.ConvertHoldDeck(rbtnDeck.Text); 
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        #endregion EVENTHANDLER AREA ******************************************
    }
}
