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

namespace Tsb.Catos.Cm.Mobile.Win.YtList
{
    public partial class YtListView : BaseMobileBizView, YtListInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-YtListView";

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        private YtListController ThisController
        {
            get { return this.Controller as YtListController; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public YtListView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new YtListController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnCreateB1.Click += new EventHandler(btnCreateB1_Click);
            this.btnAllOfYt.Click += new EventHandler(btnAllOfYt_Click);
            this.btnPoolOfYt.Click += new EventHandler(btnPoolOfYt_Click);

            this.YtList.ItemButton_Clicked += new ItemButtonClickedEventHandler(YtItemButton_Clicked);
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
                // added by YoungOk Kim (2019.04.16) - Mantis 90425: YO job order without YT assignment can generate B1 job order
                this.btnCreateB1.Visible = ThisController.VisibleCreateB1;
                this.btnCreateB1.Enabled = ThisController.EnableCreateB1;
                ThisController.IsSelectedCreateB1 = false;


                if (ThisController.YardTruckAll != null)
                {
                    this.btnAllOfYt.Visible = true;
                    this.btnAllOfYt.Enabled = true;

                    this.btnPoolOfYt.Visible = true;
                    this.btnPoolOfYt.Enabled = true;
                }
                else
                {
                    this.btnAllOfYt.Visible = false;
                    this.btnAllOfYt.Enabled = false;

                    this.btnPoolOfYt.Visible = false;
                    this.btnPoolOfYt.Enabled = false;
                }


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

        private void btnCreateB1_Click(object sender, EventArgs e)
        {
            try
            {
                ThisController.IsSelectedCreateB1 = true;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        void btnPoolOfYt_Click(object sender, EventArgs e)
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

        void btnAllOfYt_Click(object sender, EventArgs e)
        {
            try
            {
                ThisController.DoRetrieveDataAllOfYt();
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

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion EVENT HANDLER AREA *****************************************

        #region METHOD AREA ***************************************************

        public void SetSelectedJobInfo(string cntrNo, string jobCode, string ytNo)
        {
            try
            {
                txtCntrNo.Text = cntrNo;
                txtJobCode.Text = jobCode;

                YtList.SelectItem(ytNo);
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

        #endregion METHOD AREA ************************************************
    }
}
