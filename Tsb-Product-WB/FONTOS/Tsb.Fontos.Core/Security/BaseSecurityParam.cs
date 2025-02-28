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
* 2010.02.05     Choi       1.0	First release.
* 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Param;

namespace Tsb.Fontos.Core.Security
{
    /// <summary>
    /// Base Security Paramter class
    /// </summary>
    [Serializable]
    public class BaseSecurityParam : BaseParam
    {
        #region PROPERTY AREA *************************************
        /// <summary>
        /// Gets or Sets User ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets UserType code
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Gets or Sets program code
        /// </summary>
        public string PgmCode { get; set; }

        /// <summary>
        /// Gets or Sets Old Password
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or Sets New Password
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or Sets Confirm Password
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or Sets Encrypted New Password
        /// </summary>
        public string EncryptedNewPwd { get; set; }

        /// <summary>
        /// Gets or Sets View Name
        /// </summary>
        public string ViewName { get; set; }
        
        /// <summary>
        /// Gets or Sets View Name
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// Gets or sets parent view menu id.
        /// </summary>
        public string AuthorizedMenuId { get; set; }

        /// <summary>
        /// Gets or sets authority view menu name.
        /// </summary>
        public string AuthorizedMenuName { get; set; }

        /// <summary>
        /// Gets Target data to Addition Info
        /// </summary>
        public object AdditionInfo { get; set; }

        /// <summary>
        /// Gets or sets count of password for check duplication with new password.
        /// </summary>
        public object DuplicationCheckCount { get; set; }
        /// <summary>
        /// Gets or Sets SSO ID
        /// </summary>
        public string SSOID { get; set; }

        /// <summary>
        /// Gets or Sets the value indicating whether module use Auto Logout Interval or not.
        /// </summary>
        public bool? UseAutoLogoutInterval { get; set; }

        /// <summary>
        /// Gets or Sets the value indicating whether module use Expire Date or not.
        /// </summary>
        public bool? UseExpireDate { get; set; }
        #endregion


        #region INITIALIZE AREA *************************************
        /// <summary>
        /// Initialize BaseSecuirytParam Instance using a specified param owner
        /// </summary>
        /// <param name="paramOwner">parameter object owner object reference</param>
        public BaseSecurityParam(object paramOwner)
            : base(paramOwner)
        {
            this.ObjectID = "PAR-FT-FTCO-SEC-BaseSecurityParam";
        }

        /// <summary>
        /// Initialize BaseSecuirytParam Instance using a specified param owner and transaction 
        /// service ID
        /// </summary>
        /// <param name="paramOwner">parameter object owner object reference</param>
        /// <param name="txServiceID">Transcation service ID</param>
        public BaseSecurityParam(object paramOwner, string txServiceID)
            : base(paramOwner, txServiceID)
        {
            this.ObjectID = "PAR-FT-FTCO-SEC-BaseSecurityParam";
        }
        #endregion
    }
}
