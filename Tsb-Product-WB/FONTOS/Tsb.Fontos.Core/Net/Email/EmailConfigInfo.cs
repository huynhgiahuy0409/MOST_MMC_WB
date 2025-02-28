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
* 2009.07.25     CHOI 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Net.Email
{
    /// <summary>
    /// Email Configuration Information value object class
    /// </summary>
    public class EmailConfigInfo : TsbBaseObject
    {
        #region PROPERTY AREA **********************************
        
        /// <summary>
        /// Gets or sets the Mail Server address.
        /// </summary>
        /// <value>The Mail Server address.</value>
        public string MailServerAddress { get; set; }


        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }


        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public string From { get; set; }


        /// <summary>
        /// Gets or sets the name of the authentication user.
        /// </summary>
        /// <value>The name of the authentication user.</value>
        public string UserName { get; set; }


        /// <summary>
        /// Gets or sets the authentication password.
        /// </summary>
        /// <value>The authentication password.</value>
        public string Password { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is authentication required.
        /// </summary>
        public bool IsAuthenticationRequired { get; set; }

        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public EmailConfigInfo()
        {
            this.ObjectID = "GNR-FTCO-NET-EmailConfigInfo";
        }


        /// <summary>
        /// Initializes a new instance with specified mail server and port
        /// </summary>
        /// <param name="smtpService">The mail server address</param>
        /// <param name="port">The port.</param>
        public EmailConfigInfo(string mailServerAddress, int port)
        {
            this.MailServerAddress = mailServerAddress;
            this.Port = port;
        }
        #endregion


    }
}
