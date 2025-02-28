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
* DATE           AUTHOR		REVISION    	
* 2009.10.16    Jindols 1.0	First release.
*
*/
#endregion
using System;
using IBatisNet.DataMapper.Exceptions;
using IBatisNet.DataMapper.TypeHandlers;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Data.IBatis.Type
{
    /// <summary>
    /// YNBoolTypeHandlerCallback.
    /// </summary>
    public class YNBoolTypeHandlerCallback : ITypeHandlerCallback, ITsbBaseObject
    {

        #region FIELD AREA *************************************
        private const string YES = "Y";
        private const string NO = "N";
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
        public YNBoolTypeHandlerCallback()
        {
            this.ObjectID = "GNR-FTCO-DATTP-YNBoolTypeHandlerCallback";
        }
        #endregion
        
        #region ITypeHandlerCallback members ****************************

        public object ValueOf(string nullValue)
        {
            if (YES.Equals(nullValue))
            {
                return true;
            }
            else if (NO.Equals(nullValue))
            {
                return false;
            }
            else
            {
                throw new DataMapperException("Unexpected value " + nullValue + " found where " + YES + " or " + NO + " was expected.");
            }
        }

        public object GetResult(IResultGetter getter)
        {
            string value = getter.Value.ToString().ToUpper();

            if (value.Trim().Length == 0) return false;
            if (YES.Equals(value))
            {
                return true;
            }
            else if (NO.Equals(value))
            {
                return false;
            }
            else
            {
                throw new DataMapperException("Unexpected value " + value + " found where " + YES + " or " + NO + " was expected.");
            }
        }

        public void SetParameter(IParameterSetter setter, object parameter)
        {
            bool b = Convert.ToBoolean(parameter);
            if (b)
            {
                setter.Value = YES;
            }
            else
            {
                setter.Value = NO;
            }
        }

        public object NullValue
        {
            get { throw new InvalidCastException("YesNoBoolTypeHandlerCallback could not cast a null value in a bool field."); }
        }
        #endregion ****************************


    }
}
