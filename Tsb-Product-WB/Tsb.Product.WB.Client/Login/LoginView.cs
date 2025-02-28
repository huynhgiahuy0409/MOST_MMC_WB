/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2009 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2010.02.05     CHOI       1.0	First release.
* 2010.09.03  Tonny.Kim     1.1 Modify
* COMMENTS : Add C3ITHandler
* 
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Core.Security;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Win.Forms;
using Tsb.Fontos.Core.Security.Authentication;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Win.Security.PwdChange;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Win.Util.Controls;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Win.Message;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Win.Log;
using Tsb.Fontos.Core.Security.SSO;
using Tsb.Fontos.Core.Security.SSO.IPC;
using Tsb.Most.Wb.Client.Login;
using Tsb.Most.Wb.Client.Log;

using Tsb.Most.Wb.Client.Popup;
using Tsb.Fontos.Core.Codes;
using Tsb.Fontos.Core.Context;

using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Item;

using Tsb.Catos.Cm.Core.Codes.Item;
using Tsb.Fontos.Core.Constant;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Product.WB.Common.Info;

namespace Tsb.Most.Wb.Client.Login
{
    /// <summary>
    /// General Login View
    /// </summary>
    public partial class LoginView : BaseDialogView, ILoginView, LoginInterface
    {
        #region FIELD/PROPERTY AREA*****************************

        public IAuthenticationHandler _authenHandler = null;
        public BaseCodeHandler _baseCodeHandler = null;
        /// <summary>
        /// Gets or Sets Authentication Handler object reference
        /// </summary>
        public IAuthenticationHandler AuthenHandler
        {
            get { return this._authenHandler; }
            set { this._authenHandler = value; }
        }


        public BaseCodeHandler BaseCodeHandler
        {
            get { return this._baseCodeHandler; }
            set { this._baseCodeHandler = value; }
        }


        private BaseSecurityParam _secuParam = null;
        /// <summary>
        /// Gets or Sets Security Parameter object reference
        /// </summary>
        public BaseSecurityParam SecuParam
        {
            get { return _secuParam; }
            set { _secuParam = value; }
        }

        private BaseUserInfo _userInfo = null;
        /// <summary>
        /// Gets or Sets User Info
        /// </summary>
        public BaseUserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }

        private bool _canChangePassword = true;
        /// <summary>
        /// Gets or Sets Whether password is changeable or not. Default value is true.
        /// </summary>
        public bool CanChangePassword
        {
            get { return _canChangePassword; }
            set { _canChangePassword = value; }
        }

        public string FormName => throw new NotImplementedException();
        #endregion


        #region INITIALIZE AREA ********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LoginView()
            : base()
        {
            InitializeComponent();

            this.ApplyDefaultKeyEvent = false;
            this.SecuParam = new BaseSecurityParam(this);
            this.BindParamWithControls();
            this.AddEventHandler();
        }

        private void SetCombo()
        {
            this.FillCombItem(cmbGatePoint, CTBizConstant.CodeType.GATECD);  // CodeGeneralItem
            this.FillCombItem(cmbLaneNo, CTBizConstant.CodeType.LANE_CODE);            // CodeGeneralItem
            //this.FillCombItem(cmbSortFirst, CTBizConstant.CodeType.DELIVERY);     // CodeGeneralItem
           // cmbBindingNone.BindDataSource(CTBizConstant.CodeType.IX_CD);          // CodeDataItem
            //cmbBindingBlank.BindDataSource(CTBizConstant.CodeType.IX_CD);         // CodeDataItem
           // cmbBindingSelect.BindDataSource(CTBizConstant.CodeType.IX_CD);        // CodeDataItem
        }


        private void FillCombItem(TComboBox comboBox, string codeType, params string[] args)
        {
            IList<CodeGeneralItem> codeItemList = CodeManager.GetCodes<CodeGeneralItem>(codeType, args);
            codeItemList.Insert(0, new CodeGeneralItem()); // Blank addition.
            comboBox.DataSource = codeItemList;
            comboBox.DisplayMember = CodeConstant.PROP_NAME_CODE_NAME;
            comboBox.ValueMember = CodeConstant.PROP_NAME_CODE;
        }

        

