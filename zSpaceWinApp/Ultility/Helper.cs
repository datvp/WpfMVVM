using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
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
        public static void SetStartup(bool isStartWithWindow, string appName)
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

        public static void GetAppsInstalledInSystem()
        {
            //var path = "SELECT Name,Version FROM Win32_Product";
            //var path = "SELECT * FROM Win32_SystemDriver";
            //var path = "SELECT * FROM Win32_PnPEntity";
            var path = "SELECT DeviceName,DriverVersion FROM Win32_PnPSignedDriver where DeviceName <> NULL";

            ManagementObjectSearcher mos = new ManagementObjectSearcher(path);
            ManagementObjectCollection moc = mos.Get();
            Console.WriteLine("moc count: " + moc.Count.ToString()); ;
            Console.WriteLine("=============Start=============");
            foreach (ManagementObject mo in moc)
            {
                try
                {
                    var appName = mo["DeviceName"].ToString();
                    var version = mo["DriverVersion"].ToString();
                    Console.WriteLine("AppName: " + appName + " | Version: " + version);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            Console.WriteLine("=============End=============");
        }
    }
}
