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
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Util.Type
{
    /// <summary>
    /// String Utility class
    /// </summary>
    public class StringUtil : BaseUtil
    {
        new public const string ObjectID = "GNR-FTCO-UTL-StringUtil";

        /// <summary>
        /// Returns combined string with dot(".")
        /// </summary>
        /// <param name="args">string elements array to combine</param>
        /// <returns>combined string with dot</returns>
        public static string CombineDot(params string[] args)
        {
            StringBuilder builder = new StringBuilder(args.Length*2);

            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (i != 0)
                        builder.Append(".");

                    builder.Append(args[i]);

                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns combined string with dash("-")
        /// </summary>
        /// <param name="args">string elements array to combine</param>
        /// <returns>combined string with dash</returns>
        public static string CombineWithDash(params string[] args)
        {
            return StringUtil.CombineWithSeparator("-", args);
        }


        /// <summary>
        /// Returns combined string with a specified separator character
        /// </summary>
        /// <param name="separator">A separator character</param>
        /// <param name="args">string elements array to combine</param>
        /// <returns>combined string with a specified separator</returns>
        public static string CombineWithSeparator(string separator, params string[] args)
        {
            StringBuilder builder = new StringBuilder(args.Length * 2);

            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (i != 0)
                        builder.Append(separator);

                    builder.Append(args[i]);

                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return builder.ToString();
        }




        /// <summary>
        /// Returns string array which is splitted by a specified separator
        /// </summary>
        /// <param name="toSplitString">A target string to split</param>
        /// <param name="separtorChar">character to separate</param>
        /// <returns>combined string with dot</returns>
        public static string[] SplitWithSepartor(string toSplitString, char separtorChar)
        {
            return toSplitString.Split(separtorChar);
        }

        /// <summary>
        /// Returns object.ToString() or string.Empty case of null object.
        /// </summary>
        /// <param name="obj">A target object</param>
        /// <returns>converted string value</returns>
        public static string GetEmptyStringIsNull(object obj)
        {
            string str = string.Empty;
            str = (obj == null) ? string.Empty : obj.ToString();
            return str;
        }

        /// <summary>
        /// Add spaces to the right of a string to achieve a desired totalLength
        /// </summary>
        /// <param name="target">Target string to add</param>
        /// <param name="totalLength">Total length of string to return</param>
        /// <returns>String that spaces are added to the right</returns>
        public static string PadSpaceRight(string target, int totalLength)
        {
            string str = string.Empty;

            if (target.Length > totalLength)
            {
                str = target.Substring(0, totalLength);
            }
            else
            {
                str = target.PadRight(totalLength, ' ');
            }
            return str;
        }

        /// <summary>
        /// Add spaces to the left of a string to achieve a desired totalLength
        /// </summary>
        /// <param name="target">Target string to add</param>
        /// <param name="totalLength">Total length of string to return</param>
        /// <returns>String that spaces are added to the left</returns>
        public static string PadSpaceLeft(string target, int totalLength)
        {
            string str = string.Empty;

            if (target.Length > totalLength)
            {
                str = target.Substring(0, totalLength);
            }
            else
            {
                str = target.PadLeft(totalLength, ' ');
            }
            return str;
        }

        
        /// <summary>
        /// Converts one character string to ascii code integer value
        /// </summary>
        /// <param name="argChar">A character string to convert into ascii code integer</param>
        /// <returns>A converted ascii code integer value</returns>
        public static int ToAscii(string argStr)
        {
            return ToAscii(Convert.ToChar(argStr));
        }

        /// <summary>
        /// Converts character to ascii code integer value
        /// </summary>
        /// <param name="argChar">A character value to convert into ascii code integer</param>
        /// <returns>A converted ascii code integer value</returns>
        public static int ToAscii(char argChar)
        {
            int rtnASC = -1;
            int tempASC;
            byte[] buffer;
            char[] charArr;
            Encoding fileIOEncoding;

            try
            {
                tempASC= Convert.ToInt32(argChar);
            
                if (tempASC < 0x80)
                {
                    rtnASC = tempASC;
                }
                else
                {
                    fileIOEncoding = Encoding.Default;
                
                    charArr = new char[] { argChar };
                    
                    if (fileIOEncoding.IsSingleByte)
                    {
                        buffer = new byte[1];
                        fileIOEncoding.GetBytes(charArr, 0, 1, buffer, 0);
                        rtnASC = buffer[0];
                    }
                    else
                    {
                        buffer = new byte[2];

                        if (fileIOEncoding.GetBytes(charArr, 0, 1, buffer, 0) == 1)
                        {
                            rtnASC = buffer[0];
                        }
                        else
                        {
                            if (BitConverter.IsLittleEndian)
                            {
                                byte tempNum = buffer[0];
                                buffer[0] = buffer[1];
                                buffer[1] = tempNum;
                            }

                            rtnASC = BitConverter.ToInt16(buffer, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), StringUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + argChar);
            }
            return rtnASC;
        }
    }
}
