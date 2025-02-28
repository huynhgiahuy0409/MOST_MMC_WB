using System;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.LogOutReason
{
    public partial class LogOutReasonView : BaseMobileBizView, LogOutReasonInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-LogOutReasonView";
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        public LogOutReasonController ThisController 
        {
            get { return this.Controller as LogOutReasonController; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public LogOutReasonView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new LogOutReasonController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.logOutList.ItemButton_Clicked += new ItemButtonClickedEventHandler(logOutList_ItemButton_Clicked);
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
                ErrorMessageHandler.Show(ex, ObjectID);
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
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        private void logOutList_ItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                ThisController.SelectedLogOutReason = e.SelectedItemCode.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        #endregion EVENT HANDLER AREA *****************************************

        #region METHOD AREA ***************************************************
        
        public void SetItemList(BaseItemsList<ItemListControlItem> itemList)
        {
            try
            {
                this.logOutList.ItemList = itemList;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex, ObjectID);
            }
        }

        #endregion METHOD AREA ************************************************
    }
}
