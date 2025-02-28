namespace Tsb.Catos.Cm.Mobile.Win.MobileContainerDetail
{
    partial class MobileContainerDetailView
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
            this.tlpMainViewLayout = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.pnlButton = new Tsb.Fontos.Win.Forms.TPanel();
            this.btnSeal = new Tsb.Fontos.Win.Forms.TButton();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.pnlTitle = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblTitle = new Tsb.Fontos.Win.Forms.TLabel();
            this.tlpMainLayout = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.pnlMain = new Tsb.Fontos.Win.Forms.TPanel();
            this.tlpSearchLayout = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.lblCntrNo = new Tsb.Fontos.Win.Forms.TLabel();
            this.tbxContainerNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.pnlSearch = new Tsb.Fontos.Win.Forms.TPanel();
            this.btnSearch = new Tsb.Fontos.Win.Forms.TButton();
            this.tlpMainViewLayout.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.tlpMainLayout.SuspendLayout();
            this.tlpSearchLayout.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainViewLayout
            // 
            this.tlpMainViewLayout.ColumnCount = 1;
            this.tlpMainViewLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainViewLayout.Controls.Add(this.pnlButton, 0, 2);
            this.tlpMainViewLayout.Controls.Add(this.pnlTitle, 0, 0);
            this.tlpMainViewLayout.Controls.Add(this.tlpMainLayout, 0, 1);
            this.tlpMainViewLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainViewLayout.Location = new System.Drawing.Point(10, 10);
            this.tlpMainViewLayout.Name = "tlpMainViewLayout";
            this.tlpMainViewLayout.RowCount = 3;
            this.tlpMainViewLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainViewLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainViewLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainViewLayout.Size = new System.Drawing.Size(757, 640);
            this.tlpMainViewLayout.TabIndex = 7;
            // 
            // pnlButton
            // 
            this.pnlButton.BorderColor = System.Drawing.Color.Empty;
            this.pnlButton.Controls.Add(this.btnSeal);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(3, 593);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(751, 44);
            this.pnlButton.TabIndex = 6;
            // 
            // btnSeal
            // 
            this.btnSeal.CustomStyleName = "biz_cntrDetail_btn_ok";
            this.btnSeal.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSeal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeal.LinkedLabelName = null;
            this.btnSeal.Location = new System.Drawing.Point(0, 0);
            this.btnSeal.Name = "btnSeal";
            this.btnSeal.Size = new System.Drawing.Size(181, 44);
            this.btnSeal.TabIndex = 1;
            this.btnSeal.Text = "Seal";
            this.btnSeal.TextResourceKey = "WRD_CTMO_Seal";
            this.btnSeal.ToolTipResourceKey = null;
            this.btnSeal.UseVisualStyleBackColor = true;
            this.btnSeal.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.CustomStyleName = "biz_btn_exit";
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.LinkedLabelName = null;
            this.btnCancel.Location = new System.Drawing.Point(570, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(181, 44);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextResourceKey = "WRD_CTMO_Cancel";
            this.btnCancel.ToolTipResourceKey = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderColor = System.Drawing.Color.Empty;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(3, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(751, 44);
            this.pnlTitle.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.CustomStyleName = "biz_default_lbl_title";
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.IsAppliedLinkedStyle = false;
            this.lblTitle.LinkedLabelName = null;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(751, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Container Information";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.TextResourceKey = "WRD_CTMO_ContainerInformation";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 1;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Controls.Add(this.pnlMain, 0, 1);
            this.tlpMainLayout.Controls.Add(this.tlpSearchLayout, 0, 0);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(3, 53);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 2;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Size = new System.Drawing.Size(751, 534);
            this.tlpMainLayout.TabIndex = 7;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Empty;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 34);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(751, 500);
            this.pnlMain.TabIndex = 0;
            // 
            // tlpSearchLayout
            // 
            this.tlpSearchLayout.ColumnCount = 3;
            this.tlpSearchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.84917F));
            this.tlpSearchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.15083F));
            this.tlpSearchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 186F));
            this.tlpSearchLayout.Controls.Add(this.lblCntrNo, 0, 0);
            this.tlpSearchLayout.Controls.Add(this.tbxContainerNo, 1, 0);
            this.tlpSearchLayout.Controls.Add(this.pnlSearch, 2, 0);
            this.tlpSearchLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSearchLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpSearchLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSearchLayout.Name = "tlpSearchLayout";
            this.tlpSearchLayout.RowCount = 1;
            this.tlpSearchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSearchLayout.Size = new System.Drawing.Size(751, 34);
            this.tlpSearchLayout.TabIndex = 1;
            // 
            // lblCntrNo
            // 
            this.lblCntrNo.AutoSize = true;
            this.lblCntrNo.CustomStyleName = "biz_cntrDetail_lbl";
            this.lblCntrNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCntrNo.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCntrNo.IsAppliedLinkedStyle = false;
            this.lblCntrNo.LinkedLabelName = null;
            this.lblCntrNo.Location = new System.Drawing.Point(3, 3);
            this.lblCntrNo.Margin = new System.Windows.Forms.Padding(3);
            this.lblCntrNo.Name = "lblCntrNo";
            this.lblCntrNo.Size = new System.Drawing.Size(162, 28);
            this.lblCntrNo.TabIndex = 4;
            this.lblCntrNo.Text = "ContainerNo";
            this.lblCntrNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCntrNo.TextResourceKey = "WRD_CTMO_ContainerNo";
            this.lblCntrNo.ToolTipResourceKey = null;
            // 
            // tbxContainerNo
            // 
            this.tbxContainerNo.CustomMask = null;
            this.tbxContainerNo.CustomStyleName = "biz_cntrDetail_txt";
            this.tbxContainerNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxContainerNo.Font = new System.Drawing.Font("Tahoma", 14F);
            this.tbxContainerNo.LinkedLabelName = null;
            this.tbxContainerNo.Location = new System.Drawing.Point(171, 3);
            this.tbxContainerNo.Name = "tbxContainerNo";
            this.tbxContainerNo.Size = new System.Drawing.Size(390, 30);
            this.tbxContainerNo.TabIndex = 3;
            this.tbxContainerNo.TextResourceKey = null;
            this.tbxContainerNo.UseTextMandatoryFont = false;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BorderColor = System.Drawing.Color.Empty;
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(564, 0);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(187, 34);
            this.pnlSearch.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.CustomStyleName = "biz_cntrDetail_btn_ok";
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSearch.LinkedLabelName = null;
            this.btnSearch.Location = new System.Drawing.Point(6, 3);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(181, 30);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextResourceKey = "WRD_CTMO_Search";
            this.btnSearch.ToolTipResourceKey = null;
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // MobileContainerDetailView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(777, 660);
            this.Controls.Add(this.tlpMainViewLayout);
            this.CustomStyleName = "biz_control_view";
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "MobileContainerDetailView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "MobileContainerDetailView";
            this.tlpMainViewLayout.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.tlpMainLayout.ResumeLayout(false);
            this.tlpSearchLayout.ResumeLayout(false);
            this.tlpSearchLayout.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel tlpMainViewLayout;
        private Fontos.Win.Forms.TPanel pnlButton;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TTableLayoutPanel tlpMainLayout;
        private Fontos.Win.Forms.TPanel pnlMain;
        private Fontos.Win.Forms.TTableLayoutPanel tlpSearchLayout;
        private Fontos.Win.Forms.TTextBox tbxContainerNo;
        private Fontos.Win.Forms.TLabel lblCntrNo;
        private Fontos.Win.Forms.TPanel pnlSearch;
        private Fontos.Win.Forms.TButton btnSearch;
        private Fontos.Win.Forms.TButton btnSeal;

    }
}