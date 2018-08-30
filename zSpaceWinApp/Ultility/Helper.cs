using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using zSpaceWinApp.Model;

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

        public static ObservableCollection<Program> GetAppsInstalledInSystem()
        {
            ObservableCollection<Program> p = new ObservableCollection<Program>();
            callConnect();
            try
            {
                //var path = "SELECT Name,Version FROM Win32_Product";
                //var path = "SELECT * FROM Win32_SystemDriver";
                //var path = "SELECT * FROM Win32_PnPEntity";
                ConnectionOptions options = new ConnectionOptions();
                //options.Username = 
                //options.Password =
                //options.Authority = 
                //options.EnablePrivileges = 

                string providerPath = @"root\CIMv2";
                var path = "SELECT ClassGuid,FriendlyName,DriverVersion FROM Win32_PnPSignedDriver where FriendlyName <> NULL";
                ManagementScope scope = new ManagementScope(providerPath, options);
                scope.Connect();

                ObjectQuery query = new ObjectQuery(path);
                var mos = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection moc = mos.Get();
                Console.WriteLine("moc count: " + moc.Count.ToString()); ;
                Console.WriteLine("=============Start=============");
                var arrApp = new HashSet<string>();
                foreach (ManagementObject mo in moc)
                {
                    try
                    {
                        var guid = mo["ClassGuid"].ToString();
                        var driverName = mo["FriendlyName"].ToString();
                        var version = mo["DriverVersion"].ToString();

                        if (arrApp.Contains(driverName)) continue;

                        arrApp.Add(driverName);

                        Program program = new Program(driverName, version);
                        p.Add(program);

                        Console.WriteLine("Driver Name: " + driverName + " | Version: " + version + " | GUID: " + guid);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);                     
                    }
                }
                Console.WriteLine("=============End=============");
            }
            catch (Exception e)
            {
                Console.WriteLine("WMI exception: " + e.Message);
            }
            return p;
        }
        private static void callConnect()
        {
            Console.WriteLine("Start");
            var processName = Assembly.GetEntryAssembly().GetName().Name;
            Console.WriteLine($"Process Name: {processName}");

            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Console.WriteLine($"Directory Name: {directory}");

            var parentProcessFullPath = GetParentProcess().MainModule.FileName;
            Console.WriteLine(parentProcessFullPath);

            Console.WriteLine("End");
        }

        internal static Process GetParentProcess()
        {
            Console.WriteLine($"Enter GetParentProcess");
            int Id = Process.GetCurrentProcess().Id;
            Console.WriteLine($"Current ProcessId {Id}");
            try
            {
                using (ManagementObject mo = new ManagementObject("win32_process.handle='" + Id + "'"))
                {
                    mo.Get();
                    int parentPid = Convert.ToInt32(mo["ParentProcessId"]);                    
                    return Process.GetProcessById(parentPid);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message {ex.Message} Exception {ex.StackTrace}");
                return null;
            }
        }
        /// <summary>
        /// get current culture of machine
        /// </summary>
        /// <returns></returns>
        public static string GetMachineCurrentCulture()
        {
            return System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        public static void SourceIntialized(Window w)
        {
            IntPtr handle = (new WindowInteropHelper(w)).Handle;
            HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
        }

        #region winproc
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;
            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {

            var mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            var currentScreen = System.Windows.Forms.Screen.FromHandle(hwnd);
            var workArea = currentScreen.WorkingArea;
            var monitorArea = currentScreen.Bounds;
            mmi.ptMaxPosition.x = Math.Abs(workArea.Left - monitorArea.Left);
            mmi.ptMaxPosition.y = Math.Abs(workArea.Top - monitorArea.Top);
            mmi.ptMaxSize.x = Math.Abs(workArea.Right - workArea.Left);
            mmi.ptMaxSize.y = Math.Abs(workArea.Bottom - workArea.Top);

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:/* WM_GETMINMAXINFO */
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }
        #endregion
    }
}
