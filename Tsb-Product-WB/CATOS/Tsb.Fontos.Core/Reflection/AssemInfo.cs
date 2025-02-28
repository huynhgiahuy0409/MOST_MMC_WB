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
* 2009.07.04    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Message;

namespace Tsb.Fontos.Core.Reflection
{
    /// <summary>
    /// Assembly information class
    /// </summary>
    public class AssemInfo : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************

        public const string _objectID = "GNR-FTCO-REF-AssemInfo";
        
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AssemInfo()
        {
            this.ObjectID = _objectID;
        }
        #endregion


        #region STATIC METHOD AREA *****************************
        /// <summary>
        /// Returns calling assembly location
        /// </summary>
        /// <returns>Assembly location</returns>
        public static string GetCallingAssemblyLocation()
        {
            string rtnLocation = null;
            rtnLocation = Assembly.GetCallingAssembly().Location;
            return rtnLocation;
        }


        /// <summary>
        /// Check a specified assembly is valid or not
        /// </summary>
        /// <param name="assemblyName">Assembly name</param>
        /// <returns>Assembly Reference</returns>
        public static Assembly IsValidAssemblyName(string assemblyName)
        {
            Assembly assem = null;
            try
            {
                assem = Assembly.Load(assemblyName);
            }
            catch(Exception ex)
            {
                //MSG : A specified [{0}] assembly is invalid.		
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), AssemInfo._objectID, "MSG_FTCO_00029", DefaultMessage.NON_REG_WRD + assemblyName);
            }

            return assem;

        }

        /// <summary>
        /// Returns EXE assembly copy right string
        /// </summary>
        /// <returns>EXE assembly copy right string</returns>
        public static string GetExeAssemCopyright()
        {
            string rtnStr = string.Empty;

            object[] assemAttrs = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            
            rtnStr = ((AssemblyCopyrightAttribute)assemAttrs[0]).Copyright;

            return rtnStr;
        }

        /// <summary>
        /// Returns EXE assembly title string
        /// </summary>
        /// <returns>EXE assembly title string</returns>
        public static string GetExeAssemTitle()
        {
            string rtnStr = string.Empty;

            object[] assemAttrs = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            rtnStr = ((AssemblyTitleAttribute)assemAttrs[0]).Title;

            return rtnStr;
        }

        #endregion
    }
}
