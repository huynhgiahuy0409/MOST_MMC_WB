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
* 2019.04.24     lee.hs	        First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Security;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Param;

namespace Tsb.Fontos.Core.Validator.UI
{
    public class DuplicationValidator : BasePasswordValidator
    {
        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DuplicationValidator()
            : base()
        {
            this.ObjectID = "GNR-FTCO-VAL-DuplicationValidator";
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion

        #region METHOD AREA (VALIDATE)**************************
        /// <summary>
        /// Validates whether new password is duplicated to old ones.
        /// </summary>
        /// <param name="resultList">Result List</param>
        /// <returns>Validation result item list</returns>
        public override List<ValidResultItem> Validate(ref List<ValidResultItem> resultList)
        {
            ISecurityService securityService = BizServiceLocator.GetService<ISecurityService>(SysObjectSpec.SYS_OBJECT_SECURITY_SERVICE);
            BaseResult baseResult = securityService.InquiryPasswordHistory(new BaseSecurityParam(this) { UserID = SecurityPolicyInfo.GetInstance().CurrentUserID, DuplicationCheckCount = SecurityPolicyInfo.GetInstance().DuplicationCheckCount });

            if (baseResult.ResultObject != null && baseResult.ResultObject is IList<string> && SecurityPolicyInfo.GetInstance().PasswordEncrypter != null)
	        {
                IList<string> oldPasswords = baseResult.ResultObject as IList<string>;
                string encryptedPassword = SecurityPolicyInfo.GetInstance().PasswordEncrypter.EncryptString(this.Password, SecurityPolicyInfo.ENCRYPTION_KEY);
                IEnumerable<string> result = oldPasswords.Where(e => e == encryptedPassword);

                if (result.Count() > 0)
                {
                    //MSG:New password should not be same to last {0} old passwords.
                    resultList = this.HandleResult(ref resultList, ResultType.ERROR, "MSG_FTCO_00283", DefaultMessage.NON_REG_WRD + SecurityPolicyInfo.GetInstance().DuplicationCheckCount);
                }
                else
                {
                    //MSG:Validation is successful
                    resultList = this.HandleResult(ref resultList, ResultType.OK, "MSG_FTCO_00075", null);
                } 
	        }

            return resultList;
        }
        #endregion
    }
}
