namespace Tsb.Most.Wb.Client.Login
{
    partial class LoginView
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bdsSecuParam = new System.Windows.Forms.BindingSource(this.components);
            this.lblPassword = new Tsb.Fontos.Win.Forms.TLabel();
            this.txtPassword = new Tsb.Fontos.Win.Forms.TTextBox();
            this.txtUserId = new Tsb.Fontos.Win.Forms.TTextBox();
            this.lblUserId = new Tsb.Fontos.Win.Forms.TLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnLogin = new Tsb.Fontos.Win.Forms.TButton();
            this.tPanel1 = new Tsb.Fontos.Win.Forms.TPanel();
            this.tPictureBox1 = new Tsb.Fontos.Win.Forms.TPictureBox();
            this.tPanel3 = new Tsb.Fontos.Win.Forms.TPanel();
            this.lblLogin = new Tsb.Fontos.Win.Forms.TLabel();
            this.cmbLaneNo = new Tsb.Fontos.Win.Forms.TComboBox();
            this.lblLaneNo = new Tsb.Fontos.Win.Forms.TLabel();
            this.cmbGatePoint = new Tsb.Fontos.Win.Forms.TComboBox();
            this.lblGatePoint = new Tsb.Fontos.Win.Forms.TLabel();
            this.bdsComboBox = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSecuParam)).BeginInit();
            this.tPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tPictureBox1)).BeginInit();
            this.tPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bdsSecuParam;
            // 
            // lblPassword
            // 
            this.lblPassword.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblPassword.IsAppliedLinkedStyle = false;
            this.lblPassword.LinkedLabelName = null;
            this.lblPassword.Location = new System.Drawing.Point(22, 99);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(76, 26);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPassword.TextResourceKey = null;
            this.lblPassword.ToolTipResourceKey = null;
            // 
            // txtPassword
            // 
            this.txtPassword.CustomMask = null;
            this.txtPassword.LinkedLabelName = null;
            this.txtPassword.Location = new System.Drawing.Point(100, 99);
            this.txtPassword.MaxLength = 30;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(214, 26);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextResourceKey = null;
            this.toolTip.SetToolTip(this.txtPassword, "Input Password");
            this.txtPassword.UseTextMandatoryFont = false;
            // 
            // txtUserId
            // 
            this.txtUserId.CustomMask = null;
            this.txtUserId.LinkedLabelName = null;
            this.txtUserId.Location = new System.Drawing.Point(100, 67);
            this.txtUserId.MaxLength = 30;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(214, 26);
            this.txtUserId.TabIndex = 1;
            this.txtUserId.TextResourceKey = null;
            this.toolTip.SetToolTip(this.txtUserId, "Input User ID");
            this.txtUserId.UseTextMandatoryFont = false;
            // 
            // lblUserId
            // 
            this.lblUserId.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserId.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblUserId.IsAppliedLinkedStyle = false;
            this.lblUserId.LinkedLabelName = null;
            this.lblUserId.Location = new System.Drawing.Point(22, 67);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(76, 26);
            this.lblUserId.TabIndex = 6;
            this.lblUserId.Text = "User ID";
            this.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUserId.TextResourceKey = null;
            this.lblUserId.ToolTipResourceKey = null;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.OrangeRed;
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLogin.LinkedLabelName = null;
            this.btnLogin.Location = new System.Drawing.Point(126, 232);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(147, 38);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "Login";
            this.btnLogin.TextResourceKey = null;
            this.toolTip.SetToolTip(this.btnLogin, "Login");
            this.btnLogin.ToolTipResourceKey = null;
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // tPanel1
            // 
            this.tPanel1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.tPanel1.BorderColor = System.Drawing.Color.Empty;
            this.tPanel1.Controls.Add(this.tPictureBox1);
            this.tPanel1.Controls.Add(this.tPanel3);
            this.tPanel1.Location = new System.Drawing.Point(0, 0);
            this.tPanel1.Name = "tPanel1";
            this.tPanel1.Size = new System.Drawing.Size(851, 622);
            this.tPanel1.TabIndex = 7;
            // 
            // tPictureBox1
            // 
            this.tPictureBox1.BackgroundImage = global::Tsb.Product.WB.Client.Properties.Resources.logoLogo;
            this.tPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tPictureBox1.LinkedLabelName = null;
            this.tPictureBox1.Location = new System.Drawing.Point(3, 64);
            this.tPictureBox1.Name = "tPictureBox1";
            this.tPictureBox1.Size = new System.Drawing.Size(354, 433);
            this.tPictureBox1.TabIndex = 2;
            this.tPictureBox1.TabStop = false;
            this.tPictureBox1.TextResourceKey = null;
            this.tPictureBox1.ToolTipResourceKey = null;
            // 
            // tPanel3
            // 
            this.tPanel3.BackColor = System.Drawing.Color.Indigo;
            this.tPanel3.BorderColor = System.Drawing.Color.Empty;
            this.tPanel3.Controls.Add(this.lblLogin);
            this.tPanel3.Controls.Add(this.btnLogin);
            this.tPanel3.Controls.Add(this.cmbLaneNo);
            this.tPanel3.Controls.Add(this.lblLaneNo);
            this.tPanel3.Controls.Add(this.cmbGatePoint);
            this.tPanel3.Controls.Add(this.lblGatePoint);
            this.tPanel3.Controls.Add(this.lblPassword);
            this.tPanel3.Controls.Add(this.txtPassword);
            this.tPanel3.Controls.Add(this.txtUserId);
            this.tPanel3.Controls.Add(this.lblUserId);
            this.tPanel3.Location = new System.Drawing.Point(376, 97);
            this.tPanel3.Name = "tPanel3";
            this.tPanel3.Size = new System.Drawing.Size(343, 340);
            this.tPanel3.TabIndex = 1;
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLogin.IsAppliedLinkedStyle = false;
            this.lblLogin.LinkedLabelName = null;
            this.lblLogin.Location = new System.Drawing.Point(145, 9);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(92, 34);
            this.lblLogin.TabIndex = 15;
            this.lblLogin.Text = "Login";
            this.lblLogin.TextResourceKey = null;
            this.lblLogin.ToolTipResourceKey = null;
            // 
            // cmbLaneNo
            // 
            this.cmbLaneNo.CodeArgs = null;
            this.cmbLaneNo.CodeType = null;
            this.cmbLaneNo.DefaultFirstRowStringResourceKey = null;
            this.cmbLaneNo.DropDownWidth = 121;
            this.cmbLaneNo.FormattingEnabled = true;
            this.cmbLaneNo.LinkedLabelName = null;
            this.cmbLaneNo.Location = new System.Drawing.Point(100, 163);
            this.cmbLaneNo.Name = "cmbLaneNo";
            this.cmbLaneNo.Size = new System.Drawing.Size(214, 26);
            this.cmbLaneNo.TabIndex = 13;
            this.cmbLaneNo.TextResourceKey = null;
            this.cmbLaneNo.UseItemMandatoryFont = false;
            // 
            // lblLaneNo
            // 
            this.lblLaneNo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLaneNo.IsAppliedLinkedStyle = false;
            this.lblLaneNo.LinkedLabelName = null;
            this.lblLaneNo.Location = new System.Drawing.Point(22, 163);
            this.lblLaneNo.Name = "lblLaneNo";
            this.lblLaneNo.Size = new System.Drawing.Size(76, 26);
            this.lblLaneNo.TabIndex = 12;
            this.lblLaneNo.Text = "Lane No";
            this.lblLaneNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLaneNo.TextResourceKey = null;
            this.lblLaneNo.ToolTipResourceKey = null;
            // 
            // cmbGatePoint
            // 
            this.cmbGatePoint.CodeArgs = null;
            this.cmbGatePoint.CodeType = null;
            this.cmbGatePoint.DefaultFirstRowStringResourceKey = null;
            this.cmbGatePoint.DropDownWidth = 121;
            this.cmbGatePoint.FormattingEnabled = true;
            this.cmbGatePoint.LinkedLabelName = null;
            this.cmbGatePoint.Location = new System.Drawing.Point(100, 131);
            this.cmbGatePoint.Name = "cmbGatePoint";
            this.cmbGatePoint.Size = new System.Drawing.Size(214, 26);
            this.cmbGatePoint.TabIndex = 11;
            this.cmbGatePoint.TextResourceKey = null;
            this.cmbGatePoint.UseItemMandatoryFont = false;
            // 
            // lblGatePoint
            // 
            this.lblGatePoint.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblGatePoint.IsAppliedLinkedStyle = false;
            this.lblGatePoint.LinkedLabelName = null;
            this.lblGatePoint.Location = new System.Drawing.Point(21, 131);
            this.lblGatePoint.Name = "lblGatePoint";
            this.lblGatePoint.Size = new System.Drawing.Size(76, 26);
            this.lblGatePoint.TabIndex = 10;
            this.lblGatePoint.Text = "Gate Point";
            this.lblGatePoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGatePoint.TextResourceKey = null;
            this.lblGatePoint.ToolTipResourceKey = null;
            // 
            // LoginView
            // 
            this.AuthorizedMenuName = "LoginView";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 603);
            this.Controls.Add(this.tPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Login";
            this.ViewLocation = new System.Drawing.Point(15, -80);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsSecuParam)).EndInit();
            this.tPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tPictureBox1)).EndInit();
            this.tPanel3.ResumeLayout(false);
            this.tPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsComboBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Tsb.Fontos.Win.Forms.TTextBox txtPassword;
        private Tsb.Fontos.Win.Forms.TTextBox txtUserId;
        private Tsb.Fontos.Win.Forms.TLabel lblUserId;
        private Tsb.Fontos.Win.Forms.TLabel lblPassword;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.BindingSource bdsSecuParam;
        private Fontos.Win.Forms.TPanel tPanel1;
        private Fontos.Win.Forms.TPanel tPanel3;
        private Fontos.Win.Forms.TPictureBox tPictureBox1;
        private Fontos.Win.Forms.TComboBox cmbGatePoint;
        private Fontos.Win.Forms.TLabel lblGatePoint;
        private Fontos.Win.Forms.TComboBox cmbLaneNo;
        private Fontos.Win.Forms.TLabel lblLaneNo;
        private Fontos.Win.Forms.TButton btnLogin;
        private Fontos.Win.Forms.TLabel lblLogin;
        private System.Windows.Forms.BindingSource bdsComboBox;
    }
}