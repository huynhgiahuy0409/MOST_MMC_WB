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
using Tsb.Fontos.Core.Item;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;
//YoungHwan Choi
namespace Tsb.Catos.Cm.Mobile.Win.HoldDeck
{
    public partial class HoldDeckPopupView : BaseMobileBizView
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-HoldDeckPopupView";
        private string _selectionItem = null;
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        public string SelectionItem
        {
            get { return _selectionItem; }
            set { _selectionItem = value; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public HoldDeckPopupView()
        {
            base.ObjectID = OBJECT_ID;
            InitializeComponent();
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.holdDeckList.ItemButton_Clicked += new ItemButtonClickedEventHandler(ItemButton_Clicked);
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
        #endregion INITIALIZATION AREA *******************************************

        #region METHOD AREA ***************************************************
        private void SetItemList(BaseItemsList<ItemListControlItem> itemList)
        {
            try
            {
                this.holdDeckList.ItemList = itemList;
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
              BaseItemsList<ItemListControlItem> itemList = new BaseItemsList<ItemListControlItem>();

              var holdItem = new ItemListControlItem();
              holdItem.Code = "H";
              holdItem.CodeName = "Hold";
              holdItem.TextValue = "Hold";
              holdItem.CustomStyleName = "biz_btn_Hold";
              itemList.Add(holdItem);

              var DeckItem = new ItemListControlItem();
              DeckItem.Code = "D";
              DeckItem.CodeName = "Deck";
              DeckItem.TextValue = "Deck";
              DeckItem.CustomStyleName = "biz_btn_Deck";
              itemList.Add(DeckItem);

              this.SetItemList(itemList);
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
                string selectionItem = e.SelectedItemCode.ToString();
                this.DialogResult = DialogResult.OK;

                if (string.IsNullOrEmpty(selectionItem) == false)
                {
                    _selectionItem = selectionItem;
                }

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
