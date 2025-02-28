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
using System.Text;
using Tsb.Fontos.Win.FormTemplate;
using Tsb.Fontos.Core.Security.Encryption;
using Tsb.Fontos.Core.Security;


namespace Tsb.Most.Wb.Client.Login
{
    /// <summary>
    /// Represents General Login View
    /// </summary>
    public interface LoginInterface : BaseDialogViewInterface
    {
        #region PROPERTY AREA ***************************************
        /// <summary>
        /// Gets or Sets Security Parameter object reference
        /// </summary>
        BaseSecurityParam SecuParam { get; set; }

        string FormName { get; }
        #endregion
    }
}
