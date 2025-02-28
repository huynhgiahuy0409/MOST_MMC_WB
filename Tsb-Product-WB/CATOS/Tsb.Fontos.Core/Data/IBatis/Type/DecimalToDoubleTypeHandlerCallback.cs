#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2013 TOTAL SOFT BANK LIMITED. All Rights
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
* 2013.12.02    Jindols 1.0	First release.
*
*/
#endregion

using System;
using IBatisNet.DataMapper.Exceptions;
using IBatisNet.DataMapper.TypeHandlers;
using Tsb.Fontos.Core.Objects;
using System.Drawing;

namespace Tsb.Fontos.Core.Data.IBatis.Type
{
    /// <summary>
    /// DecimalToDoubleTypeHandlerCallback
    /// </summary>
    public class DecimalToDoubleTypeHandlerCallback : ITypeHandlerCallback
    {
        #region FIELD AREA *************************************
        #endregion

        #region PROPERTY AREA **********************************
        /// <summary>
        /// Gets and Sets Object ID
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// Gets and Sets Object Type
        /// </summary>
        public ObjectType ObjectType { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DecimalToDoubleTypeHandlerCallback()
        {
            this.ObjectID = "GNR-FTCO-DATTP-DecimalToDoubleTypeHandlerCallback";
        }
        #endregion


        #region ITypeHandlerCallback members ****************************
        /// <summary>
        /// Casts the string representation of a value into a type recognized by this
        /// type handler. This method is used to translate nullValue values into types
        /// that can be appropriately compared. If your custom type handler cannot support
        /// nullValues, or if there is no reasonable string representation for this type
        /// (e.g. File type), you can simply return the String representation as it was
        /// passed in. It is not recommended to return null, unless null was passed in.
        /// </summary>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public object ValueOf(string nullValue)
        {
            try
            {
                return Convert.ToDecimal(nullValue);
            }
            catch (Exception)
            {
                throw new DataMapperException("Unexpected value " + nullValue + " A value is System.Double string.");
            }
        }
        /// <summary>
        /// Performs processing on a value before after it has been retrieved from a IDataReader.
        /// </summary>
        /// <param name="getter">The interface for getting the value from the IDataReader.</param>
        /// <returns>The processed value.</returns>
        public object GetResult(IResultGetter getter)
        {
            string value = getter.Value.ToString();

            if (string.IsNullOrEmpty(value) == true)
            {
                return DBNull.Value;
            }

            decimal returnValue = 0;
            try
            {
                returnValue = Decimal.Parse(value);
            }
            catch (Exception)
            {

                throw new DataMapperException("Unexpected value " + value + ". This value is System.Double string.");
            }

            return (double)returnValue;
        }
        /// <summary>
        /// Performs processing on a value before it is used to set the parameter of a IDbCommand.
        /// </summary>
        /// <param name="setter">The interface for setting the value on the IDbCommand.</param>
        /// <param name="parameter">The value to be set</param>
        public void SetParameter(IParameterSetter setter, object parameter)
        {
            try
            {
                if (parameter is double)
                {
                    setter.Value = (decimal)parameter;
                }
                else
                {
                    throw new DataMapperException("Unexpected Type. Type must be System.Double");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Gets the null value for this type
        /// </summary>
        public object NullValue
        {
            get { return 0; }
        }
        #endregion ****************************
    }
}
