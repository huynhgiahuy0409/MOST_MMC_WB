using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;

namespace Tsb.Catos.Cm.Mobile.Win.VesselJobBayList
{
    partial class VesselJobBayListView
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
            this.jobBayList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.pnlBlock = new Tsb.Fontos.Win.Forms.TPanel();
            this.vesselList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.lblBay = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlButton = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.btnOk = new Tsb.Fontos.Win.Forms.TButton();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.pnlHoldDeck = new Tsb.Fontos.Win.Forms.TPanel();
            this.rbtnDeck = new Tsb.Fontos.Win.Forms.TRadioButton();
            this.rbtnHold = new Tsb.Fontos.Win.Forms.TRadioButton();
            this.pnlMain.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlbay.SuspendLayout();
            this.pnlBlock.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlHoldDeck.SuspendLayout();
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
            this.pnlMain.Size = new System.Drawing.Size(753, 543);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderColor = System.Drawing.Color.Empty;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(3, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(747, 44);
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
            this.lblTitle.Size = new System.Drawing.Size(747, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Location";
            this.lblTitle.TextResourceKey = "WRD_CTMO_Location";
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
            this.pnlInput.Size = new System.Drawing.Size(747, 440);
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
            this.lblBlock.Size = new System.Drawing.Size(362, 47);
            this.lblBlock.TabIndex = 2;
            this.lblBlock.Text = "Vessel Schedule";
            this.lblBlock.TextResourceKey = "WRD_CTMO_VesselSchedule";
            this.lblBlock.ToolTipResourceKey = null;
            // 
            // pnlbay
            // 
            this.pnlbay.BackColor = System.Drawing.Color.White;
            this.pnlbay.BorderColor = System.Drawing.Color.Empty;
            this.pnlbay.Controls.Add(this.jobBayList);
            this.pnlbay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlbay.Location = new System.Drawing.Point(381, 50);
            this.pnlbay.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlbay.Name = "pnlbay";
            this.pnlbay.Size = new System.Drawing.Size(363, 377);
            this.pnlbay.TabIndex = 4;
            // 
            // jobBayList
            // 
            this.jobBayList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.jobBayList.ButtonListCustomStyleName = "biz_blockbay_btn_list";
            this.jobBayList.ButtonListSelectedCustomStyleName = "biz_selected_btn_list";
            this.jobBayList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobBayList.GapBetweenControls = 4;
            this.jobBayList.ItemList = null;
            this.jobBayList.Location = new System.Drawing.Point(0, 0);
            this.jobBayList.MainBackColor = System.Drawing.Color.White;
            this.jobBayList.Margin = new System.Windows.Forms.Padding(0);
            this.jobBayList.MaxColumnCount = 3;
            this.jobBayList.MaxRowCount = 9;
            this.jobBayList.Name = "jobBayList";
            this.jobBayList.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.jobBayList.RightExtensionMargin = 0;
            this.jobBayList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.jobBayList.ScrollVisible = true;
            this.jobBayList.Size = new System.Drawing.Size(363, 377);
            this.jobBayList.TabIndex = 2;
            this.jobBayList.UseActivatedInAllBay = false;
            this.jobBayList.UseMultipleSelection = false;
            this.jobBayList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.jobBayList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.jobBayList.VerticalScrollWidth = 40;
            this.jobBayList.VisibleZoomSlider = false;
            // 
            // pnlBlock
            // 
            this.pnlBlock.BackColor = System.Drawing.Color.White;
            this.pnlBlock.BorderColor = System.Drawing.Color.Empty;
            this.pnlBlock.Controls.Add(this.vesselList);
            this.pnlBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBlock.Location = new System.Drawing.Point(3, 50);
            this.pnlBlock.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlBlock.Name = "pnlBlock";
            this.pnlBlock.Size = new System.Drawing.Size(362, 377);
            this.pnlBlock.TabIndex = 5;
            // 
            // vesselList
            // 
            this.vesselList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.vesselList.BackColor = System.Drawing.Color.White;
            this.vesselList.ButtonListCustomStyleName = "biz_blockbay_btn_list";
            this.vesselList.ButtonListSelectedCustomStyleName = "biz_selected_btn_list";
            this.vesselList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vesselList.GapBetweenControls = 4;
            this.vesselList.ItemList = null;
            this.vesselList.Location = new System.Drawing.Point(0, 0);
            this.vesselList.MainBackColor = System.Drawing.Color.White;
            this.vesselList.Margin = new System.Windows.Forms.Padding(0);
            this.vesselList.MaxColumnCount = 3;
            this.vesselList.MaxRowCount = 9;
            this.vesselList.Name = "vesselList";
            this.vesselList.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.vesselList.RightExtensionMargin = 0;
            this.vesselList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.vesselList.ScrollVisible = true;
            this.vesselList.Size = new System.Drawing.Size(362, 377);
            this.vesselList.TabIndex = 1;
            this.vesselList.UseActivatedInAllBay = false;
            this.vesselList.UseMultipleSelection = false;
            this.vesselList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.vesselList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.vesselList.VerticalScrollWidth = 40;
            this.vesselList.VisibleZoomSlider = false;
            // 
            // lblBay
            // 
            this.lblBay.BackColor = System.Drawing.Color.White;
            this.lblBay.CustomStyleName = "biz_default_lbl_subTitle";
            this.lblBay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBay.IsAppliedLinkedStyle = false;
            this.lblBay.LinkedLabelName = null;
            this.lblBay.Location = new System.Drawing.Point(381, 0);
            this.lblBay.Name = "lblBay";
            this.lblBay.Size = new System.Drawing.Size(363, 50);
            this.lblBay.TabIndex = 6;
            this.lblBay.Text = "Job Bay";
            this.lblBay.TextResourceKey = "WRD_CTMO_JobBay";
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
            this.pnlButton.Controls.Add(this.pnlHoldDeck, 0, 0);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(0, 493);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.RowCount = 1;
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlButton.Size = new System.Drawing.Size(753, 50);
            this.pnlButton.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.CustomStyleName = "biz_btn_ok";
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.LinkedLabelName = null;
            this.btnOk.Location = new System.Drawing.Point(382, 3);
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
            this.btnCancel.Location = new System.Drawing.Point(568, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 44);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextResourceKey = "WRD_CTMO_Cancel";
            this.btnCancel.ToolTipResourceKey = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pnlHoldDeck
            // 
            this.pnlHoldDeck.BorderColor = System.Drawing.Color.Empty;
            this.pnlHoldDeck.Controls.Add(this.rbtnDeck);
            this.pnlHoldDeck.Controls.Add(this.rbtnHold);
            this.pnlHoldDeck.Location = new System.Drawing.Point(3, 3);
            this.pnlHoldDeck.Name = "pnlHoldDeck";
            this.pnlHoldDeck.Size = new System.Drawing.Size(367, 44);
            this.pnlHoldDeck.TabIndex = 6;
            // 
            // rbtnDeck
            // 
            this.rbtnDeck.AutoSize = true;
            this.rbtnDeck.CustomStyleName = "biz_rbtn_deck";
            this.rbtnDeck.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDeck.ForeColor = System.Drawing.Color.Black;
            this.rbtnDeck.LinkedLabelName = null;
            this.rbtnDeck.Location = new System.Drawing.Point(154, 8);
            this.rbtnDeck.Name = "rbtnDeck";
            this.rbtnDeck.Size = new System.Drawing.Size(86, 33);
            this.rbtnDeck.TabIndex = 1;
            this.rbtnDeck.TabStop = true;
            this.rbtnDeck.Text = "Deck";
            this.rbtnDeck.TextResourceKey = "WRD_CTMO_Deck";
            this.rbtnDeck.UseVisualStyleBackColor = true;
            // 
            // rbtnHold
            // 
            this.rbtnHold.AutoSize = true;
            this.rbtnHold.CustomStyleName = "biz_rbtn_hold";
            this.rbtnHold.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnHold.ForeColor = System.Drawing.Color.Black;
            this.rbtnHold.LinkedLabelName = null;
            this.rbtnHold.Location = new System.Drawing.Point(60, 8);
            this.rbtnHold.Name = "rbtnHold";
            this.rbtnHold.Size = new System.Drawing.Size(81, 33);
            this.rbtnHold.TabIndex = 0;
            this.rbtnHold.Text = "Hold";
            this.rbtnHold.TextResourceKey = "WRD_CTMO_Hold";
            this.rbtnHold.UseVisualStyleBackColor = true;
            // 
            // VesselJobBayListView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(773, 563);
            this.Controls.Add(this.pnlMain);
            this.CustomStyleName = "biz_control_view";
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "VesselJobBayListView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "BlockBayListView";
            this.pnlMain.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlbay.ResumeLayout(false);
            this.pnlBlock.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlHoldDeck.ResumeLayout(false);
            this.pnlHoldDeck.PerformLayout();
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
        private ItemListControl vesselList;
        private Fontos.Win.Forms.TButton btnOk;
        private ItemListControl jobBayList;
        private Fontos.Win.Forms.TLabel lblBay;
        private Fontos.Win.Forms.TPanel pnlHoldDeck;
        private Fontos.Win.Forms.TRadioButton rbtnDeck;
        private Fontos.Win.Forms.TRadioButton rbtnHold;
    }
}