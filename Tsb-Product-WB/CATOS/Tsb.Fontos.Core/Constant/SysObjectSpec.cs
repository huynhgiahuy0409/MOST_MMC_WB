using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Service;

namespace Tsb.Fontos.Core.Constant
{
    public class SysObjectSpec : BaseServiceSpec
    {
        public readonly static String SYS_OBJECT_ID_APP_INITILIZER                  = "SYS_OBJECT_APP_INITIALIZER";
        public readonly static String SYS_OBJECT_ID_TEST_CONFIG_INIT_ADAPTER        = "SYS_OBJECT_TEST_CONFIG_INIT_ADAPTER";
        public readonly static String SYS_OBJECT_ID_LOGIN_VIEW                      = "SYS_OBJECT_LOGIN_VIEW";
        public readonly static String SYS_OBJECT_ID_APP_AUTHEN_HANDLER              = "SYS_OBJECT_APP_AUTHEN_HANDLER";
        public readonly static String SYS_OBJECT_ID_APP_AUTHEN_MSG_SERVER_HANDLER   = "SYS_OBJECT_APP_AUTHEN_MSG_SERVER_HANDLER";
        public readonly static String SYS_OBJECT_ID_APP_AUTHEN_LDAP_SERVER_HANDLER  = "SYS_OBJECT_APP_AUTHEN_LDAP_SERVER_HANDLER";
        public readonly static String SYS_OBJECT_ID_APP_AUTHEN_SSO_HANDLER          = "SYS_OBJECT_APP_AUTHEN_SSO_HANDLER";
        public readonly static String SYS_OBJECT_DB_MONITOR_SERVICE                 = "SYS_OBJECT_DB_MONITOR_SERVICE";
        public readonly static String SYS_OBJECT_SECURITY_SERVICE                   = "SYS_OBJECT_SECURITY_SERVICE";
        public readonly static String SYS_OBJECT_LICENSE_MANAGER                    = "SYS_OBJECT_LICENSE_MANAGER";

        public readonly static String SYS_OBJECT_ID_VIRTUAL_KEYBOARD_MANAGER = "SYS_OBJECT_ID_VIRTUAL_KEYBOARD_MANAGER";
        public readonly static String SYS_OBJECT_ID_VIRTUAL_NUMERIC_KEYBOARD_MANAGER = "SYS_OBJECT_ID_VIRTUAL_NUMERIC_KEYBOARD_MANAGER";

        public readonly static String SYS_OBJECT_ID_MESSAGE_DISPLAY_STYLE = "SYS_OBJECT_ID_MESSAGE_DISPLAY_STYLE";
        public readonly static String SYS_OBJECT_ID_PROGRESSBAR_DISPLAY_STYLE = "SYS_OBJECT_ID_PROGRESSBAR_DISPLAY_STYLE";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SysObjectSpec()
            : base()
        {
        }
    }
}
