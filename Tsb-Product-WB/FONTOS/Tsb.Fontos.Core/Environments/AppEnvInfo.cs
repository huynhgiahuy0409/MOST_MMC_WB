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
* DATE           AUTHOR		  REVISION    	
* 2023.01.30     LIM JC 1.0	  First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Applied Commom Environment information class
    /// </summary>
    [Serializable]
    public class AppEnvInfo : BaseEnvironmentInfo
    {
        public const string FILE_NAME_APP_ENV_INFO = "AppEnvInfo.xml";
        public const string XML_ATT_VALUE_SECURITY_POLICY = "SecurityPolicy";
        public const string XML_ATT_VALUE_USEEXPIRE_DATE = "UseExpireDate";
        public const string XML_ATT_VALUE_USE_AUTO_LOGOUT_INTERVAL = "UseAutoLogoutInterval";

        private bool? _useExpireDate = null;
        private bool? _useAutoLogoutInterval = null;
        private static AppEnvInfo _instance = null;

        public bool? UseExpireDate
        {
            get { return _useExpireDate; }
        }

        public bool? UseAutoLogoutInterval
        {
            get { return _useAutoLogoutInterval; }
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        private AppEnvInfo() : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-AppInfo";
        }

        /// <summary>
        /// Gets Architecture information object reference.
        /// </summary>
        /// <returns>Arrchitecture information object reference</returns>
        public static AppEnvInfo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AppEnvInfo();
                _instance.LoadEnvironmentInfo(Path.Combine(AppPathInfo.PATH_ROOT_ENVIRONMENT, FILE_NAME_APP_ENV_INFO));
            }

            return _instance;
        }
        public void LoadEnvironmentInfo(string path)
        {
            string useExpireDate = string.Empty;
            string useAutoLogoutInterval = string.Empty;

            XmlConfigProvider configProvider = null;

            try
            {
                this._configFileName = FILE_NAME_APP_ENV_INFO;
                this._sourcePath = path;//Path.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, FILE_NAME_APP_ENV_INFO);
            }
            catch (System.TypeInitializationException initEx)
            {
                if (initEx.InnerException is TsbBaseException)
                {
                    TsbBaseException tsbEx = initEx.InnerException as TsbBaseException;
                    ExceptionHandler.Replace(initEx, initEx.InnerException.GetType(), tsbEx.SourceObjectID, tsbEx.MsgCode, tsbEx.MsgArgs);
                }
                else
                {
                    //MSG:An error occurred when checking the configuration path
                    ExceptionHandler.Wrap(initEx, typeof(TsbSysConfigException), this.ObjectID, "MSG_FTCO_00005", null);
                }
            }
            if (string.IsNullOrEmpty(this._sourcePath))
            {
                //MSG:{0} does not exist. Please check {1}.
                throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00121",
                     this._sourcePath,
                    "WRD_FTCO_thisfile"
                    );
            }

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this._sourcePath);

                try
                {
                    useExpireDate = this.GetValidValue(ref configProvider, XML_ATT_VALUE_SECURITY_POLICY, XML_ATT_VALUE_USEEXPIRE_DATE, false);
                    if (String.IsNullOrEmpty(useExpireDate) == false)
                    {
                        _useExpireDate = Convert.ToBoolean(useExpireDate);
                    }
                    else
                    {
                        _useExpireDate = null;
                    }
                }
                catch (Exception ex)
                {
                    _useExpireDate = null;
                    GeneralLogger.Error(ex);
                }

                try
                {
                    useAutoLogoutInterval = this.GetValidValue(ref configProvider, XML_ATT_VALUE_SECURITY_POLICY, XML_ATT_VALUE_USE_AUTO_LOGOUT_INTERVAL, false);
                    if (String.IsNullOrEmpty(useAutoLogoutInterval) == false)
                    {
                        _useAutoLogoutInterval = Convert.ToBoolean(useAutoLogoutInterval);
                    }
                    else
                    {
                        _useAutoLogoutInterval = null;
                    }

                }
                catch (Exception ex)
                {
                    _useAutoLogoutInterval = null;
                    GeneralLogger.Error(ex);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }
    }
}
