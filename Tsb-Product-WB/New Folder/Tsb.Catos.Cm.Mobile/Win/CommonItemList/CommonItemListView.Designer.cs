using Tsb.Catos.Cm.Mobile.Common.Controls.ItemList;

namespace Tsb.Catos.Cm.Mobile.Win.CommonItemList
{
    partial class CommonItemListView
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
            this.pnlInput = new Tsb.Fontos.Win.Forms.TPanel();
            this.itemList = new Tsb.Catos.Cm.Mobile.Common.Controls.ItemList.ItemListControl();
            this.flpButtonLayout = new Tsb.Fontos.Win.Forms.TFlowLayoutPanel();
            this.btnCancel = new Tsb.Fontos.Win.Forms.TButton();
            this.btnOk = new Tsb.Fontos.Win.Forms.TButton();
            this.tlpMainLayout.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.flpButtonLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 1;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Controls.Add(this.pnlTitle, 0, 0);
            this.tlpMainLayout.Controls.Add(this.pnlInput, 0, 1);
            this.tlpMainLayout.Controls.Add(this.flpButtonLayout, 0, 2);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(10, 10);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 3;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainLayout.Size = new System.Drawing.Size(757, 556);
            this.tlpMainLayout.TabIndex = 10;
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
            this.lblTitle.Text = "Common Item List";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.TextResourceKey = null;
            this.lblTitle.ToolTipResourceKey = null;
            // 
            // pnlInput
            // 
            this.pnlInput.BorderColor = System.Drawing.Color.Empty;
            this.pnlInput.Controls.Add(this.itemList);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(3, 53);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(751, 450);
            this.pnlInput.TabIndex = 9;
            // 
            // itemList
            // 
            this.itemList.ArrangeOffset = new System.Drawing.Point(0, 0);
            this.itemList.ButtonListCustomStyleName = "biz_duplicatedCnte_btn_list";
            this.itemList.ButtonListSelectedCustomStyleName = "biz_duplicatedCnte_selected_btn_list";
            this.itemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemList.GapBetweenControls = 10;
            this.itemList.ItemList = null;
            this.itemList.Location = new System.Drawing.Point(0, 0);
            this.itemList.MainBackColor = System.Drawing.SystemColors.Control;
            this.itemList.MaxColumnCount = 1;
            this.itemList.MaxRowCount = 4;
            this.itemList.Name = "itemList";
            this.itemList.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.itemList.RightExtensionMargin = 0;
            this.itemList.ScrollCustomStyleName = "ResizeScrollbar_Mobile_Normal";
            this.itemList.ScrollVisible = true;
            this.itemList.Size = new System.Drawing.Size(751, 450);
            this.itemList.TabIndex = 3;
            this.itemList.UseMultipleSelection = false;
            this.itemList.UserCursorDown = System.Windows.Forms.Cursors.Default;
            this.itemList.UserPointCursor = System.Windows.Forms.Cursors.Default;
            this.itemList.VerticalScrollWidth = 40;
            this.itemList.VisibleZoomSlider = false;
            // 
            // flpButtonLayout
            // 
            this.flpButtonLayout.Controls.Add(this.btnCancel);
            this.flpButtonLayout.Controls.Add(this.btnOk);
            this.flpButtonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpButtonLayout.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpButtonLayout.Location = new System.Drawing.Point(0, 506);
            this.flpButtonLayout.Margin = new System.Windows.Forms.Padding(0);
            this.flpButtonLayout.Name = "flpButtonLayout";
            this.flpButtonLayout.Size = new System.Drawing.Size(757, 50);
            this.flpButtonLayout.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.CustomStyleName = "biz_btn_exit";
            this.btnCancel.LinkedLabelName = null;
            this.btnCancel.Location = new System.Drawing.Point(570, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(181, 44);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextResourceKey = "WRD_CTMO_Cancel";
            this.btnCancel.ToolTipResourceKey = null;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.CustomStyleName = "biz_btn_ok";
            this.btnOk.LinkedLabelName = null;
            this.btnOk.Location = new System.Drawing.Point(383, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(181, 44);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.TextResourceKey = "WRD_CTMO_Ok";
            this.btnOk.ToolTipResourceKey = null;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // CommonItemListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 576);
            this.Controls.Add(this.tlpMainLayout);
            this.CustomStyleName = "biz_control_view";
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "CommonItemListView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "DuplicatedContainerListView";
            this.tlpMainLayout.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.flpButtonLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TTableLayoutPanel tlpMainLayout;
        private Fontos.Win.Forms.TPanel pnlTitle;
        private Fontos.Win.Forms.TLabel lblTitle;
        private Fontos.Win.Forms.TPanel pnlInput;
        private ItemListControl itemList;
        private Fontos.Win.Forms.TFlowLayoutPanel flpButtonLayout;
        private Fontos.Win.Forms.TButton btnCancel;
        private Fontos.Win.Forms.TButton btnOk;

    }
}