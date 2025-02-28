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
* 2010.05.15    CHOI 1.0	First release.
* 
*/
#endregion


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Message;
using System.Threading;

namespace Tsb.Fontos.Core.Util.Type
{
    /// <summary>
    /// DateTime type Utility Class
    /// </summary>
    public class DateTimeUtil : BaseUtil
    {
        new public const string ObjectID = "GNR-FTCO-UTL-DateTimeUtil";

        #region PROPERTY AREA **********************************
        // like in java System.currentTimeMillis: 1970 UTC
        static readonly DateTime DateTimeUTC1970 = new DateTime(1970, 1, 1);
        #endregion


        #region METHOD AREA (CONVERT TO DATETIME)***************
        /// <summary>
        /// Converts the specified string representation of a date and time to its DateTime equivalent. 
        /// The format of the string representation must match a specified format exactly.
        /// </summary>
        /// <param name="yyyyMMddHHmm">A string containing a date and time to convert. The format of string must match a yyyyMMddHHmm format exactly</param>
        /// <returns>A DateTime equivalent to the date and time contained in s as yyyyMMddHHmm format</returns>
        public static DateTime ToDateTimeFromyyyyMMddHHmm(string yyyyMMddHHmm)
        {
            DateTime rtnDateTime;
            try
            {
                rtnDateTime = DateTime.ParseExact(yyyyMMddHHmm, "yyyyMMddHHmm"
                                                        , System.Globalization.DateTimeFormatInfo.InvariantInfo
                                                        , System.Globalization.DateTimeStyles.None);
            }
            catch (Exception)
            {
                //MSG : The specified date time string [{0}] is unsupported format.	
                throw new TsbSysTypeException(DateTimeUtil.ObjectID, "MSG_FTCO_00128", DefaultMessage.NON_REG_WRD + yyyyMMddHHmm);
            }

            return rtnDateTime; 
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its DateTime equivalent. 
        /// The format of the string representation must match a specified format exactly.
        /// </summary>
        /// <param name="yyyyMMddHHmm">A string containing a date and time to convert. The format of string must match a yyyyMMddHHmmss format exactly</param>
        /// <returns>A DateTime equivalent to the date and time contained in s as yyyyMMddHHmm format</returns>
        public static Nullable<DateTime> ToDateTimeFromyyyyMMddHHmmss(string yyyyMMddHHmmss)
        {
            Nullable<DateTime> rtnDateTime = null;

            if (yyyyMMddHHmmss == null) return rtnDateTime;

            try
            {
                rtnDateTime = DateTime.ParseExact(yyyyMMddHHmmss, "yyyyMMddHHmmss"
                                                        , System.Globalization.DateTimeFormatInfo.InvariantInfo
                                                        , System.Globalization.DateTimeStyles.None);
            }
            catch (Exception)
            {
                //MSG : The specified date time string [{0}] is unsupported format.	
                throw new TsbSysTypeException(DateTimeUtil.ObjectID, "MSG_FTCO_00128", DefaultMessage.NON_REG_WRD + yyyyMMddHHmmss);
            }

            return rtnDateTime;
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its DateTime equivalent using the specified format.
        /// The format of the string representation must match the specified format exactly.
        /// </summary>
        /// <param name="dateString">Date time formatted string to convert</param>
        /// <param name="formatString">The expected format of string</param>
        /// <returns>A DateTime equivalent to the date and time contained in s as specified by format</returns>
        public static DateTime ToDateTime(string dateString, string formatString)
        {
            DateTime rtnDateTime;

            try
            {
                rtnDateTime = DateTime.ParseExact(dateString, formatString
                                                        , System.Globalization.DateTimeFormatInfo.InvariantInfo
                                                        , System.Globalization.DateTimeStyles.None);
            }
            catch (Exception)
            {
                //MSG : The specified date time string [{0}] is unsupported format.	
                throw new TsbSysTypeException(DateTimeUtil.ObjectID, "MSG_FTCO_00128", DefaultMessage.NON_REG_WRD + dateString + "," + formatString);
            }

            return rtnDateTime;
        }
        #endregion


        #region METHOD AREA (CONVERT TO STRING)*****************
        /// <summary>
        /// Converts the specified DateTime to yyyyMMdd formated string. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <returns>yyyyMMdd formated string without any separator</returns>
        public static string ToStringyyyyMMdd(DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }

        /// <summary>
        /// Converts the specified DateTime to yyyyMMdd formated string with "/" character as a separator. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <returns>yyyyMMdd formated string with "/" separator</returns>
        public static string ToStringyyyyMMddWithSlash(DateTime date)
        {
            return StringUtil.CombineWithSeparator("/", date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd"));
        }

        /// <summary>
        /// Converts the specified DateTime to yyyyMMdd formated string with date unit separator. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <param name="separator">A separator character string</param>
        /// <returns>yyyyMMdd formated string with date unit separator</returns>
        public static string ToStringyyyyMMddWithSeparator(DateTime date, string separator)
        {
            return StringUtil.CombineWithSeparator(separator, date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd"));
        }
        
        /// <summary>
        /// Converts the specified DateTime to HHmm (Time Only) formated string. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <returns>HHmm (Time Only) formated string without any separator</returns>
        public static string ToStringHHmm(DateTime date)
        {
            return date.ToString("HHmm");
        }

        /// <summary>
        /// Converts the specified DateTime to HHmmss (Time Only) formated string. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <returns>HHmmss (Time Only) formated string without any separator</returns>
        public static string ToStringHHmmss(DateTime date)
        {
            return date.ToString("HHmmss");
        }

        /// <summary>
        /// Converts the specified DateTime to HHmm formated string with ":" character as a separator. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <returns>HHmm formated string with ":" character as a separator. </returns>
        public static string ToStringHHmmWithColon(DateTime date)
        {
            return StringUtil.CombineWithSeparator(":", date.ToString("HH"), date.ToString("mm"));
        }

        /// <summary>
        /// Converts the specified DateTime to HHmmss formated string with ":" character as a separator. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <returns>HHmmss formated string with ":" character as a separator. </returns>
        public static string ToStringHHmmssWithColon(DateTime date)
        {
            return StringUtil.CombineWithSeparator(":", date.ToString("HH"), date.ToString("mm"), date.ToString("ss"));
        }

        /// <summary>
        /// Converts the specified DateTime to HHmm formated string with a specified character separator. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <param name="separator">A separator character string</param>
        /// <returns>HHmm formated string with a specified character as a separator. </returns>
        public static string ToStringHHmmWithSeparator(DateTime date, string separator)
        {
            return StringUtil.CombineWithSeparator(separator, date.ToString("HH"), date.ToString("mm"));
        }

        /// <summary>
        /// Converts the specified DateTime to HHmmss formated string with ":" character as a separator. 
        /// </summary>
        /// <param name="date">A DateTime to convert</param>
        /// <param name="separator">A separator character string</param>
        /// <returns>HHmmss formated string with ":" character as a separator. </returns>
        public static string ToStringHHmmssWithSeparator(DateTime date, string separator)
        {
            return StringUtil.CombineWithSeparator(separator, date.ToString("HH"), date.ToString("mm"), date.ToString("ss"));
        }
        #endregion


        #region METHOD AREA (DATE TIME FORMAT)******************
        /// <summary>
        /// Returns a DateTime which time is set with 00:00:00
        /// </summary>
        /// <param name="date">A DateTime to get</param>
        /// <returns>DateTime which time is initialized</returns>
        public static DateTime GetDateOnly(DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// Returns TimeSpan of a specified DateTime
        /// </summary>
        /// <param name="date">A DateTime to get time spane</param>
        /// <returns>Only time (TimeSpan) of a specified DateTime</returns>
        public static TimeSpan GetTimeOnly(DateTime date)
        {
            return date.TimeOfDay;
        }
        #endregion


        #region METHOD AREA (COMPARE DATETIME)******************
        /// <summary>
        /// Returns true if date1 date time is earlier than t2.
        /// </summary>
        /// <param name="date1">The first DateTime</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is earlier than date2.</returns>
        public static bool IsEarlier(DateTime date1, DateTime date2)
        {
            int rValue = default(int);

            rValue = DateTime.Compare(date1, date2);

            if (rValue < 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// Returns true if date1 date time is earlier than t2.
        /// </summary>
        /// <param name="date1">A string containing a date and time to compare. The format of string must match a yyyyMMddHHmm format exactly</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is earlier than date2.</returns>
        public static bool IsEarlier(string date1, DateTime date2)
        {
            return IsEarlier(DateTimeUtil.ToDateTimeFromyyyyMMddHHmm(date1), date2);
        }

        /// <summary>
        /// Returns true if date1 date time is earlier or same than date2.
        /// </summary>
        /// <param name="date1">The first DateTime</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is earlier or same than date2.</returns>
        public static bool IsEarlierOrSame(DateTime date1, DateTime date2)
        {
            int rValue = default(int);

            rValue = DateTime.Compare(date1, date2);

            if (rValue <= 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if date1 date time is earlier or same than date2.
        /// </summary>
        /// <param name="date1">A string containing a date and time to compare. The format of string must match a yyyyMMddHHmm format exactly</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is earlier or same than date2.</returns>
        public static bool IsEarlierOrSame(string date1, DateTime date2)
        {
            return IsEarlierOrSame(DateTimeUtil.ToDateTimeFromyyyyMMddHHmm(date1), date2);
        }

        /// <summary>
        /// Returns true if date1 date time is the same time as date2.
        /// </summary>
        /// <param name="date1">The first DateTime</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is the same time as date2.</returns>
        public static bool IsSameTime(DateTime date1, DateTime date2)
        {
            int rValue = default(int);

            rValue = DateTime.Compare(date1, date2);

            if (rValue == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if date1 date time is the same time as date2.
        /// </summary>
        /// <param name="date1">A string containing a date and time to compare. The format of string must match a yyyyMMddHHmm format exactly</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is the same time as date2.</returns>
        public static bool IsSameTime(string date1, DateTime date2)
        {
            return IsSameTime(DateTimeUtil.ToDateTimeFromyyyyMMddHHmm(date1), date2);
        }

        /// <summary>
        /// Returns true if date1 date time is later than date2.
        /// </summary>
        /// <param name="date1">The first DateTime</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is later than date2.</returns>
        public static bool IsLater(DateTime date1, DateTime date2)
        {
            int rValue = default(int);

            rValue = DateTime.Compare(date1, date2);

            if (rValue > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if date1 date time is later than date2.
        /// </summary>
        /// <param name="date1">A string containing a date and time to compare. The format of string must match a yyyyMMddHHmm format exactly</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is later than date2.</returns>
        public static bool IsLater(string date1, DateTime date2)
        {
            return IsLater(DateTimeUtil.ToDateTimeFromyyyyMMddHHmm(date1), date2);
        }


        /// <summary>
        /// Returns true if date1 date time is later or same than date2.
        /// </summary>
        /// <param name="date1">The first DateTime</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is later or same than date2.</returns>
        public static bool IsLaterOrSame(DateTime date1, DateTime date2)
        {
            int rValue = default(int);

            rValue = DateTime.Compare(date1, date2);

            if (rValue >= 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns true if date1 date time is later or same than date2.
        /// </summary>
        /// <param name="date1">A string containing a date and time to compare. The format of string must match a yyyyMMddHHmm format exactly</param>
        /// <param name="date2">The second DateTime</param>
        /// <returns>true if date1 date time is later or same than date2.</returns>
        public static bool IsLaterOrSame(string date1, DateTime date2)
        {
            return IsLaterOrSame(DateTimeUtil.ToDateTimeFromyyyyMMddHHmm(date1), date2);
        }

        /// <summary>
        /// Returns the current time in milliseconds. 
        /// Note that while the unit of time of the return value is a millisecond, 
        /// the granularity of the value depends on the underlying operating system 
        /// and may be larger. 
        /// For example, 
        /// many operating systems measure time in units of tens of milliseconds. 
        /// </summary>
        /// <returns>the difference, measured in milliseconds, 
        /// between the current time and midnight, January 1, 1970 UTC.
        /// </returns>
        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - DateTimeUTC1970).TotalMilliseconds;
        }
        #endregion


        #region METHOD AREA (DAY OF WEEK)***********************
        /// <summary>
        /// Returns a List collection of datetime that is the specified day of week between the specified start date and end date.
        /// If the specified day of week is not found, returns empty list (count is 0)
        /// </summary>
        /// <returns>
        /// A List collection of DateTime type that containes a specified day of week.
        /// TsbSysTypeException(MSG CODE : MSG_FTCO_00152) will be thrown, if the end date is earlier than then start date.
        /// </returns>
        public static List<DateTime> DayOfWeekDates(DateTime startDate, DateTime endDate, DayOfWeek dayOfWeek)
        {
            if (DateTimeUtil.IsEarlier(endDate,startDate))
            {
                //MSG_FTCO_00152:The {0} date({1}) is earlier than the {2} date({3}).
                throw new TsbSysTypeException(ObjectID, "MSG_FTCO_00152", 
                    "WRD_FTCO_endDate", 
                    DefaultMessage.NON_REG_WRD+endDate.ToLongDateString(),
                    "WRD_FTCO_startDate",
                    DefaultMessage.NON_REG_WRD+startDate.ToLongDateString()
                    );
            }

            List<DateTime> rtnList = new List<DateTime>();
            DateTime tempDate = startDate;
            do
            {
                if (tempDate.DayOfWeek == dayOfWeek)
                {
                    rtnList.Add(tempDate);
                }
                tempDate = tempDate.AddDays(1);

            } while (DateTimeUtil.IsEarlierOrSame(tempDate,endDate));

            return rtnList;
        }
        #endregion



    }
}