        /// <summary>
        /// Show Login View
        /// </summary>
        /// <param name="authenHandler">Authentication handler object reference</param>
        public BaseUserInfo ShowLoginView(IAuthenticationHandler authenHandler)
        {
            return this.ShowLoginView(authenHandler, false);
        }

        /// <summary>
        /// Show Login View
        /// </summary>
        /// <param name="authenHandler">Authentication handler object reference</param>
        /// <param name="useMsgServer">Use Message Server</param>
        public BaseUserInfo ShowLoginView(IAuthenticationHandler authenHandler, bool useMsgServer)
        {
            Form tempOwnerView = new Form();

            try
            {
                this.AuthenHandler = authenHandler;

                #region Check SSO
                object ssoLoginObject = Tsb.Fontos.Core.Security.SSO.IPC.IPCUtil.GetSSOIPCServerLoginObject(false); //Activator.GetObject(typeof(SSOLogin), IPCUtil.GetIPCUrl("Server", SSOConstant.SSOLOGIN_PREFIX_OBJECT_URI + System.Diagnostics.Process.GetCurrentProcess().Id.ToString()));
                
                if (ssoLoginObject != null)
                {
                    try
                    {
                        SSOLogin ssoLogin = (SSOLogin)ssoLoginObject;
                        string userID = ssoLogin.GetUserID();
                        string password = ssoLogin.GetUserPassword();

                        this.AuthenHandler.SetAuthenHandler(userID, password, false);
                    }

                    catch (TsbSysNetException tsbSysNetEx)
                    {
                        MessageManager.Show(tsbSysNetEx);
                        Environment.Exit(-1);
                    }
                    catch (TsbSysSecurityException tsbSecurityEx)
                    {
                        MessageManager.Show(tsbSecurityEx);
                        Environment.Exit(-1);
                    }
                }
                #endregion
                else
                {
                    if (DeployInfo.IsDevToolEnvironment() && !useMsgServer)
                    {
                        this.AuthenHandler.SetAuthenHandler(authenHandler.PrevLoginUserID);
                    }
                    else
                    {
                        this.StartPosition = FormStartPosition.CenterScreen;

                        if (this.ShowDialog(tempOwnerView) == DialogResult.Cancel)
                        {
                            Environment.Exit(-1);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                ExceptionHandler.Propagate(ex, this.ObjectID);
            }

            return LoginedUserInfo.GetInstance().UserInfo;
        }

        #endregion


        #region DATA BINDING AREA ******************************

        /// <summary>
        /// Binds Parameter With Controls
        /// </summary>
        private void BindParamWithControls()
        {
            this.bdsSecuParam.DataSource = this.SecuParam;
            this.txtUserId.DataBindings.Add("Text", bdsSecuParam, "UserID");
            this.txtPassword.DataBindings.Add("Text", bdsSecuParam, "Password");

           
        }

        #endregion


        #region EVENT HANDLER AREA******************************
        /// <summary>
        /// Do login.
        /// </summary>
        /// <param name="txtUserId"></param>
        /// <param name="txtPassword"></param>
        /// <returns></returns>
        public bool DoLogin(string txtUserId, string txtPassword, bool doRetry)
        {
            bool bSuccess = false;
            try
            {
                bSuccess = this.AuthenHandler.SetAuthenHandler(txtUserId, txtPassword);
            }
            catch (TsbSysSecurityException secuEx)
            {
                if (ModuleInfo.PgmCode == Fontos.Core.Constant.ConfigConstant.PGM_CODE_HOME)
                {
                    throw secuEx;
                }
                //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;

                //MSG:MSG_FTCO_00127 [{0}] data retrieving failed. Contact your system administrator.
                //MSG:"MSG_FTCO_00086" The password is expired.
                if (secuEx.MsgCode.Equals("MSG_FTCO_00127") || secuEx.MsgCode.Equals("MSG_FTCO_00086"))
                {
                    MessageManager.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                    Environment.Exit(-1);
                }
                //MSG:MSG_FTCO_00086 The password is expired. You should ask the administrator.
                else if (secuEx.MsgCode.Equals("MSG_FTCO_00086"))
                {
                    MessageManager.Show("MSG_FTCO_00089", secuEx.MsgCode, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                    Environment.Exit(-1);
                }
                //MSG:MSG_FTCO_00088 Your ID is renewed by administrator. You should change password.
                else if (secuEx.MsgCode.Equals("MSG_FTCO_00088"))
                {
                    DialogResult dlgResult = default(DialogResult);
                    dlgResult = MessageManager.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, true, secuEx.MsgArgs);

                    if (doRetry == false)
                        Environment.Exit(-1);

                    this.txtPassword.Clear();
                    this.txtPassword.Focus();

                    if (dlgResult == DialogResult.OK)
                    {
                        if (this.CanChangePassword)
                        {
                            //this.btnChangePassword.Enabled = true;
                            this.ShowPasswordView(null, txtUserId);
                        }
                    }

                    return false;
                }
                //MSG:MSG_FTCO_00090 Your password will be expired after {0} days. Would you change your password?	
                else if (secuEx.MsgCode.Equals("MSG_FTCO_00090"))
                {
                    if (doRetry == false)
                    {
                        bSuccess = true;
                    }

                    if (this.CanChangePassword)
                    {
                        DialogResult dlgResult = default(DialogResult);
                        dlgResult = MessageManager.Show(secuEx.MsgCode, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                        if (dlgResult == DialogResult.Yes)
                        {
                            //자동으로 Password Change 화면로드하는 로직 추가
                            //this.btnChangePassword.Enabled = true;

                            this.ShowPasswordView(null, txtUserId);
                        }

                        // added by hs.kim 0042149
                        bSuccess = this.AuthenHandler.SetAuthenHandler(this.SecuParam.UserID, this.SecuParam.Password, false);

                        if (bSuccess)
                        {
                            this.CloseWithOK();
                        }
                    }
                }
                else
                {
                    if (secuEx.MsgCode.Equals("MSG_FTCO_00083") ||
                        secuEx.MsgCode.Equals("MSG_FTCO_00084") ||
                        secuEx.MsgCode.Equals("MSG_FTCO_00085"))
                    {
                        //TMessageBox.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                        MessageManager.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                    }
                    if (secuEx.MsgCode.Equals("MSG_FTCO_00087"))
                    {
                        MessageManager.Show("MSG_FTCO_00087", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                    }

                    //MSG: MSG_FTCO_00083 You are not an authorized user. Please check the User ID.
                    //MSG: MSG_FTCO_00087 Your ID is expired.  You should ask the administrator.
                    if (secuEx.MsgCode.Equals("MSG_FTCO_00083") || secuEx.MsgCode.Equals("MSG_FTCO_00087"))
                    {
                        if (doRetry == false)
                            Environment.Exit(-1);

                        this.txtUserId.Focus();
                        this.txtUserId.Select(0, this.txtUserId.Text.Length);
                        return false;
                    }
                    //MSG:You have no access right to this application. You should ask the administrator.
                    else if (secuEx.MsgCode.Equals("MSG_FTCO_00084"))
                    {
                        if (doRetry == false)
                            Environment.Exit(-1);

                        //this.btnCancel.Select();
                        return false;
                    }
                    //MSG:The password is incorrect. Please retype your password.	
                    else if (secuEx.MsgCode.Equals("MSG_FTCO_00085"))
                    {
                        if (doRetry == false)
                            Environment.Exit(-1);

                        this.txtPassword.Focus();
                        this.txtPassword.Select(0, this.txtPassword.Text.Length);
                        return false;
                    }
                    else
                    {
                        if (secuEx.MsgCode.StartsWith("MSG_CTCM"))
                        {
                            //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                            ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                            //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;

                            MessageManager.Show(secuEx);

                        }
                        else
                        {
                            MessageManager.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                            Environment.Exit(-1);
                        }

                        if (doRetry == false)
                            Environment.Exit(-1);
                    }
                }
            }
            catch (TsbSysNetException tsbEx)
            {
                if (ModuleInfo.PgmCode == Fontos.Core.Constant.ConfigConstant.PGM_CODE_HOME)
                {
                    throw tsbEx;
                }
                MessageManager.Show(tsbEx);
                if (doRetry == false)
                    Environment.Exit(-1);

                //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;
                this.txtPassword.Focus();
                this.txtPassword.Select(0, this.txtPassword.Text.Length);
            }
            catch (TsbBaseException tsbEx)
            {
                if (ModuleInfo.PgmCode == Fontos.Core.Constant.ConfigConstant.PGM_CODE_HOME)
                {
                    throw tsbEx;
                }
                MessageManager.Show(tsbEx);
                Environment.Exit(-1);
            }
            catch (Exception ex)
            {
                if (ModuleInfo.PgmCode == Fontos.Core.Constant.ConfigConstant.PGM_CODE_HOME)
                {
                    throw ex;
                }
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
                Environment.Exit(-1);
            }

            return bSuccess;
        }

        /// <summary>
        /// Adds Event Handlers
        /// </summary>
        private void AddEventHandler()
        {
            try
            {
                this.Load += new System.EventHandler(this.GeneralLoginView_Load);
               
                this.btnLogin.Click += new System.EventHandler(this.btnOk_Click);
               // this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
               // this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
                this.txtUserId.KeyPress += new KeyPressEventHandler(this.KeyPressEvent);
                this.txtPassword.KeyPress += new KeyPressEventHandler(this.KeyPressEvent);
                this.Shown += new System.EventHandler(LoginView_Shown);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }

        /// <summary>
        /// Form Load Event Handler
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event argument</param>
        private void GeneralLoginView_Load(object sender, EventArgs e)
        {
            try
            {
                if (SecurityPolicyInfo.GetInstance().TextChar == TextCharTypes.UpperCase)
                {
                    this.txtUserId.Masked = Mask.Upper;
                }
                else if (SecurityPolicyInfo.GetInstance().TextChar == TextCharTypes.LowerCase)
                {
                    this.txtUserId.Masked = Mask.Lower;
                }

               // this.btnChangePassword.Visible = this.CanChangePassword;

                if (!string.IsNullOrEmpty(this._authenHandler.PrevLoginUserID))
                {
                    this.txtUserId.Text = this._authenHandler.PrevLoginUserID;
                }

                this.Activate();
                this.Show();

                if (this.txtUserId.Text.Trim() != "")
                    this.txtPassword.Focus();
                else
                    this.txtUserId.Focus();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }

        
        private void btnTest_Click(object sender, EventArgs e)
        {
            SetCombo();

        }

        
            private void btnTestValue_Click(object sender, EventArgs e)
        {
            //(cmbGatePoint.DataSource.Current //as TListBoxItem).Clone() as TListBoxItem;

            MessageBox.Show(cmbGatePoint.Text);

        }
        /// <summary>
        /// OK Button Click Event Handler
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event argument</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = default(DialogResult);

            //ControlHandler.SetControlsEnabled(false, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk, this.btnChangePassword);
            ControlHandler.SetControlsEnabled(false, this.txtUserId, this.txtPassword);
            if (this.txtUserId.Text.Trim() == "")
            {
                //CAP : Login Failed
                //MSG : You did not input your ID.\nPlease input your ID.
                dialogResult = MessageManager.Show("MSG_FTCO_00079", "MSG_FTCO_00077", MessageBoxButtons.OK, MessageBoxIcon.Warning, true, null);
                this.txtUserId.Clear();
                //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;
                return;
            }

            if (this.txtPassword.Text.Trim() == "")
            {
                //CAP : Login Failed
                //MSG : You did not input your password.\nPlease input your password
                dialogResult = MessageManager.Show("MSG_FTCO_00079", "MSG_FTCO_00078", MessageBoxButtons.OK, MessageBoxIcon.Warning, true, null);
                this.txtPassword.Clear();
                //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;
                return;
            }

            this.AuthenHandler.CanChangePassword = this.CanChangePassword;

            try
            {
                DiagnosticsInfo.GetInstance().StartTimeOfLoginCheck =DateTime.Now;
                GeneralLogger.Info("Check user authority [Start]");
                bool bSuccess = this.AuthenHandler.SetAuthenHandler(this.SecuParam.UserID, this.SecuParam.Password);
                GeneralLogger.Info("Check user authority [End]");
                if (bSuccess)
                {
                    MTCommonInfo.GetInstance().GateName = ((CodeGeneralItem)this.cmbGatePoint.SelectedItem).CodeName;
                    MTCommonInfo.GetInstance().GateCode = ((CodeGeneralItem)this.cmbGatePoint.SelectedItem).Code;
                    MTCommonInfo.GetInstance().LaneName = ((CodeGeneralItem)this.cmbLaneNo.SelectedItem).CodeName;
                    MTCommonInfo.GetInstance().LaneCode = ((CodeGeneralItem)this.cmbLaneNo.SelectedItem).Code;
                    this.CloseWithOK();
                }
                else
                {
                    //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                    ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                    //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;
                }
            }
            
            catch (TsbSysSecurityException secuEx)
            {
                //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;

                //MSG:MSG_FTCO_00127 [{0}] data retrieving failed. Contact your system administrator.
                //MSG:"MSG_FTCO_00086" The password is expired.
                if (secuEx.MsgCode.Equals("MSG_FTCO_00127") || secuEx.MsgCode.Equals("MSG_FTCO_00086"))
                {
                    MessageManager.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                    Environment.Exit(-1);
                }
                //MSG:MSG_FTCO_00086 The password is expired. You should ask the administrator.
                else if (secuEx.MsgCode.Equals("MSG_FTCO_00086"))
                {
                    MessageManager.Show("MSG_FTCO_00089", secuEx.MsgCode, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                    Environment.Exit(-1);
                }
                //MSG:MSG_FTCO_00088 Your ID is renewed by administrator. You should change password.
                else if (secuEx.MsgCode.Equals("MSG_FTCO_00088"))
                {
                    DialogResult dlgResult = default(DialogResult);
                    dlgResult = MessageManager.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, true, secuEx.MsgArgs);

                    this.txtPassword.Clear();
                    this.txtPassword.Focus();

                    if (dlgResult == DialogResult.OK)
                    {
                        if (this.CanChangePassword)
                        {
                           // this.btnChangePassword.Enabled = true;
                            this.ShowPasswordView();
                        }
                    }

                    return;
                }
                //MSG:MSG_FTCO_00090 Your password will be expired after {0} days. Would you change your password?	
                else if (secuEx.MsgCode.Equals("MSG_FTCO_00090"))
                {
                    if (this.CanChangePassword)
                    {
                        DialogResult dlgResult = default(DialogResult);
                        dlgResult = MessageManager.Show(secuEx.MsgCode, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                        if (dlgResult == DialogResult.Yes)
                        {
                            //자동으로 Password Change 화면로드하는 로직 추가
                            //this.btnChangePassword.Enabled = true;

                            this.ShowPasswordView();
                        }

                        // added by hs.kim 0042149
                        bool bSuccess = this.AuthenHandler.SetAuthenHandler(this.SecuParam.UserID, this.SecuParam.Password, false);

                        if (bSuccess)
                        {
                            this.CloseWithOK();
                        }
                    }
                }
                else
                {   
                    if (secuEx.MsgCode.Equals("MSG_FTCO_00083") ||
                        secuEx.MsgCode.Equals("MSG_FTCO_00084") ||
                        secuEx.MsgCode.Equals("MSG_FTCO_00085"))
                    {
                        //TMessageBox.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                        MessageManager.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                    }
                    if (secuEx.MsgCode.Equals("MSG_FTCO_00087"))
                    {
                        MessageManager.Show("MSG_FTCO_00087", secuEx.MsgCode, MessageBoxButtons.OK,  MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                    }

                    //MSG: MSG_FTCO_00083 You are not an authorized user. Please check the User ID.
                    //MSG: MSG_FTCO_00087 Your ID is expired.  You should ask the administrator.
                    if (secuEx.MsgCode.Equals("MSG_FTCO_00083") || secuEx.MsgCode.Equals("MSG_FTCO_00087"))
                    {
                        this.txtUserId.Focus();
                        this.txtUserId.Select(0, this.txtUserId.Text.Length);
                        return;
                    }
                    //MSG:You have no access right to this application. You should ask the administrator.
                    else if (secuEx.MsgCode.Equals("MSG_FTCO_00084"))
                    {
                        //this.btnCancel.Select();
                        return;
                    }
                    //MSG:The password is incorrect. Please retype your password.	
                    else if (secuEx.MsgCode.Equals("MSG_FTCO_00085"))
                    {
                        this.txtPassword.Focus();
                        this.txtPassword.Select(0, this.txtPassword.Text.Length);
                        return;
                    }
                    else
                    {
                        if (secuEx.MsgCode.StartsWith("MSG_CTCM"))
                        {
                            //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                            ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                            //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;
                            MessageManager.Show(secuEx);

                        }
                        else
                        {
                            MessageManager.Show("MSG_FTCO_00079", secuEx.MsgCode, MessageBoxButtons.OK, MessageBoxIcon.Warning, true, secuEx.MsgArgs);
                            Environment.Exit(-1);
                        }
                    }
                }
            }
            catch (TsbSysNetException tsbEx)
            {
                MessageManager.Show(tsbEx);

                //ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword, this.btnCancel, this.btnOk);
                ControlHandler.SetControlsEnabled(true, this.txtUserId, this.txtPassword);
                //this.btnChangePassword.Enabled = this.CanChangePassword ? true : false;

                this.txtPassword.Focus();
                this.txtPassword.Select(0, this.txtPassword.Text.Length);
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
                Environment.Exit(-1);
            }
            catch (Exception ex)
            {
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message));
                Environment.Exit(-1);
            }

            return;
        }


        /// <summary>
        /// Change Password Click Event Handler
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event argument</param>
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            this.ShowPasswordView();
            return;
        }

        /// <summary>
        /// Cancel Button Click Event Handler
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event argument</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.CloseWithCancel();
            return;
        }

        /// <summary>
        /// Key Press Event
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event argument</param>
        private void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox tempTxt = null;
                if (e.KeyChar == (char)13)
                {
                    tempTxt = sender as TextBox;
                    if (tempTxt == this.txtUserId)
                    {
                        this.txtPassword.Focus();
                        return;
                    }
                    else if (tempTxt == this.txtPassword)
                    {
                       // this.btnOk.PerformClick();
                    }
                }
                else if (e.KeyChar == (char)27)
                {
                    this.CloseWithCancel();
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return;
        }


        #endregion


        #region METHOD AREA (SHOW PASSWORD CHANGE VIEW)*********
        /// <summary>
        /// Show Password Change View with specified view icon, user id.
        /// </summary>
        /// <param name="viewIcon"></param>
        /// <param name="initialUserID"></param>
        public void ShowPasswordView(Icon viewIcon, string initialUserID)
        {
            GeneralPwdChangeView pwdChangeView = null;
            string userID = null;

            try
            {
                if (this.UserInfo != null)
                {
                    userID = this.UserInfo.StaffCD;
                }
                else if (string.IsNullOrEmpty(initialUserID) == false)
                {
                    userID = initialUserID;
                }
                else if (string.IsNullOrEmpty(this.txtUserId.Text) == false)
                {
                    userID = this.txtUserId.Text;
                }

                pwdChangeView = new GeneralPwdChangeView(userID);
                if (viewIcon != null)
                {
                    pwdChangeView.Icon = viewIcon;
                }
                pwdChangeView.PasswordEncrypter = this.AuthenHandler.PasswordEncrypter;

                pwdChangeView.ShowDialog(this);

                // added hs.kim 0042149
                if (pwdChangeView.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.SecuParam.Password = pwdChangeView.SecuParam.NewPassword.ToString();
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_99998", DefaultMessage.NON_REG_WRD + ex.Message);
            }
            return;
        }

        /// <summary>
        /// Show Password Change View with specified view icon.
        /// </summary>
        /// <param name="viewIcon"></param>
        public void ShowPasswordView(Icon viewIcon)
        {
            ShowPasswordView(viewIcon, null);
        }

        /// <summary>
        /// Show Password Change View.
        /// </summary>
        public void ShowPasswordView()
        {
            ShowPasswordView(null);
        }
        #endregion

        private void tPictureBox1_Click(object sender, EventArgs e)
        {

        }

        //public TruckListPopupInterface TruckListPopupView { get; set; }
        private void tButton1_Click(object sender, EventArgs e)
        {
           //Form popup =  new TruckListPopupView();
           // popup.Show();
        }

        private void LoginView_Shown(object sender, EventArgs e)
        {
            
             SetCombo();
            
        }
    }
}

