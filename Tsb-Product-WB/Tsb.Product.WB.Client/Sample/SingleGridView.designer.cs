
namespace Tsb.Product.WB.Client.Sample
{
    partial class SingleGridView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleGridView));
            this.pnlGrid = new Tsb.Fontos.Win.Forms.TPanel();
            this.grd_Sample_SingleGrid = new Tsb.Fontos.Win.Grid.Spread.TSpreadGrid();
            this.grd_Sample_SingleGrid_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.gbxDisplayOption = new Tsb.Fontos.Win.Forms.TGroupBox();
            this.tbxUnno = new Tsb.Fontos.Win.Forms.TTextBox();
            this.lblUnno = new Tsb.Fontos.Win.Forms.TLabel();
            this.tbxImdgClass = new Tsb.Fontos.Win.Forms.TTextBox();
            this.lblImdgClass = new Tsb.Fontos.Win.Forms.TLabel();
            this.bdsSearchParam = new System.Windows.Forms.BindingSource(this.components);
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Sample_SingleGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Sample_SingleGrid_Sheet1)).BeginInit();
            this.gbxDisplayOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSearchParam)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.SystemColors.Control;
            this.pnlGrid.BorderColor = System.Drawing.Color.Empty;
            this.pnlGrid.Controls.Add(this.grd_Sample_SingleGrid);
            this.pnlGrid.Controls.Add(this.gbxDisplayOption);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(1);
            this.pnlGrid.Size = new System.Drawing.Size(891, 617);
            this.pnlGrid.TabIndex = 59;
            // 
            // grd_Sample_SingleGrid
            // 
            this.grd_Sample_SingleGrid.AccessibleDescription = "";
            this.grd_Sample_SingleGrid.AllowDragFill = true;
            this.grd_Sample_SingleGrid.AppliedColSettingName = null;
            this.grd_Sample_SingleGrid.AppliedFilterList = null;
            this.grd_Sample_SingleGrid.AppliedFilterName = null;
            this.grd_Sample_SingleGrid.AppliedSortName = null;
            this.grd_Sample_SingleGrid.AppliedSummaryName = null;
            this.grd_Sample_SingleGrid.BlinkCellTimer = null;
            this.grd_Sample_SingleGrid.BlinkedCellsDic = null;
            this.grd_Sample_SingleGrid.BlinkedColor = System.Drawing.Color.Red;
            this.grd_Sample_SingleGrid.BlinkedInterval = 2000;
            this.grd_Sample_SingleGrid.BlinkedRowsInfo = null;
            this.grd_Sample_SingleGrid.BlinkRowTimer = null;
            this.grd_Sample_SingleGrid.BoldLetteringCellDic = null;
            this.grd_Sample_SingleGrid.BoldLetteringRowGuidList = null;
            this.grd_Sample_SingleGrid.CanMoveColumn = false;
            this.grd_Sample_SingleGrid.ColumnIndexDic = null;
            this.grd_Sample_SingleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_Sample_SingleGrid.DragFillMode = Tsb.Fontos.Win.Grid.Types.DragFillMode.Normal;
            this.grd_Sample_SingleGrid.FilteredItemList = null;
            this.grd_Sample_SingleGrid.GridBizRule = null;
            this.grd_Sample_SingleGrid.GridBizRuleClassAssemblyName = "Tsb.Catos.Cm.Win";
            this.grd_Sample_SingleGrid.GridBizRuleClassName = "Tsb.Catos.Cm.Win.Grid.CTGridBizRule";
            this.grd_Sample_SingleGrid.GridColumnSchema = null;
            this.grd_Sample_SingleGrid.GridDataItemClassAssemblyName = "Tsb.Product.WB.Common";
            this.grd_Sample_SingleGrid.GridDataItemClassName = "Tsb.Product.WB.Common.Item.Sample.SingleGridItem";
            this.grd_Sample_SingleGrid.GridPrintMainTiltle = null;
            this.grd_Sample_SingleGrid.GridPrintSubTitle = null;
            this.grd_Sample_SingleGrid.InsertRowType = Tsb.Fontos.Win.Grid.Types.InsertRowTypes.ACTIVECELL;
            this.grd_Sample_SingleGrid.IsFillUpWithBlankRows = false;
            this.grd_Sample_SingleGrid.IsGridFilterApplingEvent = false;
            this.grd_Sample_SingleGrid.Location = new System.Drawing.Point(1, 52);
            this.grd_Sample_SingleGrid.LockedCellsDic = null;
            this.grd_Sample_SingleGrid.LockedRowsInfo = null;
            this.grd_Sample_SingleGrid.MandatoryFieldDic = null;
            this.grd_Sample_SingleGrid.Name = "grd_Sample_SingleGrid";
            this.grd_Sample_SingleGrid.ObjectID = "GNR-CTDG-GRD-TSpreadGrid";
            this.grd_Sample_SingleGrid.ReferenceGridXmlPath = null;
            this.grd_Sample_SingleGrid.SchemaVersion = Tsb.Fontos.Win.Grid.Types.GridSchemaVersion.FONTOS_VERSION_1_0;
            this.grd_Sample_SingleGrid.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.grd_Sample_SingleGrid_Sheet1});
            this.grd_Sample_SingleGrid.Size = new System.Drawing.Size(889, 564);
            this.grd_Sample_SingleGrid.SourceItemList = null;
            this.grd_Sample_SingleGrid.SpecifiedSchemaFileFolderName = "";
            this.grd_Sample_SingleGrid.SpecifiedSchemaFileName = "";
            this.grd_Sample_SingleGrid.TabIndex = 58;
            // 
            // grd_Sample_SingleGrid_Sheet1
            // 
            this.grd_Sample_SingleGrid_Sheet1.Reset();
            this.grd_Sample_SingleGrid_Sheet1.SheetName = "Sheet1";
            this.grd_Sample_SingleGrid_Sheet1.NamedStyles = new FarPoint.Win.Spread.NamedStyleCollection();
            // 
            // gbxDisplayOption
            // 
            this.gbxDisplayOption.Controls.Add(this.tbxUnno);
            this.gbxDisplayOption.Controls.Add(this.lblUnno);
            this.gbxDisplayOption.Controls.Add(this.tbxImdgClass);
            this.gbxDisplayOption.Controls.Add(this.lblImdgClass);
            this.gbxDisplayOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxDisplayOption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDisplayOption.Location = new System.Drawing.Point(1, 1);
            this.gbxDisplayOption.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.gbxDisplayOption.Name = "gbxDisplayOption";
            this.gbxDisplayOption.Size = new System.Drawing.Size(889, 51);
            this.gbxDisplayOption.TabIndex = 57;
            this.gbxDisplayOption.TabStop = false;
            this.gbxDisplayOption.Text = "DisplayOption";
            this.gbxDisplayOption.TextResourceKey = "WRD_PTWB_DisplayOption";
            // 
            // tbxUnno
            // 
            this.tbxUnno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbxUnno.ControlType = Tsb.Fontos.Win.Forms.ValueControlType.EDITABLE;
            this.tbxUnno.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxUnno.LinkedLabelName = null;
            this.tbxUnno.Location = new System.Drawing.Point(279, 20);
            this.tbxUnno.MaxLength = 4;
            this.tbxUnno.Name = "tbxUnno";
            this.tbxUnno.Size = new System.Drawing.Size(116, 22);
            this.tbxUnno.TabIndex = 11;
            this.tbxUnno.TextResourceKey = null;
            this.tbxUnno.UseDefaultColorStyle = true;
            this.tbxUnno.UseTextMandatoryFont = false;
            // 
            // lblUnno
            // 
            this.lblUnno.AutoSize = true;
            this.lblUnno.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnno.IsAppliedLinkedStyle = false;
            this.lblUnno.LinkedLabelName = null;
            this.lblUnno.Location = new System.Drawing.Point(231, 22);
            this.lblUnno.Name = "lblUnno";
            this.lblUnno.Size = new System.Drawing.Size(42, 14);
            this.lblUnno.TabIndex = 10;
            this.lblUnno.Text = "UNNo.";
            this.lblUnno.TextResourceKey = "WRD_PTWB_UNNo";
            this.lblUnno.ToolTipResourceKey = null;
            // 
            // tbxImdgClass
            // 
            this.tbxImdgClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tbxImdgClass.ControlType = Tsb.Fontos.Win.Forms.ValueControlType.MANDATORY;
            this.tbxImdgClass.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxImdgClass.IsMandatory = true;
            this.tbxImdgClass.LinkedLabelName = "lblImdgClass";
            this.tbxImdgClass.Location = new System.Drawing.Point(89, 20);
            this.tbxImdgClass.MaxLength = 4;
            this.tbxImdgClass.Name = "tbxImdgClass";
            this.tbxImdgClass.Size = new System.Drawing.Size(116, 22);
            this.tbxImdgClass.TabIndex = 9;
            this.tbxImdgClass.TextResourceKey = null;
            this.tbxImdgClass.UseDefaultColorStyle = true;
            this.tbxImdgClass.UseDefaultMandatoryStyle = true;
            this.tbxImdgClass.UseTextMandatoryFont = false;
            // 
            // lblImdgClass
            // 
            this.lblImdgClass.AutoSize = true;
            this.lblImdgClass.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImdgClass.IsAppliedLinkedStyle = false;
            this.lblImdgClass.LinkedLabelName = null;
            this.lblImdgClass.Location = new System.Drawing.Point(10, 22);
            this.lblImdgClass.Name = "lblImdgClass";
            this.lblImdgClass.Size = new System.Drawing.Size(65, 14);
            this.lblImdgClass.TabIndex = 8;
            this.lblImdgClass.Text = "IMDG Class";
            this.lblImdgClass.TextResourceKey = "WRD_PTWB_IMDGClass";
            this.lblImdgClass.ToolTipResourceKey = null;
            // 
            // SingleGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 617);
            this.Controls.Add(this.pnlGrid);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SingleGridView";
            this.Text = "Error!! The specified label was not found. Check TextResourceKey property";
            this.TextResourceKey = "WRD_PTWB_Menu_SingleGrid";
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Sample_SingleGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Sample_SingleGrid_Sheet1)).EndInit();
            this.gbxDisplayOption.ResumeLayout(false);
            this.gbxDisplayOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSearchParam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Tsb.Fontos.Win.Forms.TPanel pnlGrid;
        private Tsb.Fontos.Win.Grid.Spread.TSpreadGrid grd_Sample_SingleGrid;
        private FarPoint.Win.Spread.SheetView grd_Sample_SingleGrid_Sheet1;
        private Tsb.Fontos.Win.Forms.TGroupBox gbxDisplayOption;
        private Tsb.Fontos.Win.Forms.TTextBox tbxUnno;
        private Tsb.Fontos.Win.Forms.TLabel lblUnno;
        private Tsb.Fontos.Win.Forms.TTextBox tbxImdgClass;
        private Tsb.Fontos.Win.Forms.TLabel lblImdgClass;
        private System.Windows.Forms.BindingSource bdsSearchParam;

    }
}