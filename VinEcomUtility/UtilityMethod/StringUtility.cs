using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidecode.NET;

namespace VinEcomUtility.UtilityMethod
{
    public static class StringUtility
    {
        #region Extension Methods
        /// <summary>
        /// Salt and hash string by BCrypt algorithm
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>A hashed string</returns>
        public static string BCryptSaltAndHash(this string source)
        {
            return BCrypt.Net.BCrypt.HashPassword(source);
        }
        /// <summary>
        /// Check if a string is the input key of the hashed string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="correctHash"></param>
        /// <returns>True if the string is the original key of the hashed string, otherwise false</returns>
        public static bool IsCorrectHashSource(this string source, string correctHash)
        {
            if (source == null || correctHash == null) return false; 
            return BCrypt.Net.BCrypt.Verify(source, correctHash);
        }
        #endregion

        #region Other Methods
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        public static string RemoveSign4VietnameseString(this string str)
        {
            //for (int i = 1; i < VietnameseSigns.Length; i++)
            //{
            //    for (int j = 0; j < VietnameseSigns[i].Length; j++)
            //    {
            //        str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][j - 1]);
            //    }
            //}
            //return str;

            return Unidecoder.Unidecode(str);
        }
        #endregion
    }
}
