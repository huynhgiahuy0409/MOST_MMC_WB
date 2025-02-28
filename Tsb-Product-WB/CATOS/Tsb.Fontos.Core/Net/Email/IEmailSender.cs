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
* 2009.09.21    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Tsb.Fontos.Core.Net.Email
{

    /// <summary>
    /// Represent Email Seneder
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// The email service configuration object.
        /// </summary>
        EmailConfigInfo EmailConfig {get;set;}


        /// <summary>
        /// Send mail
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>true, if mail sending is successfule</returns>
        bool Send(EmailMessage message);
    }
}
