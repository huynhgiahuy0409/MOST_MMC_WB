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
    public class TypeInfo : TsbBaseObject
    {
        public const string _objectID = "GNR_FTCO_REF_TypeInfo";

        public TypeInfo()
        {
            this.ObjectID = _objectID;
        }

        /// <summary>
        /// Check a specified  class is valid or not
        /// </summary>
        /// <param name="assemblyName">Assembly name</param>
        /// <param name="classFullName"> full class name</param>
        /// <returns>true if a specified class is valid</returns>
        public static bool CheckTypeExistInAssembly(string assemblyName, string classFullName)
        {
            bool exist = false;
            Type type = null;
            
            try
            {
                type = AssemInfo.IsValidAssemblyName(assemblyName).GetType(classFullName);
                
                if(type!=null)
                {
                    exist = true;
                }
            }
            catch(TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, AssemInfo._objectID);
            }
            catch(Exception ex)
            {
                //MSG : A specified [{0}] assembly is invalid.		
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), AssemInfo._objectID, "MSG_FTCO_00029", DefaultMessage.NON_REG_WRD + assemblyName);
            }
            return exist;

        }
    }
}
