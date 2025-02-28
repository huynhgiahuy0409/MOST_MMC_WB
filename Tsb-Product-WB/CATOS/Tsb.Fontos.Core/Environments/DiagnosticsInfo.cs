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
* DATE           AUTHOR		       REVISION    	
* 2012.05.08  Tonny.Kim 1.0	    First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Exceptions.System;
using System.IO;
using Tsb.Fontos.Core.Util.Diagnostics.Type;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Message Policy Information class
    /// </summary>
    [Serializable]
    public class DiagnosticsInfo : BaseEnvironmentInfo
    {
        #region READONLY AREA **********************************
        private readonly char CHAR_COMMA = ',';
        #endregion

        #region FIELD/PROPERTY AREA*****************************
        private PerformanceLevel _logLevel = PerformanceLevel.All;
        private OutputType _outPut = OutputType.LOG;
        private ResultFormatType _perforamanceTimeFormat = ResultFormatType.MILLI_SECOND;
        private PerformanceResultFormatType _performanceResultFormat = PerformanceResultFormatType.ShortName;
        private int _maxSavedPerformanceCount = 0;
        private string[] _skipTargetTypeNames = null;
        private bool _showUsedMemory = false;

        private static DiagnosticsInfo _instance = null;

        public const string XML_ATT_VALUE_SKIP_TARGET_TYPE_NAME = "SkipTagetTypeName";
        public const string XML_ATT_VALUE_OUTPUT                = "OutPut";
        public const string XML_ATT_VALUE_TIME_FORMAT           = "TimeFormat";
        public const string XML_ATT_VALUE_RESULT_FORMAT         = "ResultFormat";
        public const string XML_ATT_VALUE_LEVEL                 = "Level";
        public const string XML_ATT_VALUE_PERFORMANCE_LOG       = "PERFORMANCE_LOG";
        public const string XML_ATT_VALUE_MAX_PERFORMANCE_COUNT = "MaxSavedPerformanceCount";
        public const string XML_ATT_VALUE_SHOW_USED_MEMORY      = "ShowUsedMemory";

        /// <summary>
        /// Performance Log Level
        /// </summary>
        public PerformanceLevel LogLevel { get { return this._logLevel; } }

        /// <summary>
        /// Performance Time Format
        /// </summary>
        public ResultFormatType PerformanceTimeFormat { get { return this._perforamanceTimeFormat; } }

        /// <summary>
        /// Performance Result Format
        /// </summary>
        public PerformanceResultFormatType PerformanceResultFormat { get { return this._performanceResultFormat; } }

        /// <summary>
        /// Performance Output Type
        /// </summary>
        public OutputType OutPut { get { return this._outPut; } }

        /// <summary>
        /// Maximum equal to performance information
        /// </summary>
        public int MaxSavedPerformanceCount { get { return this._maxSavedPerformanceCount; } }

        /// <summary>
        /// Skip Target Type Names
        /// </summary>
        public string[] SkipTargetTypeNames { get { return this._skipTargetTypeNames; } }

        /// <summary>
        /// Show Used Memory
        /// </summary>
        public bool ShowUsedMemory { get { return this._showUsedMemory; } }

        public DateTime StartTimeOfLoginCheck { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        private DiagnosticsInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-DiagnosticsInfo";
        }

        /// <summary>
        /// Gets Message Policy information object reference.
        /// </summary>
        /// <returns>Message Policy information object reference</returns>
        public static DiagnosticsInfo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DiagnosticsInfo();
                _instance.LoadDiagnosticsInfo();
            }

            return _instance;
        }
        #endregion

        #region METHOD AREA (LOAD INFORMATION)******************
        /// <summary>
        /// Load Diagnostics info
        /// </summary>
        public void LoadDiagnosticsInfo()
        {
            string logLevelValue = string.Empty;
            string perforamanceResultFormatValue = string.Empty;
            string perforamanceTimeFormatValue = string.Empty;
            string outPutValue = string.Empty;
            string countValue = string.Empty;
            string skipTargetTypeNamesValue = string.Empty;
            string showUsedMemoryValue = string.Empty;

            XmlConfigProvider configProvider = null;

            try
            {
                if (string.IsNullOrEmpty(AppPathInfo.FILE_NAME_DIAGNOSTICS_INFO)) return;

                this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, AppPathInfo.FILE_NAME_DIAGNOSTICS_INFO);
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

                logLevelValue = this.GetValidValue(ref configProvider, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_LEVEL);
                this._logLevel = this.GetValidType<PerformanceLevel>(logLevelValue, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_LEVEL);

                perforamanceTimeFormatValue = this.GetValidValue(ref configProvider, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_TIME_FORMAT);
                this._perforamanceTimeFormat = this.GetValidType<ResultFormatType>(perforamanceTimeFormatValue, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_TIME_FORMAT);

                perforamanceResultFormatValue = this.GetValidValue(ref configProvider, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_RESULT_FORMAT);
                this._performanceResultFormat = this.GetValidType<PerformanceResultFormatType>(perforamanceResultFormatValue, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_RESULT_FORMAT);

                outPutValue = this.GetValidValue(ref configProvider, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_OUTPUT);
                this._outPut = this.GetValidType<OutputType>(outPutValue, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_OUTPUT);

                countValue = this.GetValidValueNotException(ref configProvider, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_MAX_PERFORMANCE_COUNT);

                if (!string.IsNullOrEmpty(countValue))
                {
                    this._maxSavedPerformanceCount = int.Parse(countValue);
                }

                skipTargetTypeNamesValue = this.GetValidValueNotException(ref configProvider, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_SKIP_TARGET_TYPE_NAME);

                if (!string.IsNullOrEmpty(skipTargetTypeNamesValue))
                {
                    this._skipTargetTypeNames = skipTargetTypeNamesValue.Split(this.CHAR_COMMA);

                    for (int i=0; i<this._skipTargetTypeNames.Count(); i++)
                    {
                        this._skipTargetTypeNames[i] = this._skipTargetTypeNames[i].Trim();
                    }
                }

                showUsedMemoryValue = this.GetValidValueNotException(ref configProvider, DiagnosticsInfo.XML_ATT_VALUE_PERFORMANCE_LOG, DiagnosticsInfo.XML_ATT_VALUE_SHOW_USED_MEMORY);

                if (!string.IsNullOrEmpty(showUsedMemoryValue))
                {
                    this._showUsedMemory = Boolean.Parse(showUsedMemoryValue);
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        public bool IsNetworkConnected()
        {
            string dns = "dns.msftncsi.com";
            string ipAdress = "131.107.255.255";

            try
            {
                // Check NCSI DNS IP
                var dnsHost = System.Net.Dns.GetHostEntry(dns);
                if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != ipAdress)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
