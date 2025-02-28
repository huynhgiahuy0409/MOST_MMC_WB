using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Catos.Cm.Core.YardDefine.Item;
using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;
using Tsb.Catos.Cm.Mobile.Common.Handler;
using Tsb.Catos.Cm.Mobile.Common.Item;
using Tsb.Fontos.Core.BgWorker;
using Tsb.Fontos.Core.BgWorker.Event;
using Tsb.Fontos.Core.BgWorker.Info;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Resources;
using Tsb.Fontos.Win.Forms;
using Tsb.Fontos.Win.Message;
using Tsb.Fontos.Win.MobileTemplate;
using Tsb.Fontos.Win.Util.Contols;

namespace Tsb.Catos.Cm.Mobile.Win.BlockBayList
{
    public enum BayRowType
    {
        BAY,
        ROW
    };

    public partial class BlockBayListView : BaseMobileBizView, BlockBayListInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-BlockBayListView";
        static BayRowType _bayRowType = BayRowType.BAY; // to keep data
        private bool _isOnShow = true;

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        private BlockBayListController ThisController
        {
            get { return this.Controller as BlockBayListController; }
        }

        public AsyncAgent AsyncAgent { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public BlockBayListView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();

            this.AsyncAgent = new AsyncAgent(null, true, false);

            this.Controller = new BlockBayListController(this);
            this.AddEventHandler();
            this.InitControls();
        }
        
        private void AddEventHandler()
        {
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.blockList.ItemButton_Clicked += new ItemButtonClickedEventHandler(BlockItemButton_Clicked);
            this.bayList.ItemButton_Clicked += new ItemButtonClickedEventHandler(BayRowItemButton_Clicked);
            this.chbShowAll.CheckedChanged += new EventHandler(chbShowAll_CheckedChanged);

            this.AsyncAgent.CallBackResult += new ResultEventHandler(AsyncAgent_CallBackResult);
            this.AsyncAgent.CallBackError += new FaultEventHandler(AsyncAgent_CallBackError);
        }

