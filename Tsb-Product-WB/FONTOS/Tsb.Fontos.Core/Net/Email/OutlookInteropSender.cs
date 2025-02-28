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
using System.Reflection;
using Tsb.Fontos.Core.Reflection;

namespace Tsb.Fontos.Core.Net.Email
{
    /// <summary>
    /// Outlook Interop Email Sender class
    /// </summary>
    public class OutlookInteropSender : TsbBaseObject,IEmailSender
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
        public OutlookInteropSender()
        {
            this.ObjectID = "GNR-FTCO-NET-OutlookInteropSender";
        }


        /// <summary>
        /// Initializes a new instance with a specified EmailConfigInfo object
        /// </summary>
        /// <param name="config">The email config information object reference.</param>
        public OutlookInteropSender(EmailConfigInfo config)
            : this()
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
            Type outlookAppType = null;

            object objOutlookApp = null;
            object objMailItem = null;
            object objRecipients = null;
            object objAttachments = null;

            object[] args = null;

            try
            {
                outlookAppType = Type.GetTypeFromProgID("Outlook.Application");
                objOutlookApp = ObjectCreator.CreateObject(outlookAppType);

                //0 = Outlook.OlItemType.olMailItem  Enum Type
                args = new object[1] { 0 };
                objMailItem = objOutlookApp.GetType().InvokeMember("CreateItem", BindingFlags.InvokeMethod, null, objOutlookApp, args);

                // Get the Recipients property
                objRecipients = objMailItem.GetType().InvokeMember("Recipients", BindingFlags.GetProperty, null, objMailItem, null);

                // Invoke the Add method on Recipients 
                args = new object[1] { message.To };
                objRecipients.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, objRecipients, args);

                // Set the Subject property of the mail item 
                args = new object[1] { message.Subject };
                objMailItem.GetType().InvokeMember("Subject", BindingFlags.SetProperty, null, objMailItem, args);

                // Set the Body property of the mail item 
                args = new object[1] { message.Body };
                objMailItem.GetType().InvokeMember("Body", BindingFlags.SetProperty, null, objMailItem, args);

                objAttachments = objMailItem.GetType().InvokeMember("Attachments", BindingFlags.GetProperty, null, objMailItem, null);
                args = new object[4] { message.PathAttach, 1, message.Body.Length + 1, "Attachment" };
                objAttachments.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, objAttachments, args);

                // Invoke the Send method of the mail item.
                args = new object[1] { false };
                objMailItem.GetType().InvokeMember("Display", BindingFlags.InvokeMethod, null, objMailItem, args);
            }
            catch (Exception ex)
            {
                //MSG : An error occurred in sending a mail to [{0}]. Please Contact your system administrator.		
                ExceptionHandler.Wrap(ex, typeof(TsbSysNetException), this.ObjectID, "MSG_FTCO_00055", DefaultMessage.NON_REG_WRD + message.To);
            }
            finally
            {
                objOutlookApp = null;
                objMailItem = null;
                objRecipients = null;
                objAttachments = null;
            }
            return true;
       }

    }
}
