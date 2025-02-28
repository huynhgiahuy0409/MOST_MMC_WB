using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Constant
{
    public class ConfigConstant
    {
        public const string XML_ELE_XMLCONFIG_DEFAULT_SECTION = "section";
        public const string XML_ELE_XMLCONFIG_DEFAULT_SETTING = "setting";
        public const string XML_ATT_XMLCONFIG_DEFAULT_NAME    = "name";
        
        public const string XML_ELE_APPCONFIG_DEFAULT_SECTION = "appSettings";
        public const string XML_ELE_APPCONFIG_DEFAULT_SETTING = "add";
        public const string XML_ATT_APPCONFIG_DEFAULT_KEY     = "key";
        public const string XML_ATT_APPCONFIG_DEFAULT_VALUE   = "value";

        public const string XML_ELE_SQLMAP_PROPERTIES         = "properties";
        public const string XML_ATT_SQLMAP_CONNECT_STRING     = "connectionString";

        public const string APPCONFIG_KEY_PROD_ID              = "ModuleInfo.ProductID";
        public const string APPCONFIG_KEY_PROD_NAME            = "ModuleInfo.ProductName";
        public const string APPCONFIG_KEY_MODULE_ID            = "ModuleInfo.ModuleID";
        public const string APPCONFIG_KEY_MODULE_NAME          = "ModuleInfo.ModuleName";
        public const string APPCONFIG_KEY_MODULE_TITLE         = "ModuleInfo.ModuleTitle";
        public const string APPCONFIG_KEY_PGM_CODE             = "ModuleInfo.PgmCode";
        public const string APPCONFIG_KEY_CHECK_CONFIG_DIR     = "ModuleInfo.CheckConfigDir";

        public const string APPCONFIG_KEY_DEPLOY_MODE      = "DeployInfo.DeployMode";

        public const string APPCONFIG_KEY_PATH_ENVIRONMENT    = "Path.Environment";
        public const string APPCONFIG_KEY_PATH_BIZCONFIG      = "Path.BizConfig";
        public const string APPCONFIG_KEY_PATH_MESSAGE_SCHEMA = "Path.MessageSchema";
        public const string APPCONFIG_KEY_PATH_PERSISTENCE    = "Path.Persistence";
        public const string APPCONFIG_KEY_PATH_CONTEXT        = "Path.Context";
        public const string APPCONFIG_KEY_PATH_GRID           = "Path.Grid";
        public const string APPCONFIG_KEY_PATH_LOG            = "Path.Log";
        public const string APPCONFIG_KEY_PATH_LIB            = "Path.Lib";
        public const string APPCONFIG_KEY_PATH_HELP           = "Path.Help";
        public const string APPCONFIG_KEY_PATH_REPORT         = "Path.Report";
        //public const string APPCONFIG_KEY_PATH_WEB_REST       = "Path.Web.Rest";
        
        public const string APPCONFIG_KEY_PATH_GRID_SUB_CELLSTYLES_FONTOS = "Path.Grid.Sub.CellStyles.Fontos";
        public const string APPCONFIG_KEY_PATH_GRID_SUB_CELLSTYLES_TSBSDK = "Path.Grid.Sub.CellStyles.TsbSdk";

        public const string APPCONFIG_KEY_FILE_LOCALIZATION_INFO = "File.LocalizationInfo";
        public const string APPCONFIG_KEY_FILE_MODULE_INFO       = "File.ModuleInfo";
        public const string APPCONFIG_KEY_FILE_ARCHITECTURE_INFO = "File.ArchitectureInfo";
        public const string APPCONFIG_KEY_FILE_PERSISTENCE_INFO  = "File.PersistenceInfo";
        public const string APPCONFIG_KEY_FILE_SECPOLICY_INFO    = "File.SecurityPolicyInfo";
        public const string APPCONFIG_KEY_FILE_VERSION_INFO      = "File.Version";
        public const string APPCONFIG_KEY_FILE_SITE_INFO         = "File.SiteInfo";
        public const string APPCONFIG_KEY_FILE_RUNENV_INFO       = "File.RunEnvInfo";
        public const string APPCONFIG_KEY_FILE_MENU_ITEM         = "File.MenuItem";
        public const string APPCONFIG_KEY_FILE_SHORTCUTKEY_INFO  = "File.ShortcutKeyInfo";
        public const string APPCONFIG_KEY_FILE_CONTEXT_MENU_ITEM = "File.ContextMenuItem";
        public const string APPCONFIG_KEY_FILE_DIAGNOSTICS_INFO  = "File.DiagnosticsInfo";
        public const string APPCONFIG_KEY_FILE_MSGPOLICY_INFO    = "File.MessagePolicyInfo";
        public const string APPCONFIG_KEY_FILE_HELP_CHM          = "File.HelpFile";
        public const string APPCONFIG_KEY_FILE_SQLMAP_CONFIG     = "File.SqlMapConfig";
        public const string APPCONFIG_KEY_FILE_ADDITIONAL_SQLMAP_CONFIGS     = "File.AdditionalSqlMapConfigs";
        public const string APPCONFIG_KEY_FILE_UISTYLE_INFO      = "File.UIStyleInfo";
        public const string APPCONFIG_KEY_FILE_CUSTOM_UISTYLE_INFO = "File.CustomUIStyleInfo";
        public const string APPCONFIG_KEY_FILE_GRIDPOLICY_INFO = "File.GridPolicyInfo";
        public const string APPCONFIG_KEY_FILE_WEB_REST_SERVER = "File.Web.RestServer";
        public const string APPCONFIG_KEY_FILE_LOGPOLICY_INFO = "File.LogPolicyInfo";

        public const string SERVER_ROLE_MAIN_DATABASE = "MAIN_DATABASE";
        public const string SERVER_ROLE_MAIN_APP      = "MAIN_APP";
        public const string SERVER_ROLE_MAIN_WEB      = "MAIN_WEB";
        public const string SERVER_ROLE_MAIN_WAS      = "MAIN_WAS";
        public const string SERVER_ROLE_MAIN_MESSAGEQ = "MAIN_MESSAGE_QUEUE";
        public const string SERVER_ROLE_MAIN_DOMAIN   = "MAIN_DOMAIN";
        public const string SERVER_ROLE_MAIN_DNS      = "MAIN_DNS";
        public const string SERVER_ROLE_MAIN_MAIL     = "MAIN_MAIL";
        public const string SERVER_ROLE_MAIN_BPM      = "MAIN_BPM";
        public const string SERVER_ROLE_MAIN_ETC      = "MAIN_ETC";

        public const string SERVER_ROLE_USER_DATABASE = "USER_DATABASE";

        public const string PRODUCT_NAME_DB_ORACLE = "ORACLE";
        public const string PRODUCT_NAME_DB_MSSQL  = "MSSQL";
        public const string PRODUCT_NAME_DB_OLEDB = "OLEDB";
        public const string PRODUCT_NAME_DB_POSTGRE = "POSTGRE";

        public const string ENV_VAR_PATH        = "PATH";
        public const string ENV_VAR_ORACLE_HOME = "ORACLE_HOME";

		public const string APPCONFIG_KEY_ENV_USE_CATOS_CONFIG         = "UseCatosConfig";
        public const string APPCONFIG_KEY_ENV_USE_MULTI_DATASOURCE     = "UseMultiDataSource";
        public const string APPCONFIG_KEY_ENV_PRINT_PREVIEW_SHOW_MODAL = "PrintPreviewShowModal";
        public const string APPCONFIG_KEY_ENV_REPORT_PREVIEW_SHOW_MODAL = "ReportPreviewShowModal";
        public const string APPCONFIG_KEY_ENV_USE_WINDOW_POSITION      = "UseWindowPosition";
        public const string APPCONFIG_KEY_ENV_USE_WINDOW_SIZE          = "UseWindowSize";
        public const string APPCONFIG_KEY_ENV_USE_PERFORMANCE_LOG = "UsePerformanceLog";
        public const string APPCONFIG_KEY_ENV_CONTAINER_EDITOR_RULESET_REF_MODE = "ContainerEditorRuleRefMode";
        public const string APPCONFIG_KEY_ENV_RAISE_MSG_MNG_EXCEPTION = "RaiseMsgMngException";

        public const string APPCONFIG_KEY_UITEST_MAIN_WINDOW_TITLE = "UITest.MainWindowTitle";

        public const string APPCONFIG_KEY_ENV_USE_FORM_STATE_SERIALIZATION = "UseFormStateSerialization";

        public const string APPCONFIG_KEY_ENV_USAGE_DATA_COLLECTION = "UsageDataCollection";

        public const string PGM_CODE_HOME = "HO";
    }
}
