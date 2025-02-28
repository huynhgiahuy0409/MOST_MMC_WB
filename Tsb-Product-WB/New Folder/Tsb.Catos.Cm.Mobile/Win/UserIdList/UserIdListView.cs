using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;
using Tsb.Fontos.Core.Item;
using Tsb.Catos.Cm.Mobile.Common.Item;

namespace Tsb.Catos.Cm.Mobile.Win.UserIdList
{
    public partial class UserIdListView : BaseMobileBizView, UserIdListInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-UserIdListView";

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        private UserIdListController ThisController
        {
            get { return this.Controller as UserIdListController; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public UserIdListView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new UserIdListController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.userIdList.ItemButton_Clicked += new ItemButtonClickedEventHandler(ItemButton_Clicked);
        }

        private void InitControls()
        {
            try
            {
                FormDraggingHandler _formDraggingHdl = new FormDraggingHandler(this.lblTitle, this);
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
                this.userIdList.ItemList = itemList;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion METHOD AREA ************************************************

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

        private void ItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                string userId = e.SelectedItemCode.ToString();
                this.DialogResult = DialogResult.OK;

                ThisController.SelectedUserID = userId;
                ThisController.SelectedUserName = e.SelectedItemCodeName.ToString();

                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion EVENT HANDLER AREA *****************************************
    }
}
