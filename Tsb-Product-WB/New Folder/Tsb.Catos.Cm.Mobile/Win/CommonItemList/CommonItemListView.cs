using System;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Catos.Cm.Mobile.Common.Item.CommonItemList;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.CommonItemList
{
    public partial class CommonItemListView : BaseMobileBizView, CommonItemListInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-CommonItemListView";
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************
        
        public string FormName
        {
            get { return Name; }
        }
        
        private CommonItemListController ThisController
        {
            get { return this.Controller as CommonItemListController; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************
        public CommonItemListView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new CommonItemListController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.itemList.ItemButton_Clicked += new ItemButtonClickedEventHandler(ItemButton_Clicked);
            this.btnOk.Click += new EventHandler(btnOk_Click); // added by YoungOk Kim (2020.05.27) - Mantis 106042: [YQ] 필터에 블록 버튼 추가
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
                // added by YoungOk Kim (2020.05.27) - Mantis 106042: [YQ] 필터에 블록 버튼 추가
                this.btnOk.Visible = ThisController.UseMultipleSelection;
                this.itemList.UseMultipleSelection = ThisController.UseMultipleSelection;

                SetMaxRowCount(ThisController.MaxRowCount);
                SetMaxColumnCount(ThisController.MaxColumnCount);
                ThisController.DoRetrieveData();

                var selectedCode = ThisController.SelectedItem == null ? string.Empty : ThisController.SelectedItem.Code;
                this.itemList.SelectItem(selectedCode);
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
                this.itemList.ClearSelectedCode();
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
                if (ThisController.UseMultipleSelection == true) // added by YoungOk Kim (2019.07.03) - Mantis 90787: [YQ] GC 복수 선택
                {
                    CommonItemListItem selectedItem = e.SelectedItem as CommonItemListItem;
                    if (selectedItem != null)
                    {
                        if (e.IsSelected == true)
                        {
                            if (ThisController.SelectedItemList == null)
                            {
                                ThisController.SelectedItemList = new BaseItemsList<CommonItemListItem>();
                            }
                            ThisController.SelectedItemList.Add(selectedItem);
                        }
                        else
                        {
                            if (ThisController.SelectedItemList != null)
                            {
                                ThisController.SelectedItemList.Remove(selectedItem);
                            }
                        }
                    }
                }
                else
                {
                    CommonItemListItem selectedItem = e.SelectedItem as CommonItemListItem;
                    ThisController.SelectedItem = selectedItem;

                    this.itemList.ClearSelectedCode();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        // added by YoungOk Kim (2020.05.27) - Mantis 106042: [YQ] 필터에 블록 버튼 추가
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

        #endregion EVENT HANDLER AREA *****************************************

        #region METHOD AREA ***************************************************

        public void SetItemList(BaseItemsList<ItemListControlItem> itemList, string title)
        {
            try
            {
                this.itemList.ItemList = itemList;
                this.lblTitle.Text = title;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void SetMaxRowCount(int count)
        {
            try
            {
                this.itemList.MaxRowCount = count > 0 ? count : 4; // default : 4
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        public void SetMaxColumnCount(int count)
        {
            try
            {
                this.itemList.MaxColumnCount = count > 0 ? count : 1; // default : 1
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        #endregion METHOD AREA ************************************************
    }
}
