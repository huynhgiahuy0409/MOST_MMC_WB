using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Tsb.Fontos.Win.FormTemplate;
using System.Diagnostics;
using System.Collections;
using Tsb.Fontos.Win.Grid.Schema;
using System.Linq;
using Tsb.Fontos.Win.Grid.Event;
using Tsb.Catos.Cm.Core.Codes.Param;
using Tsb.Catos.Cm.Win.Grid;
using Tsb.Fontos.Win.Menu.ContextMenu;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Win.Message;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Win.Grid.Spread;
using FarPoint.Win.Spread;
using Tsb.Fontos.Win.Preview;
using FarPoint.Win.Spread.Model;
using Tsb.Fontos.Win.Usage;
using Tsb.Fontos.Core.Usage.Enum;
using Tsb.Product.WB.Common.Param.WeightBridge;
using Tsb.Product.WB.Common.Item.WeightBridge;
using System.Drawing;
using System.Linq.Expressions;
using Tsb.Fontos.Win.Forms;
using Tsb.Most.Wb.Client.Popup;
using Tsb.Fontos.Core.Param;
using Tsb.Product.WB.Common.Info;
using Tsb.Product.WB.Common.ServiceSpec;
using static Tsb.Product.WB.Common.Constance.WeightBridgeConstance;
using Tsb.Fontos.Core.Environments;
using System.IO.Ports;

namespace Tsb.Product.WB.Client.WeightBridge
{
    public partial class WBMainView : BaseSingleGridView, MainSingleGridInterface
    {
        #region FIELDS/READONLY AREA *************************************
        private TSpreadGrid _spreadGrid;
        private Color _gridForeColor;
        private Color _gridBackColor;
        private readonly string VALUE_COLUMN_NAME = "ValueColor";
        #endregion

        #region PROPERTY/DELEGATE AREA *************************************
      
        public string EnableString { get; set; }
        public string StaffCd { get { return AppEnv.UserInfo.StaffCD; } }
        public event SpreadGridRowRemovedHandler GridRowRemoved;
        public MainSingleGridItem ActiveItem { get; set; }
        private WeightInfoItem _weightInfoItem = null;
        public WeightInfoItem WeightInfoItem { 
            get
            {
                return _weightInfoItem;
            }
            set
            {
                this._weightInfoItem = value;
                this.bdsWeightInfo.DataSource = this._weightInfoItem;
            }
        }
        public MainParam MainParam { get; set; }
        public MainParam MainGridParam { get; set; }
        public JobParam JobParam { get; set; }
        public WeightBridgeParam WeightBridgeParam { get; set; }
        public WeightBridgeParam WeightBridgeGridParam { get; set; }
        public WeightInfoItem CurWeightInfoItem { get; set; }
        private SerialPort serialPort;
        #endregion

        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize Form
        /// </summary>
        public WBMainView() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize Form
        /// </summary>
        /// <param name="mdiPrnt">Parent Form</param>
        public WBMainView(Form mdiPrnt)
            : base()
        {
            this.InitViewCreate(mdiPrnt);
            this.IsDockView = true;
            this.DockBorderStyle =Tsb.Fontos.Win.Docking.Enums.DockBorderStyle.Sizable;
            this.DockAutoScroll = true;
            this.AllowedDockStyle =Tsb.Fontos.Win.Docking.Enums.AllowedDock.All;
        }

        //// <summary>
        /// Initialize Form
        /// </summary>
        /// <param name="mdiPrnt">Parent Form</param>
        private void InitViewCreate(Form mdiPrnt)
        {
            InitializeComponent();
            InitializeSerialPort();
            ProcessUIAfterInitComponent();
            this.MdiParent = mdiPrnt;
            this.Grid = this.grd_WB_WeightInfoGrid;
            /* Set GATE to UI*/
            this.tbxLane.Text = MTCommonInfo.GetInstance().LaneCode;
            this.tbxGatePoint.Text = MTCommonInfo.GetInstance().GateCode;
            this.lblUsername.Text = this.StaffCd;
            this.Controller = new MainController(this);
            this.MainParam = new MainParam(this);
            this.MainGridParam = new MainParam(this);
            this.JobParam = new JobParam(this);
            this.WeightBridgeParam = new WeightBridgeParam(this);
            this.WeightInfoItem = new WeightInfoItem();
            this.SetBindingControls();
            this.AddEventHandler();
        }
        private void InitializeSerialPort()
        {
            serialPort = new SerialPort
            {
                PortName = "COM1",
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None
            };

            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }
        #endregion

