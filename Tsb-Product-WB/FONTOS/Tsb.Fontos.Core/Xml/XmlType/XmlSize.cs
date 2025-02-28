#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2014 TOTAL SOFT BANK LIMITED. All Rights
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
* 2014.08.07   Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace Tsb.Fontos.Core.Xml.XmlType
{
    /// <summary>
    /// Defines the size of the CellElement.
    /// </summary>
    public class XmlSize
    {
        #region FIELD AREA ***************************************
        private Size _size = Size.Empty;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [XmlAttribute("Value")]
        public string Value
        {
            get { return XmlSerializationHelper.SizeToString(_size); }
            set
            {
                try
                {
                    _size = XmlSerializationHelper.StringToSize(value);
                }
                catch (Exception)
                {
                    _size = Size.Empty;
                }
            }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Default constructor.
        /// </summary>
        public XmlSize() 
        { 
        }

        /// <summary>
        /// Initialize the XmlSize object.
        /// </summary>
        /// <param name="font"></param>
        public XmlSize(Size s)
        { 
            _size = s; 
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Returns a size.
        /// </summary>
        /// <returns></returns>
        public Size ToSize()
        {
            return _size;
        }

        /// <summary>
        /// Sets the size.
        /// </summary>
        /// <param name="s"></param>
        public void FromSize(Size s)
        {
            _size = s;
        }
        #endregion

        #region METHOD AREA(IMPLICIT OPERATOR) **************************************
        /// <summary>
        /// Operates to return a size.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static implicit operator Size(XmlSize x)
        {
            return x.ToSize();
        }

        /// <summary>
        /// Operates to return a new XmlSize object.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator XmlSize(Size s)
        {
            return new XmlSize(s);
        }
        #endregion
    }
}
