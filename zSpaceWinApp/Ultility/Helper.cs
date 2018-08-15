using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace zSpaceWinApp.Ultility
{
    public class Helper
    {
        private const string NUMBER_DECIMAL_DIGITS = "F2";
        /// <summary>
        /// Convert to Gigabytes (GB)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float ConvertToGigabytes(long val)
        {
            float result = 0;
            var strVal = ((float)val / (1024 * 1024 * 1024)).ToString(NUMBER_DECIMAL_DIGITS, CultureInfo.InvariantCulture);
            float.TryParse(strVal, out result);
            return result;
        }
        /// <summary>
        /// Convert to Megabytes (MB)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float ConvertToMegabytes(long val)
        {
            float result = 0;
            var strVal = ((float)val / (1024 * 1024)).ToString(NUMBER_DECIMAL_DIGITS, CultureInfo.InvariantCulture);
            float.TryParse(strVal, out result);
            return result;
        }

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        /// <summary>
        /// Check internet available
        /// </summary>
        /// <returns></returns>
        public static bool IsNetworkAvailable()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        /// <summary>
        /// set app launch at startup
        /// </summary>
        /// <param name="isStartWithWindow"></param>
        /// <param name="appName"></param>
        private void SetStartup(bool isStartWithWindow, string appName)
        {
            if (string.IsNullOrEmpty(appName)) return;

            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (isStartWithWindow)
            {
                string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                rk.SetValue(appName, directory);
                return;
            }

            rk.DeleteValue(appName, false);
        }
    }
}
