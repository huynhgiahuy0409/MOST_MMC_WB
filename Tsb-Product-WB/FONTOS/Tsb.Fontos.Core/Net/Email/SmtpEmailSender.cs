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
using System.Text;
using System.Net;
using System.Net.Mail;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Message;

namespace Tsb.Fontos.Core.Net.Email
{
    /// <summary>
    /// SMTP Email Sender class
    /// </summary>
    public class SmtpEmailSender : TsbBaseObject,IEmailSender
    {
        #region FIELD AREA *************************************
        private EmailConfigInfo _configInfo = null;
        #endregion


        #region PROPERTY AREA **********************************

        /// <summary>
        /// The email service configuration object.
        /// </summary>
        public EmailConfigInfo EmailConfig
        {
            get { return this._configInfo; }
            set { this._configInfo = value; }
        }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public SmtpEmailSender()
        {
            this.ObjectID = "GNR-FTCO-NET-SmtpEmailSender";
        }


        /// <summary>
        /// Initializes a new instance with a specified EmailConfigInfo object
        /// </summary>
        /// <param name="config">The email config information object reference.</param>
        public SmtpEmailSender(EmailConfigInfo config) : this()
        {
            _configInfo = config;
        }
        #endregion


        /// <summary>
        /// Send mail
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>true, if mail sending is successfule</returns>
        public bool Send(EmailMessage message)
        {
            bool sent = true;
            System.Net.Mail.SmtpClient smtpClient = null;
            MailMessage mailMessage = null;

            try
            {
                mailMessage = new MailMessage(message.From, message.To, message.Subject, message.Body);
                mailMessage.IsBodyHtml = false;

                smtpClient = new SmtpClient(this._configInfo.MailServerAddress, this._configInfo.Port);

                if (this.EmailConfig.IsAuthenticationRequired)
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(this.EmailConfig.UserName, this.EmailConfig.Password);
                }

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //MSG : An error occurred in sending a mail to [{0}]. Please Contact your system administrator.		
                ExceptionHandler.Wrap(ex, typeof(TsbSysNetException), this.ObjectID, "MSG_FTCO_00055", DefaultMessage.NON_REG_WRD+message.To);
            }
            return sent;
        }

    }
}
