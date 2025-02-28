namespace Tsb.Catos.Cm.Mobile.Win.HoldDeck
{
    partial class HoldDeckPopupView
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
            this.pnlBlock = new Tsb.Fontos.Win.Forms.TPanel();
            this.pnlButton = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.holdDeckList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.pnlMain.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlInput.SuspendLayout();
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
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 3;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.Size = new System.Drawing.Size(500, 300);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderColor = System.Drawing.Color.Empty;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(3, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(494, 44);
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
            this.lblTitle.Size = new System.Drawing.Size(494, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HoldDeck Selection";
            this.lblTitle.TextResourceKey = "WRD_CTMO_HoldDeckSelection";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // pnlInput
            // 
            this.pnlInput.ColumnCount = 2;
            this.pnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlInput.Controls.Add(this.pnlBlock, 0, 0);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(3, 53);
            this.pnlInput.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.RowCount = 2;
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlInput.Size = new System.Drawing.Size(494, 197);
            this.pnlInput.TabIndex = 1;
            // 
            // pnlBlock
            // 
            this.pnlBlock.BackColor = System.Drawing.Color.White;
            this.pnlBlock.BorderColor = System.Drawing.Color.Empty;
            this.pnlBlock.Controls.Add(this.holdDeckList);
            this.pnlBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBlock.Location = new System.Drawing.Point(3, 0);
            this.pnlBlock.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlBlock.Name = "pnlBlock";
            this.pnlBlock.Size = new System.Drawing.Size(478, 184);
            this.pnlBlock.TabIndex = 5;
            // 
            // pnlButton
            // 
            this.pnlButton.ColumnCount = 2;
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlButton.Controls.Add(this.btnCancel, 1, 0);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(0, 250);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.RowCount = 1;
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlButton.Size = new System.Drawing.Size(500, 50);
            this.pnlButton.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.CustomStyleName = "biz_btn_exit";
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.LinkedLabelName = null;
            this.btnCancel.Location = new System.Drawing.Point(366, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 44);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextResourceKey = "WRD_CTMO_Cancel";
            this.btnCancel.ToolTipResourceKey = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // holdDeckList
            // 
            this.holdDeckList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.holdDeckList.BackColor = System.Drawing.Color.White;
            this.holdDeckList.ButtonListCustomStyleName = "biz_blockbay_btn_list";
            this.holdDeckList.ButtonListSelectedCustomStyleName = "biz_selected_btn_list";
            this.holdDeckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.holdDeckList.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.holdDeckList.GapBetweenControls = 4;
            this.holdDeckList.ItemList = null;
            this.holdDeckList.Location = new System.Drawing.Point(0, 0);
            this.holdDeckList.MainBackColor = System.Drawing.Color.White;
            this.holdDeckList.Margin = new System.Windows.Forms.Padding(0);
            this.holdDeckList.MaxColumnCount = 2;
            this.holdDeckList.MaxRowCount = 1;
            this.holdDeckList.MouseDragLine = null;
            this.holdDeckList.Name = "holdDeckList";
            this.holdDeckList.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.holdDeckList.RightExtensionMargin = 0;
            this.holdDeckList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.holdDeckList.ScrollVisible = true;
            this.holdDeckList.Size = new System.Drawing.Size(478, 184);
            this.holdDeckList.TabIndex = 1;
            this.holdDeckList.UseActivatedInAllBay = false;
            this.holdDeckList.UseMultipleSelection = false;
            this.holdDeckList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.holdDeckList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.holdDeckList.VerticalScrollWidth = 40;
            this.holdDeckList.VisibleZoomSlider = false;
            // 
            // HoldDeckPopupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.pnlMain);
            this.Name = "HoldDeckPopupView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HoldDeckPopupView";
            this.pnlMain.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlBlock.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel pnlMain;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TTableLayoutPanel pnlInput;
        private Fontos.Win.Forms.TTableLayoutPanel pnlButton;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TPanel pnlBlock;
        private Common.Controls.ItemList.ItemListControl holdDeckList;
    }
}