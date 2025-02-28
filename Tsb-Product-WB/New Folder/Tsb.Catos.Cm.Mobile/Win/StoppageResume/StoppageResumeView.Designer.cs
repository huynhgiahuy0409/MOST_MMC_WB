namespace Tsb.Catos.Cm.Mobile.Win.StoppageResume
{
    partial class StoppageResumeView
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
            this.pnlMain = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.pnlTitle = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblTitle = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlInput = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.tTableLayoutPanel1 = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.lblStoppage = new Tsb.Fontos.Win.Forms.TLabel();
            this.btnClose = new Tsb.Fontos.Win.Forms.TButton();
            this.pnlTime = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.dttTime = new Tsb.Fontos.Win.Forms.TDateTimePicker();
            this.dttDate = new Tsb.Fontos.Win.Forms.TDateTimePicker();
            this.lblResumeTime = new Tsb.Fontos.Win.Forms.TLabel();
            this.txtStoppage = new Tsb.Fontos.Win.Forms.TTextBox();
            this.tlpButton = new Tsb.Fontos.Win.Forms.TFlowLayoutPanel();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.btnUndo = new Tsb.Fontos.Win.Forms.TButton();
            this.btnOk = new Tsb.Fontos.Win.Forms.TButton();
            this.tmrDateTime = new System.Windows.Forms.Timer(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.tTableLayoutPanel1.SuspendLayout();
            this.pnlTime.SuspendLayout();
            this.tlpButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.pnlTitle, 0, 0);
            this.pnlMain.Controls.Add(this.pnlInput, 0, 1);
            this.pnlMain.Controls.Add(this.tlpButton, 0, 2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(10, 10);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 3;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.pnlMain.Size = new System.Drawing.Size(757, 556);
            this.pnlMain.TabIndex = 9;
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
            this.lblTitle.Text = "Resume";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.TextResourceKey = "WRD_CTMO_Stoppage_Resume";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // pnlInput
            // 
            this.pnlInput.ColumnCount = 1;
            this.pnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlInput.Controls.Add(this.tTableLayoutPanel1, 0, 0);
            this.pnlInput.Controls.Add(this.pnlTime, 0, 3);
            this.pnlInput.Controls.Add(this.lblResumeTime, 0, 2);
            this.pnlInput.Controls.Add(this.txtStoppage, 0, 1);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(3, 53);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.RowCount = 6;
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.pnlInput.Size = new System.Drawing.Size(751, 446);
            this.pnlInput.TabIndex = 9;
            // 
            // tTableLayoutPanel1
            // 
            this.tTableLayoutPanel1.ColumnCount = 2;
            this.tTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tTableLayoutPanel1.Controls.Add(this.lblStoppage, 0, 0);
            this.tTableLayoutPanel1.Controls.Add(this.btnClose, 1, 0);
            this.tTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tTableLayoutPanel1.Name = "tTableLayoutPanel1";
            this.tTableLayoutPanel1.RowCount = 1;
            this.tTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tTableLayoutPanel1.Size = new System.Drawing.Size(751, 55);
            this.tTableLayoutPanel1.TabIndex = 5;
            // 
            // lblStoppage
            // 
            this.lblStoppage.AutoSize = true;
            this.lblStoppage.CustomStyleName = "biz_stoppageResume_lbl";
            this.lblStoppage.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStoppage.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStoppage.IsAppliedLinkedStyle = false;
            this.lblStoppage.LinkedLabelName = null;
            this.lblStoppage.Location = new System.Drawing.Point(3, 3);
            this.lblStoppage.Margin = new System.Windows.Forms.Padding(3);
            this.lblStoppage.Name = "lblStoppage";
            this.lblStoppage.Size = new System.Drawing.Size(222, 49);
            this.lblStoppage.TabIndex = 1;
            this.lblStoppage.Text = "Stoppage Reason";
            this.lblStoppage.TextResourceKey = "WRD_CTMO_Stoppage_Reason";
            this.lblStoppage.ToolTipResourceKey = null;
            // 
            // btnClose
            // 
            this.btnClose.CustomStyleName = "biz_btn_exit";
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.LinkedLabelName = null;
            this.btnClose.Location = new System.Drawing.Point(564, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnClose.Size = new System.Drawing.Size(181, 49);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.TextResourceKey = "WRD_CTMO_Close";
            this.btnClose.ToolTipResourceKey = null;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlTime
            // 
            this.pnlTime.ColumnCount = 2;
            this.pnlTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.77181F));
            this.pnlTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.22819F));
            this.pnlTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 236F));
            this.pnlTime.Controls.Add(this.dttTime, 0, 0);
            this.pnlTime.Controls.Add(this.dttDate, 0, 0);
            this.pnlTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTime.Location = new System.Drawing.Point(3, 168);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.RowCount = 1;
            this.pnlTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlTime.Size = new System.Drawing.Size(745, 49);
            this.pnlTime.TabIndex = 3;
            // 
            // dttTime
            // 
            this.dttTime.CustomFormat = null;
            this.dttTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.dttTime.Enabled = false;
            this.dttTime.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dttTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dttTime.LinkedLabelName = null;
            this.dttTime.Location = new System.Drawing.Point(343, 3);
            this.dttTime.Name = "dttTime";
            this.dttTime.NullableValue = new System.DateTime(2016, 1, 27, 14, 6, 15, 937);
            this.dttTime.ShowUpDown = true;
            this.dttTime.Size = new System.Drawing.Size(231, 40);
            this.dttTime.TabIndex = 5;
            this.dttTime.TextResourceKey = null;
            this.dttTime.Value = new System.DateTime(2016, 1, 27, 14, 6, 15, 937);
            // 
            // dttDate
            // 
            this.dttDate.CustomFormat = null;
            this.dttDate.Enabled = false;
            this.dttDate.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dttDate.LinkedLabelName = null;
            this.dttDate.Location = new System.Drawing.Point(0, 3);
            this.dttDate.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.dttDate.Name = "dttDate";
            this.dttDate.NullableValue = new System.DateTime(2016, 1, 27, 14, 7, 10, 263);
            this.dttDate.Size = new System.Drawing.Size(337, 40);
            this.dttDate.TabIndex = 4;
            this.dttDate.TextResourceKey = null;
            this.dttDate.Value = new System.DateTime(2016, 1, 27, 14, 7, 10, 263);
            // 
            // lblResumeTime
            // 
            this.lblResumeTime.AutoSize = true;
            this.lblResumeTime.CustomStyleName = "biz_stoppageResume_lbl";
            this.lblResumeTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblResumeTime.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblResumeTime.IsAppliedLinkedStyle = false;
            this.lblResumeTime.LinkedLabelName = null;
            this.lblResumeTime.Location = new System.Drawing.Point(3, 113);
            this.lblResumeTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblResumeTime.Name = "lblResumeTime";
            this.lblResumeTime.Size = new System.Drawing.Size(177, 49);
            this.lblResumeTime.TabIndex = 1;
            this.lblResumeTime.Text = "Resume Time";
            this.lblResumeTime.TextResourceKey = "WRD_CTMO_ResumeTime";
            this.lblResumeTime.ToolTipResourceKey = null;
            // 
            // txtStoppage
            // 
            this.txtStoppage.CustomMask = null;
            this.txtStoppage.CustomStyleName = "biz_stoppageResume_txt";
            this.txtStoppage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStoppage.Editable = false;
            this.txtStoppage.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtStoppage.LinkedLabelName = null;
            this.txtStoppage.Location = new System.Drawing.Point(3, 58);
            this.txtStoppage.Name = "txtStoppage";
            this.txtStoppage.Size = new System.Drawing.Size(745, 40);
            this.txtStoppage.TabIndex = 2;
            this.txtStoppage.TextResourceKey = null;
            this.txtStoppage.UseTextMandatoryFont = false;
            // 
            // tlpButton
            // 
            this.tlpButton.Controls.Add(this.btnCancel);
            this.tlpButton.Controls.Add(this.btnUndo);
            this.tlpButton.Controls.Add(this.btnOk);
            this.tlpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.tlpButton.Location = new System.Drawing.Point(3, 504);
            this.tlpButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlpButton.Name = "tlpButton";
            this.tlpButton.Size = new System.Drawing.Size(751, 50);
            this.tlpButton.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.CustomStyleName = "biz_btn_exit";
            this.btnCancel.LinkedLabelName = null;
            this.btnCancel.Location = new System.Drawing.Point(564, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCancel.Size = new System.Drawing.Size(181, 43);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Log Out";
            this.btnCancel.TextResourceKey = "WRD_CTMO_LogOut";
            this.btnCancel.ToolTipResourceKey = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnUndo
            // 
            this.btnUndo.CustomStyleName = "biz_selected_btn_list";
            this.btnUndo.LinkedLabelName = null;
            this.btnUndo.Location = new System.Drawing.Point(446, 3);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnUndo.Size = new System.Drawing.Size(112, 43);
            this.btnUndo.TabIndex = 13;
            this.btnUndo.Text = "Undo";
            this.btnUndo.TextResourceKey = "WRD_CTMO_Undo";
            this.btnUndo.ToolTipResourceKey = null;
            this.btnUndo.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.CustomStyleName = "biz_btn_ok";
            this.btnOk.LinkedLabelName = null;
            this.btnOk.Location = new System.Drawing.Point(328, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOk.Size = new System.Drawing.Size(112, 43);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "Resume";
            this.btnOk.TextResourceKey = "WRD_CTMO_Resume";
            this.btnOk.ToolTipResourceKey = null;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // StoppageResumeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 576);
            this.Controls.Add(this.pnlMain);
            this.CustomStyleName = "biz_control_view";
            this.Name = "StoppageResumeView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "StoppageResumeView";
            this.pnlMain.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.tTableLayoutPanel1.ResumeLayout(false);
            this.tTableLayoutPanel1.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.tlpButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel pnlMain;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TTableLayoutPanel pnlInput;
        private Fontos.Win.Forms.TLabel lblResumeTime;
        private Fontos.Win.Forms.TTextBox txtStoppage;
        private Fontos.Win.Forms.TTableLayoutPanel pnlTime;
        private Fontos.Win.Forms.TDateTimePicker dttTime;
        private Fontos.Win.Forms.TDateTimePicker dttDate;
        private System.Windows.Forms.Timer tmrDateTime;
        private Fontos.Win.Forms.TFlowLayoutPanel tlpButton;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TButton btnUndo;
        private Fontos.Win.Forms.TButton btnOk;
        private Fontos.Win.Forms.TTableLayoutPanel tTableLayoutPanel1;
        private Fontos.Win.Forms.TLabel lblStoppage;
        private Fontos.Win.Forms.TButton btnClose;
    }
}