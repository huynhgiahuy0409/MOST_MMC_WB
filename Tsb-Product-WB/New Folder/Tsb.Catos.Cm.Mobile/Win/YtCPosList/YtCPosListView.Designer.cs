using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;

namespace Tsb.Catos.Cm.Mobile.Win.YtCPosList
{
    partial class YtCPosListView
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
            this.tlpMainLayout = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.pnlTitle = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblTitle = new Tsb.Fontos.Win.Forms.TLabel();
            this.tlpInputLayout = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tlpCntrNoLayout = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.txtCntrNo = new Tsb.Fontos.Win.Forms.TTextBox();
            this.lblCntrNo = new Tsb.Fontos.Win.Forms.TLabel();
            this.tlpJobCdLayout = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.txtJobCode = new Tsb.Fontos.Win.Forms.TTextBox();
            this.lblJobCode = new Tsb.Fontos.Win.Forms.TLabel();
            this.tPanel1 = new Tsb.Fontos.Win.Forms.TPanel();
            this.CPosList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.YtList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.tFlowLayoutPanel1 = new Tsb.Fontos.Win.Forms.TFlowLayoutPanel();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.btnOk = new Tsb.Fontos.Win.Forms.TButton();
            this.tlpMainLayout.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.tlpInputLayout.SuspendLayout();
            this.tlpCntrNoLayout.SuspendLayout();
            this.tlpJobCdLayout.SuspendLayout();
            this.tPanel1.SuspendLayout();
            this.tFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 1;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Controls.Add(this.pnlTitle, 0, 0);
            this.tlpMainLayout.Controls.Add(this.tlpInputLayout, 0, 1);
            this.tlpMainLayout.Controls.Add(this.tFlowLayoutPanel1, 0, 2);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(10, 10);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 3;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainLayout.Size = new System.Drawing.Size(930, 630);
            this.tlpMainLayout.TabIndex = 10;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderColor = System.Drawing.Color.Empty;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(3, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(924, 44);
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
            this.lblTitle.Size = new System.Drawing.Size(924, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "YT List";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.TextResourceKey = "WRD_CTMO_YtList";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // tlpInputLayout
            // 
            this.tlpInputLayout.ColumnCount = 1;
            this.tlpInputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInputLayout.Controls.Add(this.tlpCntrNoLayout, 0, 0);
            this.tlpInputLayout.Controls.Add(this.tlpJobCdLayout, 0, 1);
            this.tlpInputLayout.Controls.Add(this.tPanel1, 0, 2);
            this.tlpInputLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInputLayout.Location = new System.Drawing.Point(3, 53);
            this.tlpInputLayout.Name = "tlpInputLayout";
            this.tlpInputLayout.RowCount = 3;
            this.tlpInputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpInputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpInputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 332F));
            this.tlpInputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpInputLayout.Size = new System.Drawing.Size(924, 524);
            this.tlpInputLayout.TabIndex = 9;
            // 
            // tlpCntrNoLayout
            // 
            this.tlpCntrNoLayout.ColumnCount = 2;
            this.tlpCntrNoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.85906F));
            this.tlpCntrNoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.14094F));
            this.tlpCntrNoLayout.Controls.Add(this.txtCntrNo, 0, 0);
            this.tlpCntrNoLayout.Controls.Add(this.lblCntrNo, 0, 0);
            this.tlpCntrNoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCntrNoLayout.Location = new System.Drawing.Point(3, 3);
            this.tlpCntrNoLayout.Name = "tlpCntrNoLayout";
            this.tlpCntrNoLayout.RowCount = 1;
            this.tlpCntrNoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCntrNoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCntrNoLayout.Size = new System.Drawing.Size(918, 49);
            this.tlpCntrNoLayout.TabIndex = 4;
            // 
            // txtCntrNo
            // 
            this.txtCntrNo.CustomMask = null;
            this.txtCntrNo.CustomStyleName = "biz_ytList_txt";
            this.txtCntrNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCntrNo.Editable = false;
            this.txtCntrNo.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCntrNo.LinkedLabelName = null;
            this.txtCntrNo.Location = new System.Drawing.Point(267, 3);
            this.txtCntrNo.Name = "txtCntrNo";
            this.txtCntrNo.Size = new System.Drawing.Size(648, 40);
            this.txtCntrNo.TabIndex = 3;
            this.txtCntrNo.TextResourceKey = null;
            this.txtCntrNo.UseTextMandatoryFont = false;
            // 
            // lblCntrNo
            // 
            this.lblCntrNo.AutoSize = true;
            this.lblCntrNo.CustomStyleName = "biz_ytList_lbl";
            this.lblCntrNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCntrNo.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCntrNo.IsAppliedLinkedStyle = false;
            this.lblCntrNo.LinkedLabelName = null;
            this.lblCntrNo.Location = new System.Drawing.Point(3, 3);
            this.lblCntrNo.Margin = new System.Windows.Forms.Padding(3);
            this.lblCntrNo.Name = "lblCntrNo";
            this.lblCntrNo.Size = new System.Drawing.Size(258, 43);
            this.lblCntrNo.TabIndex = 1;
            this.lblCntrNo.Text = "Container No";
            this.lblCntrNo.TextResourceKey = "WRD_CTMO_ContainerNo";
            this.lblCntrNo.ToolTipResourceKey = null;
            // 
            // tlpJobCdLayout
            // 
            this.tlpJobCdLayout.ColumnCount = 2;
            this.tlpJobCdLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.72483F));
            this.tlpJobCdLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.27517F));
            this.tlpJobCdLayout.Controls.Add(this.txtJobCode, 0, 0);
            this.tlpJobCdLayout.Controls.Add(this.lblJobCode, 0, 0);
            this.tlpJobCdLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpJobCdLayout.Location = new System.Drawing.Point(3, 58);
            this.tlpJobCdLayout.Name = "tlpJobCdLayout";
            this.tlpJobCdLayout.RowCount = 1;
            this.tlpJobCdLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJobCdLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJobCdLayout.Size = new System.Drawing.Size(918, 49);
            this.tlpJobCdLayout.TabIndex = 5;
            // 
            // txtJobCode
            // 
            this.txtJobCode.CustomMask = null;
            this.txtJobCode.CustomStyleName = "biz_ytList_txt";
            this.txtJobCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJobCode.Editable = false;
            this.txtJobCode.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtJobCode.LinkedLabelName = null;
            this.txtJobCode.Location = new System.Drawing.Point(266, 3);
            this.txtJobCode.Name = "txtJobCode";
            this.txtJobCode.Size = new System.Drawing.Size(649, 40);
            this.txtJobCode.TabIndex = 4;
            this.txtJobCode.TextResourceKey = null;
            this.txtJobCode.UseTextMandatoryFont = false;
            // 
            // lblJobCode
            // 
            this.lblJobCode.AutoSize = true;
            this.lblJobCode.CustomStyleName = "biz_ytList_lbl";
            this.lblJobCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJobCode.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblJobCode.IsAppliedLinkedStyle = false;
            this.lblJobCode.LinkedLabelName = null;
            this.lblJobCode.Location = new System.Drawing.Point(3, 3);
            this.lblJobCode.Margin = new System.Windows.Forms.Padding(3);
            this.lblJobCode.Name = "lblJobCode";
            this.lblJobCode.Size = new System.Drawing.Size(257, 43);
            this.lblJobCode.TabIndex = 2;
            this.lblJobCode.Text = "Job Code";
            this.lblJobCode.TextResourceKey = "WRD_CTMO_JobCode";
            this.lblJobCode.ToolTipResourceKey = null;
            // 
            // tPanel1
            // 
            this.tPanel1.BorderColor = System.Drawing.Color.Empty;
            this.tPanel1.Controls.Add(this.CPosList);
            this.tPanel1.Controls.Add(this.YtList);
            this.tPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel1.Location = new System.Drawing.Point(3, 113);
            this.tPanel1.Name = "tPanel1";
            this.tPanel1.Size = new System.Drawing.Size(918, 408);
            this.tPanel1.TabIndex = 6;
            // 
            // CPosList
            // 
            this.CPosList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.CPosList.ButtonListCustomStyleName = "biz_singletwin_btn_list";
            this.CPosList.ButtonListSelectedCustomStyleName = "biz_selected_singletwin_btn_list";
            this.CPosList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CPosList.GapBetweenControls = 10;
            this.CPosList.ItemList = null;
            this.CPosList.Location = new System.Drawing.Point(0, 338);
            this.CPosList.MainBackColor = System.Drawing.SystemColors.Control;
            this.CPosList.MaxColumnCount = 2;
            this.CPosList.MaxRowCount = 1;
            this.CPosList.Name = "CPosList";
            this.CPosList.Padding = new System.Windows.Forms.Padding(3);
            this.CPosList.RightExtensionMargin = 0;
            this.CPosList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.CPosList.ScrollVisible = false;
            this.CPosList.Size = new System.Drawing.Size(918, 70);
            this.CPosList.TabIndex = 8;
            this.CPosList.UseMultipleSelection = false;
            this.CPosList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.CPosList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.CPosList.VerticalScrollWidth = 40;
            this.CPosList.VisibleZoomSlider = false;
            // 
            // YtList
            // 
            this.YtList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.YtList.ButtonListCustomStyleName = "biz_singletwin_btn_list";
            this.YtList.ButtonListSelectedCustomStyleName = "biz_selected_singletwin_btn_list";
            this.YtList.Dock = System.Windows.Forms.DockStyle.Top;
            this.YtList.GapBetweenControls = 10;
            this.YtList.ItemList = null;
            this.YtList.Location = new System.Drawing.Point(0, 0);
            this.YtList.MainBackColor = System.Drawing.SystemColors.Control;
            this.YtList.MaxColumnCount = 7;
            this.YtList.MaxRowCount = 4;
            this.YtList.Name = "YtList";
            this.YtList.Padding = new System.Windows.Forms.Padding(3);
            this.YtList.RightExtensionMargin = 0;
            this.YtList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.YtList.ScrollVisible = true;
            this.YtList.Size = new System.Drawing.Size(918, 332);
            this.YtList.TabIndex = 7;
            this.YtList.UseMultipleSelection = false;
            this.YtList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.YtList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.YtList.VerticalScrollWidth = 40;
            this.YtList.VisibleZoomSlider = false;
            // 
            // tFlowLayoutPanel1
            // 
            this.tFlowLayoutPanel1.Controls.Add(this.btnCancel);
            this.tFlowLayoutPanel1.Controls.Add(this.btnOk);
            this.tFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tFlowLayoutPanel1.Location = new System.Drawing.Point(0, 580);
            this.tFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tFlowLayoutPanel1.Name = "tFlowLayoutPanel1";
            this.tFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tFlowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tFlowLayoutPanel1.Size = new System.Drawing.Size(930, 50);
            this.tFlowLayoutPanel1.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.CustomStyleName = "biz_btn_exit";
            this.btnCancel.LinkedLabelName = null;
            this.btnCancel.Location = new System.Drawing.Point(741, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(181, 44);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextResourceKey = "WRD_CTYQ_Cancel";
            this.btnCancel.ToolTipResourceKey = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.CustomStyleName = "biz_btn_ok";
            this.btnOk.LinkedLabelName = null;
            this.btnOk.Location = new System.Drawing.Point(554, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(181, 44);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "OK";
            this.btnOk.TextResourceKey = "WRD_CTYQ_Ok";
            this.btnOk.ToolTipResourceKey = null;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // YtCPosListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 650);
            this.Controls.Add(this.tlpMainLayout);
            this.Name = "YtCPosListView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "YtListView";
            this.tlpMainLayout.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.tlpInputLayout.ResumeLayout(false);
            this.tlpCntrNoLayout.ResumeLayout(false);
            this.tlpCntrNoLayout.PerformLayout();
            this.tlpJobCdLayout.ResumeLayout(false);
            this.tlpJobCdLayout.PerformLayout();
            this.tPanel1.ResumeLayout(false);
            this.tFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel tlpMainLayout;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TTableLayoutPanel tlpInputLayout;
        private Fontos.Win.Forms.TTableLayoutPanel tlpCntrNoLayout;
        private Fontos.Win.Forms.TTextBox txtCntrNo;
        private Fontos.Win.Forms.TLabel lblCntrNo;
        private Fontos.Win.Forms.TTableLayoutPanel tlpJobCdLayout;
        private Fontos.Win.Forms.TLabel lblJobCode;
        private Fontos.Win.Forms.TTextBox txtJobCode;
        private Fontos.Win.Forms.TFlowLayoutPanel tFlowLayoutPanel1;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TButton btnOk;
        private Fontos.Win.Forms.TPanel tPanel1;
        private ItemListControl CPosList;
        private ItemListControl YtList;

    }
}