        #region EVENT HANDLER AREA *****************************
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadExisting();
            this.Invoke(new MethodInvoker(delegate
            {
                this.tbxReadWeight.Text = data;
            }));
        }

        /// <summary>
        /// Occurs when the cliboard is pasted in Grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadGrid_GridClipboardPasted(object sender, GridClipboardPastedEventArgs e)
        {
            CellRange cellRange = null;

            try
            {
                cellRange = e.CellRange;

                for (int rowIndex = cellRange.Row; rowIndex < cellRange.Row + cellRange.RowCount; rowIndex++)
                {
                    for (int colIndex = cellRange.Column; colIndex < cellRange.Column + cellRange.ColumnCount; colIndex++)
                    {
                        SpreadGridUtil.SetCellBackColor(this._spreadGrid, rowIndex, colIndex, Color.Crimson);
                    }
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
        /// Occurs when Form is loaded
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        private void Form_Load(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.EnableString) &&
            //    this.EnableString == "N")
            //{
            //    this.tbxUnno.Enabled = false;
            //}
        }

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
                    ContextMenuHelper.GetInstance().Show(Cursor.Position, true, this.ActiveItem);
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
        /// SpreadSheet's selection changed event handler
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        public void SpreadGrid_SelectionChangedSpdGrid(object sender, SelectionChangedEventArgs e)
        {
            this.ActiveGridChanged(sender);
        }

        /// <summary>
        /// Occurs when Active Grid is changed.
        /// </summary>
        /// <param name="sender">event source</param>
        private void ActiveGridChanged(object sender)
        {
            TSpreadGrid currentSpreadGrid = sender as TSpreadGrid;
            int rowIndex = SpreadGridUtil.GetActiveDataRowIndex(this._spreadGrid.ActiveSheet);

            if ((_spreadGrid.DataSource == null) || (rowIndex < 0))
            {
                return;
            }

            MainSingleGridItem item = this.SourceItemList[rowIndex] as MainSingleGridItem;

            if (item != null)
            {
                this.ActiveItem = item;
                Debug.WriteLine("rowIndex : " + rowIndex + ", Unid : " + item.Unid);
            }
        }

        /// <summary>
        /// Occurs when btnApply is clicked.
        /// </summary>
        /// <param name="sender">event source</param>
        /// <param name="e">Event args containg event data</param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            //string printProPerty = "[Scale Mode]:" + this.cmbScaleMode.Text;
            //this._spreadGrid.GetPrintPageProperty().ScaleMode = this.GetScaleMode();
            //this._spreadGrid.GetPrintPageProperty().CustomScale = this.GetCustomScale();
            //this._spreadGrid.GetPrintPageProperty().DefaultExportFileName = "Single Grid View";
            //MessageManager.Show("MSG_CTCM_00000", MessageBoxButtons.OK, MessageBoxIcon.Information, DefaultMessage.NON_REG_WRD + printProPerty);
        }
        private void btnTruckListSearch_Click(object sender, EventArgs e)
        {
            MainController controller = this.Controller as MainController;
            controller.DoRetrieveGridData();
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            MainController controller = this.Controller as MainController;
            controller.DoRetrieveWeightInfo();
        }

        private void btnRemarkUpdate_Click(object sender, EventArgs e)
        {
            MainController controller = this.Controller as MainController;
            controller.DoUpdateRemark();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.tbxRmk.Text = "";
        }

        private void btnReadWeight_Click(object sender, EventArgs e)
        {
            if(!this.CurWeightInfoCheck()) return;
            string weightString = this.tbxReadWeight.Text;
            decimal weightDecimal = Decimal.Parse(weightString);
            decimal value = weightDecimal;
            if (ProductWBServiceSpec.APP_MASS_UNIT.Equals(MassUnit.KG))
            {
                value = Math.Round(weightDecimal);
            }
            this.tbxKG.Text = value + "";
        }

        private void tbxReadWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            TTextBox textBox = sender as TTextBox;
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == '.' && !((TextBox)sender).Text.Contains("."))
            {
                return;
            }
            e.Handled = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MainController controller = this.Controller as MainController;
            controller.DoConfirmWeight();
        }

        private void tButton6_Click(object sender, EventArgs e)
        {
            TForm truckList = new TruckListPopupView();
            truckList.Show();
        }
        //private void WeightInfoGrid_GridMouseDoubleClick(object sender, GridMouseDoubleClickEventArgs e)
        //{
        //    MainController controller = this.Controller as MainController;
        //    WeightInfoItem sltItem = e.CurrDataItem as WeightInfoItem;
        //    controller.DoSelectGridItem(sltItem);
        //}
        private void WeightInfoGrid_GridActiveCellChanged(object sender, GridActiveCellChangedEventArg e)
        {
            MainController controller = this.Controller as MainController;
            WeightInfoItem sltItem = e.CurrDataItem as WeightInfoItem;
            controller.DoSelectGridItem(sltItem);
        }
        #endregion

        #region METHOD AREA *************************************
        /// <summary>
        /// Adds Event Handler
        /// </summary>
        private void AddEventHandler()
        {
            //this._spreadGrid.SelectionChanged += new SelectionChangedEventHandler(this.SpreadGrid_SelectionChangedSpdGrid);
            //this._spreadGrid.GridMouseRightClick += new GridMouseRightClickHandler(this.SpreadGrid_GridMouseRightClick);
            //this._spreadGrid.GridCellEnterKeyPressed += new GridCellEnterKeyPressedHandler(this.SpreadGrid_GridCellEnterKeyPressed);
            //this._spreadGrid.GridClipboardPasted += new GridClipboardPastedHandler(this.SpreadGrid_GridClipboardPasted);
            //this.grd_WB_WeightInfoGrid.GridMouseDoubleClick += new GridMouseDoubleClickHandler(this.WeightInfoGrid_GridMouseDoubleClick);
            this.grd_WB_WeightInfoGrid.GridActiveCellChanged += new GridActiveCellChangedHandler(this.WeightInfoGrid_GridActiveCellChanged);
            this.btnSearchLorry.Click += new System.EventHandler(this.tButton6_Click);
            //this.btnStartEnd.Click += BtnStartEnd_Click;
            //this.btnImmediately.Click += BtnImmediately_Click;
            this.btnPrintCIR.Click += new System.EventHandler(this.btnPrintCIR_Click);
            this.btnCancelJob.Click += new System.EventHandler(this.btnCancelJob_Click);
            if (this.GridRowRemoved != null)
            {
                this._spreadGrid.SpreadGridRowRemoved += this.GridRowRemoved; // It is used by Controller.
            }

            //this.btnApply.Click += new EventHandler(btnApply_Click);
            this.Load += new EventHandler(Form_Load);
        }
        public void ProcessUIAfterInitComponent()
        {
            this.dtpWeightFrom.Top = (this.tPanel4.ClientSize.Height - this.dtpWeightFrom.Height) / 2;
            this.dtpWeightTo.Top = (this.tPanel5.ClientSize.Height - this.dtpWeightTo.Height) / 2;
            this.tbxSearchGridLorryNo.Top = (this.tPanel1.ClientSize.Height - this.tbxSearchGridLorryNo.Height) / 2;

            this.btnConfirm.Top = (this.tPanel12.ClientSize.Height - this.btnConfirm.Height) / 2;
            this.btnPrintCIR.Top = (this.tPanel12.ClientSize.Height - this.btnPrintCIR.Height) / 2;
            this.btnCancelJob.Top = (this.tPanel12.ClientSize.Height - this.btnCancelJob.Height) / 2;
            this.tbtnSearchDoc.Top = (this.tPanel12.ClientSize.Height - this.tbtnSearchDoc.Height) / 2;

            this.tGroupBox8.Top = (this.tPanel2.ClientSize.Height - this.tGroupBox8.Height) / 2;
            this.tlbScanHere.Top = (this.tPanel3.ClientSize.Height - this.tlbScanHere.Height) / 2;
            this.btnSearchDoc.Top = (this.tPanel3.ClientSize.Height - this.btnSearchDoc.Height) / 2;

            this.tbxQRCd.Top = (this.tPanel3.ClientSize.Height - this.tbxQRCd.Height) / 2;

            this.tlbGatePoint.Top = (this.tPanel6.ClientSize.Height - this.tlbGatePoint.Height) / 2;
            this.tlbGatePoint.Top = (this.tPanel6.ClientSize.Height - this.tlbGatePoint.Height) / 2;
            this.tlbGateLane.Top = (this.tPanel6.ClientSize.Height - this.tlbGateLane.Height) / 2;
            this.tlbGateLane.Top = (this.tPanel6.ClientSize.Height - this.tlbGateLane.Height) / 2;

            this.tlbFirstWeight.Top = (this.tPanel10.ClientSize.Height - this.tlbFirstWeight.Height) / 2;
            this.tbxFirstWgt.Top = (this.tPanel10.ClientSize.Height - this.tbxFirstWgt.Height) / 2;
            this.tlbSecondWeight.Top = (this.tPanel10.ClientSize.Height - this.tlbSecondWeight.Height) / 2;
            this.tbxSecondWgt.Top = (this.tPanel11.ClientSize.Height - this.tbxSecondWgt.Height) / 2;
            this.tlbCargoWeight.Top = (this.tPanel13.ClientSize.Height - this.tlbCargoWeight.Height) / 2;
            this.tbxCargoWeight.Top = (this.tPanel13.ClientSize.Height - this.tbxCargoWeight.Height) / 2;
            this.tPanel14.Top = (this.tPanel15.ClientSize.Height - this.tPanel14.Height) / 2;
            this.tbxRmk.Top = (this.tPanel15.ClientSize.Height - this.tbxRmk.Height) / 2;
        }

        /// <summary>
        /// Search Parameter of Control Binding.
        /// </summary>
        private void SetBindingControls()
        {
            /* MainParam binding - Search Grid*/
            bdsMainGridParam.DataSource = typeof(MainParam);
            this.tbxSearchGridLorryNo.CreateBinding<object>(bdsMainGridParam);
            this.dtpWeightFrom.CreateBinding<object>(bdsMainGridParam);
            this.dtpWeightTo.CreateBinding<object>(bdsMainGridParam);
            bdsMainGridParam.DataSource = this.MainGridParam;

            /* MainParam binding - QR Code*/
            bdsMainParam.DataSource = typeof(MainParam);
            this.tbxQRCd.CreateBinding<object>(bdsMainParam);
            bdsMainParam.DataSource = this.MainParam;

            /* WeightInfoItem binding */
            bdsWeightInfo.DataSource = typeof(WeightInfoItem);
            this.tbxLorryNo.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxChassisNo.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxRmk.CreateBinding<WeightInfoItem>(bdsWeightInfo, val => val.Rmk);
            this.tbxReadWeight.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxKG.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxVslCallId.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxShipgNoteNo.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxGrNo.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxBlNo.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxSdoNo.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxCmdtDesc.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxCnsnShprNm.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxTransporterNm.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxFirstWgt.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxSecondWgt.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            this.tbxCargoWeight.CreateBinding<WeightInfoItem>(bdsWeightInfo);
            bdsWeightInfo.DataSource = this.WeightInfoItem;
        }
        public void setLorryValue(WeightInfoItem weightInfoItem)
        {
            MainController controller = this.Controller as MainController;
            controller.setLorryValue(weightInfoItem);
        }
        #endregion

        #region MainSingleGridInterface Implements AREA *****************
        /// <summary>
        /// Before Search, Mandatory Check.
        /// </summary>
        /// <returns>True or fase after Manadatory Check</returns>
        public bool SearchMandatoryCheck()
        {
            this.Validate();

            string strMandatory = base.MandatoryCheck(tPanel3);

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
        public bool WeightMandatoryCheck()
        {
            this.Validate();

            string strMandatory = base.MandatoryCheck(tPanel16);

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
        public bool CurWeightInfoCheck()
        {
                
            if (this.CurWeightInfoItem == null)
            {
                MessageManager.Show("MSG_PTWB_NoRecordForSearch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
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
        /// Refresh SpreadGrid
        /// </summary>
        public void SpreadRefresh()
        {
            this._spreadGrid.DataSource = null;
        }

        private void btnPrintCIR_Click(object sender, EventArgs e)
        {
            (this.Controller as MainController).DoPreviewReport();         
        }

        private void btnCancelJob_Click(object sender, EventArgs e)
        {
            (this.Controller as MainController).DoCancelJob();
        }
        #endregion

    }
}
