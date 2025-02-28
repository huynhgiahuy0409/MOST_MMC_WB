namespace Tsb.Catos.Cm.Mobile.Win.UserIdList
{
    partial class UserIdListView
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
            this.pnlButton = new Tsb.Fontos.Win.Forms.TPanel();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.pnlInput = new Tsb.Fontos.Win.Forms.TPanel();
            this.userIdList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.pnlTitle = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblTitle = new Tsb.Fontos.Win.Forms.TLabel();
            this.pnlMain.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.pnlButton, 0, 2);
            this.pnlMain.Controls.Add(this.pnlInput, 0, 1);
            this.pnlMain.Controls.Add(this.pnlTitle, 0, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 3;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.Size = new System.Drawing.Size(777, 576);
            this.pnlMain.TabIndex = 7;
            // 
            // pnlButton
            // 
            this.pnlButton.BorderColor = System.Drawing.Color.Empty;
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(3, 529);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(771, 44);
            this.pnlButton.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.CustomStyleName = "biz_btn_exit";
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.LinkedLabelName = null;
            this.btnCancel.Location = new System.Drawing.Point(551, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(200, 44);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextResourceKey = "WRD_CTMO_Cancel";
            this.btnCancel.ToolTipResourceKey = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pnlInput
            // 
            this.pnlInput.BorderColor = System.Drawing.Color.Empty;
            this.pnlInput.Controls.Add(this.userIdList);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(3, 53);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(771, 470);
            this.pnlInput.TabIndex = 4;
            // 
            // userIdList
            // 
            this.userIdList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.userIdList.ButtonListCustomStyleName = "biz_equpment_btn_list";
            this.userIdList.ButtonListSelectedCustomStyleName = "biz_selected_btn_list";
            this.userIdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userIdList.GapBetweenControls = 4;
            this.userIdList.ItemList = null;
            this.userIdList.Location = new System.Drawing.Point(0, 0);
            this.userIdList.MainBackColor = System.Drawing.SystemColors.Control;
            this.userIdList.MaxColumnCount = 7;
            this.userIdList.MaxRowCount = 8;
            this.userIdList.Name = "userIdList";
            this.userIdList.Padding = new System.Windows.Forms.Padding(3);
            this.userIdList.RightExtensionMargin = 0;
            this.userIdList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.userIdList.ScrollVisible = true;
            this.userIdList.Size = new System.Drawing.Size(771, 470);
            this.userIdList.TabIndex = 0;
            this.userIdList.UseMultipleSelection = false;
            this.userIdList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.userIdList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.userIdList.VerticalScrollWidth = 40;
            this.userIdList.VisibleZoomSlider = false;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BorderColor = System.Drawing.Color.Empty;
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(3, 3);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(771, 44);
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
            this.lblTitle.Size = new System.Drawing.Size(771, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "User ID List";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.TextResourceKey = "WRD_CTMO_UserIdList";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // UserIdListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 576);
            this.Controls.Add(this.pnlMain);
            this.Name = "UserIdListView";
            this.Text = "UserIdListView";
            this.pnlMain.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel pnlMain;
        private Fontos.Win.Forms.TPanel pnlButton;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TPanel pnlInput;
        private Common.Controls.ItemList.ItemListControl userIdList;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
    }
}