using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.EquipmentList
{
    public partial class EquipmentListView : BaseMobileBizView, EquipmentListInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-EquipmentListView";
        
        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************
        
        public string FormName
        {
            get { return Name; }
        }

        private EquipmentListController ThisController
        {
            get { return this.Controller as EquipmentListController; }
        }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public EquipmentListView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();
            this.Controller = new EquipmentListController(this);
            this.AddEventHandler();
            this.InitControls();
        }

        private void AddEventHandler()
        {
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.equList.ItemButton_Clicked += new ItemButtonClickedEventHandler(ItemButton_Clicked);
            this.btnOk.Click += new EventHandler(btnOk_Click); // added by YoungOk Kim (2019.07.03) - Mantis 90787: [YQ] GC 복수 선택
            this.cmbSortEquipmentGroup.CheckedChanged += new EventHandler(cmbSortEquipmentGroup_CheckedChanged);
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
                this.equList.ItemList = itemList;
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

                this.equList.SelectItem(ThisController.SelectedEquipmentNo);

                // added by YoungOk Kim (2019.07.03) - Mantis 90787: [YQ] GC 복수 선택
                this.btnOk.Visible = ThisController.UseMultipleSelection;
                this.equList.UseMultipleSelection = ThisController.UseMultipleSelection;
                this.cmbSortEquipmentGroup.Visible = ThisController.UseSortEquipmentGroup;
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
                if (ThisController.UseMultipleSelection == true) // added by YoungOk Kim (2019.07.03) - Mantis 90787: [YQ] GC 복수 선택
                {
                    string equNo = e.SelectedItemCode.ToString();
                    if (string.IsNullOrEmpty(equNo) == false)
                    {
                        if (e.IsSelected == true)
                        {
                            if (ThisController.SelectedEquipmentList == null)
                            {
                                ThisController.SelectedEquipmentList = new List<string>();
                            }
                            ThisController.SelectedEquipmentList.Add(equNo);
                        }
                        else
                        {
                            if (ThisController.SelectedEquipmentList != null)
                            {
                                ThisController.SelectedEquipmentList.Remove(equNo);
                            }
                        }
                    }
                }
                else
                {
                    String equNo = e.SelectedItemCode.ToString();
                    this.DialogResult = DialogResult.OK;

                    ThisController.SelectedEquipmentNo = equNo;

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        // added by YoungOk Kim (2019.07.03) - Mantis 90787: [YQ] GC 복수 선택
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
        private void cmbSortEquipmentGroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ThisController.IsSortEquipmentGroup = cmbSortEquipmentGroup.Checked;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
            }
        }

        #endregion EVENT HANDLER AREA *****************************************
    }
}
