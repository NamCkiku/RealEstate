using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Common.Helper
{
    /// <summary>
    /// Class contain DateTime utility
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  6/7/2017   created
    /// </Modified>
    public class DateTimeHelper
    {
        /// <summary>
        /// Check a string is a Date Type
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static DateTime? ParseDate(string date, CultureInfo culture)
        {
            try { return DateTime.ParseExact(date, culture.DateTimeFormat.ShortDatePattern, culture); }
            catch { return null; }
        }

        /// <summary>
        /// Check a string is a Date Type in Vietnamese culture.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ParseDate(string date)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            return ParseDate(date, culture);
        }

        /// <summary>
        /// Parse ngay thang theo format dau vao.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        public static DateTime? ParseExactDateTime(string date, string format, IFormatProvider provider)
        {
            try { return DateTime.ParseExact(date, format, provider); }
            catch { return null; }
        }

        /// <summary>
        /// Parse ngay thang theo format dau vao.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        public static DateTime? ParseExactDateTime(string date, string format)
        {
            return ParseExactDateTime(date, format, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Check time and date valid
        /// </summary>
        /// <param name="time">Time</param>
        /// <param name="date">Date</param>
        /// <param name="culture">Culture vi-VN or en-US</param>
        /// <returns></returns>
        public static DateTime? ParseDateTime(string time, string date, CultureInfo culture)
        {
            try
            {
                string dt = string.Empty;
                if (string.IsNullOrEmpty(time))
                {
                    dt = date;
                }
                else
                {
                    dt = string.Format("{0} {1}", time, date);
                }

                return DateTime.Parse(dt, culture, DateTimeStyles.NoCurrentDateDefault);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Parses the date time.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        public static DateTime? ParseDateTime(string time, string date)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            return ParseDateTime(time, date, culture);
        }

        /// <summary>
        /// Doi so phut ve dang gio:phut.
        /// </summary>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  6/7/2017   created
        /// </Modified>
        public static string ConvertTime(object minutes)
        {
            string ret = string.Empty;
            try
            {
                if (minutes == null) return ret;

                int minute = Convert.ToInt32(minutes);
                ret = string.Format("{0}h:{1}", (minute / 60), (minute % 60));
            }
            catch
            {
                ret = string.Empty;
            }
            return ret;
        }

        public static int DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var span = (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            double unixTime = span.TotalSeconds;
            return (int)unixTime;
        }
        public static byte[] Serialize<T>(T item)
        {
            byte[] b;

            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, item);
                b = stream.ToArray();
            }

            return b;
        }

        public static int DateTimeToUnixTimestampUtc(DateTime dateTime)
        {
            var span = (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            double unixTime = span.TotalSeconds;
            return (int)unixTime;
        }
    }
}
