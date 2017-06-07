using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Common.Helper
{
    /// <summary>
    /// Extension methods
    /// http://msdn.microsoft.com/en-us/library/vstudio/bb383977.aspx
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// trungtq 20/9/2013   created
    /// </Modified>
    public static class StringExtensions
    {
        /// <summary>
        /// Su dung ham constain co su dung comparison
        /// </summary>
        /// <param name="original"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool Contains(this string original, string value, StringComparison comparisonType)
        {
            return original.IndexOf(value, comparisonType) >= 0;
        }
    }
}
