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
* 
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE                      AUTHOR		            REVISION    	
* 2014.9.30    Seong-Woo Jeong(CATOS(Operation))    1.0	        First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper.TypeHandlers;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Data.IBatis.Type
{
    /// <summary>
    /// StringToNumericTypeHandlerCallback.
    /// </summary>
    public class StringToNumericTypeHandlerCallback : ITypeHandlerCallback, ITsbBaseObject
    {

        #region FIELD AREA *************************************

        #endregion

        #region PROPERTY AREA **********************************
        #region ITsbBaseObject Members

        /// <summary>
        /// Gets and Sets Object ID
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// Gets and Sets Object Type
        /// </summary>
        public ObjectType ObjectType { get; set; }

        #endregion
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public StringToNumericTypeHandlerCallback()
        {
            this.ObjectID = "GNR-FTCO-DATTP-StringToNumericTypeHandlerCallback";
        }
        #endregion

        #region ITypeHandlerCallback members ****************************
        public object ValueOf(string nullValue)
        {
            if (nullValue.Length > 0)
            {
                return nullValue;
            }
            else
            {
                return null;
            }
        }

        public object GetResult(IResultGetter getter)
        {
            string value = getter.Value.ToString().ToUpper();

            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            else
            {
                return value;
            }

        }

        public void SetParameter(IParameterSetter setter, object parameter)
        {
            object setValue = DBNull.Value;

            if (parameter != null)
            {
                setValue = Convert.ToString(parameter) == string.Empty ? DBNull.Value : parameter;
            }

            setter.Value = setValue;
        }

        public object NullValue
        {
            get
            {
                return string.Empty;
            }
        }
        #endregion ****************************

    }
}
