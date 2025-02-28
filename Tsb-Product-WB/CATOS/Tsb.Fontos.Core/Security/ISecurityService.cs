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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Service;
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Security.Profile;
using Tsb.Fontos.Core.Transaction;
using Tsb.Fontos.Core.Transaction.Type;
using System.Collections;

namespace Tsb.Fontos.Core.Security
{
    /// <summary>
    /// Represents Security Service
    /// </summary>
    public interface ISecurityService : ITsbService
    {
        /// <summary>
        /// Gets or Sets Security DAO
        /// </summary>
        ISecurityDao SecurityDao { get; set; }

        #region METHOD AREA(SEARCH)***************************
        /// <summary>
        /// Inquiry Old Passwords
        /// </summary>
        /// <returns>BaseResult object reference that includes a result of opreation</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryPasswordHistory(BaseSecurityParam param);

        /// <summary>
        /// Inquiry User Expiration Policy Information
        /// </summary>
        /// <returns>BaseResult object reference that includes a result of opreation</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryExpirationPolicy();

        /// <summary>
        /// Inquiry User Expiration Policy Information
        /// </summary>
        /// <param name="param"></param>
        /// <returns>BaseResult object reference that includes a result of opreation</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryExpirationPolicy(BaseSecurityParam param);

        /// <summary>
        /// Inquiry User Information
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>BaseResult object reference that includes a result of opreation</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryUserInfo(BaseSecurityParam param);

        /// <summary>
        /// Inquiry Application Access Right Information
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>BaseResult object reference that includes a result of opreation</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryModuleAccessUserRightSingle(BaseSecurityParam param);

        /// <summary>
        /// Inquiry Application Access Right Information
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>BaseResult object reference that includes a result of opreation</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryModuleAccessUserRightGroup(BaseSecurityParam param);

        /// <summary>
        /// Inquiry application's operation authorization information list
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <param name="userInfo">User information object referecne</param>
        /// <returns>A list of authorization information item objects</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryOperationAuthorInfoList(BaseSecurityParam param, BaseUserInfo userInfo);

        /// <summary>
        /// Inquiry application's menu information list
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <param name="userInfo">User information object referecne</param>
        /// <returns>A list of menu information item objects</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryMenuInfoList(BaseSecurityParam param, BaseUserInfo userInfo);

        /// <summary>
        /// Check whether MenuId is in Menu List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuInfos">Menu List</param>
        /// <param name="menuId">Menu ID</param>
        /// <returns></returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        bool IsMenuIdInMenuList(IList menuInfos, string menuId);

        /// <summary>
        /// Check whether MenuId is in Menu List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="menuInfos">Menu List</param>
        /// <param name="menuId">Menu ID</param>
        /// <returns></returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryAuthorityControls(BaseSecurityParam param);

        /// <summary>
        /// Inquiry IP Access
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>A list of menu information item objects</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquiryIpAccess(BaseSecurityParam param);

        /// <summary>
        /// Inquiry lincense information.
        /// </summary>
        /// <returns></returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        BaseResult InquirySite();
        #endregion

        #region METHOD AREA(CUD)******************************
        /// <summary>
        /// Update Staff as expired
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>updated rows count</returns>
        [TransactionOption(TransactionScopeTypes.Required)]
        int UpdateStaffAsExpired(BaseSecurityParam param);

        /// <summary>
        /// Update Staff wrong password count info
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>updated rows count</returns>
        [TransactionOption(TransactionScopeTypes.Required)]
        int UpdateWrongPwdRetry(BaseSecurityParam param);

        /// <summary>
        /// Update Staff after login success
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>updated rows count</returns>
        [TransactionOption(TransactionScopeTypes.Required)]
        int UpdateLoginSuccess(BaseSecurityParam param);

        /// <summary>
        /// Update User Password
        /// </summary>
        /// <param name="param">Parameter object reference</param>
        /// <returns>updated rows count</returns>
        [TransactionOption(TransactionScopeTypes.Required)]
        int UpdateUserPassword(BaseSecurityParam param);

        /// <summary>
        /// Insert Pgrogram Access Log.
        /// </summary>
        /// <param name="item">Item object reference</param>
        /// <returns>Inserted object</returns>
        [TransactionOption(TransactionScopeTypes.Required)]
        object InsertProgramAccessLog(Tsb.Fontos.Core.Security.Logging.ProgramAccessLogItem item);
        #endregion

        #region METHOD AREA(VALIDATION)***********************
        /// <summary>
        /// Check user level and returns true if user level is valid
        /// </summary>
        /// <param name="userInfo">User Information object reference</param>
        /// <returns>true if user level is valid</returns>
        [TransactionOption(TransactionScopeTypes.Support)]
        bool CheckValidUserLevel(BaseUserInfo userInfo);

        #endregion

    }
}
