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
* 2010.06.24    Jindols 1.0	First release.
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
    /// StringToColorTypeHandlerCallback.
    /// </summary>
    public class StringToColorTypeHandlerCallback : ITypeHandlerCallback//, ITsbBaseObject
    {
        #region FIELD AREA *************************************
        private const string YES = "Y";
        private const string NO = "N";
        private const string WHITE = "White";
        private const string BLOCK = "Black";
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
        public StringToColorTypeHandlerCallback()
        {
            this.ObjectID = "GNR-FTCO-DATTP-ColorTypeHandlerCallback";
        }
        #endregion


        #region ITypeHandlerCallback members ****************************
        public object ValueOf(string nullValue)
        {
            try
            {
                return Color.FromName(nullValue);
            }
            catch (Exception)
            {

                throw new DataMapperException("Unexpected value " + nullValue + "  A value that is the name of a predefined color. Valid names are the same as the names of the elements of the System.Drawing.KnownColor enumeration.");
            }
        }

        public object GetResult(IResultGetter getter)
        {

            string value = getter.Value.ToString().ToUpper();

            if (value.Trim().Length == 0) return DBNull.Value;

            int colorValue = 0;
            try
            {
                colorValue = Convert.ToInt32(value);
            }
            catch (Exception)
            {

                throw new DataMapperException("Unexpected value " + value + ". This value is 32-bit ARGB string.");
            }

            return System.Drawing.ColorTranslator.FromWin32(colorValue);
        }

        public void SetParameter(IParameterSetter setter, object parameter)
        {
            try
            {

                if (parameter is System.Drawing.Color)
                {
                    Color color = (System.Drawing.Color)parameter;
                    setter.Value = System.Drawing.ColorTranslator.ToWin32(color);
                }
                else
                {
                    throw new DataMapperException("Unexpected Type. Type must be System.Drawing.Color");
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object NullValue
        {
            get { return Color.Empty; } 
        }
        #endregion ****************************
    }
}
