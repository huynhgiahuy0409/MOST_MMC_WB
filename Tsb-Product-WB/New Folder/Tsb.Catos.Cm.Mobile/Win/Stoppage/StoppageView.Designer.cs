using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;

namespace Tsb.Catos.Cm.Mobile.Win.Stoppage
{
    partial class StoppageView
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
            this.pnlButton = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.btnOk = new Tsb.Fontos.Win.Forms.TButton();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.pnlTitle = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblTitle = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlInput = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.pnlEquipment = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.txtEquipment = new Tsb.Fontos.Win.Forms.TTextBox();
            this.lblEquipment = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlTime = new Tsb.Fontos.Win.Forms.TTableLayoutPanel();
            this.lblTim = new Tsb.Fontos.Win.Forms.TLabel();
            this.dttDate = new Tsb.Fontos.Win.Forms.TDateTimePicker();
            this.dttTime = new Tsb.Fontos.Win.Forms.TDateTimePicker();
            this.stoppageList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.pnlMain.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlEquipment.SuspendLayout();
            this.pnlTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.pnlButton, 0, 2);
            this.pnlMain.Controls.Add(this.pnlTitle, 0, 0);
            this.pnlMain.Controls.Add(this.pnlInput, 0, 1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(10, 10);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 3;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.Size = new System.Drawing.Size(757, 556);
            this.pnlMain.TabIndex = 8;
            // 
            // pnlButton
            // 
            this.pnlButton.ColumnCount = 3;
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.84397F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.15603F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.pnlButton.Controls.Add(this.btnOk, 1, 0);
            this.pnlButton.Controls.Add(this.btnCancel, 2, 0);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(0, 506);
            this.pnlButton.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.RowCount = 1;
            this.pnlButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlButton.Size = new System.Drawing.Size(757, 50);
            this.pnlButton.TabIndex = 8;
            // 
            // btnOk
            // 
            this.btnOk.CustomStyleName = "biz_btn_ok";
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.LinkedLabelName = null;
            this.btnOk.Location = new System.Drawing.Point(375, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(179, 44);
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
            this.btnCancel.Location = new System.Drawing.Point(567, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 9, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(181, 44);
            this.btnCancel.TabIndex = 2;
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
            this.lblTitle.Text = "Stoppage";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.TextResourceKey = "WRD_CTMO_Stoppage_Reason";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // pnlInput
            // 
            this.pnlInput.ColumnCount = 1;
            this.pnlInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlInput.Controls.Add(this.pnlEquipment, 0, 0);
            this.pnlInput.Controls.Add(this.pnlTime, 0, 1);
            this.pnlInput.Controls.Add(this.stoppageList, 0, 2);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(3, 53);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.RowCount = 3;
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlInput.Size = new System.Drawing.Size(751, 450);
            this.pnlInput.TabIndex = 7;
            // 
            // pnlEquipment
            // 
            this.pnlEquipment.ColumnCount = 2;
            this.pnlEquipment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.01342F));
            this.pnlEquipment.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.98658F));
            this.pnlEquipment.Controls.Add(this.txtEquipment, 1, 0);
            this.pnlEquipment.Controls.Add(this.lblEquipment, 0, 0);
            this.pnlEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEquipment.Location = new System.Drawing.Point(3, 3);
            this.pnlEquipment.Name = "pnlEquipment";
            this.pnlEquipment.RowCount = 1;
            this.pnlEquipment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlEquipment.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlEquipment.Size = new System.Drawing.Size(745, 49);
            this.pnlEquipment.TabIndex = 0;
            // 
            // txtEquipment
            // 
            this.txtEquipment.CustomMask = null;
            this.txtEquipment.CustomStyleName = "biz_stoppage_txt";
            this.txtEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEquipment.Editable = false;
            this.txtEquipment.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtEquipment.LinkedLabelName = null;
            this.txtEquipment.Location = new System.Drawing.Point(166, 3);
            this.txtEquipment.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.txtEquipment.Name = "txtEquipment";
            this.txtEquipment.Size = new System.Drawing.Size(576, 40);
            this.txtEquipment.TabIndex = 0;
            this.txtEquipment.TextResourceKey = null;
            this.txtEquipment.UseTextMandatoryFont = false;
            // 
            // lblEquipment
            // 
            this.lblEquipment.AutoSize = true;
            this.lblEquipment.CustomStyleName = "biz_stoppage_lbl";
            this.lblEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEquipment.IsAppliedLinkedStyle = false;
            this.lblEquipment.LinkedLabelName = null;
            this.lblEquipment.Location = new System.Drawing.Point(3, 3);
            this.lblEquipment.Margin = new System.Windows.Forms.Padding(3);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(157, 43);
            this.lblEquipment.TabIndex = 1;
            this.lblEquipment.Text = "Equipment";
            this.lblEquipment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEquipment.TextResourceKey = "WRD_CTMO_Equipment";
            this.lblEquipment.ToolTipResourceKey = null;
            // 
            // pnlTime
            // 
            this.pnlTime.ColumnCount = 3;
            this.pnlTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.23938F));
            this.pnlTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.76062F));
            this.pnlTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.pnlTime.Controls.Add(this.lblTim, 0, 0);
            this.pnlTime.Controls.Add(this.dttDate, 1, 0);
            this.pnlTime.Controls.Add(this.dttTime, 2, 0);
            this.pnlTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTime.Location = new System.Drawing.Point(3, 58);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.RowCount = 1;
            this.pnlTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlTime.Size = new System.Drawing.Size(745, 49);
            this.pnlTime.TabIndex = 1;
            // 
            // lblTim
            // 
            this.lblTim.AutoSize = true;
            this.lblTim.CustomStyleName = "biz_stoppage_lbl";
            this.lblTim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTim.IsAppliedLinkedStyle = false;
            this.lblTim.LinkedLabelName = null;
            this.lblTim.Location = new System.Drawing.Point(3, 3);
            this.lblTim.Margin = new System.Windows.Forms.Padding(3);
            this.lblTim.Name = "lblTim";
            this.lblTim.Size = new System.Drawing.Size(154, 43);
            this.lblTim.TabIndex = 1;
            this.lblTim.Text = "Time";
            this.lblTim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTim.TextResourceKey = "WRD_CTMO_Time";
            this.lblTim.ToolTipResourceKey = null;
            // 
            // dttDate
            // 
            this.dttDate.CustomFormat = null;
            this.dttDate.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dttDate.LinkedLabelName = null;
            this.dttDate.Location = new System.Drawing.Point(163, 3);
            this.dttDate.Name = "dttDate";
            this.dttDate.NullableValue = new System.DateTime(2016, 1, 27, 14, 7, 10, 263);
            this.dttDate.Size = new System.Drawing.Size(330, 40);
            this.dttDate.TabIndex = 2;
            this.dttDate.TextResourceKey = null;
            this.dttDate.Value = new System.DateTime(2016, 1, 27, 14, 7, 10, 263);
            // 
            // dttTime
            // 
            this.dttTime.CustomFormat = null;
            this.dttTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dttTime.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dttTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dttTime.LinkedLabelName = null;
            this.dttTime.Location = new System.Drawing.Point(499, 3);
            this.dttTime.Name = "dttTime";
            this.dttTime.NullableValue = new System.DateTime(2016, 1, 27, 14, 6, 15, 937);
            this.dttTime.ShowUpDown = true;
            this.dttTime.Size = new System.Drawing.Size(243, 40);
            this.dttTime.TabIndex = 3;
            this.dttTime.TextResourceKey = null;
            this.dttTime.Value = new System.DateTime(2016, 1, 27, 14, 6, 15, 937);
            // 
            // stoppageList
            // 
            this.stoppageList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.stoppageList.ButtonListCustomStyleName = "biz_stoppage_btn_list";
            this.stoppageList.ButtonListSelectedCustomStyleName = "biz_selected_stoppage_btn_list";
            this.stoppageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stoppageList.GapBetweenControls = 10;
            this.stoppageList.ItemList = null;
            this.stoppageList.Location = new System.Drawing.Point(3, 113);
            this.stoppageList.MainBackColor = System.Drawing.SystemColors.Control;
            this.stoppageList.MaxColumnCount = 4;
            this.stoppageList.MaxRowCount = 4;
            this.stoppageList.Name = "stoppageList";
            this.stoppageList.Padding = new System.Windows.Forms.Padding(3);
            this.stoppageList.RightExtensionMargin = 0;
            this.stoppageList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.stoppageList.ScrollVisible = true;
            this.stoppageList.Size = new System.Drawing.Size(745, 334);
            this.stoppageList.TabIndex = 2;
            this.stoppageList.UseActivatedInAllBay = false;
            this.stoppageList.UseMultipleSelection = false;
            this.stoppageList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.stoppageList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.stoppageList.VerticalScrollWidth = 40;
            this.stoppageList.VisibleZoomSlider = false;
            // 
            // StoppageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 576);
            this.Controls.Add(this.pnlMain);
            this.CustomStyleName = "biz_control_view";
            this.Name = "StoppageView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "StoppageView";
            this.pnlMain.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlEquipment.ResumeLayout(false);
            this.pnlEquipment.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel pnlMain;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TTableLayoutPanel pnlInput;
        private Fontos.Win.Forms.TTableLayoutPanel pnlEquipment;
        private Fontos.Win.Forms.TTextBox txtEquipment;
        private Fontos.Win.Forms.TLabel lblEquipment;
        private Fontos.Win.Forms.TTableLayoutPanel pnlButton;
        private Fontos.Win.Forms.TButton btnOk;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TTableLayoutPanel pnlTime;
        private Fontos.Win.Forms.TLabel lblTim;
        private Fontos.Win.Forms.TDateTimePicker dttDate;
        private Fontos.Win.Forms.TDateTimePicker dttTime;
        private ItemListControl stoppageList;
    }
}