        private void InitControls()
        {
            try
            {
                // deleted by YoungOk Kim (2019.12.11) - Mantis 103992: [YQ] 블록 & 베이 창 위치를 고정
                //FormDraggingHandler _formDraggingHdl = new FormDraggingHandler(this.lblTitle, this); // added by YoungOk Kim (2019.05.16) - Mantis 89476: [Tally] 창 이동 기능
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
                this.blockList.ItemList = itemList;

                if (string.IsNullOrEmpty(ThisController.CurrentBlockArea) == false)
                {
                    var item = this.blockList.ItemList.Where(p => p != null && p.Code.Equals(ThisController.CurrentBlockArea)).FirstOrDefault();
                    if (item != null)
                    {
                        this.blockList.SelectItem(ThisController.CurrentBlockArea);
                        SelectBlockAreaItem(ThisController.CurrentBlockArea);

                        ThisController.CurrentBlockArea = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void SetBayRowLabel()
        {
            try
            {
                if (_bayRowType == BayRowType.BAY)
                {
                    lblBay.Text = ResourceFactory.GetResource().GetLabel("WRD_CTMO_Bay");
                }
                else if (_bayRowType == BayRowType.ROW)
                {
                    lblBay.Text = ResourceFactory.GetResource().GetLabel("WRD_CTMO_Row");
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void GetBlockAreaList()
        {
            try
            {
                Expression<Func<BlockBayListController, object>> expression = o => o.DoRetrieveData(this.chbShowAll.CheckState == CheckState.Checked);
                this.AsyncAgent.RunWorkerAsync(new AsyncFuncWorker<BlockBayListController>("Block Bay  List View", ThisController, expression));
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void Clear()
        {
            try
            {
                this.blockList.SelectItem(string.Empty);
                this.bayList.SelectItem(string.Empty);

                if (this.bayList.ItemList != null)
                {
                    this.bayList.ItemList.Clear();
                    this.bayList.RemoveButtons();
                }

                this.btnOk.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void SelectBlockAreaItem(string blockArea)
        {
            try
            {
                if (string.IsNullOrEmpty(blockArea) == false)
                {
                    BaseDataItem blockAreaItem;
                    if (ThisController.IsBlock(blockArea))
                    {
                        blockAreaItem = ThisController.BlockItems.GetBlock(blockArea);

                        btnOk.Enabled = false;
                    }
                    else
                    {
                        blockAreaItem = ThisController.AreaItems.GetArea(blockArea);

                        btnOk.Enabled = true;
                    }
                    ThisController.SelectedBlockAreaItem = blockAreaItem;

                    if (blockAreaItem is BlockItem)
                    {
                        BlockItem blockItem = blockAreaItem as BlockItem;
                        if (blockItem.Facility.Equals(CTBizConstant.BlockType.SC))
                        {
                            _bayRowType = BayRowType.ROW;
                        }
                        else
                        {
                            _bayRowType = BayRowType.BAY;
                        }

                        this.bayList.ItemList = ThisController.GetBayList(blockArea, _bayRowType);

                        if (string.IsNullOrEmpty(ThisController.CurrentBayRow) == false)
                        {
                            var item = this.bayList.ItemList.Where(p => p != null && p.Code.Equals(ThisController.CurrentBayRow)).FirstOrDefault();
                            if (item != null)
                            {
                                this.bayList.SelectItem(ThisController.CurrentBayRow);
                                SelectBayRowItem(ThisController.CurrentBayRow);

                                ThisController.CurrentBayRow = string.Empty;
                            }
                        }
                        else
                        {
                            this.bayList.SelectItem(string.Empty);
                        }
                    } 
                    else
                    {
                        this.bayList.ItemList = null;
                        ThisController.CurrentBayRow = string.Empty;
                        ThisController.SelectedBayRowItem = null;
                    }

                    this.SetBayRowLabel();

                    if (ThisController.IsSimpleCommand)
                    {   
                        if (!ThisController.IsBlock(blockArea) && !_isOnShow)
                        {
                            DialogResult result = DialogResult.Cancel;
                            result = MessageManager.Show("MSG_FTCO_00234", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                            if (result == DialogResult.OK)
                            {
                                this.DialogResult = DialogResult.OK;
                                ThisController.NotifySyncAgent();
                                this.Close();
                            }
                            else
                            {
                                this.DialogResult = DialogResult.Cancel;
                                this.Close();
                            }
                        }
                        _isOnShow = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void SelectBayRowItem(string bayRow)
        {
            try
            {
                if (string.IsNullOrEmpty(bayRow) == false)
                {
                    if (ThisController.SelectedBlockAreaItem is BlockItem)
                    {
                        BaseDataItem bayRowItem = null;

                        BlockItem blockItem = ThisController.SelectedBlockAreaItem as BlockItem;
                        if (blockItem.Facility.Equals(CTBizConstant.BlockType.SC))
                        {
                            bayRowItem = ThisController.RowItemList.Where(p => p != null && p.Block.Equals(blockItem.Name) == true && p.Key.Equals(bayRow) == true).FirstOrDefault();
                        }
                        else
                        {
                            bayRowItem = ThisController.BayItemList.Where(p => p != null && p.Block.Equals(blockItem.Name) == true && p.Key.Equals(bayRow) == true).FirstOrDefault();
                        }
                        ThisController.SelectedBayRowItem = bayRowItem;

                        btnOk.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion METHOD AREA ************************************************

        #region EVENTHANDLER AREA *********************************************

        protected override void OnShown(EventArgs e)
        {
            try
            {
                _isOnShow = true;
                this.Clear();

                if (ThisController.IsSimpleCommand)
                {
                    this.btnOk.Visible = false;
                }

                if (ThisController.IsAlwaysShowAll)
                {
                    this.chbShowAll.Checked = true;
                    this.chbShowAll.Visible = false;
                }

                // added by kimxyuhwan(2023.05.15) 0148989: YQ 로그인 화면의 블럭 선택 화면 개선 요청
                if (ThisController.IsFilterFacility)
                {
                    this.chbShowAll.Checked = true;
                }

                if (this.blockList.ItemList == null)
                {
                    GetBlockAreaList();
                }
                else
                {
                    if (string.IsNullOrEmpty(ThisController.CurrentBlockArea) == false)
                    {
                        var item = this.blockList.ItemList.Where(p => p != null && p.Code.Equals(ThisController.CurrentBlockArea)).FirstOrDefault();
                        if (item != null)
                        {
                            this.blockList.SelectItem(ThisController.CurrentBlockArea);
                            SelectBlockAreaItem(ThisController.CurrentBlockArea);

                            ThisController.CurrentBlockArea = string.Empty;
                        }
                    }
                }

                this.SetBayRowLabel();

                this.btnOk.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
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
                ErrorMessageHandler.Show(ex);
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
                ErrorMessageHandler.Show(ex);
            }
        }

        private void chbShowAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TCheckBox cbx = sender as TCheckBox;
                if (cbx != null)
                {
                    if (ThisController.IsAlwaysShowAll == false)
                    {
                        this.Clear();

                        if (ThisController.EquItem != null)
                        {
                            // modified by YoungOk Kim (2018.09.10) - Mantis 85753: [Location] Cannot uncheck Show all Block/Area checkbox
                            //GetBlockAreaList();
                            var itemList = ThisController.DoRetrieveData(this.chbShowAll.CheckState == CheckState.Checked);
                            if (itemList != null && itemList.Any())
                            {
                                SetItemList(itemList);
                            }
                            else
                            {
                                MessageManager.Show("WRD_CTMO_YardPosition", "MSG_CTMO_00003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.chbShowAll.Checked = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
            }
        }

        private void BlockItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                string blockArea = e.SelectedItemCode.ToString();
                SelectBlockAreaItem(blockArea);
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
            }
        }

        private void BayRowItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                var bayRow = e.SelectedItemCode.ToString();
                SelectBayRowItem(bayRow);

                if (ThisController.IsSimpleCommand)
                {
                    DialogResult result = DialogResult.Cancel;
                    result = MessageManager.Show("MSG_FTCO_00234", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (result == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        ThisController.NotifySyncAgent();
                        this.Close();
                    } 
                    else
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.Show(ex);
            }
        }

        private void AsyncAgent_CallBackResult(object sender, AsyncResultEventArgs e)
        {
            try
            {
                BaseItemsList<ItemListControlItem> itemList = e.ResultObject is BaseItemsList<ItemListControlItem>  ? e.ResultObject as BaseItemsList<ItemListControlItem> : null;
                if (itemList != null && itemList.Count > 0)
                {
                    SetItemList(itemList);
                }
                else
                {
                    if (ThisController.EquItem == null)
                    {
                        // skip
                    }
                    else
                    {
                        this.chbShowAll.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        private void AsyncAgent_CallBackError(object sender, AsyncFaultEventArgs e)
        {
            try
            {
                if (e.Error is TsbBaseException)
                {
                    var ex = e.Error as TsbBaseException;
                    if (ex != null)
                    {
                        ErrorMessageHandler.Show(ex);
                    }
                }
                else
                {
                    ErrorMessageHandler.ErrorLog(e.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        #endregion EVENTHANDLER AREA ******************************************
    }
}
