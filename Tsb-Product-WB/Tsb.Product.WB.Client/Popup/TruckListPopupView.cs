using FarPoint.Win.Spread;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tsb.Catos.Cm.Core.Codes.Param;
using Tsb.Catos.Cm.Win.Grid;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Item;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Security.Encryption;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Win.Grid;
using Tsb.Fontos.Win.Grid.Event;
using Tsb.Fontos.Win.Grid.Spread;
using Tsb.Fontos.Win.Menu.ContextMenu;
using Tsb.Fontos.Win.Message;
using Tsb.Product.WB.Client.WeightBridge;
using Tsb.Product.WB.Common.Item.Popup;
using Tsb.Product.WB.Common.Item.Sample;
using Tsb.Product.WB.Common.Item.WeightBridge;
using Tsb.Product.WB.Common.Param.Popup;
using Tsb.Product.WB.Common.Param.Sample;

namespace Tsb.Most.Wb.Client.Popup
{
    public partial class TruckListPopupView : BaseSingleGridView, TruckListPopupInterface
    {
        #region FIELDS/READONLY AREA ********************************
        private TSpreadGrid _spreadGrid;
        private Color _gridForeColor;
        private Color _gridBackColor;
        #endregion

        #region Initialize
        public TruckListPopupView()
        {
            InitializeComponent();
            InitViewCreate();
        }
        public TruckListPopupView(Form mdiPrnt)
        {
            InitializeComponent();
            this.InitViewCreate(mdiPrnt);
        }
        #region PROPERTY/DELEGATE AREA ******************************
        /// <summary>
        /// Gets FormsName.
        /// </summary>
        public string FormName { get { return this.Name; } }

        /// <summary>
        /// Gets or sets SearchParam.
        /// </summary>
        public TruckListPopupParam SearchParam { get; set; }

        /// <summary>
        /// Occurs before GridRow removed.
        /// </summary>
        public event SpreadGridRowRemovedHandler GridRowRemoved;

        /// <summary>
        /// Gets or sets ActiveItem.
        /// </summary>
        public TruckListPopupItem ActiveItem { get; set; }

