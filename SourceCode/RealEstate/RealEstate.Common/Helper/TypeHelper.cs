using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Helper
{
    /// <summary>
    /// The type helper.
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Converts an object to a type.
        /// </summary>
        /// <param name="value">
        /// Object to convert
        /// </param>
        /// <param name="type">
        /// Type to convert to e.g. System.Guid
        /// </param>
        /// <returns>
        /// The convert object to type.
        /// </returns>
        public static object ConvertObjectToType(object value, string type)
        {
            Type convertType;

            try
            {
                convertType = Type.GetType(type, true, true);
            }
            catch
            {
                convertType = Type.GetType("System.Guid", false);
            }

            if (value.GetType().ToString() == "System.String")
            {
                switch (convertType.ToString())
                {
                    case "System.Guid":

                        // do a "manual conversion" from string to Guid
                        return new Guid(Convert.ToString(value));
                    case "System.Int32":
                        return Convert.ToInt32(value);
                    case "System.Int64":
                        return Convert.ToInt64(value);
                }
            }

            return Convert.ChangeType(value, convertType);
        }

        /// <summary>
        /// Gets an Int from an Object value
        /// </summary>
        /// <param name="expression">
        /// </param>
        /// <returns>
        /// The valid int.
        /// </returns>
        public static int ValidInt(object expression)
        {
            int value = 0;

            if (expression != null)
            {
                try
                {
                    int.TryParse(expression.ToString(), out value);
                }
                catch
                {
                }
            }

            return value;
        }

        /// <summary>
        /// The verify int 32.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <returns>
        /// The verify int 32.
        /// </returns>
        public static int VerifyInt32(object o)
        {
            return Convert.ToInt32(o);
        }

        /// <summary>
        /// The verify bool.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        /// <returns>
        /// The verify bool.
        /// </returns>
        public static bool VerifyBool(object o)
        {
            return Convert.ToBoolean(o);
        }

        /// <summary>
        /// Converts the object to the class (T) or returns null if it's not 
        /// an instance of that class or instance is null.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="instance">
        /// </param>
        /// <returns>
        /// </returns>
        public static T ToClass<T>(this object instance) where T : class
        {
            if (instance != null && instance is T)
            {
                return instance as T;
            }

            return null;
        }

        /// <summary>
        /// Converts an object to Type using the Convert.ChangeType() call.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="instance">
        /// </param>
        /// <returns>
        /// </returns>
        public static T ToType<T>(this object instance)
        {
            return (T)Convert.ChangeType(instance, typeof(T));
        }

        /// <summary>
        /// http://codereview.stackexchange.com/questions/17982/improve-my-trycastt-method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryCast<T>(this object value, out T result)
        {
            if (value is T)
            {
                result = (T)value;
                return true;
            }

            var from = value.GetType().GUID;
            var to = typeof(T).GUID;
            var converter = new Converter<object, T>(s => (T)s);
            if (converter != null)
            {
                result = (T)converter.Convert(value);
                return true;
            }

            result = default(T);
            return false;
        }
        /// <summary>
        /// The to generic list.
        /// </summary>
        /// <param name="listObjects">
        /// The list objects.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public static List<T> ToGenericList<T>(this IList listObjects)
        {
            var convertedList = new List<T>(listObjects.Count);

            foreach (object listObject in listObjects)
            {
                convertedList.Add((T)listObject);
            }

            return convertedList;
        }

        /// <summary>
        /// Converts an object to a different object (class) by copying fields (if they exist).
        /// Used to convert annonomous objects to strongly typed objects.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public static T ToDifferentClassType<T>(this object obj) where T : class
        {
            // create instance of T type object:
            var tmp = Activator.CreateInstance(typeof(T));

            // loop through the fields of the object you want to covert:       
            foreach (FieldInfo fi in obj.GetType().GetFields())
            {
                try
                {
                    tmp.GetType().GetField(fi.Name).SetValue(tmp, fi.GetValue(obj));
                }
                catch
                {
                }
            }

            // return the T type object:         
            return (T)tmp;
        }

        /// <summary>
        /// The get custom attributes.
        /// </summary>
        /// <param name="objectType">
        /// The object type.
        /// </param>
        /// <param name="attributeType">
        /// The attribute type.
        /// </param>
        /// <returns>
        /// </returns>
        public static object[] GetCustomAttributes(Type objectType, Type attributeType)
        {
            object[] myAttrOnType = objectType.GetCustomAttributes(attributeType, false);
            if (myAttrOnType.Length > 0)
            {
                return myAttrOnType;
            }

            return null;
        }

        /// <summary>
        /// Kiem tra 1 object co chua Method nay ko/
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>
        ///   <c>true</c> if the specified obj has method; otherwise, <c>false</c>.
        /// </returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  31/7/2012   created
        /// </Modified>
        public static bool HasMethod<T>(this T obj, string methodName) where T : class
        {
            MethodInfo method = obj.GetType().GetMethod(methodName);

            return (method != null);
        }


        /// <summary>
        /// Lay ra thoi gian mac dinh khi ma thoi gian truyen vao null or = DateTime.MinValue
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  14/9/2012   created
        /// </Modified>
        public static DateTime GetDateTime(Nullable<DateTime> dt, DateTime defaultValue)
        {
            return (dt.HasValue && dt.Value > DateTime.MinValue) ? dt.Value : defaultValue;
        }

        /// <summary>
        /// Kiem tra doi tuong truyen vao co phai la so ko?
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <Modified>
        /// Name     Date         Comments
        /// trungtq  29/11/2012   created
        /// </Modified>
        public static bool IsNumeric(this object obj)
        {
            Type type = obj.GetType();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return Nullable.GetUnderlyingType(type).IsNumeric();
                    }
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Check type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Is<T>(this object obj)
        {
            return (obj is T);
        }

        /// <summary>
        /// Ep kieu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As<T>(this object obj)
        {
            return (obj != null) ? (T)obj : default(T);
        }

        /// <summary>
        /// check object co bi null khong?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return (obj == null || obj.Equals(DBNull.Value));
        }

        /// <summary>
        /// Check object khong bi null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return !obj.IsNull();
        }

        /// <summary>
        /// Check kieu cua doi tuong
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsTypeOf<T>(this object obj)
        {
            return obj.GetType() == typeof(T);
        }

        /// <summary>
        /// Lay ra gia tri tu thuoc tinh cua object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetValue<T>(this object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);

            return (property != null) ? property.GetValue(obj, null).As<T>() : default(T);
        }


        /// <summary>
        /// Get default value for an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetDefaultValue<T>(object obj)
        {
            T ret = default(T);
            try
            {
                if (obj == null)
                {
                    ret = (T)(object)"NULL";
                }
                else if (obj.GetType() == typeof(Guid))
                {
                    obj.TryCast<T>(out ret);
                }

                else if (obj != null)
                {
                    ret = ToType<T>(obj);
                }
            }
            catch { }
            return ret;
        }

        /// <summary>
        /// Lấy giá trị từ DataColumn
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// PhongNC  18/07/2014   created
        /// </Modified>
        public static T GetValue<T>(object value)
        {
            T result = default(T);
            try
            {
                //object value = column.ToString();
                if (value == null)
                {
                    return default(T);
                }
                if (!typeof(T).IsEnum)
                {
                    Type t = typeof(T);

                    result = (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(t) ?? t);
                }
                else
                {
                    if (value != null)
                    {
                        result = (T)Enum.ToObject(typeof(T), value);
                    }
                }
            }
            catch { }
            return result;
        }


        /// <summary>
        /// trả về datetime theo culture hiện tại từ value và culture của value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="culOfValue">culture format của value</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// PhongNC  06/08/2014   created
        /// </Modified>
        //public static DateTime GetValue(object value, CultureInfo culOfValue)
        //{
        //    DateTime result = default(DateTime);
        //    try
        //    {
        //        DateTimeFormatInfo culOfValueDtfi = culOfValue.DateTimeFormat;
        //        DateTimeFormatInfo currentCultDtfi = new CultureInfo(CultureHelper.GetCurrentCulture()).DateTimeFormat;
        //        string str = Convert.ToDateTime(value, culOfValueDtfi).ToString(currentCultDtfi.ShortDatePattern);
        //        result = Convert.ToDateTime(str);
        //    }
        //    catch { }
        //    return result;
        //}
    }
}
