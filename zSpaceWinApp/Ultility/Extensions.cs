namespace LightApp.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    public static class Extensions
    {
        #region members
        #endregion     

        #region methods       
        public static DateTime toDate(this string input, bool throwExceptionIfFailed = false)
        {
            DateTime result;
            var valid = DateTime.TryParse(input, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException($"'{input}' cannot be converted as DateTime");
            return result;
        }
        public static int toInt(this string input, bool throwExceptionIfFailed = false)
        {
            int result;
            var valid = int.TryParse(input, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException($"'{input}' cannot be converted as int");
            return result;
        }
        public static double toDouble(this string input, bool throwExceptionIfFailed = false)
        {
            double result;
            var valid = double.TryParse(input, NumberStyles.AllowDecimalPoint,
              new NumberFormatInfo { NumberDecimalSeparator = "." }, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException($"'{input}' cannot be converted as double");
            return result;
        }
        public static string reverse(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        /// <summary>
        /// Matching all capital letters in the input and seperate them with spaces to form a sentence.
        /// If the input is an abbreviation text, no space will be added and returns the same input.
        /// </summary>
        /// <example>
        /// input : HelloWorld
        /// output : Hello World
        /// </example>
        /// <example>
        /// input : BBC
        /// output : BBC
        /// </example>
        /// <param name="input" />
        /// <returns>
        public static string toSentence(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            //return as is if the input is just an abbreviation
            if (Regex.Match(input, "[0-9A-Z]+$").Success)
                return input;
            //add a space before each capital letter, but not the first one.
            var result = Regex.Replace(input, "(\\B[A-Z])", " $1");
            return result;
        }
        public static bool isEmail(this string input)
        {
            var match = Regex.Match(input,
              @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success;
        }
        public static bool isPhone(this string input)
        {
            var match = Regex.Match(input,
              @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase);
            return match.Success;
        }
        public static bool isNumber(this string input)
        {
            var match = Regex.Match(input, @"^[0-9]+$", RegexOptions.IgnoreCase);
            return match.Success;
        }
        /// <summary>
        /// Example: Assert.AreEqual(100, "in 100 between".extractNumber());
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int extractNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            var match = Regex.Match(input, "[0-9]+", RegexOptions.IgnoreCase);
            return match.Success ? match.Value.toInt() : 0;
        }
        public static string extractEmail(this string input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input)) return string.Empty;

            var match = Regex.Match(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success ? match.Value : string.Empty;
        }
        #endregion
    }
}
