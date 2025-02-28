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
* 2009.09.21    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Net.Email
{
    /// <summary>
    /// Email Message value object class
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// From email address.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// To email address.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Subject of email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Body of email.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Path of attachment
        /// </summary>
        public string PathAttach { get; set; }
    }
}
