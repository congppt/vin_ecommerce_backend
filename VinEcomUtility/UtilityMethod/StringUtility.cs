using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(source, salt);
        }
        /// <summary>
        /// Check if a string is the input key of the hashed string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="correctHash"></param>
        /// <returns>True if the string is the original key of the hashed string, otherwise false</returns>
        public static bool IsCorrectHashSource(this string source, string correctHash) => BCrypt.Net.BCrypt.Verify(source, correctHash);
        #endregion
        #region Other Methods
        #endregion
    }
}
