using System;
using System.Collections.Generic;
using System.Linq;
using Tsb.Catos.Cm.Core.Configuration.Client;
using Tsb.Catos.Cm.Core.Constants;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Context;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.Business;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Security.Authentication;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Security.SSO;
using Tsb.Fontos.Core.Validator;
using Tsb.Fontos.Core.Validator.IO;
using Tsb.Fontos.Win.Init;
using Tsb.Fontos.Win.Message;
using Tsb.Fontos.Win.Security.Login;

namespace Tsb.Catos.Cm.Mobile.Common.Handler
{
    public class LoginHandler : TsbBaseObject
    {
        private static string ObjectId = "CTL-CT-CTMO-LoginHandler";

        public BaseAppInitializer AppInitializer { get; set; }

        public BaseAuthenHandler AuthenHandler { get; set; }
        public bool IsLoginView { get; set; }
        public bool VisibleLogo { get; set; }

        public LoginHandler()
        {
            try
            {
                AuthenHandler = null;
                Initialize();
            }
            catch (TsbBaseException tsbEx)
            {
                MessageManager.Show(tsbEx);
            }
            catch (Exception ex)
            {
                //MSG : An unexpected error occurred. An internal system error message is [{0}]. Please, contact your administrator.
                MessageManager.Show(new TsbBaseException(ex, this.ObjectID, "MSG-FTCO-99998", DefaultMessage.NON_REG_WRD + ex.Message));
            }
        }

        private ILoginView GetLoginView()
        {
            ILoginView loginView = null;

            try
            {
                loginView = (ILoginView)ObjectBuilder.GetObjectBuilder().GetObject(SysObjectSpec.SYS_OBJECT_ID_LOGIN_VIEW);
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
            return loginView;
        }

        private void Initialize(bool useMsgServer = true)
        {
            try
            {
                // CTAppInitializer Class 의 BaseUserInfo StandardInitApp(bool useMsgServer, BaseAuthenHandler preAuthenHandler, BaseAuthenHandler postAuthenHandler, bool userDefineAuthen) 함수 복사해서 사용

                AppInitializer = (BaseAppInitializer)ObjectBuilder.GetObjectBuilder().GetObject(SysObjectSpec.SYS_OBJECT_ID_APP_INITILIZER);
                AppInitializer.VisibleLogo = VisibleLogo;

                BaseValidator validChain = new ValidatorChain();
                validChain += new FileExistValidator() { ExitOnError = true, TargetName = CTConfigConstant.FILE_DESC_CATOS_CONFIG, FileName = CTConfigConstant.FILE_NAME_CATOS_CONFIG, Path = AppPathInfo.PATH_APP_BASE };
                AppInitializer.PreInitConfigCheckChain = validChain as ValidatorChain;
                AppInitializer.InitApp();

                // Must write Code after function<InitApp()>
                Server server = SysServersInfo.GetServerInfo(ConfigConstant.SERVER_ROLE_MAIN_APP);


                BaseAuthenHandler passwordAuthenHandler =
                        (BaseAuthenHandler)ObjectBuilder.GetObjectBuilder().GetObject(SysObjectSpec.SYS_OBJECT_ID_APP_AUTHEN_HANDLER);

                passwordAuthenHandler.PrevLoginUserID = (AppInitializer.ConfigInfoImpl as CTConfigInfo).GetCatosConfigCommonValue(CTConfigConstant.CATOS_CONFIG_COMMON_KEY_AL_UID);
                passwordAuthenHandler.DoExpireCheck = true;

                BaseAuthenHandler msgAuthenHandler = null;

                if (useMsgServer)
                {
                    msgAuthenHandler =
                        (BaseAuthenHandler)ObjectBuilder.GetObjectBuilder().GetObject(SysObjectSpec.SYS_OBJECT_ID_APP_AUTHEN_MSG_SERVER_HANDLER);
                    msgAuthenHandler.PrevLoginUserID = passwordAuthenHandler.PrevLoginUserID;
                }

                BaseAuthenHandler ldapAuthenHandler = null;

                // LDAP Connect
                if (server.UseLDAP)
                {
                    ldapAuthenHandler =
                        (BaseAuthenHandler)ObjectBuilder.GetObjectBuilder().GetObject(SysObjectSpec.SYS_OBJECT_ID_APP_AUTHEN_LDAP_SERVER_HANDLER);
                    ldapAuthenHandler.PrevLoginUserID = passwordAuthenHandler.PrevLoginUserID;
                }

                if (useMsgServer &&
                    server.UseLDAP &&
                    msgAuthenHandler != null &&
                    ldapAuthenHandler != null)
                {
                    passwordAuthenHandler += msgAuthenHandler;
                    ldapAuthenHandler += passwordAuthenHandler;         // LDAP + Password + C3IT
                    AuthenHandler = ldapAuthenHandler;
                }
                else if (useMsgServer &&
                         msgAuthenHandler != null)
                {
                    msgAuthenHandler += passwordAuthenHandler;     // C3IT + Password
                    AuthenHandler = msgAuthenHandler;
                }
                else if (server.UseLDAP &&
                         ldapAuthenHandler != null)
                {
                    ldapAuthenHandler += passwordAuthenHandler;     // LDAP + Password
                    AuthenHandler = ldapAuthenHandler;
                }
                else
                {
                    AuthenHandler = passwordAuthenHandler;
                }

                IsLoginView = useMsgServer || server.UseLDAP ? true : false;
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, ObjectID);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Wrap(ex, typeof(TsbBizServiceException), ObjectID, "MSG_CTCM_00008", null);
            }
        }

        //public BaseUserInfo LoginProcess(List<string> userAccount = null)
        public BaseUserInfo LoginProcess()
        {
            BaseUserInfo userInfo = null;
            try
            {
                AppInitializer.OnLogInViewLoad(EventArgs.Empty);

                //userInfo = ShowLogin(userAccount);
                userInfo = ShowLogin();

                if (VisibleLogo) AppInitializer.StartLogoView.Close();
                AppInitializer.ProcessAfterLogin();
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
            return userInfo;
        }

        //public BaseUserInfo ShowLogin(List<string> userAccount = null)
        public BaseUserInfo ShowLogin()
        {
            BaseUserInfo userInfo = null;
            try
            {
                var loginView = GetLoginView();
                userInfo = loginView.ShowLoginView(AuthenHandler, IsLoginView);

                // added by YoungOk Kim (2018.12.24) - Mantis 88171: [모든단말기] 로그인시 Password 기본 저장 기능
                if (userInfo != null)
                {
                    var password = this.AuthenHandler.PasswordEncrypter.Decrypt(userInfo.EncrypedPassword, SecurityPolicyInfo.ENCRYPTION_KEY);
                    userInfo.Password = password;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHandler.ErrorLog(ex);
            }
            return userInfo;
        }
    }
}
