using System;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;
using Tsb.Fontos.Win.Message;

namespace Tsb.Catos.Cm.Mobile.Win.YtListSingleTwin
{
    public partial class YtListSingleTwinView : BaseMobileBizView, YtListSingleTwinInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-YtListSingleTwin";

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        private YtListSingleTwinController ThisController
        {
            get { return this.Controller as YtListSingleTwinController; }
        }

        // added by jaeok (2020.08.25) Mantis 108828: [YQ] 20FT 선적분 작업시 YQ 수동배차 화면 인터페이스 및 업무 프로세스 변경 요청
        public bool VisibleJobType
        {
            set
            {
                pnlJobType.Visible = value;
                if (pnlJobType.Visible == true)
                {
                    YtList.MaxRowCount = 3;
                }
                else
                {
                    YtList.MaxRowCount = 4;
                }
            }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public YtListSingleTwinView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new YtListSingleTwinController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.YtList.ItemButton_Clicked += new ItemButtonClickedEventHandler(YtItemButton_Clicked);
            this.CPosList.ItemButton_Clicked += new ItemButtonClickedEventHandler(CPosItemButton_Clicked); // added by jaeok (2020.07.08) Mantis 107938: [YQ] 수동 배차 기능 관련하여 수정
            this.SingleTwin.ItemButton_Clicked += new ItemButtonClickedEventHandler(SingleTwinButton_Clicked);
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

        #region EVENT HANDLER AREA ********************************************

        protected override void OnShown(EventArgs e)
        {
            try
            {
                ThisController.DoRetrieveData();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
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
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(YtList.GetSelectedItemCode()) == true)
                {
                    MessageManager.Show("MSG_CTYQ_00075", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(SingleTwin.GetSelectedItemCode()) == true)
                {
                    MessageManager.Show("MSG_CTYQ_00080", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void SingleTwinButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                String singleTwin = e.SelectedItemCode.ToString();
                if (singleTwin.Equals(ThisController.JOB_TYPE_NONE) == true) // 기존 유지
                {
                    ThisController.SelectedSingleTwin = "";
                }
                else
                {
                    ThisController.SelectedSingleTwin = singleTwin;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void YtItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                String ytNo = e.SelectedItemCode.ToString();
                ThisController.setSelectedYt(ytNo);

                if (ytNo.Equals(ThisController.YtNo) == true)
                {
                    CPosList.SelectItem(ThisController.CPos);
                    ThisController.setSelectedCPos(ThisController.CPos);
                }
                else
                {
                    if (ThisController.CPos.Equals(ThisController.CHASSIS_POSITION_MIDDLE) == true)
                    {
                        // do nothing
                    }
                    else
                    {
                        string occupiedCPos = ThisController.YtList.GetItem(ytNo).Remark;
                        if (occupiedCPos.Contains(ThisController.CHASSIS_POSITION_AFTER) && occupiedCPos.Contains(ThisController.CHASSIS_POSITION_FORE))
                        {
                            // do nothing
                        }
                        else if (occupiedCPos.Contains(ThisController.CHASSIS_POSITION_AFTER) == true)
                        {
                            CPosList.SelectItem(ThisController.CHASSIS_POSITION_FORE);
                            ThisController.setSelectedCPos(ThisController.CHASSIS_POSITION_FORE);
                        }
                        else if (occupiedCPos.Contains(ThisController.CHASSIS_POSITION_FORE) == true)
                        {
                            CPosList.SelectItem(ThisController.CHASSIS_POSITION_AFTER);
                            ThisController.setSelectedCPos(ThisController.CHASSIS_POSITION_AFTER);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        // added by jaeok (2020.07.08) Mantis 107938: [YQ] 수동 배차 기능 관련하여 수정
        private void CPosItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                String cPos = e.SelectedItemCode.ToString();
                ThisController.setSelectedCPos(cPos);
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion EVENT HANDLER AREA *****************************************

        #region METHOD AREA ***************************************************

        public void SetSelectedJobInfo(string cntrNo, string jobCode, string ytNo, string cPos)
        {
            try
            {
                lblCntrNoData.Text = cntrNo;
                lblJobCodeData.Text = jobCode;

                YtList.SelectItem(ytNo);
                CPosList.SelectItem(cPos); // added by jaeok (2020.07.08) Mantis 107938: [YQ] 수동 배차 기능 관련하여 수정
                SingleTwin.SelectItem(ThisController.JOB_TYPE_NONE);
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void SetSingleTwinItem(string singleTwin)
        {
            try
            {
                ThisController.SelectedSingleTwin = singleTwin;
                SingleTwin.SelectItem(singleTwin);
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void SetItemList(BaseItemsList<ItemListControlItem> itemList)
        {
            try
            {
                this.YtList.ItemList = itemList;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        public void SetSingleTwinItemList(BaseItemsList<ItemListControlItem> itemList)
        {
            try
            {
                this.SingleTwin.ItemList = itemList;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        // added by jaeok (2020.07.08) Mantis 107938: [YQ] 수동 배차 기능 관련하여 수정
        public void SetCPosItemList(BaseItemsList<ItemListControlItem> itemList)
        {
            try
            {
                this.CPosList.ItemList = itemList;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        #endregion METHOD AREA ************************************************
    }
}