        /// <summary>
        /// Gets or sets GridForeColor.
        /// </summary>
        public Color GridForeColor
        {
            get { return this._gridForeColor; }
            set
            {
                this._gridForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets GridBackColor.
        /// </summary>
        public Color GridBackColor
        {
            get { return this._gridBackColor; }
            set
            {
                this._gridBackColor = value;
            }
        }

        TruckListPopupItem TruckListPopupInterface.ActiveItem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion


        /// <summary>
        /// Initialize Form
        /// </summary>
        /// <param name="mdiPrnt">Parent Form</param>
        private void InitViewCreate(Form mdiPrnt)
        {
            
            this.MdiParent = mdiPrnt;
            this.Grid = this.grd_WB_TruckListPopupGrid;
            this.Controller = new TruckListPopupController(this);

            this._spreadGrid = this.grd_WB_TruckListPopupGrid;
            this.SearchParam = new TruckListPopupParam(this);

            this.Load += TruckListPopupView_Load;

          

        }

        private void InitViewCreate()
        {
            this.Grid = this.grd_WB_TruckListPopupGrid;
            this.Controller = new TruckListPopupController(this);

            this._spreadGrid = this.grd_WB_TruckListPopupGrid;
            this.SearchParam = new TruckListPopupParam(this);

            this.Load += TruckListPopupView_Load;
        }

        private void TruckListPopupView_Load(object sender, EventArgs e)
        {
            this.SetBindingControls();
            this.AddEventHandler();
        }
        #endregion

        #region EVENT HANDLER AREA **********************************
        /// <summary>
        /// Occurs when Enterkey is Pressed in SpreadGrid's cell.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        private void SpreadGrid_GridCellEnterKeyPressed(object sender, GridCellEnterKeyPressedEventArgs e)
        {
            if (this._spreadGrid.ActiveSheet.ActiveCell.Locked)
            {
                return;
            }

            bool IsBizRuleColumn = false;
            CodeGeneralParam codeDataParam = null;
            CTGridBizRule bizRule = null;

            try
            {
                bizRule = new CTGridBizRule(this._spreadGrid, this.MdiParent);
                codeDataParam = new CodeGeneralParam(this);

                IsBizRuleColumn = bizRule.HandleGridCellEnterKeyPressed(codeDataParam, e);

                if (!IsBizRuleColumn)
                {
                    ContextMenuHelper.GetInstance().Show(Cursor.Position);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return;
        }

        /// <summary>
        /// Occurs when mouse's right button is clicked in SpreadGrid's cell.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        private void SpreadGrid_GridMouseRightClick(object sender, GridMouseRightClickEventArgs e)
        {
            if (this._spreadGrid.ActiveSheet.ActiveCell.Locked)
            {
                return;
            }

            bool IsBizRuleColumn = false;
            CodeGeneralParam codeDataParam = null;
            CTGridBizRule bizRule = null;

            try
            {
                bizRule = new CTGridBizRule(this._spreadGrid, this.MdiParent);
                codeDataParam = new CodeGeneralParam(this);

                IsBizRuleColumn = bizRule.HandleGridRightClick(codeDataParam, e);

                if (!IsBizRuleColumn)
                {
                    ContextMenuHelper.GetInstance().Show(Cursor.Position);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }

            return;
        }


        private void SpreadGrid_GridMouseDoubleClick(object sender, GridMouseDoubleClickEventArgs e)
        {
            WeightInfoItem curInList = (WeightInfoItem)SpreadGridUtil.GetSelectedRowDataItemObject(this._spreadGrid);
            String formName = "WBMainView";
            foreach (Form frm in Application.OpenForms)
            {
                if (formName == frm.Name)
                {
                    ((WBMainView)frm).setLorryValue(curInList);
                    this.Close();
                    break;
                }
            }          

            
        }

        /// <summary>
        /// SpreadSheet's selection changed event handler
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        public void SpreadGrid_SelectionChangedSpdGrid(object sender, SelectionChangedEventArgs e)
        {
            this.ActiveGridChanged(sender);
        }

        private void ActiveGridChanged(object sender)
        {
            TSpreadGrid currentSpreadGrid = sender as TSpreadGrid;
            int rowIndex = SpreadGridUtil.GetActiveDataRowIndex(this._spreadGrid.ActiveSheet);

            if ((_spreadGrid.DataSource == null) || (rowIndex < 0))
            {
                return;
            }

            TruckListPopupItem item = this.SourceItemList[rowIndex] as TruckListPopupItem;

            if (item != null)
            {
                this.ActiveItem = item;
                Debug.WriteLine("rowIndex : " + rowIndex );
            }
        }
        #endregion

        #region METHOD AREA *****************************************
        /// Adds Event Handler
        private void AddEventHandler()
        {
           // this._spreadGrid.SelectionChanged += new SelectionChangedEventHandler(SpreadGrid_SelectionChangedSpdGrid);
            this._spreadGrid.GridMouseDoubleClick += new GridMouseDoubleClickHandler(SpreadGrid_GridMouseDoubleClick);
            //  this._spreadGrid.GridCellEnterKeyPressed += new GridCellEnterKeyPressedHandler(SpreadGrid_GridCellEnterKeyPressed);
            //if (this.GridRowRemoved != null)
            //{
            //    this._spreadGrid.SpreadGridRowRemoved += this.GridRowRemoved; // It is used by Controller.
            //}

            this.tbxLorryNo.KeyPress  += new System.Windows.Forms.KeyPressEventHandler(this.tbxLorryNo_KeyPress); //new System.Windows.Forms.KeyPress
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            
           // 1 this.btnShowMsg.Click += BtnShowMsg_Click;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                (this.Controller as TruckListPopupController).DoRetrieveData();
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", null));
            }

        }


        private void BtnShowMsg_Click(object sender, EventArgs e)
        {
            MessageManager.Show("MSG_MSED_00001", MessageBoxButtons.OK, MessageBoxIcon.Information, DefaultMessage.NON_REG_WRD + "Message!");
            //메세지 출력후 Object Id Error로깅
            //Exception ex = new Exception("Custom Exception");
            //MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
        }

        /// <summary>
        /// Search Parameter of Control Binding 
        /// </summary>
        private void SetBindingControls()
        {
            bdsSearchParam.DataSource = typeof(TruckListPopupParam);

            tbxLorryNo.CreateBinding<TruckListPopupParam>(bdsSearchParam);
    

            bdsSearchParam.DataSource = this.SearchParam;
        }
        #endregion

        #region SingleGridInterface Implements AREA *****************
        /// <summary>
        /// Before Search, Mandatory Check.
        /// </summary>
        /// <returns>True or fase after Manadatory Check</returns>
        public bool SearchMandatoryCheck()
        {
            this.Validate();

            string strMandatory =  base.MandatoryCheck(gbxDisplayOption);

            if (!string.IsNullOrEmpty(strMandatory))
            {
                MessageManager.Show("MSG_CTCM_00000", MessageBoxButtons.OK, MessageBoxIcon.Warning, DefaultMessage.NON_REG_WRD + strMandatory);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Mandatory Check Function
        /// </summary>
        /// <returns>Message String</returns>
        public string SaveMandatoryCheck()
        {
            return this._spreadGrid.GridHelper.CheckMandatory();
        }

        /// <summary>
        /// Call to Toolbar of Delete
        /// </summary>
        public void DeleteRow(object sender, EventArgs e)
        {
            this._spreadGrid.DeleteRow();
        }

        /// <summary>
        /// Refresh SpreadGrid
        /// </summary>
        public void SpreadRefresh()
        {
            this._spreadGrid.DataSource = null;
        }
        #endregion

        private void gbxDisplayOption_Enter(object sender, EventArgs e)
        {

        }

        private void tbxLorryNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
    }
}
