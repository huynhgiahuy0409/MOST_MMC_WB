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
* 2009.07.15    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Tsb.Fontos.Core.Configuration;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Configuration.Provider;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Transaction.Type;
using Tsb.Fontos.Core.Message.Type;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Net.Types;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Message Policy Information class
    /// </summary>
    [Serializable]
    public class MessagePolicyInfo  : BaseEnvironmentInfo
    {
        #region FIELD/PROPERTY AREA**********************************

        private static MessagePolicyInfo _instance = null;

        public const char COMMA_CHAR = ',';
        public const string XML_ATT_VALUE_MESSAGE_DISPLAY  = "MESSAGE_DISPLAY";
        public const string XML_ATT_VALUE_DIALOG           = "Dialog";
        public const string XML_ATT_VALUE_SKIP_MESSAGE_ID  = "SkipDisplayMessageID";
        public const string XML_ATT_VALUE_TIMEOUT = "Timeout";
        public const string XML_ATT_VALUE_EXCEPTION_REPORT = "EXCEPTION_REPORT";
        public const string XML_ATT_VALUE_EMAIL_PROTOCOL   = "EMailProtocol";
        public const string XML_ATT_VALUE_EMAIL_SERVER     = "EMaileServer";
        public const string XML_ATT_VALUE_SEND_REPORT      = "SendReport";
        public const string XML_ATT_VALUE_CONTACT_EMAIL    = "ContactEmail";

        private MsgDialogTypes _msgDialogtype;
        /// <summary>
        /// Gets Message Dialog Type
        /// </summary>
        public MsgDialogTypes MsgDialogType
        {
            get { return _msgDialogtype; }
        }

        private EmailProtocolType _eMailProtocolType;
        /// <summary>
        /// Gets Email Protocol
        /// </summary>
        public EmailProtocolType EMailProtocol
        {
            get { return _eMailProtocolType; }
        }

        private string _eMailServer;
         /// <summary>
        /// Gets EMail server address
        /// </summary>
        public string EMailServer
        {
            get { return _eMailServer; }
        }

        private string _sendReportYN;
        /// <summary>
        /// Gets true if sending report is required
        /// </summary>
        public bool DoSendReport
        {
            get { return ConvertUtil.ToBoolean(this._sendReportYN); }
        }

        private string _contactEmail;
        /// <summary>
        /// Gets Error Report contact eMail address
        /// </summary>
        public string ContactEmail
        {
            get { return _contactEmail; }
        }

        private string _skipMessageID;

        /// <summary>
        /// Gets Skip Message ID
        /// </summary>
        public string SkipMessageID
        {
            get { return _skipMessageID; }
        }

        private string[] skipMessageIDs;

        private int _timeout;

        /// <summary>
        /// Timeout for MessageManager.ShowWithTimeout Method<br/>
        /// Timeout unit is milisecond, 0 or empty is not set<br/>
        /// if you want to initialize this property, you should add 'Timeout' tag as 'MESSAGE_DISPLAY' tag's child tag in 'MessagePolicyInfo.xml' config file.
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
        }
        #endregion


        #region INITIALIZATION AREA *********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        private MessagePolicyInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-MessagePolicyInfo";
        }

        /// <summary>
        /// Gets Message Policy information object reference.
        /// </summary>
        /// <returns>Message Policy information object reference</returns>
        public static MessagePolicyInfo GetInstance()
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new MessagePolicyInfo();
                    _instance.LoadMessagePolicyInfo();
                }
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
        /// Load message policy info
        /// </summary>
        public void LoadMessagePolicyInfo()
        {
            string msgDiplayDlg  = string.Empty;
            string emailProtocol = string.Empty;
            string timeoutStr = string.Empty;

            XmlConfigProvider configProvider = null;

            try
            {
                this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, AppPathInfo.FILE_NAME_MSGPOLICY_INFO);
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

                msgDiplayDlg        = this.GetValidValue(ref configProvider, MessagePolicyInfo.XML_ATT_VALUE_MESSAGE_DISPLAY, MessagePolicyInfo.XML_ATT_VALUE_DIALOG);
                this._msgDialogtype = this.GetValidType<MsgDialogTypes>(msgDiplayDlg, MessagePolicyInfo.XML_ATT_VALUE_MESSAGE_DISPLAY, MessagePolicyInfo.XML_ATT_VALUE_DIALOG);

                this._skipMessageID = this.GetValidValueNotException(ref configProvider, MessagePolicyInfo.XML_ATT_VALUE_MESSAGE_DISPLAY, MessagePolicyInfo.XML_ATT_VALUE_SKIP_MESSAGE_ID);
                this.SetSkipMessageID(this._skipMessageID);

                timeoutStr = this.GetValidValueNotException(ref configProvider, MessagePolicyInfo.XML_ATT_VALUE_MESSAGE_DISPLAY, MessagePolicyInfo.XML_ATT_VALUE_TIMEOUT);

                if (string.IsNullOrEmpty(timeoutStr) == false)
                {
                    int result = 0;
                    int.TryParse(timeoutStr, out result);
                    _timeout = result;
                }

                emailProtocol       = this.GetValidValue(ref configProvider, MessagePolicyInfo.XML_ATT_VALUE_EXCEPTION_REPORT, MessagePolicyInfo.XML_ATT_VALUE_EMAIL_PROTOCOL);
                this._eMailProtocolType = this.GetValidType<EmailProtocolType>(emailProtocol, MessagePolicyInfo.XML_ATT_VALUE_EXCEPTION_REPORT, MessagePolicyInfo.XML_ATT_VALUE_EMAIL_PROTOCOL);

                this._eMailServer  = this.GetValidValue(ref configProvider, MessagePolicyInfo.XML_ATT_VALUE_EXCEPTION_REPORT, MessagePolicyInfo.XML_ATT_VALUE_EMAIL_SERVER);
                this._sendReportYN = this.GetValidValue(ref configProvider, MessagePolicyInfo.XML_ATT_VALUE_EXCEPTION_REPORT, MessagePolicyInfo.XML_ATT_VALUE_SEND_REPORT);
                this._contactEmail = this.GetValidValue(ref configProvider, MessagePolicyInfo.XML_ATT_VALUE_EXCEPTION_REPORT, MessagePolicyInfo.XML_ATT_VALUE_CONTACT_EMAIL);

            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        #endregion

        #region METHOD(Skip Message ID) AREA ************************
        /// <summary>
        /// Sets Skip Message ID
        /// </summary>
        /// <param name="skipMessageID"></param>
        private void SetSkipMessageID(string skipMessageID)
        {
            try
            {
                if (!string.IsNullOrEmpty(skipMessageID))
                {
                    skipMessageIDs = skipMessageID.Split(new char[] { MessagePolicyInfo.COMMA_CHAR });
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Exsit Skip Message ID
        /// </summary>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public bool ExistsSkipMessageID(string messageID)
        {
            try
            {
                if (skipMessageIDs != null)
                {
                    IEnumerable<string> messageIds = skipMessageIDs.Where<string>(w => w.Trim().Equals(messageID));
                    if (messageIds.Count() > 0) return true;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
            return false;
        }
        #endregion
    }
}
