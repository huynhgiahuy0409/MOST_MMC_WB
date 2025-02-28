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
    /// Defines the color of the CellElement.
    /// </summary>
    public class XmlColor
    {
        #region FIELD AREA ***************************************
        private Color _color = Color.Black;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [XmlAttribute("Value")]
        public string Value
        {
            get { return XmlSerializationHelper.ColorToString(_color, false); }
            set
            {
                try
                {
                    _color = XmlSerializationHelper.StringToColor(value, false);
                }
                catch (Exception)
                {
                    _color = Color.Black;
                }
            }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Default constructor.
        /// </summary>
        public XmlColor()
        { 
        }

        /// <summary>
        /// Initialize the XmlColor object.
        /// </summary>
        /// <param name="c"></param>
        public XmlColor(Color c)
        { 
            _color = c; 
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Returns a color.
        /// </summary>
        /// <returns></returns>
        public Color ToColor()
        {
            return _color;
        }
        #endregion

        #region METHOD AREA(IMPLICIT OPERATOR) **************************************
        /// <summary>
        /// Operates to return the value of the ToColor method.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static implicit operator Color(XmlColor x)
        {
            return x.ToColor();
        }

        /// <summary>
        /// Operates to return a new XmlColor object.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static implicit operator XmlColor(Color c)
        {
            return new XmlColor(c);
        }
        #endregion

    }
}
