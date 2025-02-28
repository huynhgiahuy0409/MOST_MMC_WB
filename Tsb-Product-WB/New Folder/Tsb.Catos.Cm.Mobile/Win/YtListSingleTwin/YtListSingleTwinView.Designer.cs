using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;

namespace Tsb.Catos.Cm.Mobile.Win.YtListSingleTwin
{
    partial class YtListSingleTwinView
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
            this.lblCntrNo = new Tsb.Fontos.Win.Forms.TLabel();
            this.lblCntrNoData = new Tsb.Fontos.Win.Forms.TLabel();
            this.tlpJobCdLayout = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.lblJobCode = new Tsb.Fontos.Win.Forms.TLabel();
            this.lblJobCodeData = new Tsb.Fontos.Win.Forms.TLabel();
            this.tPanel1 = new Tsb.Fontos.Win.Forms.TPanel();
            this.YtList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.CPosList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.pnlJobType = new Tsb.Fontos.Win.Forms.TPanel();
            this.SingleTwin = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.lblType = new Tsb.Fontos.Win.Forms.TLabel();
            this.tFlowLayoutPanel1 = new Tsb.Fontos.Win.Forms.TFlowLayoutPanel();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.btnOk = new Tsb.Fontos.Win.Forms.TButton();
            this.tlpMainLayout.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.tlpInputLayout.SuspendLayout();
            this.tlpCntrNoLayout.SuspendLayout();
            this.tlpJobCdLayout.SuspendLayout();
            this.tPanel1.SuspendLayout();
            this.pnlJobType.SuspendLayout();
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
            this.tlpCntrNoLayout.Controls.Add(this.lblCntrNo, 0, 0);
            this.tlpCntrNoLayout.Controls.Add(this.lblCntrNoData, 1, 0);
            this.tlpCntrNoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCntrNoLayout.Location = new System.Drawing.Point(3, 3);
            this.tlpCntrNoLayout.Name = "tlpCntrNoLayout";
            this.tlpCntrNoLayout.RowCount = 1;
            this.tlpCntrNoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCntrNoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCntrNoLayout.Size = new System.Drawing.Size(918, 49);
            this.tlpCntrNoLayout.TabIndex = 4;
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
            // lblCntrNoData
            // 
            this.lblCntrNoData.AutoSize = true;
            this.lblCntrNoData.BackColor = System.Drawing.Color.White;
            this.lblCntrNoData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCntrNoData.CustomStyleName = "biz_ytList_lbl_data";
            this.lblCntrNoData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCntrNoData.IsAppliedLinkedStyle = false;
            this.lblCntrNoData.LinkedLabelName = null;
            this.lblCntrNoData.Location = new System.Drawing.Point(267, 0);
            this.lblCntrNoData.Name = "lblCntrNoData";
            this.lblCntrNoData.Size = new System.Drawing.Size(648, 49);
            this.lblCntrNoData.TabIndex = 2;
            this.lblCntrNoData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCntrNoData.TextResourceKey = null;
            this.lblCntrNoData.ToolTipResourceKey = null;
            // 
            // tlpJobCdLayout
            // 
            this.tlpJobCdLayout.ColumnCount = 2;
            this.tlpJobCdLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.72483F));
            this.tlpJobCdLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.27517F));
            this.tlpJobCdLayout.Controls.Add(this.lblJobCode, 0, 0);
            this.tlpJobCdLayout.Controls.Add(this.lblJobCodeData, 1, 0);
            this.tlpJobCdLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpJobCdLayout.Location = new System.Drawing.Point(3, 58);
            this.tlpJobCdLayout.Name = "tlpJobCdLayout";
            this.tlpJobCdLayout.RowCount = 1;
            this.tlpJobCdLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJobCdLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpJobCdLayout.Size = new System.Drawing.Size(918, 49);
            this.tlpJobCdLayout.TabIndex = 5;
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
            // lblJobCodeData
            // 
            this.lblJobCodeData.AutoSize = true;
            this.lblJobCodeData.BackColor = System.Drawing.Color.White;
            this.lblJobCodeData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblJobCodeData.CustomStyleName = "biz_ytList_lbl_data";
            this.lblJobCodeData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJobCodeData.IsAppliedLinkedStyle = false;
            this.lblJobCodeData.LinkedLabelName = null;
            this.lblJobCodeData.Location = new System.Drawing.Point(266, 0);
            this.lblJobCodeData.Name = "lblJobCodeData";
            this.lblJobCodeData.Size = new System.Drawing.Size(649, 49);
            this.lblJobCodeData.TabIndex = 3;
            this.lblJobCodeData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblJobCodeData.TextResourceKey = null;
            this.lblJobCodeData.ToolTipResourceKey = null;
            // 
            // tPanel1
            // 
            this.tPanel1.BorderColor = System.Drawing.Color.Empty;
            this.tPanel1.Controls.Add(this.YtList);
            this.tPanel1.Controls.Add(this.CPosList);
            this.tPanel1.Controls.Add(this.pnlJobType);
            this.tPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel1.Location = new System.Drawing.Point(3, 113);
            this.tPanel1.Name = "tPanel1";
            this.tPanel1.Size = new System.Drawing.Size(918, 408);
            this.tPanel1.TabIndex = 6;
            // 
            // YtList
            // 
            this.YtList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.YtList.ButtonListCustomStyleName = "biz_singletwin_btn_list";
            this.YtList.ButtonListSelectedCustomStyleName = "biz_selected_singletwin_btn_list";
            this.YtList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YtList.GapBetweenControls = 10;
            this.YtList.ItemList = null;
            this.YtList.Location = new System.Drawing.Point(0, 0);
            this.YtList.MainBackColor = System.Drawing.SystemColors.Control;
            this.YtList.MaxColumnCount = 7;
            this.YtList.MaxRowCount = 3;
            this.YtList.Name = "YtList";
            this.YtList.Padding = new System.Windows.Forms.Padding(3);
            this.YtList.RightExtensionMargin = 0;
            this.YtList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.YtList.ScrollVisible = true;
            this.YtList.Size = new System.Drawing.Size(918, 221);
            this.YtList.TabIndex = 7;
            this.YtList.UseActivatedInAllBay = false;
            this.YtList.UseMultipleSelection = false;
            this.YtList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.YtList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.YtList.VerticalScrollWidth = 40;
            this.YtList.VisibleZoomSlider = false;
            // 
            // CPosList
            // 
            this.CPosList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.CPosList.ButtonListCustomStyleName = "biz_singletwin_btn_list";
            this.CPosList.ButtonListSelectedCustomStyleName = "biz_selected_singletwin_btn_list";
            this.CPosList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CPosList.GapBetweenControls = 10;
            this.CPosList.ItemList = null;
            this.CPosList.Location = new System.Drawing.Point(0, 221);
            this.CPosList.MainBackColor = System.Drawing.SystemColors.Control;
            this.CPosList.MaxColumnCount = 2;
            this.CPosList.MaxRowCount = 1;
            this.CPosList.Name = "CPosList";
            this.CPosList.Padding = new System.Windows.Forms.Padding(3);
            this.CPosList.RightExtensionMargin = 0;
            this.CPosList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.CPosList.ScrollVisible = false;
            this.CPosList.Size = new System.Drawing.Size(918, 67);
            this.CPosList.TabIndex = 10;
            this.CPosList.UseActivatedInAllBay = false;
            this.CPosList.UseMultipleSelection = false;
            this.CPosList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.CPosList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.CPosList.VerticalScrollWidth = 40;
            this.CPosList.VisibleZoomSlider = false;
            // 
            // pnlJobType
            // 
            this.pnlJobType.BorderColor = System.Drawing.Color.Empty;
            this.pnlJobType.Controls.Add(this.SingleTwin);
            this.pnlJobType.Controls.Add(this.lblType);
            this.pnlJobType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlJobType.Location = new System.Drawing.Point(0, 288);
            this.pnlJobType.Margin = new System.Windows.Forms.Padding(0);
            this.pnlJobType.Name = "pnlJobType";
            this.pnlJobType.Size = new System.Drawing.Size(918, 120);
            this.pnlJobType.TabIndex = 11;
            // 
            // SingleTwin
            // 
            this.SingleTwin.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.SingleTwin.ButtonListCustomStyleName = "biz_singletwin_btn_list";
            this.SingleTwin.ButtonListSelectedCustomStyleName = "biz_selected_singletwin_btn_list";
            this.SingleTwin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SingleTwin.GapBetweenControls = 10;
            this.SingleTwin.ItemList = null;
            this.SingleTwin.Location = new System.Drawing.Point(0, 53);
            this.SingleTwin.MainBackColor = System.Drawing.SystemColors.Control;
            this.SingleTwin.MaxColumnCount = 3;
            this.SingleTwin.MaxRowCount = 1;
            this.SingleTwin.Name = "SingleTwin";
            this.SingleTwin.Padding = new System.Windows.Forms.Padding(3);
            this.SingleTwin.RightExtensionMargin = 0;
            this.SingleTwin.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.SingleTwin.ScrollVisible = false;
            this.SingleTwin.Size = new System.Drawing.Size(918, 67);
            this.SingleTwin.TabIndex = 11;
            this.SingleTwin.UseActivatedInAllBay = false;
            this.SingleTwin.UseMultipleSelection = false;
            this.SingleTwin.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.SingleTwin.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.SingleTwin.VerticalScrollWidth = 40;
            this.SingleTwin.VisibleZoomSlider = false;
            // 
            // lblType
            // 
            this.lblType.CustomStyleName = "biz_default_lbl_title";
            this.lblType.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblType.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.IsAppliedLinkedStyle = false;
            this.lblType.LinkedLabelName = null;
            this.lblType.Location = new System.Drawing.Point(0, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(918, 53);
            this.lblType.TabIndex = 10;
            this.lblType.Text = "Job Type";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblType.TextResourceKey = "WRD_CTYQ_Job_Type";
            this.lblType.ToolTipResourceKey = null;
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
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "OK";
            this.btnOk.TextResourceKey = "WRD_CTYQ_Ok";
            this.btnOk.ToolTipResourceKey = null;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // YtListSingleTwinView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 650);
            this.Controls.Add(this.tlpMainLayout);
            this.Name = "YtListSingleTwinView";
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
            this.pnlJobType.ResumeLayout(false);
            this.tFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel tlpMainLayout;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TTableLayoutPanel tlpInputLayout;
        private Fontos.Win.Forms.TTableLayoutPanel tlpCntrNoLayout;
        private Fontos.Win.Forms.TLabel lblCntrNo;
        private Fontos.Win.Forms.TTableLayoutPanel tlpJobCdLayout;
        private Fontos.Win.Forms.TLabel lblJobCode;
        private Fontos.Win.Forms.TFlowLayoutPanel tFlowLayoutPanel1;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TButton btnOk;
        private Fontos.Win.Forms.TPanel tPanel1;
        private ItemListControl YtList;
        private Fontos.Win.Forms.TLabel lblJobCodeData;
        private Fontos.Win.Forms.TLabel lblCntrNoData;
        private ItemListControl CPosList;
        private Fontos.Win.Forms.TPanel pnlJobType;
        private ItemListControl SingleTwin;
        private Fontos.Win.Forms.TLabel lblType;

    }
}