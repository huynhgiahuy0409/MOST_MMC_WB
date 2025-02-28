using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;

namespace Tsb.Catos.Cm.Mobile.Win.EquipmentList
{
    partial class EquipmentListView
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
            this.btnOk = new Tsb.Fontos.Win.Forms.TButton();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.cmbSortEquipmentGroup = new Tsb.Fontos.Win.Forms.TCheckBox();
            this.pnlInput = new Tsb.Fontos.Win.Forms.TPanel();
            this.equList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
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
            this.pnlMain.Location = new System.Drawing.Point(10, 10);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 3;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnlMain.Size = new System.Drawing.Size(757, 556);
            this.pnlMain.TabIndex = 6;
            // 
            // pnlButton
            // 
            this.pnlButton.BorderColor = System.Drawing.Color.Empty;
            this.pnlButton.Controls.Add(this.btnOk);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.cmbSortEquipmentGroup);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButton.Location = new System.Drawing.Point(3, 509);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(751, 44);
            this.pnlButton.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.CustomStyleName = "biz_btn_ok";
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.LinkedLabelName = null;
            this.btnOk.Location = new System.Drawing.Point(345, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(200, 44);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.TextResourceKey = "WRD_CTMO_Ok";
            this.btnOk.ToolTipResourceKey = null;
            this.btnOk.UseVisualStyleBackColor = true;
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
            // cmbSortEquipmentGroup
            // 
            this.cmbSortEquipmentGroup.AutoSize = true;
            this.cmbSortEquipmentGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbSortEquipmentGroup.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbSortEquipmentGroup.ForeColor = System.Drawing.Color.Black;
            this.cmbSortEquipmentGroup.LinkedLabelName = null;
            this.cmbSortEquipmentGroup.Location = new System.Drawing.Point(0, 0);
            this.cmbSortEquipmentGroup.Name = "cmbSortEquipmentGroup";
            this.cmbSortEquipmentGroup.Size = new System.Drawing.Size(252, 44);
            this.cmbSortEquipmentGroup.TabIndex = 7;
            this.cmbSortEquipmentGroup.Text = "Sort Equipment Grouping";
            this.cmbSortEquipmentGroup.TextResourceKey = "WRD_CTMO_SortEquipmentGroupings";
            this.cmbSortEquipmentGroup.ToolTipResourceKey = null;
            this.cmbSortEquipmentGroup.UseDefaultStyle = false;
            this.cmbSortEquipmentGroup.UseVisualStyleBackColor = true;
            // 
            // pnlInput
            // 
            this.pnlInput.BorderColor = System.Drawing.Color.Empty;
            this.pnlInput.Controls.Add(this.equList);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(3, 53);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(751, 450);
            this.pnlInput.TabIndex = 4;
            // 
            // equList
            // 
            this.equList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.equList.ButtonListCustomStyleName = "biz_equpment_btn_list";
            this.equList.ButtonListSelectedCustomStyleName = "biz_selected_btn_list";
            this.equList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equList.GapBetweenControls = 4;
            this.equList.ItemList = null;
            this.equList.Location = new System.Drawing.Point(0, 0);
            this.equList.MainBackColor = System.Drawing.SystemColors.Control;
            this.equList.MaxColumnCount = 7;
            this.equList.MaxRowCount = 8;
            this.equList.Name = "equList";
            this.equList.Padding = new System.Windows.Forms.Padding(3);
            this.equList.RightExtensionMargin = 0;
            this.equList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.equList.ScrollVisible = true;
            this.equList.Size = new System.Drawing.Size(751, 450);
            this.equList.TabIndex = 0;
            this.equList.UseActivatedInAllBay = false;
            this.equList.UseMultipleSelection = false;
            this.equList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.equList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.equList.VerticalScrollWidth = 40;
            this.equList.VisibleZoomSlider = false;
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
            this.lblTitle.Text = "Equipment List";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.TextResourceKey = "WRD_CTMO_EquipmentList";
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // EquipmentListView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(777, 576);
            this.Controls.Add(this.pnlMain);
            this.CustomStyleName = "biz_control_view";
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "EquipmentListView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "EquipmentListView";
            this.pnlMain.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlButton.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel pnlMain;
        private Fontos.Win.Forms.TPanel pnlButton;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TPanel pnlInput;
        private ItemListControl equList;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TButton btnOk;
        private Fontos.Win.Forms.TCheckBox cmbSortEquipmentGroup;
    }
}