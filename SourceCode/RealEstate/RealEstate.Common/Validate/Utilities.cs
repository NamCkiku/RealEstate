using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Validate
{
    public class Utilities
    {
        /// <summary>
        /// Calculate Total Pages
        /// </summary>
        /// <param name="numberOfRecords"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static int CalculateTotalPages(long numberOfRecords, Int32 pageSize)
        {
            long result;
            int totalPages;

            Math.DivRem(numberOfRecords, pageSize, out result);

            if (result > 0)
                totalPages = (int)((numberOfRecords / pageSize)) + 1;
            else
                totalPages = (int)(numberOfRecords / pageSize);

            return totalPages;

        }

        /// <summary>
        /// Check if date is a valid format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Boolean IsDate(string date)
        {
            DateTime dateTime;
            return DateTime.TryParse(date, out dateTime);
        }

        /// <summary>
        /// IsNumeric
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsNumeric(object entity)
        {
            if (entity == null) return false;

            int result;
            return int.TryParse(entity.ToString(), out result);
        }

        /// <summary>
        /// IsDouble
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDouble(object entity)
        {
            if (entity == null) return false;

            string e = entity.ToString();

            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = e.IndexOf(".", i)) != -1)
            {
                i += ".".Length;
                count++;
            }
            if (count > 1) return false;

            e = e.Replace(".", "");

            int result;
            return int.TryParse(e, out result);
        }

        public static List<String> Message(string message)
        {
            List<String> returnMessage = new List<String>();
            returnMessage.Add(message);
            return returnMessage;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            if (str == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9') || (str[i] >= 'A' && str[i] <= 'z' || (str[i] == '.' || str[i] == '_')))
                    sb.Append(str[i]);
            }

            return sb.ToString();
        }

    }
}
