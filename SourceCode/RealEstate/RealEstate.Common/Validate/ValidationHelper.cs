using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RealEstate.Common.Validate
{
    /// <summary>
    /// The validation helper.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// The is valid email.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The is valid email.
        /// </returns>
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([0-9a-z]+[-._+&])*[0-9a-z]+@([-0-9a-z]+[.])+[a-z]{2,6}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// The is valid url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The is valid url.
        /// </returns>
        public static bool IsValidURL(string url)
        {
            return Regex.IsMatch(url, @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$");
        }

        /// <summary>
        /// The is valid int.
        /// </summary>
        /// <param name="intstr">
        /// The intstr.
        /// </param>
        /// <returns>
        /// The is valid int.
        /// </returns>
        public static bool IsValidInt(string intstr)
        {
            int value;
            return int.TryParse(intstr, out value);
        }

        /// <summary>
        /// Validate for phonenumber
        /// </summary>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^([0-9() +-]){9,15}$");
        }

        /// <summary>
        /// Validate EmailAddress
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static Boolean ValidateEmailAddress(object emailAddress)
        {
            //string regexPattern =  @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            //string regexPattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

            string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
                       + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                       + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                       + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                       + @"[a-zA-Z]{2,}))$";

            if (emailAddress == null) return true;
            return Regex.Match(emailAddress.ToString(), pattern).Success;
        }

        /// <summary>
        /// Validate URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Boolean ValidateURL(object url)
        {

            if (url == null) return true;

            string testUrl = url.ToString();

            Uri tryuri = null;
            return Uri.TryCreate(testUrl, UriKind.Absolute, out tryuri);

        }


        /// <summary>
        /// Validate Required Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateRequired(object entity)
        {
            if (entity == null) return false;
            if (entity.ToString().Length == 0) return false;
            return true;
        }

        /// <summary>
        /// Validate Required Entity Guid
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateRequiredGuid(object entity)
        {
            if (entity == null) return false;

            Guid testGuid;
            if (Guid.TryParse(entity.ToString(), out testGuid) == false) return false;
            if (testGuid == Guid.Empty) return false;

            return true;
        }


        /// <summary>
        /// Validate Length
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Boolean ValidateLength(object entity, int maxLength)
        {
            if (entity == null) return true;
            if (entity.ToString().Length > maxLength) return false;
            return true;
        }

        /// <summary>
        /// Validate Greater Than Zero
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateGreaterThanZero(object entity)
        {
            if (entity == null) return false;

            if (IsInteger(entity) == false) return false;

            int test = Convert.ToInt32(entity);
            if (test < 1) return false;

            return true;
        }

        /// <summary>
        /// Validate Decimal Greater Than Zero
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateDecimalGreaterThanZero(object entity)
        {
            if (entity == null) return false;

            if (IsDecimal(entity) == false) return false;

            decimal test = Convert.ToDecimal(entity);
            if (test < 1) return false;

            return true;
        }

        /// <summary>
        /// Validate Decimal Is Not Zero
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean ValidateDecimalIsNotZero(object entity)
        {
            if (entity == null) return false;

            if (IsDecimal(entity) == false) return false;

            decimal test = Convert.ToDecimal(entity);
            if (test == 0) return false;

            return true;
        }


        /// <summary>
        /// IsInteger
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsInteger(object entity)
        {
            if (entity == null) return false;

            int result;
            return int.TryParse(entity.ToString(), out result);
        }

        /// <summary>
        /// IsDecimal
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDecimal(object entity)
        {
            if (entity == null) return false;

            decimal result;
            return decimal.TryParse(entity.ToString(), out result);
        }

        /// <summary>
        /// Is Date
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDate(object entity)
        {
            if (entity == null) return false;
            return Utilities.IsDate(entity.ToString());
        }

        /// <summary>
        /// Is Date or Null Date
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDateOrNullDate(object entity)
        {
            if (entity == null) return true;
            return Utilities.IsDate(entity.ToString());
        }

        /// <summary>
        /// Is Date Greater than default Date
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDateGreaterThanDefaultDate(object entity)
        {
            if (entity == null) return false;
            if (Utilities.IsDate(entity.ToString()) == false) return false;

            DateTime testDate = Convert.ToDateTime(entity.ToString());
            long test = testDate.Ticks;
            if (test == 0) return false;

            return true;

        }

        /// <summary>
        /// Is Date Greater than default Date
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Boolean IsDateGreaterThanOrEqualToToday(object entity)
        {
            if (entity == null) return false;
            if (Utilities.IsDate(entity.ToString()) == false) return false;

            DateTime testDate = Convert.ToDateTime(entity.ToString());
            if (testDate < DateTime.Today) return false;

            return true;

        }

    }
}
