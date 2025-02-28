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

namespace Tsb.Catos.Cm.Mobile.Win.YtCPosList
{
    public partial class YtCPosListView : BaseMobileBizView, YtCPosListInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-YtCPosListView";

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        private YtCPosListController ThisController
        {
            get { return this.Controller as YtCPosListController; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public YtCPosListView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new YtCPosListController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnOk.Click += new EventHandler(btnOk_Click);

            this.YtList.ItemButton_Clicked += new ItemButtonClickedEventHandler(YtItemButton_Clicked);
            this.CPosList.ItemButton_Clicked += new ItemButtonClickedEventHandler(CPosItemButton_Clicked);
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

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

        #endregion EVENT HANDLER AREA *****************************************

        #region METHOD AREA ***************************************************

        public void SetSelectedJobInfo(string cntrNo, string jobCode, string ytNo, string cPos)
        {
            try
            {
                txtCntrNo.Text = cntrNo;
                txtJobCode.Text = jobCode;

                YtList.SelectItem(ytNo);
                CPosList.SelectItem(cPos);
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void SetYtItemList(BaseItemsList<ItemListControlItem> itemList)
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
