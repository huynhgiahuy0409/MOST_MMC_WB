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
* 2012.02.21  Tonny.Kim 1.0	    First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Validator.UI
{
    public abstract class BasePasswordValidator : BaseValidator
    {
        #region FIELD/PROPERTY AREA*****************************
        public string Password { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BasePasswordValidator()
            : base()
        {
            this.ObjectID = "GNR-FTCO-VAL-BasePasswordValidator";
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion

        #region METHOD AREA ************************************
        /// <summary>
        /// Sets Password
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            this.Password = password;
            try
            {
                if (this.NextValidator != null && this.NextValidator != this)
                {
                    BasePasswordValidator passwordValidator = this.NextValidator as BasePasswordValidator;

                    if (passwordValidator != null)
                    {
                        passwordValidator.SetPassword(password);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion
    }
}
