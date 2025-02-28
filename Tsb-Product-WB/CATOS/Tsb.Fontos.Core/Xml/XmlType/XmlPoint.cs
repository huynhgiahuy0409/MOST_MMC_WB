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
    /// Defines the point of the CellElement.
    /// </summary>
    public class XmlPoint
    {
        #region FIELD AREA ***************************************
        private Point _point = Point.Empty;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [XmlAttribute("Value")]
        public string Value
        {
            get { return XmlSerializationHelper.PointToString(_point); }
            set
            {
                try
                {
                    _point = XmlSerializationHelper.StringToPoint(value);
                }
                catch (Exception)
                {
                    _point = Point.Empty;
                }
            }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Default constructor.
        /// </summary>
        public XmlPoint()
        {
        }

        /// <summary>
        /// Initialize the XmlPoint object.
        /// </summary>
        /// <param name="font"></param>
        public XmlPoint(Point s)
        {
            _point = s;
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Returns a point.
        /// </summary>
        /// <returns></returns>
        public Point ToValue()
        {
            return _point;
        }
        #endregion

        #region METHOD AREA(IMPLICIT OPERATOR) **************************************
        /// <summary>
        /// Operates to return a point.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static implicit operator Point(XmlPoint x)
        {
            return x.ToValue();
        }

        /// <summary>
        /// Operates to return a new XmlPoint object.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator XmlPoint(Point s)
        {
            return new XmlPoint(s);
        }
        #endregion
    }
}
