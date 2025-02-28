﻿#region Class Definitions
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
* 2012.02.21  Tonny.Kim 1.0	    First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Transaction;
using System.Text.RegularExpressions;

namespace Tsb.Fontos.Core.Validator.UI
{
    public class SpecialCreditValidator : BasePasswordValidator
    {
        #region READONLY AREA **********************************
        /// <summary>
        /// Password Regx
        /// </summary>
        private readonly string PASSWORD_REGX = @"(?=(?:.*?[!@#$%*()_+^&}{:;?.]){" + SecurityPolicyInfo.GetInstance().SpecialCredit + "})";
        #endregion

        #region FIELD/PROPERTY AREA*****************************
        /// <summary>
        /// Validator Type
        /// </summary>
        public SecurityPolicyInfo.PasswordValidationResultTypes ValidatorType 
        {
            get
            {
                return SecurityPolicyInfo.PasswordValidationResultTypes.SPECIAL;
            }
        }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SpecialCreditValidator()
            : base()
        {
            this.ObjectID = "GNR-FTCO-VAL-SpecialCreditValidator";
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion

        #region METHOD AREA (VALIDATE)**************************
        /// <summary>
        /// Validates whether Special character
        /// </summary>
        /// <param name="resultList">Result List</param>
        /// <returns>Validation result item list</returns>
        public override List<ValidResultItem> Validate(ref List<ValidResultItem> resultList)
        {
            if (!Regex.IsMatch(this.Password, this.PASSWORD_REGX))
            {
                //MSG:Special characters(!@#$%*()_+^&}{:;?.) should be more than {0}.
                resultList = this.HandleResult(ref resultList, ResultType.ERROR, "MSG_FTCO_00190", DefaultMessage.NON_REG_WRD + SecurityPolicyInfo.GetInstance().SpecialCredit);
            }
            else
            {
                //MSG:Validation is successful
                resultList = this.HandleResult(ref resultList, ResultType.OK, "MSG_FTCO_00075", null);
            }

            return resultList;
        }
        #endregion
    }
}
