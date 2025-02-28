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
* 2009.08.06    CHOI 1.0	First release.
* 
*/
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Exceptions.System;
using System.Drawing;
using Tsb.Fontos.Core.Log;
using System.Reflection;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Tsb.Fontos.Core.Util.Type
{
    /// <summary>
    /// Converting Utility class
    /// </summary>
    public class ConvertUtil : BaseUtil
    {
        new public const string ObjectID = "GNR-FTCO-UTL-ConvertUtil";
        
        /// <summary>
        /// Returns true or false using a specified boolean string ("Y","y", "Yes", "true"...)
        /// </summary>
        /// <param name="boolString"></param>
        /// <returns></returns>
        public static bool ToBoolean(string boolString)
        {
            bool rtnBoolean = false;
            
            if (string.IsNullOrEmpty(boolString)) return false;

            string temp = boolString.ToUpper();

            switch (temp)
            {
                case "1":
                case "Y":
                case "YES":
                case "TRUE":
                    rtnBoolean = true;
                    break;
                case "0":
                case "N":
                case "NO":
                case "FALSE":
                    rtnBoolean = false;
                    break;
                default:
                    //MSG:Value({0}) can not be converted to [1]
                    throw new TsbSysTypeException(ObjectID, "MSG_FTCO_00125", boolString, "WRD_FTCO_trueorfalse");
            }

            return rtnBoolean;
        }

        /// <summary>
        /// Returns true or false using a specified boolean string ("Y","y", "Yes", "true"...)
        /// </summary>
        /// <param name="boolString"></param>
        /// <returns></returns>
        public static bool ToBooleanNotException(string boolString)
        {
            bool rtnBoolean = false;
            string temp = boolString.ToUpper();

            switch (temp)
            {
                case "1":
                case "Y":
                case "YES":
                case "TRUE":
                    rtnBoolean = true;
                    break;
                default:
                    rtnBoolean = false;
                    break;
            }

            return rtnBoolean;
        }

        /// <summary>
        /// Returns true or false using a specified boolean string ("Y","y", "Yes", "true", 1, true...)
        /// </summary>
        /// <param name="boolString"></param>
        /// <returns></returns>
        public static bool ToBoolean(object data)
        {
            if (data == null) return false;
            string dataString = data.ToString().ToUpper();

            switch (dataString)
            {
                case "1":
                case "Y":
                case "YES":
                case "TRUE":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns true or false using a specified boolean string ("Y","y", "Yes", "true", 1, true...)
        /// </summary>
        /// <param name="boolString"></param>
        /// <returns></returns>
        public static Nullable<bool> ToNullableBoolean(object data)
        {
            if (data == null) return null;
            string dataString = data.ToString().ToUpper();

            switch (dataString)
            {
                case "1":
                case "Y":
                case "YES":
                case "TRUE":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns "Y" or "N" or null
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToYN(bool data)
        {
            if (data == true)
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }

        /// <summary>
        /// Returns "Y" or "N" or null
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToNullableYN(Nullable<bool> data)
        {
            if (data == true)
            {
                return "Y";
            }
            else if (data == false)
            {
                return "N";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns 0 or 1 or 2
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ToIntForYN(object data)
        {
            if (data == null) return 2;
            int rtnInt = 2;
            string dataString = data.ToString().ToUpper();

            switch (dataString)
            {
                case "1":
                case "Y":
                case "YES":
                case "TRUE":
                    rtnInt = 1;
                    break;
                case "0":
                case "N":
                case "NO":
                case "FALSE":
                    rtnInt = 0;
                    break;
                default:
                    rtnInt = 2;
                    break;
            }

            return rtnInt;
        }

        /// <summary>
        /// Returns 0 or 1 or 2
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToYNForInt(int data)
        {
            string rtnString = null;

            switch (data)
            {
                case 1:
                    rtnString = "Y";
                    break;
                case 0:
                    rtnString = "N";
                    break;
                default:
                    rtnString = null;
                    break;
            }

            return rtnString;
        }

		public static Color StringToColor(string colorString)
		{
			Color rtnColor = Color.Empty;
            int colorNumber = -1;
			try
			{
                if (Int32.TryParse(colorString, out colorNumber))
                {
                    rtnColor = ColorTranslator.FromWin32(colorNumber);
                }
                else
                {
                    rtnColor = Color.Empty;
                }
			}
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

			return rtnColor;
		}

		public static string ColorToString(Color color)
		{
			string rtnString = string.Empty;

			try
			{
				int colorNumber = ColorTranslator.ToWin32(color);
				rtnString = StringUtil.GetEmptyStringIsNull(colorNumber); //colorNumber.ToString(); 				
			}
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

			return rtnString;
		}


        public static string ToDescriptionString(Enum value)
        {
            string returnValue = string.Empty;

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
            {
                returnValue = attributes[0].Description;
            }

            return returnValue;
        }

        /// <summary>
        /// String convert to guid struct.
        /// Wrong guid string return Guid.Empty.
        /// </summary>
        /// <param name="guidString">Guid struct</param>
        /// <returns></returns>
        public static Guid StringToGuid(string guidString)
        {
            Guid rtnGuid = Guid.Empty;

            Regex regex = null;
            Match match = null;

            try
            {
                regex = new Regex("^[a-fA-F0-9]{8}-([a-fA-F0-9]{4}-){3}[a-fA-F0-9]{12}$");
                match = regex.Match(guidString);
                
                if (match.Success == true)
                {
                    rtnGuid = new Guid(guidString);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnGuid;
        }
    }
}
