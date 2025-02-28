#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2016 TOTAL SOFT BANK LIMITED. All Rights
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
* 2016.09.21    JIndols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Configuration.Provider;
using System.IO;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Message;
using System.Windows.Forms;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Message Policy Information class
    /// </summary>
    [Serializable]
    public class LogPolicyInfo : BaseEnvironmentInfo
    {
        #region FIELD/PROPERTY AREA**********************************
        private static LogPolicyInfo _instance = null;

        public const string DEFAULT_FILE_NAME = "LogPolicyInfo.xml";

        public const string XML_ATT_VALUE_BASE_INFO = "BASE_INFO";
        public const string XML_ATT_VALUE_LogFolder = "LogFolder";
        public const string XML_ATT_VALUE_LogFileSearchKey = "LogFileSearchKey";

        public const string XML_ATT_VALUE_DELETE_LOG_FILE = "DELETE_LOG_FILE";
        public const string XML_ATT_VALUE_ENABLE = "Enable";
        public const string XML_ATT_VALUE_CheckStyle = "CheckStyle";
        public const string XML_ATT_VALUE_CheckNumber = "CheckNumber";


        public bool IsExistLogPolicyInfoFile { get; private set; }

        public string LogFolder { get; private set; }
        public string LogFileSearchKey { get; private set; }

        public bool DeleteEnable { get; private set; }
        public DeleteCheckStyle DeleteCheckType { get; private set; }
        public int DeleteCheckNumber { get; private set; }



        #endregion

        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        private LogPolicyInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-GridPolicyInfo";
            this.DeleteCheckType = DeleteCheckStyle.Day;
            this.DeleteEnable = false;
            this.IsExistLogPolicyInfoFile = false;
        }

        /// <summary>
        /// Gets Grid Policy information object reference.
        /// </summary>
        /// <returns>Grid Policy information object reference</returns>
        public static LogPolicyInfo GetInstance()
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new LogPolicyInfo();
                    _instance.DeleteCheckType = DeleteCheckStyle.Day;
                    _instance.LoadLogPolicyInfo();
                }
            }
            catch (TsbBaseException tex)
            {
                MessageBox.Show(MessageBuilder.BuildMessage(tex));
                GeneralLogger.Error(tex);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
            return _instance;
        }
        #endregion

        #region METHOD AREA (LOAD INFORMATION)******************
        /// <summary>
        /// Load grid policy info
        /// </summary>
        public void LoadLogPolicyInfo()
        {
             XmlConfigProvider configProvider = null;

            try
            {                
                if (String.IsNullOrEmpty(AppPathInfo.FILE_NAME_LOGPOLICY_INFO))
                {
                    //string path = AppPathInfo.PATH_APP_USER_ENVIRONMENT;

                    //If App.config file do not exist file name about grid policy information.
                    //this._sourcePath = Path.Combine(AppPathInfo.PATH_ROOT_ENVIRONMENT, LogPolicyInfo.DEFAULT_FILE_NAME);

                    //

                    //사용자 모듈 기준으로 설정 파일이 존재하는 확인 : Environment/{모듈폴더}/LogPolicyInfo.xml
                    this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_USER_ENVIRONMENT, LogPolicyInfo.DEFAULT_FILE_NAME);

                    if (File.Exists(this._sourcePath) == false)
                    {
                        this.IsExistLogPolicyInfoFile = false;
                        return;

                        /*
                        //사용자 모듈 기준으로 설정 파일이 존재하는 확인 : Environment/LogPolicyInfo.xml
                        this._sourcePath = Path.Combine(AppPathInfo.PATH_ROOT_ENVIRONMENT, LogPolicyInfo.DEFAULT_FILE_NAME);

                        if (File.Exists(this._sourcePath) == false) 
                        {
                            this.IsExistLogPolicyInfoFile = false;
                            return;
                        }
                        */
                    }



                }
                else
                {
                    if(string.IsNullOrEmpty(AppPathInfo.FILE_NAME_LOGPOLICY_INFO) == true)
                    {
                        this.IsExistLogPolicyInfoFile = false;
                        return;
                    }

                    //If App.config file do exist file name about grid policy information.
                    //this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, AppPathInfo.FILE_NAME_LOGPOLICY_INFO);

                    //사용자 모듈 기준으로 설정 파일이 존재하는 확인 : Environment/{모듈폴더}/{모듈에서 지정한 log policy xml file}
                    this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_USER_ENVIRONMENT, AppPathInfo.FILE_NAME_LOGPOLICY_INFO);

                    if (File.Exists(this._sourcePath) == false)
                    {
                        this.IsExistLogPolicyInfoFile = false;
                        
                        //MSG:{0} does not exist. Please check {1}.
                        throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00121",
                             DefaultMessage.NON_REG_WRD + this._sourcePath,
                            "WRD_FTCO_thisfile"
                            );
                    }
                }

                
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

            try
            {
                configProvider = (XmlConfigProvider)ConfigContext.GetXmlConfigProvider(this._sourcePath);

                //Sets the sorting type.
                this.LogFolder = this.GetValidValueNotException(ref configProvider, LogPolicyInfo.XML_ATT_VALUE_BASE_INFO, LogPolicyInfo.XML_ATT_VALUE_LogFolder);
                this.LogFileSearchKey = this.GetValidValueNotException(ref configProvider, LogPolicyInfo.XML_ATT_VALUE_BASE_INFO, LogPolicyInfo.XML_ATT_VALUE_LogFileSearchKey);

                string enableValue = this.GetValidValueNotException(ref configProvider, LogPolicyInfo.XML_ATT_VALUE_DELETE_LOG_FILE, LogPolicyInfo.XML_ATT_VALUE_ENABLE);
                this.DeleteEnable = false;

                //if (string.IsNullOrWhiteSpace(enableValue) ==  false)
                if (string.IsNullOrEmpty(enableValue) == false)
                {
                    if(enableValue.Trim().ToUpper().Equals("TRUE") == true)
                    {
                        this.DeleteEnable = true;
                    }
                }

                // if (string.IsNullOrEmpty(this.LogFolder) == true || string.IsNullOrEmpty(LogFolder.Trim()) == true){

                string checkStyleValue = this.GetValidValueNotException(ref configProvider, LogPolicyInfo.XML_ATT_VALUE_DELETE_LOG_FILE, LogPolicyInfo.XML_ATT_VALUE_CheckStyle);

                if (checkStyleValue != string.Empty)
                {
                    this.DeleteCheckType = this.GetValidType<DeleteCheckStyle>(checkStyleValue, LogPolicyInfo.XML_ATT_VALUE_DELETE_LOG_FILE, LogPolicyInfo.XML_ATT_VALUE_CheckStyle);
                }

                string checkNumberValue = this.GetValidValueNotException(ref configProvider, LogPolicyInfo.XML_ATT_VALUE_DELETE_LOG_FILE, LogPolicyInfo.XML_ATT_VALUE_CheckNumber);
                int tempcheckNumber = -1;
                int.TryParse(checkNumberValue, out tempcheckNumber);
                this.DeleteCheckNumber = tempcheckNumber;

                //if (string.IsNullOrWhiteSpace(this.LogFolder) == true){
                if (string.IsNullOrEmpty(this.LogFolder) == true || string.IsNullOrEmpty(this.LogFolder.Trim()) == true)
                {
                    this.IsExistLogPolicyInfoFile = false;

                    //MSG:Inputed {0} value({1}) is not supported.
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00124",
                            DefaultMessage.NON_REG_WRD + "LogFolder at LogPolicyInfo.xml",
                            DefaultMessage.NON_REG_WRD + " This value must be entered.."
                            );
                }


                    //if (string.IsNullOrWhiteSpace(this.LogFileSearchKey) == true)
                if (string.IsNullOrEmpty(this.LogFileSearchKey) == true || string.IsNullOrEmpty(this.LogFileSearchKey.Trim()) == true)
                {
                    this.IsExistLogPolicyInfoFile = false;

                    //MSG:Inputed {0} value({1}) is not supported.
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00124",
                            DefaultMessage.NON_REG_WRD + "LogFileSearchKey at LogPolicyInfo.xml",
                            DefaultMessage.NON_REG_WRD + " This value must be entered.."
                            );
                }

                if (tempcheckNumber <= 0)
                {
                    this.DeleteCheckNumber = 1;
                    this.IsExistLogPolicyInfoFile = false;

                    //MSG:Inputed {0} value({1}) is not supported.
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00124",
                            DefaultMessage.NON_REG_WRD + "DeleteCheckNumber at LogPolicyInfo.xml",
                           DefaultMessage.NON_REG_WRD + tempcheckNumber + " This value must be greater than 0."
                           );
                }

                this.IsExistLogPolicyInfoFile = true;
            }
            catch (TsbBaseException tsbEx)
            {
                this.IsExistLogPolicyInfoFile = false;
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        #endregion
    }

    public enum DeleteCheckStyle
    {
        Day,
        File
    }

}
