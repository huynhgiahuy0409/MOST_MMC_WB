using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace Tsb.Product.WB.Client.WeightBridge
{
    partial class WBMainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer enhancedFocusIndicatorRenderer2 = new FarPoint.Win.Spread.EnhancedFocusIndicatorRenderer();
            this.tbxLorryNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxVslCallId = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxShipgNoteNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxGrNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxSdoNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxCmdtDesc = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxCnsnShprNm = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxTransporterNm = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxBlNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tbxSecondWgt = new Tsb.Fontos.Win.Forms.TTextBox();
            this.bdsToolbar = new System.Windows.Forms.BindingSource(this.components);
            this.tTableLayoutPanel1 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tTableLayoutPanel2 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tGroupBox1 = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tTableLayoutPanel9 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tPanel7 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tPanel8 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tPanel16 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tLabel9 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tbxKG = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tPanel15 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tbxRmk = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tPanel14 = new Tsb.Fontos.Win.Forms.TPanel();
            this.btnClear = new Tsb.Fontos.Win.Forms.TButton();
            this.btnRemarkUpdate = new Tsb.Fontos.Win.Forms.TButton();
            this.tbxReadWeight = new Tsb.Fontos.Win.Forms.TTextBox();
            this.btnReadWeight = new Tsb.Fontos.Win.Forms.TButton();
            this.tLabel10 = new Tsb.Fontos.Win.Forms.TLabel();
            this.btnSearchLorry = new Tsb.Fontos.Win.Forms.TButton();
            this.tbxChassisNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tLabel8 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tLabel5 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tGroupBox3 = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tTableLayoutPanel6 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tGroupBox7 = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tGroupBox5 = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tTableLayoutPanel5 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tGroupBox2 = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tLabel18 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tLabel17 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tLabel16 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tLabel15 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tLabel14 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tLabel13 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tLabel12 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tLabel11 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tGroupBox6 = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tTableLayoutPanel10 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tPanel13 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tbxCargoWeight = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tlbCargoWeight = new Tsb.Fontos.Win.Forms.TLabel();
            this.tPanel11 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tlbSecondWeight = new Tsb.Fontos.Win.Forms.TLabel();
            this.tPanel10 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tbxFirstWgt = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tlbFirstWeight = new Tsb.Fontos.Win.Forms.TLabel();
            this.tTableLayoutPanel3 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tTableLayoutPanel4 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tGroupBox4 = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tTableLayoutPanel7 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tPanel1 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tbxSearchGridLorryNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tLabel4 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tPanel5 = new Tsb.Fontos.Win.Forms.TPanel();
            this.dtpWeightTo = new Tsb.Fontos.Win.Forms.TDateTimePicker();
            this.tLabel2 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tPanel4 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tLabel1 = new Tsb.Fontos.Win.Forms.TLabel();
            this.dtpWeightFrom = new Tsb.Fontos.Win.Forms.TDateTimePicker();
            this.tPanel12 = new Tsb.Fontos.Win.Forms.TPanel();
            this.btnCancelJob = new Tsb.Fontos.Win.Forms.TButton();
            this.btnPrintCIR = new Tsb.Fontos.Win.Forms.TButton();
            this.btnConfirm = new Tsb.Fontos.Win.Forms.TButton();
            this.tbtnSearchDoc = new Tsb.Fontos.Win.Forms.TButton();
            this.tPanel2 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tGroupBox8 = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tLabel3 = new Tsb.Fontos.Win.Forms.TLabel();
            this.tPanel9 = new Tsb.Fontos.Win.Forms.TPanel();
            this.grd_WB_WeightInfoGrid = new Tsb.Fontos.Win.Grid.Spread.TSpreadGrid();
            this.grd_WB_WeightInfoGrid_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tTableLayoutPanel8 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tPanel3 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tlbScanHere = new Tsb.Fontos.Win.Forms.TLabel();
            this.btnSearchDoc = new Tsb.Fontos.Win.Forms.TButton();
            this.tbxQRCd = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tPanel6 = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxLane = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tlbGateLane = new Tsb.Fontos.Win.Forms.TLabel();
            this.tlbGatePoint = new Tsb.Fontos.Win.Forms.TLabel();
            this.tbxGatePoint = new Tsb.Fontos.Win.Forms.TTextBox();
            this.bdsSearchParam = new System.Windows.Forms.BindingSource(this.components);
            this.bdsMainParam = new System.Windows.Forms.BindingSource(this.components);
            this.bdsMainGridParam = new System.Windows.Forms.BindingSource(this.components);
            this.bdsWeightInfo = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bdsToolbar)).BeginInit();
            this.tTableLayoutPanel1.SuspendLayout();
            this.tTableLayoutPanel2.SuspendLayout();
            this.tGroupBox1.SuspendLayout();
            this.tTableLayoutPanel9.SuspendLayout();
            this.tPanel8.SuspendLayout();
            this.tPanel16.SuspendLayout();
            this.tPanel15.SuspendLayout();
            this.tPanel14.SuspendLayout();
            this.tGroupBox3.SuspendLayout();
            this.tTableLayoutPanel6.SuspendLayout();
            this.tTableLayoutPanel5.SuspendLayout();
            this.tGroupBox2.SuspendLayout();
            this.tGroupBox6.SuspendLayout();
            this.tTableLayoutPanel10.SuspendLayout();
            this.tPanel13.SuspendLayout();
            this.tPanel11.SuspendLayout();
            this.tPanel10.SuspendLayout();
            this.tTableLayoutPanel3.SuspendLayout();
            this.tTableLayoutPanel4.SuspendLayout();
            this.tGroupBox4.SuspendLayout();
            this.tTableLayoutPanel7.SuspendLayout();
            this.tPanel1.SuspendLayout();
            this.tPanel5.SuspendLayout();
            this.tPanel4.SuspendLayout();
            this.tPanel12.SuspendLayout();
            this.tPanel2.SuspendLayout();
            this.tPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_WB_WeightInfoGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_WB_WeightInfoGrid_Sheet1)).BeginInit();
            this.tTableLayoutPanel8.SuspendLayout();
            this.tPanel3.SuspendLayout();
            this.tPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSearchParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMainParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMainGridParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsWeightInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxLorryNo
            // 
            this.tbxLorryNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxLorryNo.CustomMask = null;
            this.tbxLorryNo.Editable = false;
            this.tbxLorryNo.LinkedLabelName = null;
            this.tbxLorryNo.Location = new System.Drawing.Point(81, 4);
            this.tbxLorryNo.Name = "tbxLorryNo";
            this.tbxLorryNo.ReadOnly = true;
            this.tbxLorryNo.Size = new System.Drawing.Size(167, 22);
            this.tbxLorryNo.TabIndex = 1;
            this.tbxLorryNo.TextResourceKey = null;
            this.tbxLorryNo.UseTextMandatoryFont = false;
            // 
            // tbxVslCallId
            // 
            this.tbxVslCallId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxVslCallId.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxVslCallId.CustomMask = null;
            this.tbxVslCallId.Editable = false;
            this.tbxVslCallId.LinkedLabelName = null;
            this.tbxVslCallId.Location = new System.Drawing.Point(180, 29);
            this.tbxVslCallId.Name = "tbxVslCallId";
            this.tbxVslCallId.Size = new System.Drawing.Size(161, 22);
            this.tbxVslCallId.TabIndex = 3;
            this.tbxVslCallId.TextResourceKey = null;
            this.tbxVslCallId.UseTextMandatoryFont = false;
            // 
            // tbxShipgNoteNo
            // 
            this.tbxShipgNoteNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxShipgNoteNo.CustomMask = null;
            this.tbxShipgNoteNo.Editable = false;
            this.tbxShipgNoteNo.LinkedLabelName = null;
            this.tbxShipgNoteNo.Location = new System.Drawing.Point(180, 60);
            this.tbxShipgNoteNo.Name = "tbxShipgNoteNo";
            this.tbxShipgNoteNo.ShortcutsEnabled = false;
            this.tbxShipgNoteNo.Size = new System.Drawing.Size(161, 22);
            this.tbxShipgNoteNo.TabIndex = 5;
            this.tbxShipgNoteNo.TextResourceKey = null;
            this.tbxShipgNoteNo.UseTextMandatoryFont = false;
            // 
            // tbxGrNo
            // 
            this.tbxGrNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxGrNo.CustomMask = null;
            this.tbxGrNo.Editable = false;
            this.tbxGrNo.LinkedLabelName = null;
            this.tbxGrNo.Location = new System.Drawing.Point(180, 93);
            this.tbxGrNo.Name = "tbxGrNo";
            this.tbxGrNo.Size = new System.Drawing.Size(161, 22);
            this.tbxGrNo.TabIndex = 7;
            this.tbxGrNo.TextResourceKey = null;
            this.tbxGrNo.UseTextMandatoryFont = false;
            // 
            // tbxSdoNo
            // 
            this.tbxSdoNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxSdoNo.CustomMask = null;
            this.tbxSdoNo.Editable = false;
            this.tbxSdoNo.LinkedLabelName = null;
            this.tbxSdoNo.Location = new System.Drawing.Point(540, 93);
            this.tbxSdoNo.Name = "tbxSdoNo";
            this.tbxSdoNo.Size = new System.Drawing.Size(155, 22);
            this.tbxSdoNo.TabIndex = 11;
            this.tbxSdoNo.TextResourceKey = null;
            this.tbxSdoNo.UseTextMandatoryFont = false;
            // 
            // tbxCmdtDesc
            // 
            this.tbxCmdtDesc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxCmdtDesc.CustomMask = null;
            this.tbxCmdtDesc.Editable = false;
            this.tbxCmdtDesc.LinkedLabelName = null;
            this.tbxCmdtDesc.Location = new System.Drawing.Point(180, 125);
            this.tbxCmdtDesc.Name = "tbxCmdtDesc";
            this.tbxCmdtDesc.Size = new System.Drawing.Size(515, 22);
            this.tbxCmdtDesc.TabIndex = 13;
            this.tbxCmdtDesc.TextResourceKey = null;
            this.tbxCmdtDesc.UseTextMandatoryFont = false;
            // 
            // tbxCnsnShprNm
            // 
            this.tbxCnsnShprNm.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxCnsnShprNm.CustomMask = null;
            this.tbxCnsnShprNm.Editable = false;
            this.tbxCnsnShprNm.LinkedLabelName = null;
            this.tbxCnsnShprNm.Location = new System.Drawing.Point(180, 156);
            this.tbxCnsnShprNm.Name = "tbxCnsnShprNm";
            this.tbxCnsnShprNm.Size = new System.Drawing.Size(515, 22);
            this.tbxCnsnShprNm.TabIndex = 15;
            this.tbxCnsnShprNm.TextResourceKey = null;
            this.tbxCnsnShprNm.UseTextMandatoryFont = false;
            // 
            // tbxTransporterNm
            // 
            this.tbxTransporterNm.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxTransporterNm.CustomMask = null;
            this.tbxTransporterNm.Editable = false;
            this.tbxTransporterNm.LinkedLabelName = null;
            this.tbxTransporterNm.Location = new System.Drawing.Point(180, 189);
            this.tbxTransporterNm.Name = "tbxTransporterNm";
            this.tbxTransporterNm.Size = new System.Drawing.Size(515, 22);
            this.tbxTransporterNm.TabIndex = 17;
            this.tbxTransporterNm.TextResourceKey = null;
            this.tbxTransporterNm.UseTextMandatoryFont = false;
            // 
            // tbxBlNo
            // 
            this.tbxBlNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxBlNo.CustomMask = null;
            this.tbxBlNo.Editable = false;
            this.tbxBlNo.LinkedLabelName = null;
            this.tbxBlNo.Location = new System.Drawing.Point(540, 60);
            this.tbxBlNo.Name = "tbxBlNo";
            this.tbxBlNo.Size = new System.Drawing.Size(155, 22);
            this.tbxBlNo.TabIndex = 18;
            this.tbxBlNo.TextResourceKey = null;
            this.tbxBlNo.UseTextMandatoryFont = false;
            // 
            // tbxSecondWgt
            // 
            this.tbxSecondWgt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxSecondWgt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxSecondWgt.CustomMask = null;
            this.tbxSecondWgt.Editable = false;
            this.tbxSecondWgt.LinkedLabelName = null;
            this.tbxSecondWgt.Location = new System.Drawing.Point(139, 6);
            this.tbxSecondWgt.Name = "tbxSecondWgt";
            this.tbxSecondWgt.Size = new System.Drawing.Size(134, 22);
            this.tbxSecondWgt.TabIndex = 5;
            this.tbxSecondWgt.TextResourceKey = null;
            this.tbxSecondWgt.UseTextMandatoryFont = false;
            // 
            // tTableLayoutPanel1
            // 
            this.tTableLayoutPanel1.ColumnCount = 1;
            this.tTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel1.Controls.Add(this.tTableLayoutPanel2, 0, 1);
            this.tTableLayoutPanel1.Controls.Add(this.tTableLayoutPanel3, 0, 2);
            this.tTableLayoutPanel1.Controls.Add(this.tTableLayoutPanel8, 0, 0);
            this.tTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tTableLayoutPanel1.Name = "tTableLayoutPanel1";
            this.tTableLayoutPanel1.RowCount = 3;
            this.tTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tTableLayoutPanel1.Size = new System.Drawing.Size(1984, 845);
            this.tTableLayoutPanel1.TabIndex = 0;
            // 
            // tTableLayoutPanel2
            // 
            this.tTableLayoutPanel2.ColumnCount = 3;
            this.tTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.33021F));
            this.tTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.00025F));
            this.tTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66954F));
            this.tTableLayoutPanel2.Controls.Add(this.tGroupBox1, 0, 0);
            this.tTableLayoutPanel2.Controls.Add(this.tGroupBox3, 2, 0);
            this.tTableLayoutPanel2.Controls.Add(this.tTableLayoutPanel5, 1, 0);
            this.tTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel2.Location = new System.Drawing.Point(3, 70);
            this.tTableLayoutPanel2.Name = "tTableLayoutPanel2";
            this.tTableLayoutPanel2.RowCount = 1;
            this.tTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel2.Size = new System.Drawing.Size(1978, 348);
            this.tTableLayoutPanel2.TabIndex = 1;
            // 
            // tGroupBox1
            // 
            this.tGroupBox1.Controls.Add(this.tTableLayoutPanel9);
            this.tGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tGroupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.tGroupBox1.Name = "tGroupBox1";
            this.tGroupBox1.Size = new System.Drawing.Size(752, 342);
            this.tGroupBox1.TabIndex = 0;
            this.tGroupBox1.TabStop = false;
            this.tGroupBox1.Text = "Weight Scale Measurement";
            this.tGroupBox1.TextResourceKey = "WRD_PTWB_Main_GroupBox_WeightScaleMesurement";
            this.tGroupBox1.UseDefaultStyle = false;
            // 
            // tTableLayoutPanel9
            // 
            this.tTableLayoutPanel9.ColumnCount = 2;
            this.tTableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tTableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tTableLayoutPanel9.Controls.Add(this.tPanel7, 0, 0);
            this.tTableLayoutPanel9.Controls.Add(this.tPanel8, 1, 0);
            this.tTableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel9.Location = new System.Drawing.Point(3, 18);
            this.tTableLayoutPanel9.Name = "tTableLayoutPanel9";
            this.tTableLayoutPanel9.RowCount = 1;
            this.tTableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel9.Size = new System.Drawing.Size(746, 321);
            this.tTableLayoutPanel9.TabIndex = 0;
            // 
            // tPanel7
            // 
            this.tPanel7.BackgroundImage = global::Tsb.Product.WB.Client.Properties.Resources.truck;
            this.tPanel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tPanel7.BorderColor = System.Drawing.Color.Empty;
            this.tPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel7.Location = new System.Drawing.Point(3, 3);
            this.tPanel7.Name = "tPanel7";
            this.tPanel7.Size = new System.Drawing.Size(292, 315);
            this.tPanel7.TabIndex = 0;
            // 
            // tPanel8
            // 
            this.tPanel8.BorderColor = System.Drawing.Color.Empty;
            this.tPanel8.Controls.Add(this.tPanel16);
            this.tPanel8.Controls.Add(this.tPanel15);
            this.tPanel8.Controls.Add(this.tbxReadWeight);
            this.tPanel8.Controls.Add(this.btnReadWeight);
            this.tPanel8.Controls.Add(this.tLabel10);
            this.tPanel8.Controls.Add(this.btnSearchLorry);
            this.tPanel8.Controls.Add(this.tbxChassisNo);
            this.tPanel8.Controls.Add(this.tLabel8);
            this.tPanel8.Controls.Add(this.tbxLorryNo);
            this.tPanel8.Controls.Add(this.tLabel5);
            this.tPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel8.Location = new System.Drawing.Point(301, 3);
            this.tPanel8.Name = "tPanel8";
            this.tPanel8.Size = new System.Drawing.Size(442, 315);
            this.tPanel8.TabIndex = 1;
            // 
            // tPanel16
            // 
            this.tPanel16.BorderColor = System.Drawing.Color.Empty;
            this.tPanel16.Controls.Add(this.tLabel9);
            this.tPanel16.Controls.Add(this.tbxKG);
            this.tPanel16.Location = new System.Drawing.Point(44, 61);
            this.tPanel16.Name = "tPanel16";
            this.tPanel16.Size = new System.Drawing.Size(218, 31);
            this.tPanel16.TabIndex = 15;
            // 
            // tLabel9
            // 
            this.tLabel9.AutoSize = true;
            this.tLabel9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel9.IsAppliedLinkedStyle = false;
            this.tLabel9.LinkedLabelName = null;
            this.tLabel9.Location = new System.Drawing.Point(2, 8);
            this.tLabel9.Name = "tLabel9";
            this.tLabel9.Size = new System.Drawing.Size(27, 14);
            this.tLabel9.TabIndex = 5;
            this.tLabel9.Text = "KG:";
            this.tLabel9.TextResourceKey = "WRD_PTWB_Main_Label_KG";
            this.tLabel9.ToolTipResourceKey = null;
            this.tLabel9.UseDefaultStyle = false;
            // 
            // tbxKG
            // 
            this.tbxKG.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxKG.CustomMask = null;
            this.tbxKG.Editable = false;
            this.tbxKG.IsMandatory = true;
            this.tbxKG.LinkedLabelName = "tLabel9";
            this.tbxKG.Location = new System.Drawing.Point(37, 4);
            this.tbxKG.Name = "tbxKG";
            this.tbxKG.Size = new System.Drawing.Size(166, 22);
            this.tbxKG.TabIndex = 6;
            this.tbxKG.TextResourceKey = null;
            this.tbxKG.UseTextMandatoryFont = false;
            // 
            // tPanel15
            // 
            this.tPanel15.BorderColor = System.Drawing.Color.Empty;
            this.tPanel15.Controls.Add(this.tbxRmk);
            this.tPanel15.Controls.Add(this.tPanel14);
            this.tPanel15.Location = new System.Drawing.Point(79, 131);
            this.tPanel15.Name = "tPanel15";
            this.tPanel15.Size = new System.Drawing.Size(256, 172);
            this.tPanel15.TabIndex = 14;
            // 
            // tbxRmk
            // 
            this.tbxRmk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxRmk.CustomMask = null;
            this.tbxRmk.LinkedLabelName = null;
            this.tbxRmk.Location = new System.Drawing.Point(3, 3);
            this.tbxRmk.Multiline = true;
            this.tbxRmk.Name = "tbxRmk";
            this.tbxRmk.Size = new System.Drawing.Size(164, 166);
            this.tbxRmk.TabIndex = 9;
            this.tbxRmk.TextResourceKey = null;
            this.tbxRmk.UseTextMandatoryFont = false;
            // 
            // tPanel14
            // 
            this.tPanel14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tPanel14.BorderColor = System.Drawing.Color.Empty;
            this.tPanel14.Controls.Add(this.btnClear);
            this.tPanel14.Controls.Add(this.btnRemarkUpdate);
            this.tPanel14.Location = new System.Drawing.Point(171, 48);
            this.tPanel14.Name = "tPanel14";
            this.tPanel14.Size = new System.Drawing.Size(82, 82);
            this.tPanel14.TabIndex = 13;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClear.LinkedLabelName = null;
            this.btnClear.Location = new System.Drawing.Point(-1, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(83, 38);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = " Clear";
            this.btnClear.TextResourceKey = "WRD_PTWB_Main_Button_Clear";
            this.btnClear.ToolTipResourceKey = null;
            this.btnClear.UseDefaultStyle = false;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemarkUpdate
            // 
            this.btnRemarkUpdate.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnRemarkUpdate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemarkUpdate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRemarkUpdate.LinkedLabelName = null;
            this.btnRemarkUpdate.Location = new System.Drawing.Point(0, 44);
            this.btnRemarkUpdate.Name = "btnRemarkUpdate";
            this.btnRemarkUpdate.Size = new System.Drawing.Size(82, 38);
            this.btnRemarkUpdate.TabIndex = 4;
            this.btnRemarkUpdate.Text = "Update";
            this.btnRemarkUpdate.TextResourceKey = "WRD_PTWB_Main_Button_Update";
            this.btnRemarkUpdate.ToolTipResourceKey = null;
            this.btnRemarkUpdate.UseDefaultStyle = false;
            this.btnRemarkUpdate.UseVisualStyleBackColor = false;
            this.btnRemarkUpdate.Click += new System.EventHandler(this.btnRemarkUpdate_Click);
            // 
            // tbxReadWeight
            // 
            this.tbxReadWeight.CustomMask = null;
            this.tbxReadWeight.LinkedLabelName = null;
            this.tbxReadWeight.Location = new System.Drawing.Point(81, 100);
            this.tbxReadWeight.Name = "tbxReadWeight";
            this.tbxReadWeight.Size = new System.Drawing.Size(108, 22);
            this.tbxReadWeight.TabIndex = 11;
            this.tbxReadWeight.TextResourceKey = null;
            this.tbxReadWeight.UseTextMandatoryFont = false;
            this.tbxReadWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxReadWeight_KeyPress);
            // 
            // btnReadWeight
            // 
            this.btnReadWeight.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnReadWeight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadWeight.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReadWeight.LinkedLabelName = null;
            this.btnReadWeight.Location = new System.Drawing.Point(195, 92);
            this.btnReadWeight.Name = "btnReadWeight";
            this.btnReadWeight.Size = new System.Drawing.Size(141, 38);
            this.btnReadWeight.TabIndex = 10;
            this.btnReadWeight.Text = "Read Weight";
            this.btnReadWeight.TextResourceKey = "WRD_PTWB_Main_Button_ReadWeight";
            this.btnReadWeight.ToolTipResourceKey = null;
            this.btnReadWeight.UseDefaultStyle = false;
            this.btnReadWeight.UseVisualStyleBackColor = false;
            this.btnReadWeight.Click += new System.EventHandler(this.btnReadWeight_Click);
            // 
            // tLabel10
            // 
            this.tLabel10.AutoSize = true;
            this.tLabel10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel10.IsAppliedLinkedStyle = false;
            this.tLabel10.LinkedLabelName = null;
            this.tLabel10.Location = new System.Drawing.Point(18, 203);
            this.tLabel10.Name = "tLabel10";
            this.tLabel10.Size = new System.Drawing.Size(57, 14);
            this.tLabel10.TabIndex = 8;
            this.tLabel10.Text = "Remark:";
            this.tLabel10.TextResourceKey = "WRD_PTWB_Main_Label_Remark";
            this.tLabel10.ToolTipResourceKey = null;
            this.tLabel10.UseDefaultStyle = false;
            // 
            // btnSearchLorry
            // 
            this.btnSearchLorry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSearchLorry.Image = global::Tsb.Product.WB.Client.Properties.Resources.Find;
            this.btnSearchLorry.LinkedLabelName = null;
            this.btnSearchLorry.Location = new System.Drawing.Point(254, 3);
            this.btnSearchLorry.Name = "btnSearchLorry";
            this.btnSearchLorry.Size = new System.Drawing.Size(24, 24);
            this.btnSearchLorry.TabIndex = 4;
            this.btnSearchLorry.TextResourceKey = null;
            this.btnSearchLorry.ToolTipResourceKey = null;
            this.btnSearchLorry.UseVisualStyleBackColor = true;
            // 
            // tbxChassisNo
            // 
            this.tbxChassisNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxChassisNo.CustomMask = null;
            this.tbxChassisNo.Editable = false;
            this.tbxChassisNo.LinkedLabelName = null;
            this.tbxChassisNo.Location = new System.Drawing.Point(81, 35);
            this.tbxChassisNo.Name = "tbxChassisNo";
            this.tbxChassisNo.Size = new System.Drawing.Size(167, 22);
            this.tbxChassisNo.TabIndex = 3;
            this.tbxChassisNo.TextResourceKey = null;
            this.tbxChassisNo.UseTextMandatoryFont = false;
            // 
            // tLabel8
            // 
            this.tLabel8.AutoSize = true;
            this.tLabel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel8.IsAppliedLinkedStyle = false;
            this.tLabel8.LinkedLabelName = null;
            this.tLabel8.Location = new System.Drawing.Point(0, 38);
            this.tLabel8.Name = "tLabel8";
            this.tLabel8.Size = new System.Drawing.Size(75, 14);
            this.tLabel8.TabIndex = 2;
            this.tLabel8.Text = "Chassis No:";
            this.tLabel8.TextResourceKey = "WRD_PTWB_Main_Label_ChassisNo";
            this.tLabel8.ToolTipResourceKey = null;
            this.tLabel8.UseDefaultStyle = false;
            // 
            // tLabel5
            // 
            this.tLabel5.AutoSize = true;
            this.tLabel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel5.IsAppliedLinkedStyle = false;
            this.tLabel5.LinkedLabelName = null;
            this.tLabel5.Location = new System.Drawing.Point(12, 8);
            this.tLabel5.Name = "tLabel5";
            this.tLabel5.Size = new System.Drawing.Size(63, 14);
            this.tLabel5.TabIndex = 0;
            this.tLabel5.Text = "Lorry No:";
            this.tLabel5.TextResourceKey = "WRD_PTWB_Main_Label_LorryNo";
            this.tLabel5.ToolTipResourceKey = null;
            this.tLabel5.UseDefaultStyle = false;
            // 
            // tGroupBox3
            // 
            this.tGroupBox3.Controls.Add(this.tTableLayoutPanel6);
            this.tGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGroupBox3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tGroupBox3.Location = new System.Drawing.Point(1651, 3);
            this.tGroupBox3.Name = "tGroupBox3";
            this.tGroupBox3.Size = new System.Drawing.Size(324, 342);
            this.tGroupBox3.TabIndex = 2;
            this.tGroupBox3.TabStop = false;
            this.tGroupBox3.Text = "Monitoring";
            this.tGroupBox3.TextResourceKey = "WRD_PTWB_Main_GroupBox_Monitoring";
            this.tGroupBox3.UseDefaultStyle = false;
            // 
            // tTableLayoutPanel6
            // 
            this.tTableLayoutPanel6.ColumnCount = 1;
            this.tTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tTableLayoutPanel6.Controls.Add(this.tGroupBox7, 0, 1);
            this.tTableLayoutPanel6.Controls.Add(this.tGroupBox5, 0, 0);
            this.tTableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel6.Location = new System.Drawing.Point(3, 18);
            this.tTableLayoutPanel6.Name = "tTableLayoutPanel6";
            this.tTableLayoutPanel6.RowCount = 2;
            this.tTableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tTableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tTableLayoutPanel6.Size = new System.Drawing.Size(318, 321);
            this.tTableLayoutPanel6.TabIndex = 0;
            // 
            // tGroupBox7
            // 
            this.tGroupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGroupBox7.Location = new System.Drawing.Point(3, 163);
            this.tGroupBox7.Name = "tGroupBox7";
            this.tGroupBox7.Size = new System.Drawing.Size(312, 155);
            this.tGroupBox7.TabIndex = 1;
            this.tGroupBox7.TabStop = false;
            this.tGroupBox7.TextResourceKey = null;
            // 
            // tGroupBox5
            // 
            this.tGroupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGroupBox5.Location = new System.Drawing.Point(3, 3);
            this.tGroupBox5.Name = "tGroupBox5";
            this.tGroupBox5.Size = new System.Drawing.Size(312, 154);
            this.tGroupBox5.TabIndex = 0;
            this.tGroupBox5.TabStop = false;
            this.tGroupBox5.TextResourceKey = null;
            // 
            // tTableLayoutPanel5
            // 
            this.tTableLayoutPanel5.ColumnCount = 1;
            this.tTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tTableLayoutPanel5.Controls.Add(this.tGroupBox2, 0, 0);
            this.tTableLayoutPanel5.Controls.Add(this.tGroupBox6, 0, 1);
            this.tTableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel5.Location = new System.Drawing.Point(758, 0);
            this.tTableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tTableLayoutPanel5.Name = "tTableLayoutPanel5";
            this.tTableLayoutPanel5.RowCount = 2;
            this.tTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tTableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tTableLayoutPanel5.Size = new System.Drawing.Size(890, 348);
            this.tTableLayoutPanel5.TabIndex = 0;
            // 
            // tGroupBox2
            // 
            this.tGroupBox2.Controls.Add(this.tbxBlNo);
            this.tGroupBox2.Controls.Add(this.tbxTransporterNm);
            this.tGroupBox2.Controls.Add(this.tLabel18);
            this.tGroupBox2.Controls.Add(this.tbxCnsnShprNm);
            this.tGroupBox2.Controls.Add(this.tLabel17);
            this.tGroupBox2.Controls.Add(this.tbxCmdtDesc);
            this.tGroupBox2.Controls.Add(this.tLabel16);
            this.tGroupBox2.Controls.Add(this.tbxSdoNo);
            this.tGroupBox2.Controls.Add(this.tLabel15);
            this.tGroupBox2.Controls.Add(this.tLabel14);
            this.tGroupBox2.Controls.Add(this.tbxGrNo);
            this.tGroupBox2.Controls.Add(this.tLabel13);
            this.tGroupBox2.Controls.Add(this.tbxShipgNoteNo);
            this.tGroupBox2.Controls.Add(this.tLabel12);
            this.tGroupBox2.Controls.Add(this.tbxVslCallId);
            this.tGroupBox2.Controls.Add(this.tLabel11);
            this.tGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGroupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tGroupBox2.Location = new System.Drawing.Point(3, 3);
            this.tGroupBox2.Name = "tGroupBox2";
            this.tGroupBox2.Size = new System.Drawing.Size(884, 272);
            this.tGroupBox2.TabIndex = 1;
            this.tGroupBox2.TabStop = false;
            this.tGroupBox2.Text = "Cargo Infomation";
            this.tGroupBox2.TextResourceKey = "WRD_PTWB_Main_GroupBox_CargoInfomation";
            this.tGroupBox2.UseDefaultStyle = false;
            // 
            // tLabel18
            // 
            this.tLabel18.AutoSize = true;
            this.tLabel18.IsAppliedLinkedStyle = false;
            this.tLabel18.LinkedLabelName = null;
            this.tLabel18.Location = new System.Drawing.Point(57, 193);
            this.tLabel18.Name = "tLabel18";
            this.tLabel18.Size = new System.Drawing.Size(120, 14);
            this.tLabel18.TabIndex = 16;
            this.tLabel18.Text = "Transporter Name:";
            this.tLabel18.TextResourceKey = "WRD_PTWB_Main_Label_TransporterNm";
            this.tLabel18.ToolTipResourceKey = null;
            this.tLabel18.UseDefaultStyle = false;
            // 
            // tLabel17
            // 
            this.tLabel17.AutoSize = true;
            this.tLabel17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel17.IsAppliedLinkedStyle = false;
            this.tLabel17.LinkedLabelName = null;
            this.tLabel17.Location = new System.Drawing.Point(12, 159);
            this.tLabel17.Name = "tLabel17";
            this.tLabel17.Size = new System.Drawing.Size(165, 14);
            this.tLabel17.TabIndex = 14;
            this.tLabel17.Text = "Shipper/Consignee Name:";
            this.tLabel17.TextResourceKey = "WRD_PTWB_Main_Label_ShprOrCnsnNm";
            this.tLabel17.ToolTipResourceKey = null;
            this.tLabel17.UseDefaultStyle = false;
            // 
            // tLabel16
            // 
            this.tLabel16.AutoSize = true;
            this.tLabel16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel16.IsAppliedLinkedStyle = false;
            this.tLabel16.LinkedLabelName = null;
            this.tLabel16.Location = new System.Drawing.Point(33, 129);
            this.tLabel16.Name = "tLabel16";
            this.tLabel16.Size = new System.Drawing.Size(143, 14);
            this.tLabel16.TabIndex = 12;
            this.tLabel16.Text = "Comodity Description:";
            this.tLabel16.TextResourceKey = "WRD_PTWB_Main_Label_CmdtDesc";
            this.tLabel16.ToolTipResourceKey = null;
            this.tLabel16.UseDefaultStyle = false;
            // 
            // tLabel15
            // 
            this.tLabel15.IsAppliedLinkedStyle = false;
            this.tLabel15.LinkedLabelName = null;
            this.tLabel15.Location = new System.Drawing.Point(392, 95);
            this.tLabel15.Name = "tLabel15";
            this.tLabel15.Size = new System.Drawing.Size(146, 18);
            this.tLabel15.TabIndex = 10;
            this.tLabel15.Text = "Sub Delivery Order No:";
            this.tLabel15.TextResourceKey = "WRD_PTWB_Main_Label_SubDeliveryOrderNo";
            this.tLabel15.ToolTipResourceKey = null;
            this.tLabel15.UseDefaultStyle = false;
            // 
            // tLabel14
            // 
            this.tLabel14.AutoSize = true;
            this.tLabel14.IsAppliedLinkedStyle = false;
            this.tLabel14.LinkedLabelName = null;
            this.tLabel14.Location = new System.Drawing.Point(427, 64);
            this.tLabel14.Name = "tLabel14";
            this.tLabel14.Size = new System.Drawing.Size(110, 14);
            this.tLabel14.TabIndex = 8;
            this.tLabel14.Text = "Bill of Lading No:";
            this.tLabel14.TextResourceKey = "WRD_PTWB_Main_Label_BillOfLaddingNo";
            this.tLabel14.ToolTipResourceKey = null;
            this.tLabel14.UseDefaultStyle = false;
            // 
            // tLabel13
            // 
            this.tLabel13.AutoSize = true;
            this.tLabel13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel13.IsAppliedLinkedStyle = false;
            this.tLabel13.LinkedLabelName = null;
            this.tLabel13.Location = new System.Drawing.Point(61, 96);
            this.tLabel13.Name = "tLabel13";
            this.tLabel13.Size = new System.Drawing.Size(113, 14);
            this.tLabel13.TabIndex = 6;
            this.tLabel13.Text = "Good Receipt No:";
            this.tLabel13.TextResourceKey = "WRD_PTWB_Main_Label_GoodReceiptNo";
            this.tLabel13.ToolTipResourceKey = null;
            this.tLabel13.UseDefaultStyle = false;
            // 
            // tLabel12
            // 
            this.tLabel12.AutoSize = true;
            this.tLabel12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel12.IsAppliedLinkedStyle = false;
            this.tLabel12.LinkedLabelName = null;
            this.tLabel12.Location = new System.Drawing.Point(59, 64);
            this.tLabel12.Name = "tLabel12";
            this.tLabel12.Size = new System.Drawing.Size(118, 14);
            this.tLabel12.TabIndex = 4;
            this.tLabel12.Text = "Shipping Note No:";
            this.tLabel12.TextResourceKey = "WRD_PTWB_Main_Label_ShippingNoteNo";
            this.tLabel12.ToolTipResourceKey = null;
            this.tLabel12.UseDefaultStyle = false;
            // 
            // tLabel11
            // 
            this.tLabel11.AutoSize = true;
            this.tLabel11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel11.IsAppliedLinkedStyle = false;
            this.tLabel11.LinkedLabelName = null;
            this.tLabel11.Location = new System.Drawing.Point(87, 33);
            this.tLabel11.Name = "tLabel11";
            this.tLabel11.Size = new System.Drawing.Size(90, 14);
            this.tLabel11.TabIndex = 2;
            this.tLabel11.Text = "Vessel Call Id:";
            this.tLabel11.TextResourceKey = "WRD_PTWB_Main_Label_VesselCallId";
            this.tLabel11.ToolTipResourceKey = null;
            this.tLabel11.UseDefaultStyle = false;
            // 
            // tGroupBox6
            // 
            this.tGroupBox6.Controls.Add(this.tTableLayoutPanel10);
            this.tGroupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGroupBox6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tGroupBox6.Location = new System.Drawing.Point(3, 281);
            this.tGroupBox6.Name = "tGroupBox6";
            this.tGroupBox6.Size = new System.Drawing.Size(884, 64);
            this.tGroupBox6.TabIndex = 2;
            this.tGroupBox6.TabStop = false;
            this.tGroupBox6.Text = "Weight Infomation";
            this.tGroupBox6.TextResourceKey = "WRD_PTWB_Main_GroupBox_WeightInfomation";
            this.tGroupBox6.UseDefaultStyle = false;
            // 
            // tTableLayoutPanel10
            // 
            this.tTableLayoutPanel10.AutoScroll = true;
            this.tTableLayoutPanel10.ColumnCount = 3;
            this.tTableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tTableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tTableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tTableLayoutPanel10.Controls.Add(this.tPanel13, 2, 0);
            this.tTableLayoutPanel10.Controls.Add(this.tPanel11, 1, 0);
            this.tTableLayoutPanel10.Controls.Add(this.tPanel10, 0, 0);
            this.tTableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel10.Location = new System.Drawing.Point(3, 18);
            this.tTableLayoutPanel10.Name = "tTableLayoutPanel10";
            this.tTableLayoutPanel10.RowCount = 1;
            this.tTableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tTableLayoutPanel10.Size = new System.Drawing.Size(878, 43);
            this.tTableLayoutPanel10.TabIndex = 8;
            // 
            // tPanel13
            // 
            this.tPanel13.BorderColor = System.Drawing.Color.Empty;
            this.tPanel13.Controls.Add(this.tbxCargoWeight);
            this.tPanel13.Controls.Add(this.tlbCargoWeight);
            this.tPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel13.Location = new System.Drawing.Point(587, 3);
            this.tPanel13.Name = "tPanel13";
            this.tPanel13.Size = new System.Drawing.Size(288, 37);
            this.tPanel13.TabIndex = 2;
            // 
            // tbxCargoWeight
            // 
            this.tbxCargoWeight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxCargoWeight.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxCargoWeight.CustomMask = null;
            this.tbxCargoWeight.Editable = false;
            this.tbxCargoWeight.LinkedLabelName = null;
            this.tbxCargoWeight.Location = new System.Drawing.Point(113, 6);
            this.tbxCargoWeight.Name = "tbxCargoWeight";
            this.tbxCargoWeight.Size = new System.Drawing.Size(141, 22);
            this.tbxCargoWeight.TabIndex = 7;
            this.tbxCargoWeight.TextResourceKey = null;
            this.tbxCargoWeight.UseTextMandatoryFont = false;
            // 
            // tlbCargoWeight
            // 
            this.tlbCargoWeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlbCargoWeight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbCargoWeight.IsAppliedLinkedStyle = false;
            this.tlbCargoWeight.LinkedLabelName = null;
            this.tlbCargoWeight.Location = new System.Drawing.Point(0, 0);
            this.tlbCargoWeight.Name = "tlbCargoWeight";
            this.tlbCargoWeight.Size = new System.Drawing.Size(114, 37);
            this.tlbCargoWeight.TabIndex = 6;
            this.tlbCargoWeight.Text = "Cargo Weight:";
            this.tlbCargoWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tlbCargoWeight.TextResourceKey = "WRD_PTWB_Main_Label_CargoWeight";
            this.tlbCargoWeight.ToolTipResourceKey = null;
            this.tlbCargoWeight.UseDefaultStyle = false;
            // 
            // tPanel11
            // 
            this.tPanel11.BorderColor = System.Drawing.Color.Empty;
            this.tPanel11.Controls.Add(this.tlbSecondWeight);
            this.tPanel11.Controls.Add(this.tbxSecondWgt);
            this.tPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel11.Location = new System.Drawing.Point(295, 3);
            this.tPanel11.Name = "tPanel11";
            this.tPanel11.Size = new System.Drawing.Size(286, 37);
            this.tPanel11.TabIndex = 1;
            // 
            // tlbSecondWeight
            // 
            this.tlbSecondWeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlbSecondWeight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbSecondWeight.IsAppliedLinkedStyle = false;
            this.tlbSecondWeight.LinkedLabelName = null;
            this.tlbSecondWeight.Location = new System.Drawing.Point(0, 0);
            this.tlbSecondWeight.Name = "tlbSecondWeight";
            this.tlbSecondWeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tlbSecondWeight.Size = new System.Drawing.Size(139, 37);
            this.tlbSecondWeight.TabIndex = 2;
            this.tlbSecondWeight.Text = "2st Weight Scale:";
            this.tlbSecondWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tlbSecondWeight.TextResourceKey = "WRD_PTWB_Main_Grid_SecondWeight";
            this.tlbSecondWeight.ToolTipResourceKey = null;
            this.tlbSecondWeight.UseDefaultStyle = false;
            // 
            // tPanel10
            // 
            this.tPanel10.BorderColor = System.Drawing.Color.Empty;
            this.tPanel10.Controls.Add(this.tbxFirstWgt);
            this.tPanel10.Controls.Add(this.tlbFirstWeight);
            this.tPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel10.Location = new System.Drawing.Point(3, 3);
            this.tPanel10.Name = "tPanel10";
            this.tPanel10.Size = new System.Drawing.Size(286, 37);
            this.tPanel10.TabIndex = 0;
            // 
            // tbxFirstWgt
            // 
            this.tbxFirstWgt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxFirstWgt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxFirstWgt.CustomMask = null;
            this.tbxFirstWgt.Editable = false;
            this.tbxFirstWgt.LinkedLabelName = null;
            this.tbxFirstWgt.Location = new System.Drawing.Point(136, 6);
            this.tbxFirstWgt.Name = "tbxFirstWgt";
            this.tbxFirstWgt.Size = new System.Drawing.Size(130, 22);
            this.tbxFirstWgt.TabIndex = 3;
            this.tbxFirstWgt.TextResourceKey = null;
            this.tbxFirstWgt.UseTextMandatoryFont = false;
            // 
            // tlbFirstWeight
            // 
            this.tlbFirstWeight.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlbFirstWeight.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbFirstWeight.IsAppliedLinkedStyle = false;
            this.tlbFirstWeight.LinkedLabelName = null;
            this.tlbFirstWeight.Location = new System.Drawing.Point(0, 0);
            this.tlbFirstWeight.Name = "tlbFirstWeight";
            this.tlbFirstWeight.Size = new System.Drawing.Size(139, 37);
            this.tlbFirstWeight.TabIndex = 4;
            this.tlbFirstWeight.Text = "1st Weight Scale:";
            this.tlbFirstWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tlbFirstWeight.TextResourceKey = "WRD_PTWB_Main_Label_FirstWeight";
            this.tlbFirstWeight.ToolTipResourceKey = null;
            this.tlbFirstWeight.UseDefaultStyle = false;
            // 
            // tTableLayoutPanel3
            // 
            this.tTableLayoutPanel3.ColumnCount = 1;
            this.tTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel3.Controls.Add(this.tTableLayoutPanel4, 0, 0);
            this.tTableLayoutPanel3.Controls.Add(this.tPanel9, 0, 1);
            this.tTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel3.Location = new System.Drawing.Point(3, 424);
            this.tTableLayoutPanel3.Name = "tTableLayoutPanel3";
            this.tTableLayoutPanel3.RowCount = 2;
            this.tTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tTableLayoutPanel3.Size = new System.Drawing.Size(1978, 418);
            this.tTableLayoutPanel3.TabIndex = 2;
            // 
            // tTableLayoutPanel4
            // 
            this.tTableLayoutPanel4.ColumnCount = 3;
            this.tTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tTableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tTableLayoutPanel4.Controls.Add(this.tGroupBox4, 0, 0);
            this.tTableLayoutPanel4.Controls.Add(this.tPanel12, 1, 0);
            this.tTableLayoutPanel4.Controls.Add(this.tPanel2, 2, 0);
            this.tTableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tTableLayoutPanel4.Name = "tTableLayoutPanel4";
            this.tTableLayoutPanel4.RowCount = 1;
            this.tTableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel4.Size = new System.Drawing.Size(1972, 77);
            this.tTableLayoutPanel4.TabIndex = 0;
            // 
            // tGroupBox4
            // 
            this.tGroupBox4.Controls.Add(this.tTableLayoutPanel7);
            this.tGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGroupBox4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tGroupBox4.Location = new System.Drawing.Point(3, 3);
            this.tGroupBox4.Name = "tGroupBox4";
            this.tGroupBox4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tGroupBox4.Size = new System.Drawing.Size(881, 71);
            this.tGroupBox4.TabIndex = 0;
            this.tGroupBox4.TabStop = false;
            this.tGroupBox4.Text = "Search";
            this.tGroupBox4.TextResourceKey = "WRD_PTWB_Main_GroupBox_Search";
            this.tGroupBox4.UseDefaultStyle = false;
            // 
            // tTableLayoutPanel7
            // 
            this.tTableLayoutPanel7.ColumnCount = 3;
            this.tTableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tTableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tTableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tTableLayoutPanel7.Controls.Add(this.tPanel1, 0, 0);
            this.tTableLayoutPanel7.Controls.Add(this.tPanel5, 0, 0);
            this.tTableLayoutPanel7.Controls.Add(this.tPanel4, 0, 0);
            this.tTableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel7.Location = new System.Drawing.Point(3, 15);
            this.tTableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tTableLayoutPanel7.Name = "tTableLayoutPanel7";
            this.tTableLayoutPanel7.RowCount = 1;
            this.tTableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel7.Size = new System.Drawing.Size(875, 53);
            this.tTableLayoutPanel7.TabIndex = 1;
            // 
            // tPanel1
            // 
            this.tPanel1.AutoScroll = true;
            this.tPanel1.AutoSize = true;
            this.tPanel1.BorderColor = System.Drawing.Color.Empty;
            this.tPanel1.Controls.Add(this.tbxSearchGridLorryNo);
            this.tPanel1.Controls.Add(this.tLabel4);
            this.tPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel1.Location = new System.Drawing.Point(615, 3);
            this.tPanel1.Name = "tPanel1";
            this.tPanel1.Size = new System.Drawing.Size(257, 47);
            this.tPanel1.TabIndex = 3;
            // 
            // tbxSearchGridLorryNo
            // 
            this.tbxSearchGridLorryNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxSearchGridLorryNo.BackColor = System.Drawing.SystemColors.Window;
            this.tbxSearchGridLorryNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxSearchGridLorryNo.CustomMask = null;
            this.tbxSearchGridLorryNo.LinkedLabelName = null;
            this.tbxSearchGridLorryNo.Location = new System.Drawing.Point(77, 14);
            this.tbxSearchGridLorryNo.Name = "tbxSearchGridLorryNo";
            this.tbxSearchGridLorryNo.Size = new System.Drawing.Size(141, 22);
            this.tbxSearchGridLorryNo.TabIndex = 2;
            this.tbxSearchGridLorryNo.TextResourceKey = null;
            this.tbxSearchGridLorryNo.UseTextMandatoryFont = false;
            // 
            // tLabel4
            // 
            this.tLabel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.tLabel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel4.IsAppliedLinkedStyle = false;
            this.tLabel4.LinkedLabelName = null;
            this.tLabel4.Location = new System.Drawing.Point(0, 0);
            this.tLabel4.Name = "tLabel4";
            this.tLabel4.Size = new System.Drawing.Size(79, 47);
            this.tLabel4.TabIndex = 1;
            this.tLabel4.Text = "Lorry No:";
            this.tLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tLabel4.TextResourceKey = null;
            this.tLabel4.ToolTipResourceKey = null;
            this.tLabel4.UseDefaultStyle = false;
            // 
            // tPanel5
            // 
            this.tPanel5.AutoScroll = true;
            this.tPanel5.AutoSize = true;
            this.tPanel5.BorderColor = System.Drawing.Color.Empty;
            this.tPanel5.Controls.Add(this.dtpWeightTo);
            this.tPanel5.Controls.Add(this.tLabel2);
            this.tPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel5.Location = new System.Drawing.Point(309, 3);
            this.tPanel5.Name = "tPanel5";
            this.tPanel5.Size = new System.Drawing.Size(300, 47);
            this.tPanel5.TabIndex = 2;
            this.tPanel5.UseDefaultStyle = false;
            // 
            // dtpWeightTo
            // 
            this.dtpWeightTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpWeightTo.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWeightTo.CustomFormat = "dd/MM/yyyy";
            this.dtpWeightTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWeightTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWeightTo.LinkedLabelName = null;
            this.dtpWeightTo.Location = new System.Drawing.Point(123, 8);
            this.dtpWeightTo.Name = "dtpWeightTo";
            this.dtpWeightTo.NullableValue = new System.DateTime(2024, 6, 25, 18, 8, 11, 360);
            this.dtpWeightTo.Size = new System.Drawing.Size(144, 22);
            this.dtpWeightTo.TabIndex = 3;
            this.dtpWeightTo.TextResourceKey = null;
            this.dtpWeightTo.Value = DateTime.Now;
            // 
            // tLabel2
            // 
            this.tLabel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel2.IsAppliedLinkedStyle = false;
            this.tLabel2.LinkedLabelName = null;
            this.tLabel2.Location = new System.Drawing.Point(0, 0);
            this.tLabel2.Name = "tLabel2";
            this.tLabel2.Size = new System.Drawing.Size(127, 47);
            this.tLabel2.TabIndex = 2;
            this.tLabel2.Text = "Weight Date To:";
            this.tLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tLabel2.TextResourceKey = null;
            this.tLabel2.ToolTipResourceKey = null;
            this.tLabel2.UseDefaultStyle = false;
            // 
            // tPanel4
            // 
            this.tPanel4.AutoScroll = true;
            this.tPanel4.AutoSize = true;
            this.tPanel4.BorderColor = System.Drawing.Color.Empty;
            this.tPanel4.Controls.Add(this.tLabel1);
            this.tPanel4.Controls.Add(this.dtpWeightFrom);
            this.tPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel4.Location = new System.Drawing.Point(3, 3);
            this.tPanel4.Name = "tPanel4";
            this.tPanel4.Size = new System.Drawing.Size(300, 47);
            this.tPanel4.TabIndex = 1;
            // 
            // tLabel1
            // 
            this.tLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel1.IsAppliedLinkedStyle = false;
            this.tLabel1.LinkedLabelName = null;
            this.tLabel1.Location = new System.Drawing.Point(0, 0);
            this.tLabel1.Name = "tLabel1";
            this.tLabel1.Size = new System.Drawing.Size(147, 47);
            this.tLabel1.TabIndex = 1;
            this.tLabel1.Text = "Weight Date From:";
            this.tLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tLabel1.TextResourceKey = null;
            this.tLabel1.ToolTipResourceKey = null;
            this.tLabel1.UseDefaultStyle = false;
            // 
            // dtpWeightFrom
            // 
            this.dtpWeightFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpWeightFrom.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWeightFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpWeightFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWeightFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWeightFrom.LinkedLabelName = null;
            this.dtpWeightFrom.Location = new System.Drawing.Point(147, 8);
            this.dtpWeightFrom.Name = "dtpWeightFrom";
            this.dtpWeightFrom.NullableValue = new System.DateTime(2024, 6, 25, 18, 8, 11, 390);
            this.dtpWeightFrom.Size = new System.Drawing.Size(132, 22);
            this.dtpWeightFrom.TabIndex = 0;
            this.dtpWeightFrom.TextResourceKey = null;
            this.dtpWeightFrom.Value = DateTime.Now;
            // 
            // tPanel12
            // 
            this.tPanel12.BackColor = System.Drawing.SystemColors.Control;
            this.tPanel12.BorderColor = System.Drawing.Color.Empty;
            this.tPanel12.Controls.Add(this.btnCancelJob);
            this.tPanel12.Controls.Add(this.btnPrintCIR);
            this.tPanel12.Controls.Add(this.btnConfirm);
            this.tPanel12.Controls.Add(this.tbtnSearchDoc);
            this.tPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel12.Location = new System.Drawing.Point(890, 3);
            this.tPanel12.Name = "tPanel12";
            this.tPanel12.Size = new System.Drawing.Size(684, 71);
            this.tPanel12.TabIndex = 1;
            // 
            // btnCancelJob
            // 
            this.btnCancelJob.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancelJob.AutoSize = true;
            this.btnCancelJob.BackColor = System.Drawing.Color.OrangeRed;
            this.btnCancelJob.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelJob.LinkedLabelName = null;
            this.btnCancelJob.Location = new System.Drawing.Point(382, 15);
            this.btnCancelJob.Name = "btnCancelJob";
            this.btnCancelJob.Size = new System.Drawing.Size(100, 38);
            this.btnCancelJob.TabIndex = 3;
            this.btnCancelJob.Text = "Cancel Job";
            this.btnCancelJob.TextResourceKey = "WRD_PTWB_Main_Button_CancleJob";
            this.btnCancelJob.ToolTipResourceKey = null;
            this.btnCancelJob.UseDefaultStyle = false;
            this.btnCancelJob.UseVisualStyleBackColor = false;
            // 
            // btnPrintCIR
            // 
            this.btnPrintCIR.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPrintCIR.AutoSize = true;
            this.btnPrintCIR.BackColor = System.Drawing.Color.MediumOrchid;
            this.btnPrintCIR.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintCIR.LinkedLabelName = null;
            this.btnPrintCIR.Location = new System.Drawing.Point(261, 15);
            this.btnPrintCIR.Name = "btnPrintCIR";
            this.btnPrintCIR.Size = new System.Drawing.Size(100, 38);
            this.btnPrintCIR.TabIndex = 2;
            this.btnPrintCIR.Text = "Print CIR";
            this.btnPrintCIR.TextResourceKey = "WRD_PTWB_Main_Button_PrintCIR";
            this.btnPrintCIR.ToolTipResourceKey = null;
            this.btnPrintCIR.UseDefaultStyle = false;
            this.btnPrintCIR.UseVisualStyleBackColor = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnConfirm.AutoSize = true;
            this.btnConfirm.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.LinkedLabelName = null;
            this.btnConfirm.Location = new System.Drawing.Point(137, 15);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 38);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.TextResourceKey = "WRD_PTWB_Main_Button_Confirm";
            this.btnConfirm.ToolTipResourceKey = null;
            this.btnConfirm.UseDefaultStyle = false;
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tbtnSearchDoc
            // 
            this.tbtnSearchDoc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbtnSearchDoc.AutoSize = true;
            this.tbtnSearchDoc.BackColor = System.Drawing.Color.DarkTurquoise;
            this.tbtnSearchDoc.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbtnSearchDoc.LinkedLabelName = null;
            this.tbtnSearchDoc.Location = new System.Drawing.Point(14, 15);
            this.tbtnSearchDoc.Name = "tbtnSearchDoc";
            this.tbtnSearchDoc.Size = new System.Drawing.Size(100, 38);
            this.tbtnSearchDoc.TabIndex = 0;
            this.tbtnSearchDoc.Text = "Search";
            this.tbtnSearchDoc.TextResourceKey = "WRD_PTWB_Main_Button_Search";
            this.tbtnSearchDoc.ToolTipResourceKey = null;
            this.tbtnSearchDoc.UseDefaultStyle = false;
            this.tbtnSearchDoc.UseVisualStyleBackColor = false;
            this.tbtnSearchDoc.Click += new System.EventHandler(this.btnTruckListSearch_Click);
            // 
            // tPanel2
            // 
            this.tPanel2.BorderColor = System.Drawing.Color.Empty;
            this.tPanel2.Controls.Add(this.tGroupBox8);
            this.tPanel2.Controls.Add(this.tLabel3);
            this.tPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel2.Location = new System.Drawing.Point(1580, 3);
            this.tPanel2.Name = "tPanel2";
            this.tPanel2.Size = new System.Drawing.Size(389, 71);
            this.tPanel2.TabIndex = 2;
            // 
            // tGroupBox8
            // 
            this.tGroupBox8.AutoSize = true;
            this.tGroupBox8.BackColor = System.Drawing.SystemColors.Control;
            this.tGroupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGroupBox8.Location = new System.Drawing.Point(127, 0);
            this.tGroupBox8.Name = "tGroupBox8";
            this.tGroupBox8.Size = new System.Drawing.Size(262, 71);
            this.tGroupBox8.TabIndex = 1;
            this.tGroupBox8.TabStop = false;
            this.tGroupBox8.TextResourceKey = null;
            // 
            // tLabel3
            // 
            this.tLabel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.tLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tLabel3.IsAppliedLinkedStyle = false;
            this.tLabel3.LinkedLabelName = null;
            this.tLabel3.Location = new System.Drawing.Point(0, 0);
            this.tLabel3.Name = "tLabel3";
            this.tLabel3.Size = new System.Drawing.Size(127, 71);
            this.tLabel3.TabIndex = 0;
            this.tLabel3.Text = "File Infomation:";
            this.tLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tLabel3.TextResourceKey = "WRD_PTWB_Main_Label_FileInfomation";
            this.tLabel3.ToolTipResourceKey = null;
            this.tLabel3.UseDefaultStyle = false;
            // 
            // tPanel9
            // 
            this.tPanel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tPanel9.BorderColor = System.Drawing.Color.Empty;
            this.tPanel9.Controls.Add(this.grd_WB_WeightInfoGrid);
            this.tPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel9.Location = new System.Drawing.Point(3, 86);
            this.tPanel9.Name = "tPanel9";
            this.tPanel9.Size = new System.Drawing.Size(1972, 329);
            this.tPanel9.TabIndex = 1;
            // 
            // grd_WB_WeightInfoGrid
            // 
            this.grd_WB_WeightInfoGrid.AccessibleDescription = "";
            this.grd_WB_WeightInfoGrid.ActiveRowPrevColorDic = null;
            this.grd_WB_WeightInfoGrid.AllowDragFill = true;
            this.grd_WB_WeightInfoGrid.AlreadyDrewGuidList = null;
            this.grd_WB_WeightInfoGrid.AppliedColSettingName = null;
            this.grd_WB_WeightInfoGrid.AppliedFilterList = null;
            this.grd_WB_WeightInfoGrid.AppliedFilterName = null;
            this.grd_WB_WeightInfoGrid.AppliedSortName = null;
            this.grd_WB_WeightInfoGrid.AppliedSummaryName = null;
            this.grd_WB_WeightInfoGrid.ApplyHeaderFilter = false;
            this.grd_WB_WeightInfoGrid.BlinkCellTimer = null;
            this.grd_WB_WeightInfoGrid.BlinkedCellsDic = null;
            this.grd_WB_WeightInfoGrid.BlinkedInterval = 2000;
            this.grd_WB_WeightInfoGrid.BlinkedRowsInfo = null;
            this.grd_WB_WeightInfoGrid.BlinkRowTimer = null;
            this.grd_WB_WeightInfoGrid.BoldLetteringCellDic = null;
            this.grd_WB_WeightInfoGrid.BoldLetteringRowGuidList = null;
            this.grd_WB_WeightInfoGrid.BoldLetteringSchema = null;
            this.grd_WB_WeightInfoGrid.BorderIndicatorColor = System.Drawing.Color.Black;
            this.grd_WB_WeightInfoGrid.BorderIndicatorThick = 2;
            this.grd_WB_WeightInfoGrid.ButtonDrawMode = ((FarPoint.Win.Spread.ButtonDrawModes)((FarPoint.Win.Spread.ButtonDrawModes.AlwaysPrimaryButton | FarPoint.Win.Spread.ButtonDrawModes.AlwaysSecondaryButton)));
            this.grd_WB_WeightInfoGrid.ColSettingFreezeColumnCount = 0;
            this.grd_WB_WeightInfoGrid.ColumnIndexDic = null;
            this.grd_WB_WeightInfoGrid.CurrentIndicatedRowIndex = 0;
            this.grd_WB_WeightInfoGrid.DefaultSkin = FarPoint.Win.Spread.DefaultSpreadSkins.Office2007;
            this.grd_WB_WeightInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_WB_WeightInfoGrid.DragFillMode = Tsb.Fontos.Win.Grid.Types.DragFillMode.Normal;
            this.grd_WB_WeightInfoGrid.EnableDragFillMenu = false;
            this.grd_WB_WeightInfoGrid.FilteredItemList = null;
            this.grd_WB_WeightInfoGrid.FocusRenderer = enhancedFocusIndicatorRenderer2;
            this.grd_WB_WeightInfoGrid.GridBizRule = null;
            this.grd_WB_WeightInfoGrid.GridBizRuleClassAssemblyName = "Tsb.Catos.Cm.Win";
            this.grd_WB_WeightInfoGrid.GridBizRuleClassName = "Tsb.Catos.Cm.Win.Grid.CTGridBizRule";
            this.grd_WB_WeightInfoGrid.GridColumnSchema = null;
            this.grd_WB_WeightInfoGrid.GridDataItemClassAssemblyName = "Tsb.Product.WB.Common";
            this.grd_WB_WeightInfoGrid.GridDataItemClassName = "Tsb.Product.WB.Common.Item.WeightBridge.WeightInfoItem";
            this.grd_WB_WeightInfoGrid.GridHelper = null;
            this.grd_WB_WeightInfoGrid.GridName = null;
            this.grd_WB_WeightInfoGrid.GridPrintMainTiltle = null;
            this.grd_WB_WeightInfoGrid.GridPrintSubTitle = null;
            this.grd_WB_WeightInfoGrid.InsertRowType = Tsb.Fontos.Win.Grid.Types.InsertRowTypes.ACTIVECELL;
            this.grd_WB_WeightInfoGrid.IsFillUpWithBlankRows = false;
            this.grd_WB_WeightInfoGrid.IsFreezeColumn = false;
            this.grd_WB_WeightInfoGrid.IsFreezePanes = false;
            this.grd_WB_WeightInfoGrid.IsGridFilterApplingEvent = false;
            this.grd_WB_WeightInfoGrid.IsGridFilterClearedEvent = false;
            this.grd_WB_WeightInfoGrid.Location = new System.Drawing.Point(0, 0);
            this.grd_WB_WeightInfoGrid.LockedCellsDic = null;
            this.grd_WB_WeightInfoGrid.LockedRowsInfo = null;
            this.grd_WB_WeightInfoGrid.MandatoryFieldDic = null;
            this.grd_WB_WeightInfoGrid.Margin = new System.Windows.Forms.Padding(2);
            this.grd_WB_WeightInfoGrid.Name = "grd_WB_WeightInfoGrid";
            this.grd_WB_WeightInfoGrid.ObjectID = "GNR-FTWN-GRD-TSpreadGrid";
            this.grd_WB_WeightInfoGrid.PrevActiveRowIndex = -1;
            this.grd_WB_WeightInfoGrid.ReferenceGridXmlPath = "";
            this.grd_WB_WeightInfoGrid.RowLockColumnIndex = -1;
            this.grd_WB_WeightInfoGrid.SchemaVersion = Tsb.Fontos.Win.Grid.Types.GridSchemaVersion.FONTOS_VERSION_1_0;
            this.grd_WB_WeightInfoGrid.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grd_WB_WeightInfoGrid_Sheet1});
            this.grd_WB_WeightInfoGrid.ShowBorderIndicator = false;
            this.grd_WB_WeightInfoGrid.Size = new System.Drawing.Size(1972, 329);
            this.grd_WB_WeightInfoGrid.SourceItemList = null;
            this.grd_WB_WeightInfoGrid.SpecifiedSchemaFileFolderName = "ProductWB";
            this.grd_WB_WeightInfoGrid.SpecifiedSchemaFileName = "";
            this.grd_WB_WeightInfoGrid.TabIndex = 1;
            this.grd_WB_WeightInfoGrid.TextResourceKey = null;
            this.grd_WB_WeightInfoGrid.UseCheckAllSummaryGrid = false;
            // 
            // grd_WB_WeightInfoGrid_Sheet1
            // 
            this.grd_WB_WeightInfoGrid_Sheet1.Reset();
            this.grd_WB_WeightInfoGrid_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.grd_WB_WeightInfoGrid_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnFooter.DefaultStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnFooter.DefaultStyle.ForeColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnFooter.DefaultStyle.Locked = false;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnFooterSheetCornerStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnFooterSheetCornerStyle.ForeColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnFooterSheetCornerStyle.Locked = false;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.ColumnHeader.DefaultStyle.Locked = false;
            this.grd_WB_WeightInfoGrid_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.DefaultStyle.Locked = false;
            this.grd_WB_WeightInfoGrid_Sheet1.FilterBar.DefaultStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.FilterBar.DefaultStyle.ForeColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.FilterBar.DefaultStyle.Locked = false;
            this.grd_WB_WeightInfoGrid_Sheet1.FilterBarHeaderStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.FilterBarHeaderStyle.ForeColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.FilterBarHeaderStyle.Locked = false;
            this.grd_WB_WeightInfoGrid_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.RowHeader.DefaultStyle.Locked = false;
            this.grd_WB_WeightInfoGrid_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty;
            this.grd_WB_WeightInfoGrid_Sheet1.SheetCornerStyle.Locked = false;
            this.grd_WB_WeightInfoGrid_Sheet1.SheetCornerStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.grd_WB_WeightInfoGrid_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // tTableLayoutPanel8
            // 
            this.tTableLayoutPanel8.AutoScroll = true;
            this.tTableLayoutPanel8.ColumnCount = 2;
            this.tTableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tTableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tTableLayoutPanel8.Controls.Add(this.tPanel3, 0, 0);
            this.tTableLayoutPanel8.Controls.Add(this.tPanel6, 1, 0);
            this.tTableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tTableLayoutPanel8.Name = "tTableLayoutPanel8";
            this.tTableLayoutPanel8.RowCount = 1;
            this.tTableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tTableLayoutPanel8.Size = new System.Drawing.Size(1978, 61);
            this.tTableLayoutPanel8.TabIndex = 3;
            // 
            // tPanel3
            // 
            this.tPanel3.BackColor = System.Drawing.SystemColors.Control;
            this.tPanel3.BorderColor = System.Drawing.Color.Empty;
            this.tPanel3.Controls.Add(this.tlbScanHere);
            this.tPanel3.Controls.Add(this.btnSearchDoc);
            this.tPanel3.Controls.Add(this.tbxQRCd);
            this.tPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel3.ForeColor = System.Drawing.SystemColors.Control;
            this.tPanel3.Location = new System.Drawing.Point(3, 3);
            this.tPanel3.Name = "tPanel3";
            this.tPanel3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tPanel3.Size = new System.Drawing.Size(587, 55);
            this.tPanel3.TabIndex = 0;
            // 
            // tlbScanHere
            // 
            this.tlbScanHere.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tlbScanHere.BackColor = System.Drawing.SystemColors.Control;
            this.tlbScanHere.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbScanHere.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.tlbScanHere.IsAppliedLinkedStyle = false;
            this.tlbScanHere.LinkedLabelName = null;
            this.tlbScanHere.Location = new System.Drawing.Point(4, 20);
            this.tlbScanHere.Name = "tlbScanHere";
            this.tlbScanHere.Size = new System.Drawing.Size(129, 25);
            this.tlbScanHere.TabIndex = 15;
            this.tlbScanHere.Text = "Scan Here:";
            this.tlbScanHere.TextResourceKey = "WRD_PTWB_Main_Label_ScanHere";
            this.tlbScanHere.ToolTipResourceKey = null;
            this.tlbScanHere.UseDefaultStyle = false;
            // 
            // btnSearchDoc
            // 
            this.btnSearchDoc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearchDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSearchDoc.Image = global::Tsb.Product.WB.Client.Properties.Resources.Find;
            this.btnSearchDoc.LinkedLabelName = null;
            this.btnSearchDoc.Location = new System.Drawing.Point(435, 24);
            this.btnSearchDoc.Name = "btnSearchDoc";
            this.btnSearchDoc.Size = new System.Drawing.Size(24, 24);
            this.btnSearchDoc.TabIndex = 2;
            this.btnSearchDoc.TextResourceKey = null;
            this.btnSearchDoc.ToolTipResourceKey = null;
            this.btnSearchDoc.UseVisualStyleBackColor = true;
            this.btnSearchDoc.Click += new System.EventHandler(this.btnSearchDoc_Click);
            // 
            // tbxQRCd
            // 
            this.tbxQRCd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxQRCd.CustomMask = null;
            this.tbxQRCd.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxQRCd.IsMandatory = true;
            this.tbxQRCd.LinkedLabelName = "tlbScanHere";
            this.tbxQRCd.Location = new System.Drawing.Point(134, 19);
            this.tbxQRCd.Name = "tbxQRCd";
            this.tbxQRCd.Size = new System.Drawing.Size(298, 30);
            this.tbxQRCd.TabIndex = 1;
            this.tbxQRCd.TextResourceKey = null;
            this.tbxQRCd.UseDefaultStyle = false;
            this.tbxQRCd.UseTextMandatoryFont = false;
            // 
            // tPanel6
            // 
            this.tPanel6.BorderColor = System.Drawing.Color.Empty;
            this.tPanel6.Controls.Add(this.lblUsername);
            this.tPanel6.Controls.Add(this.label2);
            this.tPanel6.Controls.Add(this.tbxLane);
            this.tPanel6.Controls.Add(this.tlbGateLane);
            this.tPanel6.Controls.Add(this.tlbGatePoint);
            this.tPanel6.Controls.Add(this.tbxGatePoint);
            this.tPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel6.Location = new System.Drawing.Point(596, 3);
            this.tPanel6.Name = "tPanel6";
            this.tPanel6.Size = new System.Drawing.Size(1379, 55);
            this.tPanel6.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUsername.Location = new System.Drawing.Point(1263, 4);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(0, 16);
            this.lblUsername.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(1207, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "User ID:";
            // 
            // tbxLane
            // 
            this.tbxLane.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxLane.CustomMask = null;
            this.tbxLane.LinkedLabelName = null;
            this.tbxLane.Location = new System.Drawing.Point(365, 15);
            this.tbxLane.Name = "tbxLane";
            this.tbxLane.Size = new System.Drawing.Size(195, 22);
            this.tbxLane.TabIndex = 3;
            this.tbxLane.TextResourceKey = null;
            this.tbxLane.UseTextMandatoryFont = false;
            // 
            // tlbGateLane
            // 
            this.tlbGateLane.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tlbGateLane.AutoSize = true;
            this.tlbGateLane.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbGateLane.IsAppliedLinkedStyle = false;
            this.tlbGateLane.LinkedLabelName = null;
            this.tlbGateLane.Location = new System.Drawing.Point(322, 19);
            this.tlbGateLane.Name = "tlbGateLane";
            this.tlbGateLane.Size = new System.Drawing.Size(40, 14);
            this.tlbGateLane.TabIndex = 2;
            this.tlbGateLane.Text = "Lane:";
            this.tlbGateLane.TextResourceKey = "WRD_PTWB_Main_Label_Lane";
            this.tlbGateLane.ToolTipResourceKey = null;
            this.tlbGateLane.UseDefaultStyle = false;
            // 
            // tlbGatePoint
            // 
            this.tlbGatePoint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tlbGatePoint.AutoSize = true;
            this.tlbGatePoint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbGatePoint.IsAppliedLinkedStyle = false;
            this.tlbGatePoint.LinkedLabelName = null;
            this.tlbGatePoint.Location = new System.Drawing.Point(5, 19);
            this.tlbGatePoint.Name = "tlbGatePoint";
            this.tlbGatePoint.Size = new System.Drawing.Size(76, 14);
            this.tlbGatePoint.TabIndex = 1;
            this.tlbGatePoint.Text = "Gate Point:";
            this.tlbGatePoint.TextResourceKey = "WRD_PTWB_Main_Label_GatePoint";
            this.tlbGatePoint.ToolTipResourceKey = null;
            this.tlbGatePoint.UseDefaultStyle = false;
            // 
            // tbxGatePoint
            // 
            this.tbxGatePoint.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxGatePoint.CustomMask = null;
            this.tbxGatePoint.LinkedLabelName = null;
            this.tbxGatePoint.Location = new System.Drawing.Point(83, 15);
            this.tbxGatePoint.Name = "tbxGatePoint";
            this.tbxGatePoint.Size = new System.Drawing.Size(195, 22);
            this.tbxGatePoint.TabIndex = 0;
            this.tbxGatePoint.TextResourceKey = null;
            this.tbxGatePoint.UseTextMandatoryFont = false;
            // 
            // WBMainView
            // 
            this.AuthorizedMenuName = "MainView";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1984, 845);
            this.Controls.Add(this.tTableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "WBMainView";
            this.ShowInTaskbar = false;
            this.Text = "Fontos Framework Sample";
            this.TextResourceKey = "WRD_PTWB_ApplicationName";
            this.ViewLocation = new System.Drawing.Point(-510, 0);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.bdsToolbar)).EndInit();
            this.tTableLayoutPanel1.ResumeLayout(false);
            this.tTableLayoutPanel2.ResumeLayout(false);
            this.tGroupBox1.ResumeLayout(false);
            this.tTableLayoutPanel9.ResumeLayout(false);
            this.tPanel8.ResumeLayout(false);
            this.tPanel8.PerformLayout();
            this.tPanel16.ResumeLayout(false);
            this.tPanel16.PerformLayout();
            this.tPanel15.ResumeLayout(false);
            this.tPanel15.PerformLayout();
            this.tPanel14.ResumeLayout(false);
            this.tGroupBox3.ResumeLayout(false);
            this.tTableLayoutPanel6.ResumeLayout(false);
            this.tTableLayoutPanel5.ResumeLayout(false);
            this.tGroupBox2.ResumeLayout(false);
            this.tGroupBox2.PerformLayout();
            this.tGroupBox6.ResumeLayout(false);
            this.tTableLayoutPanel10.ResumeLayout(false);
            this.tPanel13.ResumeLayout(false);
            this.tPanel13.PerformLayout();
            this.tPanel11.ResumeLayout(false);
            this.tPanel11.PerformLayout();
            this.tPanel10.ResumeLayout(false);
            this.tPanel10.PerformLayout();
            this.tTableLayoutPanel3.ResumeLayout(false);
            this.tTableLayoutPanel4.ResumeLayout(false);
            this.tGroupBox4.ResumeLayout(false);
            this.tTableLayoutPanel7.ResumeLayout(false);
            this.tTableLayoutPanel7.PerformLayout();
            this.tPanel1.ResumeLayout(false);
            this.tPanel1.PerformLayout();
            this.tPanel5.ResumeLayout(false);
            this.tPanel4.ResumeLayout(false);
            this.tPanel12.ResumeLayout(false);
            this.tPanel12.PerformLayout();
            this.tPanel2.ResumeLayout(false);
            this.tPanel2.PerformLayout();
            this.tPanel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_WB_WeightInfoGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_WB_WeightInfoGrid_Sheet1)).EndInit();
            this.tTableLayoutPanel8.ResumeLayout(false);
            this.tPanel3.ResumeLayout(false);
            this.tPanel3.PerformLayout();
            this.tPanel6.ResumeLayout(false);
            this.tPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSearchParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMainParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMainGridParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsWeightInfo)).EndInit();
            this.ResumeLayout(false);

        }
        private void Button1_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int borderWidth = 1; // Độ rộng của viền
                Color borderColor = Color.Gray; // Màu của viền

                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawEllipse(pen, 0, 0, button.Width - borderWidth, button.Height - borderWidth);
                }
            }
        }
        #endregion

        private System.Windows.Forms.BindingSource bdsToolbar;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel1;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel2;
        private Tsb.Fontos.Win.Forms.TGroupBox tGroupBox2;
        private Tsb.Fontos.Win.Forms.TGroupBox tGroupBox3;
        private Tsb.Fontos.Win.Forms.TGroupBox tGroupBox1;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel3;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel4;
        private Tsb.Fontos.Win.Forms.TGroupBox tGroupBox4;
        private Tsb.Fontos.Win.Forms.TPanel tPanel12;
        private Tsb.Fontos.Win.Forms.TButton tbtnSearchDoc;
        private Tsb.Fontos.Win.Forms.TButton btnCancelJob;
        private Tsb.Fontos.Win.Forms.TButton btnPrintCIR;
        private Tsb.Fontos.Win.Forms.TButton btnConfirm;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel5;
        private Tsb.Fontos.Win.Forms.TGroupBox tGroupBox6;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel6;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel7;
        private Tsb.Fontos.Win.Forms.TDateTimePicker dtpWeightFrom;
        private Tsb.Fontos.Win.Forms.TPanel tPanel4;
        private Tsb.Fontos.Win.Forms.TPanel tPanel5;
        private Tsb.Fontos.Win.Forms.TLabel tLabel1;
        private Tsb.Fontos.Win.Forms.TDateTimePicker dtpWeightTo;
        private Tsb.Fontos.Win.Forms.TLabel tLabel2;
        private Tsb.Fontos.Win.Forms.TGroupBox tGroupBox5;
        private Tsb.Fontos.Win.Forms.TGroupBox tGroupBox7;
        private Tsb.Fontos.Win.Forms.TPanel tPanel2;
        private Tsb.Fontos.Win.Forms.TLabel tLabel3;
        private Tsb.Fontos.Win.Forms.TGroupBox tGroupBox8;
        private Tsb.Fontos.Win.Forms.TPanel tPanel1;
        private Tsb.Fontos.Win.Forms.TTextBox tbxSearchGridLorryNo;
        private Tsb.Fontos.Win.Forms.TLabel tLabel4;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel8;
        private Tsb.Fontos.Win.Forms.TPanel tPanel3;
        private Tsb.Fontos.Win.Forms.TTextBox tbxQRCd;
        private Tsb.Fontos.Win.Forms.TButton btnSearchDoc;
        private Tsb.Fontos.Win.Forms.TPanel tPanel6;
        private Tsb.Fontos.Win.Forms.TTextBox tbxLane;
        private Tsb.Fontos.Win.Forms.TLabel tlbGateLane;
        private Tsb.Fontos.Win.Forms.TLabel tlbGatePoint;
        private Tsb.Fontos.Win.Forms.TTextBox tbxGatePoint;
        private Label label2;
        private Label lblUsername;
        private Tsb.Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel9;
        private Tsb.Fontos.Win.Forms.TPanel tPanel7;
        private Tsb.Fontos.Win.Forms.TPanel tPanel8;
        private Tsb.Fontos.Win.Forms.TLabel tLabel5;
        private Tsb.Fontos.Win.Forms.TTextBox tbxChassisNo;
        private Tsb.Fontos.Win.Forms.TLabel tLabel8;
        private Tsb.Fontos.Win.Forms.TButton btnSearchLorry;
        private Tsb.Fontos.Win.Forms.TTextBox tbxKG;
        private Tsb.Fontos.Win.Forms.TLabel tLabel9;
        private Tsb.Fontos.Win.Forms.TTextBox tbxRmk;
        private Tsb.Fontos.Win.Forms.TLabel tLabel10;
        private Tsb.Fontos.Win.Forms.TButton btnReadWeight;
        private Tsb.Fontos.Win.Forms.TTextBox tbxReadWeight;
        private Tsb.Fontos.Win.Forms.TButton btnClear;
        private Tsb.Fontos.Win.Forms.TButton btnRemarkUpdate;
        private Tsb.Fontos.Win.Forms.TLabel tLabel18;
        private Tsb.Fontos.Win.Forms.TLabel tLabel17;
        private Tsb.Fontos.Win.Forms.TLabel tLabel16;
        private Tsb.Fontos.Win.Forms.TLabel tLabel15;
        private Tsb.Fontos.Win.Forms.TLabel tLabel14;
        private Tsb.Fontos.Win.Forms.TLabel tLabel13;
        private Tsb.Fontos.Win.Forms.TLabel tLabel12;
        private Tsb.Fontos.Win.Forms.TLabel tLabel11;
        private Tsb.Fontos.Win.Forms.TLabel tlbCargoWeight;
        private Tsb.Fontos.Win.Forms.TPanel tPanel9;
        private Tsb.Fontos.Win.Forms.TTextBox tbxLorryNo;
        private Tsb.Fontos.Win.Forms.TTextBox tbxVslCallId;
        private Tsb.Fontos.Win.Forms.TTextBox tbxShipgNoteNo;
        private Tsb.Fontos.Win.Forms.TTextBox tbxGrNo;
        private Tsb.Fontos.Win.Forms.TTextBox tbxSdoNo;
        private Tsb.Fontos.Win.Forms.TTextBox tbxCmdtDesc;
        private Tsb.Fontos.Win.Forms.TTextBox tbxCnsnShprNm;
        private Tsb.Fontos.Win.Forms.TTextBox tbxTransporterNm;
        private Tsb.Fontos.Win.Forms.TTextBox tbxBlNo;
        private Tsb.Fontos.Win.Forms.TTextBox tbxSecondWgt;
        private System.Windows.Forms.BindingSource bdsSearchParam;
        private System.Windows.Forms.BindingSource bdsMainParam;
        private System.Windows.Forms.BindingSource bdsMainGridParam;
        private System.Windows.Forms.BindingSource bdsWeightInfo;
        private Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel10;
        private Fontos.Win.Forms.TTextBox tbxCargoWeight;
        private Fontos.Win.Forms.TLabel tlbFirstWeight;
        private Fontos.Win.Forms.TTextBox tbxFirstWgt;
        private Fontos.Win.Forms.TLabel tlbSecondWeight;
        private Fontos.Win.Forms.TPanel tPanel13;
        private Fontos.Win.Forms.TPanel tPanel11;
        private Fontos.Win.Forms.TPanel tPanel10;
        private Fontos.Win.Forms.TPanel tPanel14;
        private Fontos.Win.Forms.TPanel tPanel15;
        private Fontos.Win.Forms.TLabel tlbScanHere;
        private Fontos.Win.Forms.TPanel tPanel16;
        private Fontos.Win.Grid.Spread.TSpreadGrid grd_WB_WeightInfoGrid;
        private FarPoint.Win.Spread.SheetView grd_WB_WeightInfoGrid_Sheet1;
    }
}