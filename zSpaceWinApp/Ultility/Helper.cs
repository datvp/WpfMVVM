using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSpaceWinApp.Ultility
{
    public class Helper
    {
        private const string NUMBER_DECIMAL_DIGITS = "F2";
        public static float ConvertToGigabytes(long val)
        {
            float result = 0;
            var strVal = ((float)val / (1024 * 1024 * 1024)).ToString(NUMBER_DECIMAL_DIGITS, CultureInfo.InvariantCulture);
            float.TryParse(strVal, out result);
            return result;
        }

        public static float ConvertToMegabytes(long val)
        {
            float result = 0;
            var strVal = ((float)val / (1024 * 1024)).ToString(NUMBER_DECIMAL_DIGITS, CultureInfo.InvariantCulture);
            float.TryParse(strVal, out result);
            return result;
        }
    }
}
