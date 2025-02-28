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
* 2010.05.13    CHOI 1.0	First release.
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
using System.IO;
using System.IO.Compression;

namespace Tsb.Fontos.Core.Util.Converter
{
    /// <summary>
    /// Encoding Utility class
    /// </summary>
    public class EncodeUtil : BaseUtil
    {
        #region FIELD/PROPERTY AREA*****************************
        new public const string ObjectID = "GNR-FTCO-UTL-EncodeUtil";
        #endregion


        #region METHOD AREA (Convert string to byte array)******
        /// <summary>
        /// Converts the specified String, which is encoded as UTF16(Default Unicode), to an equivalent bytes array
        /// </summary>
        /// <param name="source"> A string, which is encoded as UTF16(Default Unicode)</param>
        /// <returns>Bytes array</returns>
        public static byte[] UTF16ToBytes(string source)
        {
            byte[] rtnBytes = null;

            try
            {
                rtnBytes = Encoding.Unicode.GetBytes(source);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + source);
            }
            return rtnBytes;
        }

        /// <summary>
        /// Converts the specified String, which is encoded as UTF8, to an equivalent bytes array
        /// </summary>
        /// <param name="source"> A string, which is encoded as UTF8</param>
        /// <returns>Bytes array</returns>
        public static byte[] UTF8ToBytes(string source)
        {
            byte[] rtnBytes = null;

            try
            {
                rtnBytes = Encoding.UTF8.GetBytes(source);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + source);
            }
            return rtnBytes;
        }

        /// <summary>
        /// Converts the specified String, which is encoded as Base64, to an equivalent bytes array
        /// </summary>
        /// <param name="source"> A string, which is encoded as Base64</param>
        /// <returns>Bytes array</returns>
        public static byte[] Base64ToBytes(string source)
        {
            byte[] rtnBytes = null;

            try
            {
                rtnBytes = System.Convert.FromBase64String(source);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + source);
            }
            return rtnBytes;
        }

        /// <summary>
        /// Converts the specified String, which is encoded as ASCII, to an equivalent bytes array
        /// </summary>
        /// <param name="source"> A string, which is encoded as ASCII</param>
        /// <returns>Bytes array</returns>
        public static byte[] AsciiToBytes(string source)
        {
            byte[] rtnBytes = null;

            try
            {
                rtnBytes = Encoding.ASCII.GetBytes(source);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + source);
            }
            return rtnBytes;
        }

        #endregion


        #region METHOD AREA (Convert byte array to string)******
        /// <summary>
        /// Converts the specified byte array to an equivalent a string encoded with UTF16(Unicode) encoding
        /// </summary>
        /// <param name="source"> A byte array</param>
        /// <returns>A string encoded with UTF16(Unicode) encoding</returns>
        public static string BytesToUTF16(byte[] srcBytes)
        {
            return BytesToUTF16(srcBytes, srcBytes.Length);
        }

        /// <summary>
        /// Converts the specified byte array to an equivalent a string encoded with UTF16(Unicode) encoding
        /// </summary>
        /// <param name="source"> A byte array</param>
        /// <param name="readLength">Length to read</param>
        /// <returns>A string encoded with UTF16(Unicode) encoding</returns>
        public static string BytesToUTF16(byte[] srcBytes, int readLength)
        {
            string rtnStr = null;

            try
            {
                rtnStr = Encoding.Unicode.GetString(srcBytes, 0, readLength);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + srcBytes.ToString());
            }
            return rtnStr;
        }

        /// <summary>
        /// Converts the specified byte array to an equivalent a string encoded with UTF8 encoding
        /// </summary>
        /// <param name="srcBytes"> A byte array</param>
        /// <returns>A string encoded with UTF8 encoding</returns>
        public static string BytesToUTF8(byte[] srcBytes)
        {
            string rtnStr = null;

            try
            {
                rtnStr = Encoding.UTF8.GetString(srcBytes);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + srcBytes.ToString());
            }
            return rtnStr;
        }


        /// <summary>
        /// Converts the specified byte array to an equivalent a string encoded with UTF8 encoding
        /// </summary>
        /// <param name="srcBytes"> A byte array</param>
        /// <param name="readLength">Length to read</param>
        /// <returns>A string encoded with UTF8 encoding</returns>
        public static string BytesToUTF8(byte[] srcBytes, int readLength)
        {
            string rtnStr = null;

            try
            {
                rtnStr = Encoding.UTF8.GetString(srcBytes, 0, readLength);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + srcBytes.ToString());
            }
            return rtnStr;
        }

        /// <summary>
        /// Converts the specified byte array to an equivalent a string encoded with Base64 encoding
        /// </summary>
        /// <param name="source"> A byte array</param>
        /// <returns>A string encoded with Base64 encoding</returns>
        public static string BytesToBase64(byte[] srcBytes)
        {
            string rtnStr = null;

            try
            {
                rtnStr = System.Convert.ToBase64String(srcBytes);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + srcBytes.ToString());
            }
            return rtnStr;
        }

        /// <summary>
        /// Converts the specified byte array to an equivalent a string encoded with Ascii encoding
        /// </summary>
        /// <param name="source"> A byte array</param>
        /// <returns>A string encoded with Ascii encoding</returns>
        public static string BytesToAscii(byte[] srcBytes)
        {
            return BytesToAscii(srcBytes, srcBytes.Length);
        }

        /// <summary>
        /// Converts the specified byte array to an equivalent a string encoded with Ascii encoding
        /// </summary>
        /// <param name="source"> A byte array</param>
        /// <param name="readLength">Length to read</param>
        /// <returns>A string encoded with Ascii encoding</returns>
        public static string BytesToAscii(byte[] srcBytes, int readLength)
        {
            string rtnStr = null;

            try
            {
                rtnStr = Encoding.ASCII.GetString(srcBytes, 0, readLength);
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + srcBytes.ToString());
            }
            return rtnStr;
        }
        #endregion


        #region METHOD AREA (CONVERT STRING BASE64<->UTF16)*****
        /// <summary>
        /// Converts the specified String, which is encoded as UTF16(Default Unicode), to an equivalent string representation encoded with Base64
        /// </summary>
        /// <param name="source"> A string, which is encoded as UTF16(Default Unicode)</param>
        /// <returns>A string representation encoded with Base64</returns>
        public static string UTF16ToBase64(string source)
        {
            string rtnStr = null;
            
            try
            {
                rtnStr = System.Convert.ToBase64String(Encoding.Unicode.GetBytes(source));
            }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + source);
            }
            return rtnStr;
        }

        /// <summary>
        /// Converts the specified string, which is encode as Base64, to an equivalent string representation encoded with UTF16(Unicode)
        /// </summary>
        /// <param name="source">A string, which is encoded as Base64 encoding</param>
        /// <returns>A string encoded with UTF16(Unicode) encoding</returns>
        public static string Base64ToUTF16(string source)
        {
            string rtnStr = null;

            try
            {
                rtnStr = Encoding.Unicode.GetString(System.Convert.FromBase64String(source));
              }
            catch (Exception ex)
            {
                //MSG: The system encountered an error when converting [{0}] value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), EncodeUtil.ObjectID, "MSG_FTCO_00066", DefaultMessage.NON_REG_WRD + source);
            }
            return rtnStr;
        }
        #endregion


        #region METHOD AREA (CONVERT STRING HEX<->String)*****
        /// <summary>
        /// Converts the specified String, to an equivalent string representation encoded with Hex
        /// </summary>
        /// <param name="source"> A string, which is encoded as Hex</param>
        /// <returns>A string encoded with Hex encoding</returns>
        public static string StringToHex(string source)
        {
            int iTemp;
            string returnValue = "";
            char[] sourceChars = source.ToCharArray();

            if (sourceChars.Length == 0) return null;

            for (int i = 0; i < sourceChars.Length; i++)
            {
                iTemp = Convert.ToInt32(sourceChars[i]);

                if (iTemp < 16)
                {
                    returnValue += "0" + iTemp.ToString("X");
                }
                else
                {
                    returnValue += iTemp.ToString("X");
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Converts the specified String, to an equivalent string representation decoded with Hex
        /// </summary>
        /// <param name="source"> A string, which is decoded as Hex</param>
        /// <returns>A string encoded with Hex decoding</returns>
        public static string HexToString(string source)
        {
            string returnValue = "";

            if (source.Length == 0) return null;

            for (int i = 0; i < source.Length; i = i + 2)
            {
                returnValue += Convert.ToChar(int.Parse(source.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
            }

            return returnValue;
        }
        #endregion


        #region METHOD AREA (Compress string)
        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int count;

            while ((count = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, count);
            }
        }

        public static byte[] ZipString(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static string UnzipString(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        } 
        #endregion
    }
}
