#region Interface Definitions
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
* 2010.02.05     CHOI       1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Security.Authorization;
using Tsb.Fontos.Core.Security.Authentication;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Item;

namespace Tsb.Fontos.Core.Security
{
    /// <summary>
    /// Represents Security Data Access Object
    /// </summary>
    public interface ISecurityDao
    {
        /// <summary>
        /// Inquiry Old Passwords
        /// </summary>
        /// <returns>Old Passwords</returns>
        IList<string> InquiryPasswordHistory(BaseSecurityParam param);

        /// <summary>
        /// Inquiry User Expiration Policy Information
        /// </summary>
        /// <returns>BaseSecurityItemList object reference that includes BaseSecurityItem objects list</returns>
        BaseExpirePolicyItem InquiryExpirationPolicy();

        /// <summary>
        /// Inquiry User Expiration Policy Information
        /// </summary>
        /// <param name="param"></param>
        /// <returns>BaseSecurityItemList object reference that includes BaseSecurityItem objects list</returns>
        BaseExpirePolicyItem InquiryExpirationPolicy(BaseSecurityParam param);

        /// <summary>
        /// Inquiry User Information
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>BaseUserInfo object reference</returns>
        BaseUserInfo InquiryUserInfo(BaseSecurityParam param);


        /// <summary>
        /// Inquiry a application access right information
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>A ModuleAccessRightItem object</returns>
        ModuleAccessRightItem InquiryModuleAccessUserRight(BaseSecurityParam param);


        /// <summary>
        /// Inquiry application authorization information list
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>A list of authorization information item objects</returns>
        IList<T> InquiryAuthorInfoList<T>(BaseSecurityParam param);

        /// <summary>
        /// Inquiry application menu information list
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>A list of menu information item objects</returns>
        IList<T> InquiryMenuInfoList<T>(BaseSecurityParam param);

        /// <summary>
        /// Inquiry Authority Control list
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>A list of menu information item objects</returns>
        IList<T> InquiryAuthorityControls<T>(BaseSecurityParam param);

        /// <summary>
        /// Inquiry IP Access
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>A list of menu information item objects</returns>
        IList<T> InquiryIpAccess<T>(BaseSecurityParam param);

        /// <summary>
        /// Inquiry lincense information.
        /// </summary>
        /// <returns></returns>
        SiteItem InquirySite();
        
        /// <summary>
        /// Update Staff as expired
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>updated rows count</returns>
        int UpdateStaffAsExpired(BaseSecurityParam param);
        

        /// <summary>
        /// Update Staff wrong password count info
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>updated rows count</returns>
        int UpdateWrongPwdRetry(BaseSecurityParam param);


        /// <summary>
        /// Update Staff after login success
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>updated rows count</returns>
        int UpdateLoginSuccess(BaseSecurityParam param);


        /// <summary>
        /// Update User Password
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>updated rows count</returns>
        int UpdateUserPassword(BaseSecurityParam param);

        /// <summary>
        /// Insert Pgrogram Access Log.
        /// </summary>
        /// <param name="item">Item object reference</param>
        /// <returns>Inserted object.</returns>
        object InsertProgramAccessLog(Tsb.Fontos.Core.Security.Logging.ProgramAccessLogItem item);
    }
}
