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

namespace Tsb.Catos.Cm.Mobile.Win.BlockList
{
    public enum BayRowType
    {
        BAY,
        ROW
    };

    public partial class BlockListView : BaseMobileBizView, BlockListInterface
    {
        #region CONST & FIELD AREA ********************************************

        private readonly string OBJECT_ID = "VIW-CT-CTMO-CTRL-BlockListView";
        static BayRowType _bayRowType = BayRowType.BAY; // to keep data

        #endregion CONST & FIELD AREA *****************************************

        #region PROPERTY AREA *************************************************

        public string FormName
        {
            get { return Name; }
        }

        private BlockListController ThisController
        {
            get { return this.Controller as BlockListController; }
        }

        public AsyncAgent AsyncAgent { get; set; }

        #endregion PROPERTY AREA **********************************************

        #region INITIALIZATION AREA *******************************************

        public BlockListView()
        {
            ObjectID = OBJECT_ID;
            InitializeComponent();

            this.AsyncAgent = new AsyncAgent(null, true, false);

            this.Controller = new BlockListController(this);
            this.AddEventHandler();
            this.InitControls();
        }
        
        private void AddEventHandler()
        {
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.blockList.ItemButton_Clicked += new ItemButtonClickedEventHandler(BlockItemButton_Clicked);

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
                        SelectBlockItem(ThisController.CurrentBlockArea);

                        ThisController.CurrentBlockArea = string.Empty;
                    }
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
                Expression<Func<BlockListController, object>> expression = o => o.DoRetrieveData(true);
                this.AsyncAgent.RunWorkerAsync(new AsyncFuncWorker<BlockListController>("Block Bay  List View", ThisController, expression));
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
                this.btnOk.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
        }

        public void SelectBlockItem(string block)
        {
            try
            {
                if (string.IsNullOrEmpty(block) == false)
                {
                    BaseDataItem blockItem;
                    if (ThisController.IsBlock(block))
                    {
                        blockItem = ThisController.BlockItems.GetBlock(block);
                        btnOk.Enabled = true;
                    }
                    else
                    {
                        return;
                    }
                    ThisController.SelectedBlockAreaItem = blockItem;

                    if (blockItem is BlockItem)
                    {
                        if ((blockItem as BlockItem).Facility.Equals(CTBizConstant.BlockType.SC))
                        {
                            _bayRowType = BayRowType.ROW;
                        }
                        else
                        {
                            _bayRowType = BayRowType.BAY;
                        }
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
                this.Clear();

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
                            SelectBlockItem(ThisController.CurrentBlockArea);

                            ThisController.CurrentBlockArea = string.Empty;
                        }
                    }
                }

                //this.btnOk.Enabled = false;
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
                //ThisController.NotifySyncAgent();

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

        private void BlockItemButton_Clicked(object sender, ItemListViewEventArgs e)
        {
            try
            {
                string blockArea = e.SelectedItemCode.ToString();
                SelectBlockItem(blockArea);
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
