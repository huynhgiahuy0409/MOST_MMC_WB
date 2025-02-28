using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;

namespace Tsb.Catos.Cm.Mobile.Win.LogOutReason
{
    partial class LogOutReasonView
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
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.pnlTitle = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblTitle = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlInput = new Tsb.Fontos.Win.Forms.TPanel();
            this.logOutList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.pnlMain.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlInput.SuspendLayout();
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
            this.pnlMain.TabIndex = 9;
            // 
            // pnlButton
            // 
            this.pnlButton.ColumnCount = 3;
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.0194F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.9806F));
            this.pnlButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 189F));
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
            // btnCancel
            // 
            this.btnCancel.CustomStyleName = "biz_btn_exit";
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.LinkedLabelName = null;
            this.btnCancel.Location = new System.Drawing.Point(570, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
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
            this.lblTitle.Text = "WRD_CTTA_LogOut_Reason";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.TextResourceKey = "WRD_CTMO_LogOutReason";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // pnlInput
            // 
            this.pnlInput.BorderColor = System.Drawing.Color.Empty;
            this.pnlInput.Controls.Add(this.logOutList);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(3, 53);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(751, 450);
            this.pnlInput.TabIndex = 9;
            // 
            // logOutList
            // 
            this.logOutList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.logOutList.ButtonListCustomStyleName = "biz_logoutreason_btn_list";
            this.logOutList.ButtonListSelectedCustomStyleName = "biz_logoutreason_selected_btn_list";
            this.logOutList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logOutList.GapBetweenControls = 10;
            this.logOutList.ItemList = null;
            this.logOutList.Location = new System.Drawing.Point(0, 0);
            this.logOutList.MainBackColor = System.Drawing.SystemColors.Control;
            this.logOutList.MaxColumnCount = 1;
            this.logOutList.MaxRowCount = 4;
            this.logOutList.MouseDragLine = null;
            this.logOutList.Name = "logOutList";
            this.logOutList.Padding = new System.Windows.Forms.Padding(3);
            this.logOutList.RightExtensionMargin = 0;
            this.logOutList.Size = new System.Drawing.Size(751, 450);
            this.logOutList.TabIndex = 3;
            this.logOutList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.logOutList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.logOutList.VisibleZoomSlider = false;
            // 
            // LogOutReasonView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(777, 576);
            this.Controls.Add(this.pnlMain);
            this.CustomStyleName = "biz_control_view";
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "LogOutReasonView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "LogOutReasonView";
            this.pnlMain.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel pnlMain;
        private Fontos.Win.Forms.TTableLayoutPanel pnlButton;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TPanel pnlInput;
        private ItemListControl logOutList;
    }
}