using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;

namespace Tsb.Catos.Cm.Mobile.Win.BlockBayList
{
    partial class BlockBayListView
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
            this.pnlMain = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.pnlTitle = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblTitle = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlInput = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.lblBlock = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlbay = new Tsb.Fontos.Win.Forms.TPanel();
            this.bayList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.pnlBlock = new Tsb.Fontos.Win.Forms.TPanel();
            this.blockList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.lblBay = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlButton = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.btnOk = new Tsb.Fontos.Win.Forms.TButton();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.chbShowAll = new Tsb.Fontos.Win.Forms.TCheckBox();
            this.pnlMain.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlbay.SuspendLayout();
            this.pnlBlock.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.pnlTitle, 0, 0);
            this.pnlMain.Controls.Add(this.pnlInput, 0, 1);
            this.pnlMain.Controls.Add(this.pnlButton, 0, 2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(10, 10);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 3;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.Size = new System.Drawing.Size(880, 630);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderColor = System.Drawing.Color.Empty;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(3, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(874, 44);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.CustomStyleName = "biz_default_lbl_title";
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.IsAppliedLinkedStyle = false;
            this.lblTitle.LinkedLabelName = null;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(874, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Yard Position";
            this.lblTitle.TextResourceKey = "WRD_CTMO_YardPosition";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // pnlInput
            // 
            this.pnlInput.ColumnCount = 3;
            this.pnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlInput.Controls.Add(this.lblBlock, 0, 0);
            this.pnlInput.Controls.Add(this.pnlbay, 2, 1);
            this.pnlInput.Controls.Add(this.pnlBlock, 0, 1);
            this.pnlInput.Controls.Add(this.lblBay, 2, 0);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(3, 53);
            this.pnlInput.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.RowCount = 3;
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlInput.Size = new System.Drawing.Size(874, 527);
            this.pnlInput.TabIndex = 1;
            // 
            // lblBlock
            // 
            this.lblBlock.BackColor = System.Drawing.Color.White;
            this.lblBlock.CustomStyleName = "biz_default_lbl_subTitle";
            this.lblBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBlock.IsAppliedLinkedStyle = false;
            this.lblBlock.LinkedLabelName = null;
            this.lblBlock.Location = new System.Drawing.Point(3, 3);
            this.lblBlock.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(426, 47);
            this.lblBlock.TabIndex = 2;
            this.lblBlock.Text = "Block / Area";
            this.lblBlock.TextResourceKey = "WRD_CTMO_BlockArea";
            this.lblBlock.ToolTipResourceKey = null;
            // 
            // pnlbay
            // 
            this.pnlbay.BackColor = System.Drawing.Color.White;
            this.pnlbay.BorderColor = System.Drawing.Color.Empty;
            this.pnlbay.Controls.Add(this.bayList);
            this.pnlbay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlbay.Location = new System.Drawing.Point(445, 50);
            this.pnlbay.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlbay.Name = "pnlbay";
            this.pnlbay.Size = new System.Drawing.Size(426, 464);
            this.pnlbay.TabIndex = 4;
            // 
            // bayList
            // 
            this.bayList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.bayList.ButtonListCustomStyleName = "biz_blockbay_btn_list";
            this.bayList.ButtonListSelectedCustomStyleName = "biz_selected_btn_list";
            this.bayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bayList.GapBetweenControls = 4;
            this.bayList.ItemList = null;
            this.bayList.Location = new System.Drawing.Point(0, 0);
            this.bayList.MainBackColor = System.Drawing.Color.White;
            this.bayList.Margin = new System.Windows.Forms.Padding(0);
            this.bayList.MaxColumnCount = 4;
            this.bayList.MaxRowCount = 9;
            this.bayList.Name = "bayList";
            this.bayList.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.bayList.RightExtensionMargin = 0;
            this.bayList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.bayList.ScrollVisible = true;
            this.bayList.Size = new System.Drawing.Size(426, 464);
            this.bayList.TabIndex = 2;
            this.bayList.UseMultipleSelection = false;
            this.bayList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.bayList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.bayList.VerticalScrollWidth = 40;
            this.bayList.VisibleZoomSlider = false;
            // 
            // pnlBlock
            // 
            this.pnlBlock.BackColor = System.Drawing.Color.White;
            this.pnlBlock.BorderColor = System.Drawing.Color.Empty;
            this.pnlBlock.Controls.Add(this.blockList);
            this.pnlBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBlock.Location = new System.Drawing.Point(3, 50);
            this.pnlBlock.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlBlock.Name = "pnlBlock";
            this.pnlBlock.Size = new System.Drawing.Size(426, 464);
            this.pnlBlock.TabIndex = 5;
            // 
            // blockList
            // 
            this.blockList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.blockList.BackColor = System.Drawing.Color.White;
            this.blockList.ButtonListCustomStyleName = "biz_blockbay_btn_list";
            this.blockList.ButtonListSelectedCustomStyleName = "biz_selected_btn_list";
            this.blockList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockList.GapBetweenControls = 4;
            this.blockList.ItemList = null;
            this.blockList.Location = new System.Drawing.Point(0, 0);
            this.blockList.MainBackColor = System.Drawing.Color.White;
            this.blockList.Margin = new System.Windows.Forms.Padding(0);
            this.blockList.MaxColumnCount = 4;
            this.blockList.MaxRowCount = 9;
            this.blockList.Name = "blockList";
            this.blockList.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.blockList.RightExtensionMargin = 0;
            this.blockList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.blockList.ScrollVisible = true;
            this.blockList.Size = new System.Drawing.Size(426, 464);
            this.blockList.TabIndex = 1;
            this.blockList.UseMultipleSelection = false;
            this.blockList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.blockList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.blockList.VerticalScrollWidth = 40;
            this.blockList.VisibleZoomSlider = false;
            // 
            // lblBay
            // 
            this.lblBay.BackColor = System.Drawing.Color.White;
            this.lblBay.CustomStyleName = "biz_default_lbl_subTitle";
            this.lblBay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBay.IsAppliedLinkedStyle = false;
            this.lblBay.LinkedLabelName = null;
            this.lblBay.Location = new System.Drawing.Point(445, 0);
            this.lblBay.Name = "lblBay";
            this.lblBay.Size = new System.Drawing.Size(426, 50);
            this.lblBay.TabIndex = 6;
            this.lblBay.Text = "Bay";
            this.lblBay.TextResourceKey = "WRD_CTMO_Bay";
            this.lblBay.ToolTipResourceKey = null;
            // 
            // pnlButton
            // 
            this.pnlButton.ColumnCount = 3;
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.0177F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.9823F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 187F));
            this.pnlButton.Controls.Add(this.btnOk, 1, 0);
            this.pnlButton.Controls.Add(this.btnCancel, 2, 0);
            this.pnlButton.Controls.Add(this.chbShowAll, 0, 0);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(0, 580);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.RowCount = 1;
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlButton.Size = new System.Drawing.Size(880, 50);
            this.pnlButton.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.CustomStyleName = "biz_btn_ok";
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.LinkedLabelName = null;
            this.btnOk.Location = new System.Drawing.Point(509, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(180, 44);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.TextResourceKey = "WRD_CTMO_Ok";
            this.btnOk.ToolTipResourceKey = null;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.CustomStyleName = "biz_btn_exit";
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.LinkedLabelName = null;
            this.btnCancel.Location = new System.Drawing.Point(695, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 44);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextResourceKey = "WRD_CTMO_Cancel";
            this.btnCancel.ToolTipResourceKey = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chbShowAll
            // 
            this.chbShowAll.AutoSize = true;
            this.chbShowAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.chbShowAll.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chbShowAll.ForeColor = System.Drawing.Color.Black;
            this.chbShowAll.LinkedLabelName = null;
            this.chbShowAll.Location = new System.Drawing.Point(236, 3);
            this.chbShowAll.Name = "chbShowAll";
            this.chbShowAll.Size = new System.Drawing.Size(218, 44);
            this.chbShowAll.TabIndex = 6;
            this.chbShowAll.Text = "Show all Block / Area";
            this.chbShowAll.TextResourceKey = "WRD_CTMO_ShowAllBlockArea";
            this.chbShowAll.ToolTipResourceKey = null;
            this.chbShowAll.UseDefaultStyle = false;
            this.chbShowAll.UseVisualStyleBackColor = true;
            // 
            // BlockBayListView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.pnlMain);
            this.CustomStyleName = "biz_control_view";
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "BlockBayListView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "BlockBayListView";
            this.pnlMain.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlbay.ResumeLayout(false);
            this.pnlBlock.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel pnlMain;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TTableLayoutPanel pnlInput;
        private Fontos.Win.Forms.TTableLayoutPanel pnlButton;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TLabel lblBlock;
        private Fontos.Win.Forms.TPanel pnlbay;
        private Fontos.Win.Forms.TPanel pnlBlock;
        private ItemListControl blockList;
        private Fontos.Win.Forms.TButton btnOk;
        private Fontos.Win.Forms.TCheckBox chbShowAll;
        private ItemListControl bayList;
        private Fontos.Win.Forms.TLabel lblBay;
    }
}