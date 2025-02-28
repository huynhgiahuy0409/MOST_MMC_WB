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
* 2010.02.03    CHOI 1.0	First release.
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
using Tsb.Fontos.Core.Security.Type;
using Tsb.Fontos.Core.Logging;
using System.Text.RegularExpressions;
using Tsb.Fontos.Core.Validator;
using Tsb.Fontos.Core.Validator.UI;
using System.Diagnostics;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Security.Encryption;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Applied System Security Policy information class
    /// </summary>
    [Serializable]
    public class SecurityPolicyInfo  : BaseEnvironmentInfo
    {
        #region ENUM AREA **************************************
        public enum PasswordValidationResultTypes { NONE, MINIMUM, MAXIMUM, LOWER, UPPER, DIGIT, SPECIAL };
        #endregion

        #region CONST AREA *************************************
        private readonly ITsbLog log = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string XML_ATT_VALUE_AUTHENTICATION = "Authentication";
        public const string XML_ATT_VALUE_POLICY         = "Policy";
        public const string XML_ATT_VALUE_ENCRYPTION     = "Encryption";
        public const string XML_ATT_VALUE_AUTHORIZATION  = "Authorization";
        public const string XML_ATT_VALUE_LOGGER = "Logger";
        public const string XML_ATT_VALUE_INSERTPROGRAMACCESSLOG = "InsertProgramAccessLog";
        public const string XML_ATT_VALUE_LEVEL          = "Level";
        public const string XML_ATT_VALUE_PASSWORD_VALIDATION = "PasswordValidation";
        public const string XML_ATT_VALUE_MINIMUM_LENGTH = "MinimumLength";
        public const string XML_ATT_VALUE_USERID_CONVERSION = "UserIdConversion";
        public const string XML_ATT_VALUE_MAXIMUM_LENGTH = "MaximumLength";
        public const string XML_ATT_VALUE_DUPLICATION_CHK_COUNT = "DuplicationCheckCount";
        public const string XML_ATT_VALUE_LOWER_CREDIT   = "LowerCredit";
        public const string XML_ATT_VALUE_UPPER_CREDIT   = "UpperCredit";
        public const string XML_ATT_VALUE_DIGIT_CREDIT   = "DigitCredit";
        public const string XML_ATT_VALUE_SPECIAL_CREDIT = "SpecialCredit";
        public const string XML_ATT_VALUE_TEXT_CHAR_TYPE = "TextCharType";
        public const string ENCRYPTION_KEY               = "d;e^}$pfh2_-Qc>6(Y)X";
        #endregion

        #region FIELD AREA *************************************
        private AuthenPolicyTypes  _authenPolicy;
        private AuthenEncryptTypes _authenEncryptType;
        private AuthorLevelTypes   _authorLevel;
        private TextCharTypes _textChar;
        private BaseValidator _validChain;
        private bool _insertProgramAccessLog = true;
        private bool _isPasswordCheck = false;
        private int _minimumLength = -1;
        private int _maximumLength = -1;
        private int _duplicationCheckCount = -1;
        private int _lowerCredit = -1;
        private int _upperCredit = -1;
        private int _digitCredit = -1;
        private int _specialCredit = -1;
        private PasswordValidationResultTypes _passwordValidationResult = PasswordValidationResultTypes.NONE;

        private static SecurityPolicyInfo _instance = null;
        #endregion
        
        #region PROPERTY AREA **********************************
        public string CurrentUserID { get; set; }
        public IBaseEncrypter PasswordEncrypter { get; set; }

        /// <summary>
        /// Authentication Policy
        /// </summary>
        public AuthenPolicyTypes AuthenPolicy
        {
            get { return _authenPolicy; }
        }

        /// <summary>
        /// Authentication Encryption Type
        /// </summary>
        public AuthenEncryptTypes AuthenEncryptType
        {
            get { return _authenEncryptType; }
        }

        /// <summary>
        /// Authorization check level
        /// </summary>
        public AuthorLevelTypes AuthorLevel
        {
            get { return _authorLevel; }
        }

        /// <summary>
        /// Text Char
        /// </summary>
        public TextCharTypes TextChar
        {
            get { return _textChar; }
        }

        /// <summary>
        /// Whether password check
        /// </summary>
        public bool IsPasswordCheck
        {
            get { return _isPasswordCheck; }
        }
        
        /// <summary>
         /// Whether insert program access log.
         /// </summary>
        public bool InsertProgramAccessLog
        {
            get { return _insertProgramAccessLog; }
        }

        /// <summary>
        /// Minimum Length
        /// </summary>
        public int MinimumLength
        {
            get { return _minimumLength; }
        }

        /// <summary>
        /// Maximum Length
        /// </summary>
        public int MaximumLength
        {
            get { return _maximumLength; }
        }

        /// <summary>
        /// Duplication Check Count
        /// </summary>
        public int DuplicationCheckCount
        {
            get { return _duplicationCheckCount; }
        }

        /// <summary>
        /// Lower Credit
        /// </summary>
        public int LowerCredit
        {
            get { return _lowerCredit; }
        }

        /// <summary>
        /// Upper Credit
        /// </summary>
        public int UpperCredit
        {
            get { return _upperCredit; }
        }

        /// <summary>
        /// Digit Credit
        /// </summary>
        public int DigitCredit
        {
            get { return _digitCredit; }
        }

        /// <summary>
        /// Special Credit
        /// </summary>
        public int SpecialCredit
        {
            get { return _specialCredit; }
        }

        /// <summary>
        /// Password Validation Result
        /// </summary>
        public PasswordValidationResultTypes PasswordValidationResult
        {
            get { return _passwordValidationResult; }
        }
        #endregion

        #region INITIALIZE AREA ********************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        private SecurityPolicyInfo()
            : base()
        {
            this.ObjectID = "GNR-FTCO-ENV-SecurityPolicyInfo";
            this._validChain = new ValidatorChain();
        }

        /// <summary>
        /// Gets Architecture information object reference.
        /// </summary>
        /// <returns>Arrchitecture information object reference</returns>
        public static SecurityPolicyInfo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SecurityPolicyInfo();
                _instance.LoadEnvironmentInfo();
            }

            return _instance;
        }

        /// <summary>
        /// Load architecture info
        /// </summary>
        public void LoadEnvironmentInfo()
        {
            string textChar = string.Empty;
            string authenPolicy      = string.Empty;
            string authenEncryptType = string.Empty;
            string authorLevel       = string.Empty;
            string insertProgramAccessLog = string.Empty;
            string strMinimumLength     = string.Empty;
            string strMaximumLength     = string.Empty;
            string strDuplChkCount      = string.Empty;
            string strLowerCredit       = string.Empty;
            string strUpperCredit       = string.Empty;
            string strDigitCredit       = string.Empty;
            string strSpecialCredit     = string.Empty;

            XmlConfigProvider configProvider = null;

            try
            {
                this._configFileName = AppPathInfo.FILE_NAME_SECPOLICY_INFO;
                this._sourcePath = Path.Combine(AppPathInfo.PATH_APP_ENVIRONMENT, AppPathInfo.FILE_NAME_SECPOLICY_INFO);
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

                authenPolicy = this.GetValidValue(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_AUTHENTICATION, SecurityPolicyInfo.XML_ATT_VALUE_POLICY);
                this._authenPolicy = this.GetValidType<AuthenPolicyTypes>(authenPolicy, SecurityPolicyInfo.XML_ATT_VALUE_AUTHENTICATION, SecurityPolicyInfo.XML_ATT_VALUE_POLICY);

                authenEncryptType = this.GetValidValue(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_AUTHENTICATION, SecurityPolicyInfo.XML_ATT_VALUE_ENCRYPTION);
                this._authenEncryptType = this.GetValidType<AuthenEncryptTypes>(authenEncryptType, SecurityPolicyInfo.XML_ATT_VALUE_AUTHENTICATION, SecurityPolicyInfo.XML_ATT_VALUE_ENCRYPTION);

                authorLevel = this.GetValidValue(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_AUTHORIZATION, SecurityPolicyInfo.XML_ATT_VALUE_LEVEL);
                this._authorLevel = this.GetValidType<AuthorLevelTypes>(authorLevel, SecurityPolicyInfo.XML_ATT_VALUE_AUTHORIZATION, SecurityPolicyInfo.XML_ATT_VALUE_LEVEL);

                insertProgramAccessLog = this.GetValidValue(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_LOGGER, SecurityPolicyInfo.XML_ATT_VALUE_INSERTPROGRAMACCESSLOG, false);
                if (string.IsNullOrEmpty(insertProgramAccessLog) == false)
                {
                    if (bool.FalseString.ToUpper().Equals(insertProgramAccessLog.ToUpper()))
                    {
                        _insertProgramAccessLog = false;
                    }
                    else
                    {
                        _insertProgramAccessLog = true;
                    }
                }

                textChar = this.GetValidValueNotException(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_USERID_CONVERSION, SecurityPolicyInfo.XML_ATT_VALUE_TEXT_CHAR_TYPE);
                
                if (!string.IsNullOrEmpty(textChar))
                {
                    this._textChar = this.GetValidType<TextCharTypes>(textChar, SecurityPolicyInfo.XML_ATT_VALUE_USERID_CONVERSION, SecurityPolicyInfo.XML_ATT_VALUE_TEXT_CHAR_TYPE);
                }

                strMinimumLength = this.GetValidValueNotException(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_PASSWORD_VALIDATION, SecurityPolicyInfo.XML_ATT_VALUE_MINIMUM_LENGTH);
                this.GetValidNumberValue(strMinimumLength, ref this._minimumLength);
                if (this._minimumLength > 0)
                {
                    this._validChain += new MinimumLengthValidator() { ExitOnError = false, TargetName = SecurityPolicyInfo.XML_ATT_VALUE_MINIMUM_LENGTH };
                }

                strMaximumLength = this.GetValidValueNotException(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_PASSWORD_VALIDATION, SecurityPolicyInfo.XML_ATT_VALUE_MAXIMUM_LENGTH);
                this.GetValidNumberValue(strMaximumLength, ref this._maximumLength);
                if (this._maximumLength > 0)
                {
                    this._validChain += new MaximumLengthValidator() { ExitOnError = false, TargetName = SecurityPolicyInfo.XML_ATT_VALUE_MAXIMUM_LENGTH };
                }

                strDuplChkCount = this.GetValidValueNotException(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_PASSWORD_VALIDATION, SecurityPolicyInfo.XML_ATT_VALUE_DUPLICATION_CHK_COUNT);
                this.GetValidNumberValue(strDuplChkCount, ref this._duplicationCheckCount);
                if (this._duplicationCheckCount > 0)
                {
                    this._validChain += new DuplicationValidator() { ExitOnError = false, TargetName = SecurityPolicyInfo.XML_ATT_VALUE_DUPLICATION_CHK_COUNT };
                }

                strLowerCredit = this.GetValidValueNotException(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_PASSWORD_VALIDATION, SecurityPolicyInfo.XML_ATT_VALUE_LOWER_CREDIT);
                this.GetValidNumberValue(strLowerCredit, ref this._lowerCredit);
                if (this._lowerCredit > 0)
                {
                    this._validChain += new LowerCreditValidator() { ExitOnError = false, TargetName = SecurityPolicyInfo.XML_ATT_VALUE_LOWER_CREDIT };
                }

                strUpperCredit = this.GetValidValueNotException(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_PASSWORD_VALIDATION, SecurityPolicyInfo.XML_ATT_VALUE_UPPER_CREDIT);
                this.GetValidNumberValue(strUpperCredit, ref this._upperCredit);
                if (this._upperCredit > 0)
                {
                    this._validChain += new UpperCreditValidator() { ExitOnError = false, TargetName = SecurityPolicyInfo.XML_ATT_VALUE_UPPER_CREDIT };
                }

                strDigitCredit = this.GetValidValueNotException(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_PASSWORD_VALIDATION, SecurityPolicyInfo.XML_ATT_VALUE_DIGIT_CREDIT);
                this.GetValidNumberValue(strDigitCredit, ref this._digitCredit);
                if (this._digitCredit > 0)
                {
                    this._validChain += new DigitCreditValidator() { ExitOnError = false, TargetName = SecurityPolicyInfo.XML_ATT_VALUE_DIGIT_CREDIT };
                }

                strSpecialCredit = this.GetValidValueNotException(ref configProvider, SecurityPolicyInfo.XML_ATT_VALUE_PASSWORD_VALIDATION, SecurityPolicyInfo.XML_ATT_VALUE_SPECIAL_CREDIT);
                this.GetValidNumberValue(strSpecialCredit, ref this._specialCredit);
                if (this._specialCredit > 0)
                {
                    this._validChain += new SpecialCreditValidator() { ExitOnError = false, TargetName = SecurityPolicyInfo.XML_ATT_VALUE_SPECIAL_CREDIT };
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return;
        }

        /// <summary>
        /// Gets Valid Number Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private void GetValidNumberValue(string value, ref int numberValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    numberValue = Int32.Parse(value);

                    if (numberValue > 0)
                    {
                        this._isPasswordCheck = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        #endregion

        #region Regx Validation METHOD AREA ********************
        /// <summary>
        /// Whether password validate
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<ValidResultItem> GetPasswordValidationItems(string password)
        {
            List<ValidResultItem> resultList = new List<ValidResultItem>();

            try
            {
                if (this._isPasswordCheck)
                {
                    BasePasswordValidator passwordValidator = this._validChain.NextValidator as BasePasswordValidator;

                    if (passwordValidator != null)
                    {
                        passwordValidator.SetPassword(password);
                        passwordValidator.Validate(ref resultList);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
            return resultList;
        }
        #endregion
    }
}
