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
    /// Defines the font of the CellElement.
    /// </summary>
    class XmlFont
    {
        #region FIELD AREA ***************************************
        private Font _font = new Font("Tahoma", 7.0f, FontStyle.Regular);
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        [XmlAttribute]
        public string Value
        {
            get { return XmlSerializationHelper.SerializeDefaultCultureFont(_font); }
            set
            {
                _font = XmlSerializationHelper.DeserializeDefaultCultureFont(value);
            }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Default constructor.
        /// </summary>
        public XmlFont() 
        { 
        }

        /// <summary>
        /// Initialize the XmlFont object.
        /// </summary>
        /// <param name="font"></param>
        public XmlFont(Font font)
        {
            _font = font;
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Returns a font.
        /// </summary>
        /// <returns></returns>
        public Font ToValue()
        {
            return _font;
        }
        #endregion

        #region METHOD AREA(IMPLICIT OPERATOR) **************************************
        /// <summary>
        /// Operates to return a font.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static implicit operator Font(XmlFont x)
        {
            return x.ToValue();
        }

        /// <summary>
        /// Operates to return a new XmlFont object.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static implicit operator XmlFont(Font c)
        {
            return new XmlFont(c);
        }
        #endregion
         

         

         
    }
}
