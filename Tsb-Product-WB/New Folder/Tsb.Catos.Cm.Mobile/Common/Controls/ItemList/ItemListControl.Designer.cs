namespace Tsb.Catos.Cm.Mobile.Common.Controls.ItemList
{
    partial class ItemListControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new Tsb.Fontos.Win.Forms.TPanel();
            this.pnlInput = new Tsb.Fontos.Win.Forms.TPanel();
            this.pnlButtonList = new Tsb.Fontos.Win.Forms.TPanel();
            this.pnlScroll = new Tsb.Fontos.Win.Forms.TPanel();
            this.scrButtonList = new Tsb.Fontos.Win.Controls.ScrollBar.TVResizeScrollBar();
            this.pnlMain.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.Empty;
            this.pnlMain.Controls.Add(this.pnlInput);
            this.pnlMain.Controls.Add(this.pnlScroll);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(378, 314);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlInput
            // 
            this.pnlInput.BackColor = System.Drawing.SystemColors.Control;
            this.pnlInput.BorderColor = System.Drawing.Color.Empty;
            this.pnlInput.Controls.Add(this.pnlButtonList);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(350, 314);
            this.pnlInput.TabIndex = 1;
            // 
            // pnlButtonList
            // 
            this.pnlButtonList.BackColor = System.Drawing.SystemColors.Control;
            this.pnlButtonList.BorderColor = System.Drawing.Color.Empty;
            this.pnlButtonList.Location = new System.Drawing.Point(24, 32);
            this.pnlButtonList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButtonList.Name = "pnlButtonList";
            this.pnlButtonList.Size = new System.Drawing.Size(280, 53);
            this.pnlButtonList.TabIndex = 1;
            // 
            // pnlScroll
            // 
            this.pnlScroll.BorderColor = System.Drawing.Color.Empty;
            this.pnlScroll.Controls.Add(this.scrButtonList);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlScroll.Location = new System.Drawing.Point(350, 0);
            this.pnlScroll.Margin = new System.Windows.Forms.Padding(0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(28, 314);
            this.pnlScroll.TabIndex = 0;
            // 
            // scrButtonList
            // 
            this.scrButtonList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scrButtonList.CustomStyleName = null;
            this.scrButtonList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrButtonList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrButtonList.LargeChangeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.scrButtonList.LargeChangeForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scrButtonList.Location = new System.Drawing.Point(0, 0);
            this.scrButtonList.Margin = new System.Windows.Forms.Padding(0);
            this.scrButtonList.Name = "scrButtonList";
            this.scrButtonList.Size = new System.Drawing.Size(28, 314);
            this.scrButtonList.SmallChangeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.scrButtonList.SmallChangeForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.scrButtonList.TabIndex = 0;
            this.scrButtonList.ThumbBackColor = System.Drawing.Color.LightGray;
            this.scrButtonList.TrackBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // ItemListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "ItemListControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(384, 320);
            this.pnlMain.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlScroll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Fontos.Win.Forms.TPanel pnlMain;
        private Fontos.Win.Forms.TPanel pnlInput;
        private Fontos.Win.Forms.TPanel pnlScroll;
        private Fontos.Win.Controls.ScrollBar.TVResizeScrollBar scrButtonList;
        private Fontos.Win.Forms.TPanel pnlButtonList;

    }
}
