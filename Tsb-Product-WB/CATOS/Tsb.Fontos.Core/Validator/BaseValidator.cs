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
* 2010.02.04   CHOI 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Environments;
using System.Windows.Forms;
using Tsb.Fontos.Core.Transaction;

namespace Tsb.Fontos.Core.Validator
{
    /// <summary>
    /// Base validator class
    /// </summary>
    public abstract class BaseValidator : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************

        private BaseValidator _nextValidator;
        /// <summary>
        /// Gets or Sets next validator
        /// </summary>
        public BaseValidator NextValidator
        {
            get{ return _nextValidator;  }
            set{ _nextValidator = value; }
        }

        private string _targetName=null;
        /// <summary>
        /// Gets or Sets Target Name to validate
        /// </summary>
        public string TargetName
        {
            get { return _targetName; }
            set { _targetName = value; }
        }

        private bool _exitOnError = false;
        /// <summary>
        /// Do exit application on error occure. Default value is false.
        /// </summary>
        public bool ExitOnError
        {
            get { return _exitOnError; }
            set { _exitOnError = value; }
        }

        private bool _stopOnError = false;
        /// <summary>
        /// Stop validator chain processing on error occure. Default value is false.
        /// </summary>
        public bool StopOnError
        {
            get { return _stopOnError; }
            set { _stopOnError = value; }
        }

        private ValidResultType _validResultType = ValidResultType.WITH_RESULT_ITEM;
        /// <summary>
        /// Validation result type. Default validation result type is a ValidResultType.WITH_RESULT_ITEM
        /// </summary>
        public ValidResultType ValidResultType
        {
            get { return _validResultType; }
            set { _validResultType = value; }
        }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseValidator()
            : base()
        {
            this.ObjectID = "GNR-FTCO-VAL-BaseValidator";
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion


        #region METHOD AREA (VALIDATE)**************************
        /// <summary>
        /// Validates condition
        /// </summary>
        /// <param name="resultList">Validation result item list</param>
        /// <returns>Validation Result Item List</returns>
        public abstract List<ValidResultItem> Validate(ref List<ValidResultItem> resultList);
        #endregion

        
        #region METHOD AREA (OPERATOR OVERLOADING)**************
        /// <summary>
        /// operator overloading to make chain of validator
        /// </summary>
        /// <param name="leftValidator">left side validator</param>
        /// <param name="rightValidator">right side validator</param>
        /// <returns>chain of validators</returns>
        public static BaseValidator operator +(BaseValidator leftValidator, BaseValidator rightValidator)
        {
            BaseValidator last = leftValidator;

            while (last.NextValidator != null)
            {
                last = last.NextValidator;
            }

            last.NextValidator = rightValidator;

            return leftValidator;
        }
        #endregion


        #region METHOD AREA (OPERATOR OVERLOADING)**************
        /// <summary>
        /// operator overloading to make chain of validator
        /// </summary>
        /// <param name="leftValidator">left side validator</param>
        /// <param name="rightValidator">right side validator</param>
        /// <returns>chain of validators</returns>
        public virtual List<ValidResultItem> HandleResult(ref List<ValidResultItem> resultList, ResultType resultType, string msgCode, params string[] args)
        {
            string resultMsg = string.Empty;

            try
            {
                if (this.ExitOnError || this.ValidResultType == ValidResultType.WITH_RESULT_ITEM)
                {
                    resultMsg = MessageBuilder.BuildMessage(msgCode, args);
                }

                if (this.ExitOnError && resultType== ResultType.ERROR)
                {
                    if (ArchitectureInfo.GetInstance().UITech == Tsb.Fontos.Core.Environments.Type.UITechTypes.WinForm)
                    {
                        DialogResult dialogResult = MessageBox.Show(resultMsg, DefaultMessage.MSG_STR_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        if (dialogResult == DialogResult.OK)
                        {
                            Environment.Exit(-1);
                        }
                    }
                }

                if (this.ValidResultType == ValidResultType.WITH_EXCEPTION)
                {
                    throw new Tsb.Fontos.Core.Exceptions.Business.TsbBizValidationException(this.ObjectID, msgCode, args);
                }
                else
                {
                    if (resultList == null)
                    {
                        resultList = new List<ValidResultItem>();
                    }

                    resultList.Add(new ValidResultItem(resultType, resultMsg, this.ObjectID));
                }

                
                if (this.StopOnError && resultType == ResultType.ERROR)
                {
                    return resultList;
                }

                if (this.NextValidator != null && this.NextValidator != this)
                {
                    resultList = this.NextValidator.Validate(ref resultList);
                }

            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }

            return resultList;
        }
        #endregion

    }
}
