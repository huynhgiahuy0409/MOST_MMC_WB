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
using Tsb.Fontos.Core.Util.Type;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Tsb.Fontos.Core.Log;
using System.Windows.Forms;
using System.Globalization;

namespace Tsb.Fontos.Core.Xml.XmlType
{
    /// <summary>
    /// Provides the functions to convert the properties of the CellElement to string(XML) or the other way.
    /// </summary>
    public class XmlSerializationHelper
    {
        #region METHOD AREA (COLOR) **************************************
        /// <summary>
        /// Convert a string to a color.
        /// </summary>
        /// <param name="valueString"></param>
        /// <param name="allowNull"></param>
        /// <returns></returns>
        public static Color StringToColor(string valueString, bool allowNull)
        {
            Color rtnColor = Color.Empty;
            try
            {
                if (allowNull == true && valueString == null)
                {
                    return Color.Transparent;
                }

                string[] valueArray = valueString.Split(',');
                rtnColor = Color.FromArgb(int.Parse(valueArray[0])
                    , int.Parse(valueArray[1])
                    , int.Parse(valueArray[2])
                    , int.Parse(valueArray[3]));
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnColor;
        }

        /// <summary>
        /// Convert a color to a string.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="allowNull"></param>
        /// <returns></returns>
        public static string ColorToString(Color color, bool allowNull)
        {
            string rtnString = string.Empty;

            try
            {
                if (allowNull == true)
                {
                    if (Color.Transparent.ToArgb() == color.ToArgb())
                    {
                        return null;
                    }
                }

                rtnString = color.A + "," + color.R + "," + color.G + "," + color.B;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnString;
        }
        #endregion

        #region METHOD AREA (SIZE) **************************************
        /// <summary>
        /// Convert a size to a string.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string SizeToString(Size size)
        {
            string rtnString = string.Empty;

            try
            {
                rtnString = size.Width + "," + size.Height;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnString;
        }

        /// <summary>
        /// Convert a string to a size.
        /// </summary>
        /// <param name="sizeStr"></param>
        /// <returns></returns>
        public static Size StringToSize(string sizeStr)
        {

            Size returValue = Size.Empty;
            try
            {
                string[] sizeArray = sizeStr.Split(',');
                returValue = new Size(int.Parse(sizeArray[0]), int.Parse(sizeArray[1]));
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return returValue;
        }
        #endregion

        #region METHOD AREA (POINT) **************************************
        /// <summary>
        /// Convert a point to a string.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static string PointToString(Point point)
        {
            string rtnString = string.Empty;

            try
            {
                rtnString = point.X + "," + point.Y;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnString;
        }

        /// <summary>
        /// Convert a string to a point.
        /// </summary>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        public static Point StringToPoint(string valueStr)
        {

            Point returValue = Point.Empty;
            try
            {
                string[] valueArray = valueStr.Split(',');
                returValue = new Point(int.Parse(valueArray[0]), int.Parse(valueArray[1]));
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return returValue;
        }
        #endregion

        #region METHOD AREA (POINT ARRAY) **************************************
        /// <summary>
        /// Convert a point array to a string.
        /// </summary>
        /// <param name="pointArray"></param>
        /// <returns></returns>
        public static string PointArrayToString(IList<Point> pointArray)
        {
            string rtnString = string.Empty;

            try
            {
                if (pointArray == null)
                {
                    rtnString = null;
                }
                else
                {
                    int length = pointArray.Count;
                    for (int i = 0; i < length; i++)
                    {
                        if (i != 0)
                        {
                            rtnString = rtnString + " ";
                        }
                        rtnString = rtnString + pointArray[i].X + "," + pointArray[i].Y;
                    }
                }
                
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnString;
        }

        /// <summary>
        /// Convert a string to a point array.
        /// </summary>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        public static IList<Point> StringToPointArray(string valueStr)
        {

            IList<Point> returValue = new List<Point>();
            try
            {
                if (string.IsNullOrEmpty(valueStr) == true)
                {
                    returValue = null;
                }
                else
                {
                    string[] valueGroupArray = valueStr.Split(' ');

                    foreach (string pointStr in valueGroupArray)
                    {
                        string[] valueArray = pointStr.Split(',');
                        returValue.Add(new Point(int.Parse(valueArray[0]), int.Parse(valueArray[1])));
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return returValue;
        }
        #endregion

        #region METHOD AREA (FONT) **************************************
        /// <summary>
        /// Deseralize a font.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Font DeserializeFont(string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
            return converter.ConvertFromString(null, value) as Font;
        }


        /// <summary>
        /// Deseralize a font.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Font DeserializeDefaultCultureFont(string value)
        {
            return DeserializeFont(new CultureInfo("en-US"), value);
        }

        /// <summary>
        /// Deseralize a font.
        /// </summary>
        /// <param name="culture">
        ///  A System.Globalization.CultureInfo. If null is passed, the current culture
        ///  is assumed.
        /// </param>
        /// <param name="value">The System.String to convert.</param>
        /// <returns> An System.Object that represents the converted text.</returns>
        public static Font DeserializeFont(CultureInfo culture, string value)
        {
            Font font = null;

            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                font = converter.ConvertFromString(null, culture, value) as Font;
            }
            catch (Exception ex)
            {
                font = new System.Drawing.Font("Tahoma", 9.0f);
                GeneralLogger.Error(ex);
            }

            return font;
        }

        /// <summary>
        /// Serialize a font.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeFont(Font value)
        {
            if (value == null)
            {
                return null;
            }

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));

            return converter.ConvertToString(value);
        }

        /// <summary>
        /// Serialize a font.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeDefaultCultureFont(Font value)
        {
            return SerializeFont(new CultureInfo("en-US"), value);
        }

        /// <summary>
        /// Serialize a font.
        /// </summary>
        /// <param name="culture">
        ///  A System.Globalization.CultureInfo. If null is passed, the current culture
        ///  is assumed.
        /// </param>
        /// <param name="value">The System.Object to convert.</param>
        /// <returns>An System.Object that represents the converted value.</returns>
        public static string SerializeFont(CultureInfo culture, Font value)
        {
            if (value == null)
            {
                return null;
            }

            String fontStr = String.Empty;

            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                fontStr = converter.ConvertToString(null, culture, value);
            }
            catch (Exception ex)
            {
                fontStr = "Tahoma, 9.0pt";
                GeneralLogger.Error(ex);

                throw ex;
            }

            return fontStr;
        }
        #endregion
        
        #region METHOD AREA (PADDING) **************************************
        /// <summary>
        /// Convert a padding to a string.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static string PaddingToString(Padding point)
        {
            string rtnString = string.Empty;

            try
            {
                //int left, int top, int right, int bottom
                rtnString = point.Left
                    + "," + point.Top
                    + "," + point.Right
                    + "," + point.Bottom;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnString;
        }

        /// <summary>
        /// Convert a string to a padding.
        /// </summary>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        public static Padding StringToPadding(string valueStr)
        {

            Padding returValue = Padding.Empty;
            try
            {
                string[] valueArray = valueStr.Split(',');
                if (valueArray.Length == 1)
                {
                    returValue = new Padding(int.Parse(valueArray[0]));
                }
                else if (valueArray.Length == 4)
                {
                    returValue = new Padding(int.Parse(valueArray[0])
                    , int.Parse(valueArray[1])
                    , int.Parse(valueArray[2])
                    , int.Parse(valueArray[3]));
                }
                else
                {
                    throw new Exception("Not supported format for padding string.");
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return returValue;
        }
        #endregion
    }
}
