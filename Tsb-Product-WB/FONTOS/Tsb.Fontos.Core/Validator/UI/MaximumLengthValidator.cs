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
* 2019.04.22     lee.hs	        First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using System.Text.RegularExpressions;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Message;

namespace Tsb.Fontos.Core.Validator.UI
{
    public class MaximumLengthValidator : BasePasswordValidator
    {
        #region READONLY AREA **********************************
        /// <summary>
        /// Password Regx
        /// </summary>
        private readonly string PASSWORD_REGX = @"(?=^.{0," + SecurityPolicyInfo.GetInstance().MaximumLength + "}$)";
        #endregion

         #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MaximumLengthValidator()
            : base()
        {
            this.ObjectID = "GNR-FTCO-VAL-MaximumLengthValidator";
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion

        #region METHOD AREA (VALIDATE)**************************
        /// <summary>
        /// Validates whether maximum length
        /// </summary>
        /// <param name="resultList">Result List</param>
        /// <returns>Validation result item list</returns>
        public override List<ValidResultItem> Validate(ref List<ValidResultItem> resultList)
        {
            if (!Regex.IsMatch(this.Password, this.PASSWORD_REGX))
            {
                //MSG:New password has exceeded the maximum length of {0} characters.
                resultList = this.HandleResult(ref resultList, ResultType.ERROR, "MSG_FTCO_00282", DefaultMessage.NON_REG_WRD + SecurityPolicyInfo.GetInstance().MaximumLength);
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
