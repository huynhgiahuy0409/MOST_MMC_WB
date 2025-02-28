#region Class Definitions
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
* 2010.02.03    CHOI 1.0   First release.
* 2010.09.06  Tonny.Kim    extends BaseAuthenHandler
* 2010.12.06  Tonny.Kim    Add to LDAP
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Security.Authentication;
using Tsb.Fontos.Core.Security.Encryption;
using Tsb.Fontos.Core.Monitoring.Database;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Security.Authorization;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Util.Converter;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Security.Authentication
{
    /// <summary>
    /// Password based Authentication Handler Class
    /// </summary>
    public class PasswordAuthenHandler : BaseAuthenHandler
    {
        #region FIELD/PROPERTY AREA*****************************
        /// <summary>
        /// App Main Server
        /// </summary>
        private Server appServer;

        /// <summary>
        ///  Gets or Sets Security Service Implements
        /// </summary>
        public override ISecurityService SecurityService { get; set; }

        /// <summary>
        /// Gets or Sets User ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets User Info Item object reference
        /// </summary>
        public BaseUserInfo UserInfo { get; set; }

        /// <summary>
        /// Gets or Sets Access Right Item object reference
        /// </summary>
        public ModuleAccessRightItem AccessRightItem { get; set; }

        /// <summary>
        /// Gets or Sets Global application's operation authorization Information List
        /// </summary>
        public IList<AuthorInfoItem> OperationAuthorInfoList { get; set; }

        /// <summary>
        /// Gets or Sets user expiration policy item object reference
        /// </summary>
        public BaseExpirePolicyItem ExpirePolicyItem { get; set; }

        private bool _doExpireCheck = true;
        /// <summary>
        /// Gets or Sets whether user exprie check is required or not. Default value is true
        /// </summary>
        public override bool DoExpireCheck
        {
            get { return this._doExpireCheck; }
            set { this._doExpireCheck = value; }
        }
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PasswordAuthenHandler()
            : base()
        {
            this.ObjectID = "GNR-FTCO-SEC-PasswordAuthenHandler";
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion


        #region METHOD AREA (CheckHandler)**********************
        protected override bool CheckHandler(string userID, string password)
        {
            return this.CheckHandler(userID, password, true);
        }


        protected override bool CheckHandler(string userId, string password, bool isPwExpiredCheck)
        {
            this.appServer = SysServersInfo.GetServerInfo(ConfigConstant.SERVER_ROLE_MAIN_APP);

            if (DeployInfo.IsDevToolEnvironment())
            {
                this.AuthenticateAsDev(userId);
            }
            else
            {
                this.Authenticate(userId, password, isPwExpiredCheck);
            }
            return true;
        }

        protected override bool CheckHandler(BaseSecurityParam param, bool isPwExpiredCheck)
        {
            return CheckHandler(param.UserID, param.Password, isPwExpiredCheck);
        }
        #endregion

        #region METHOD AREA (AUTHENTICATION)********************
        public void Authenticate(string userID, string password)
        {
            this.Authenticate(userID, password, true);
        }

        /// <summary>
        /// Authenticates and returns user info object reference if authentication is successful
        /// </summary>
        /// <returns>User info object reference if authentication is successful</returns>
        public void Authenticate(string userID, string password, bool isPwExpiredCheck)
        {
            try
            {
                this.UserId = userID;
                this.Password = password;

                if (!this.appServer.UseLDAP)
                {
                    this.RetrieveUserInfo();
                }
                else
                {
                    this.UserInfo = LoginedUserInfo.GetInstance().UserInfo;
                }

                if (this.UserInfo == null)
                {
                    //MSG:You have no access right to this application. You should ask the administrator.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00084", null);
                }

                this.InquiryModuleAccessUserRight();

                if (!this.appServer.UseLDAP)
                {

                    if (this.DoExpireCheck)
                    {
                        this.InquiryExpirationPolicy();
                        this.PreCheckUserExpire();
                    }

                    this.VerifyPasssord();

                    if (this.DoExpireCheck && isPwExpiredCheck)
                    {
                        this.PostCheckUserExpire();
                    }
                }

                this.InquiryAuthorInfo();

                this.UpdateLoginSuccess();
                this.UserInfo.Authenticated = true;
                this.UserInfo.UserGroups = new List<BaseGroupInfo>() { new BaseGroupInfo(this.UserInfo.UserGroup) };

                LoginedUserInfo.GetInstance().UserInfo = this.UserInfo;
                LoginedUserInfo.GetInstance().OPAuthorInfoList = this.OperationAuthorInfoList;
                AppEnv.UserInfo = LoginedUserInfo.GetInstance().UserInfo;
                AppEnv.OpAuthorInfoList = LoginedUserInfo.GetInstance().OPAuthorInfoList;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }
        }

        #endregion


        #region METHOD AREA (AUTHENTICATION FOR DEVELOPER)******
        /// <summary>
        /// CAUTION!!!, use only this method for development
        /// </summary>
        /// <returns>User info object reference if authentication is successful</returns>
        public void AuthenticateAsDev(string userID)
        {
            try
            {
                if (DeployInfo.IsDevToolEnvironment() == false)
                {
                    //MSG:You have no access right to this application. You should ask the administrator.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00084", null);
                }
                else
                {
                    this.UserId = userID;

                    if (!this.appServer.UseLDAP)
                    {
                        this.RetrieveUserInfo();
                    }
                    else
                    {
                        this.UserInfo = LoginedUserInfo.GetInstance().UserInfo;
                    }

                    if (this.UserInfo == null)
                    {
                        //MSG:You have no access right to this application. You should ask the administrator.
                        throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00084", null);
                    }

                    this.InquiryAuthorInfo();
                    this.UserInfo.Authenticated = true;
                    this.UserInfo.UserGroups = new List<BaseGroupInfo>() { new BaseGroupInfo(this.UserInfo.UserGroup) };

                    LoginedUserInfo.GetInstance().UserInfo = this.UserInfo;
                    LoginedUserInfo.GetInstance().OPAuthorInfoList = this.OperationAuthorInfoList;
                    AppEnv.UserInfo = LoginedUserInfo.GetInstance().UserInfo;
                    AppEnv.OpAuthorInfoList = LoginedUserInfo.GetInstance().OPAuthorInfoList;
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }
        }

        #endregion


        #region METHOD AREA (RETRIEVE USER INFORMATION)*********
        /// <summary>
        /// Retrieves and sets user information to SecurityItem instance property. Return true if user information query is successful.
        /// </summary>
        /// <returns>true if user information query is successful</returns>
        public bool RetrieveUserInfo()
        {
            BaseResult baseResult = null;
            bool isSuccess = false;
            bool isValidUserLevel = false;

            try
            {
                this.SecurityService = BizServiceLocator.GetService<ISecurityService>(SysObjectSpec.SYS_OBJECT_SECURITY_SERVICE);

                baseResult = this.SecurityService.InquiryUserInfo(new BaseSecurityParam(this) { UserID = this.UserId });

                if (baseResult.ResultObject != null && baseResult.ResultObject is BaseUserInfo)
                {
                    this.UserInfo = baseResult.ResultObject as BaseUserInfo;

                    isValidUserLevel = this.SecurityService.CheckValidUserLevel(this.UserInfo);

                    if (isValidUserLevel)
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess == false)
                {
                    //MSG:You are not an authorized user. Please check the User ID.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00083", null);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return isSuccess;
        }
        #endregion


        #region METHOD AREA (RETRIEVE ACCESS RIGHT INFO)********
        /// <summary>
        /// Retrieves and sets application access right information information to AccessRightItem instance property. 
        /// Return true if app access  information query is successful.
        /// </summary>
        /// <returns>true if user information query is successful</returns>
        public bool InquiryModuleAccessUserRight()
        {
            BaseResult baseResult = null;
            bool isSuccess = false;

            try
            {
                this.SecurityService = BizServiceLocator.GetService<ISecurityService>(SysObjectSpec.SYS_OBJECT_SECURITY_SERVICE);

                if (appServer.UseLDAP)
                {
                    baseResult = this.SecurityService.InquiryModuleAccessUserRightGroup(new BaseSecurityParam(this) { UserID = this.UserId });
                }
                else
                {
                    baseResult = this.SecurityService.InquiryModuleAccessUserRightSingle(new BaseSecurityParam(this) { UserID = this.UserId });
                }

                if (baseResult.ResultObject != null && baseResult.ResultObject is ModuleAccessRightItem)
                {
                    this.AccessRightItem = baseResult.ResultObject as ModuleAccessRightItem;

                    if (!string.IsNullOrEmpty(this.AccessRightItem.UseCheckYN) && this.AccessRightItem.UseCheckYN.Equals("Y"))
                    {
                        isSuccess = true;
                    }
                }

                if (isSuccess == false)
                {
                    //MSG:You have no access right to this application. You should ask the administrator.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00084", null);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return isSuccess;
        }
        #endregion


        #region METHOD AREA (RETRIEVE EXPIRE POLICY)************
        /// <summary>
        /// Retrieves and sets user expiration policy information to ExpirePolicyItem instance property. 
        /// Return true if app access  information query is successful.
        /// </summary>
        /// <returns>true if user information query is successful</returns>
        private bool InquiryExpirationPolicy()
        {
            BaseResult baseResult = null;
            bool isSuccess = false;

            try
            {
                this.SecurityService = BizServiceLocator.GetService<ISecurityService>(SysObjectSpec.SYS_OBJECT_SECURITY_SERVICE);
                baseResult = this.SecurityService.InquiryExpirationPolicy(new BaseSecurityParam(this) { UseAutoLogoutInterval = AppEnvInfo.GetInstance().UseAutoLogoutInterval });

                if (baseResult.ResultObject != null && baseResult.ResultObject is BaseExpirePolicyItem)
                {
                    this.ExpirePolicyItem = baseResult.ResultObject as BaseExpirePolicyItem;
                    isSuccess = true;
                }

                if (isSuccess == false)
                {
                    //MSG:[{0}] data retrieving failed. Contact your system administrator.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00127", DefaultMessage.NON_REG_WRD + "USER EXPIRATION POLICY");
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return isSuccess;
        }
        #endregion


        #region METHOD AREA (VERIFY PASSWORD)*******************
        /// <summary>
        /// Verifies password and returns true if password verification is successful
        /// </summary>
        /// <returns>true if password verification is successful</returns>
        private bool VerifyPasssord()
        {
            bool isSuccess = false;
            string decryptedPwd = null;

            try
            {
                decryptedPwd = this.PasswordEncrypter.Decrypt(this.UserInfo.EncrypedPassword, SecurityPolicyInfo.ENCRYPTION_KEY);

                if (decryptedPwd.Equals(this.Password))
                {
                    isSuccess = true;
                }
                else
                {
                    this.SecurityService = BizServiceLocator.GetService<ISecurityService>(SysObjectSpec.SYS_OBJECT_SECURITY_SERVICE);
                    this.SecurityService.UpdateWrongPwdRetry(new BaseSecurityParam(this) { UserID = this.UserId });

                    //MSG:The password is incorrect. Please retype your password.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00085", null);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return isSuccess;
        }
        #endregion


        #region METHOD AREA (CHECK USER EXPIRE PRE-CHK)*********
        /// <summary>
        /// Pre-Check user expiration returns true if user is not expired
        /// </summary>
        /// <returns>true if user is not expired</returns>
        private bool PreCheckUserExpire()
        {
            bool isNotExpired = false;

            try
            {
                if (this.UserInfo.Retry >= this.ExpirePolicyItem.ExpireRetry)
                {
                    //MSG:The password is expired.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00086", null);
                }

                if (this.UserInfo.IsExpire)
                {
                    //MSG:Your ID is expired.  You should ask the administrator.	
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00087", null);
                }

                isNotExpired = true;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return isNotExpired;
        }
        #endregion


        #region METHOD AREA (CHECK USER EXPIRE POST-CHK)********
        /// <summary>
        /// Post-Check user expiration returns true if user is not expired
        /// </summary>
        /// <returns>true if user is not expired</returns>
        private bool PostCheckUserExpire()
        {
            bool isNotExpired = false;
            bool noLoginIsLong = false;
            bool noPwdChangeIsLong = false;
            bool isWarningPeriod = false;
            DateTime pwdChangeLimitDate = default(DateTime);
            DateTime pwdChangeWarnDate = default(DateTime);
            DateTime dbTodayDate = default(DateTime);


            try
            {
                if (this.UserInfo.PwdChangeTime.Equals(default(DateTime)))
                {
                    //MSG:Your ID is renewed by administrator. You should change password.	
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00088", null);
                }

                pwdChangeLimitDate = this.UserInfo.PwdChangeTime.AddDays(this.ExpirePolicyItem.ExpireTerm);
                dbTodayDate = this.UserInfo.Today;
                pwdChangeWarnDate = dbTodayDate.AddDays(this.ExpirePolicyItem.WarningTerm);

                noLoginIsLong = this.UserInfo.DaysAfterLastLogin > this.ExpirePolicyItem.ExpireLogin;
                noPwdChangeIsLong = pwdChangeLimitDate < dbTodayDate;
                isWarningPeriod = (pwdChangeLimitDate - pwdChangeWarnDate).Days <= this.ExpirePolicyItem.WarningTerm;

                if (noLoginIsLong || noPwdChangeIsLong)
                {
                    this.SecurityService = BizServiceLocator.GetService<ISecurityService>(SysObjectSpec.SYS_OBJECT_SECURITY_SERVICE);
                    this.SecurityService.UpdateStaffAsExpired(new BaseSecurityParam(this) { UserID = this.UserId });

                    //MSG:The password is expired. You should ask the administrator.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00086", null);
                }

                if (this.CanChangePassword && isWarningPeriod)
                {
                    int remainDays = (pwdChangeLimitDate - pwdChangeWarnDate).Days + 1;
                    //MSG:Your password will be expired after {0} days. Would you change your password?	
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00090", DefaultMessage.NON_REG_WRD + Convert.ToString(remainDays));
                }

                isNotExpired = true;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return isNotExpired;
        }
        #endregion


        #region METHOD AREA (RETRIEVE AUTHORIZATION INFO)*******
        /// <summary>
        /// Retrieves and sets user's authorization information for this module
        /// Return true if app access  information query is successful.
        /// </summary>
        /// <returns>true if authorization information query is successful</returns>
        private bool InquiryAuthorInfo()
        {
            BaseResult baseResult = null;
            AuthorInfoItemList authorList = null;
            bool isSuccess = false;

            try
            {
                this.SecurityService = BizServiceLocator.GetService<ISecurityService>(SysObjectSpec.SYS_OBJECT_SECURITY_SERVICE);
                baseResult = this.SecurityService.InquiryOperationAuthorInfoList(new BaseSecurityParam(this) { UserID = this.UserId }, this.UserInfo);

                if (baseResult.ResultType != ResultType.OK || baseResult.ResultObject == null)
                {
                    //MSG:[{0}] data retrieving failed. Contact your system administrator.
                    throw new TsbSysSecurityException(this.ObjectID, "MSG_FTCO_00127", DefaultMessage.NON_REG_WRD + "APP AUTHORIZATION");
                }
                else
                {
                    authorList = baseResult.ResultObject as AuthorInfoItemList;

                    if (authorList == null || authorList.RawList == null)
                    {
                        authorList = new AuthorInfoItemList();
                        authorList.RawList = new BaseItemsList<AuthorInfoItem>();
                    }

                    this.OperationAuthorInfoList = authorList.RawList;
                    isSuccess = true;
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return isSuccess;
        }
        #endregion


        #region METHOD AREA (UPDATE LOGIN SUCCESS)**************
        /// <summary>
        /// Update Staff after login success
        /// </summary>
        /// <returns>true if user is updating operation is successful</returns>
        private bool UpdateLoginSuccess()
        {
            bool isSuccess = false;

            try
            {
                this.SecurityService = BizServiceLocator.GetService<ISecurityService>(SysObjectSpec.SYS_OBJECT_SECURITY_SERVICE);
                this.SecurityService.UpdateLoginSuccess(new BaseSecurityParam(this) { UserID = this.UserId });

                isSuccess = true;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error while security check. Contact your system administrator. System error message is [{0}]	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00082", DefaultMessage.NON_REG_WRD + ex.Message);
            }

            return isSuccess;
        }
        #endregion




        
    }
}
