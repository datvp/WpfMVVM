using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using zSpaceWinApp.Ultility;

namespace zSpaceWinApp.Processor
{
    public static class MainProcessor
    {       
        public static Model.MainModel getPowerStatus()
        {
            Model.MainModel MainModel = new Model.MainModel();
            PowerState state = PowerState.GetPowerState();
            if (state == null) return MainModel;
            MainModel.ACLineStatus = state.ACLineStatus;
            MainModel.BatteryFlag = state.BatteryFlag;
            MainModel.BatteryLifePercent = state.BatteryLifePercent;
            MainModel.BatteryLifeTime = state.BatteryLifeTime;
            MainModel.BatteryFullLifeTime = state.BatteryFullLifeTime;
            return MainModel;
        }

        public static ObservableCollection<Model.HardDriveModel> getHardDriveInfo()
        {
            HardDriveInfo cls = new HardDriveInfo();
            var result = cls.getHardDriveInfo();
            return result;
        }
              
        public static ObservableCollection<Model.LocModel> getLanguages()
        {
            ObservableCollection<Model.LocModel> lst = new ObservableCollection<Model.LocModel>();
            var fullPath = LocUtil.GetLocXAMLFilePath("en-US");
            var content = System.IO.File.ReadLines(fullPath).Select(line => line.ToString()).Where(w => w.ToString().Contains("Key") && !w.ToString().Contains("zSpace-localization"));
            if (content == null) return lst;

            foreach (var item in content)
            {
                var key = item.betweenStrings("Key=\"", "\">");
                var val = item.betweenStrings(">", "</");
                lst.Add(new Model.LocModel()
                {
                    Keyword = key,
                    English = val,
                    OldEnglish = val,                    
                });
            }

            //get chinese
            fullPath = fullPath.Replace("en-US", "zh-CN");
            content = System.IO.File.ReadLines(fullPath).Select(line => line.ToString()).Where(w => w.ToString().Contains("Key") && !w.ToString().Contains("zSpace-localization"));
            if (content != null)
            {
                foreach (var item in content)
                {
                    var key = item.betweenStrings("Key=\"", "\">");
                    var val = item.betweenStrings(">", "</");
                    var o = lst.Where(w => w.Keyword == key).FirstOrDefault();
                    if (o != null)
                    {
                        o.Chinese = val;
                        o.OldChinese = val;
                    }
                }
            }

            return lst;
        }

        public static void SaveLanguage(ObservableCollection<Model.LocModel> lst)
        {
            if (lst == null) return;
            var en_path = LocUtil.GetLocXAMLFilePath("en-US");
            var zh_path = en_path.Replace("en-US", "zh-CN");
            foreach (var item in lst)
            {
                //save english
                if (!item.OldEnglish.Equals(item.English))
                {
                    LocUtil.ReplaceString(en_path, item.OldEnglish, item.English);
                }

                //save chinese
                if (!item.OldChinese.Equals(item.Chinese))
                {
                    LocUtil.ReplaceString(zh_path, item.OldChinese, item.Chinese);
                }
            }
            MessageBox.Show("Save successfully!");
        }
        //public void CheckPowerStatus()
        //{
        //    if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery)
        //    {
        //        //Desktop
        //    }
        //    else
        //    {
        //        //Laptop
        //    }
        //}

        //private void ShowPowerStatus()
        //{
        //    PowerStatus status = SystemInformation.PowerStatus;
        //    txtChargeStatus.Text = status.BatteryChargeStatus.ToString();
        //    if (status.BatteryFullLifetime == -1)
        //        txtFullLifetime.Text = "Unknown";
        //    else
        //        txtFullLifetime.Text =
        //            status.BatteryFullLifetime.ToString();
        //    txtCharge.Text = status.BatteryLifePercent.ToString("P0");
        //    if (status.BatteryLifeRemaining == -1)
        //        txtLifeRemaining.Text = "Unknown";
        //    else
        //        txtLifeRemaining.Text =
        //            status.BatteryLifeRemaining.ToString();
        //    txtLineStatus.Text = status.PowerLineStatus.ToString();
        //}

        ///// <summary>
        ///// Indicates if we're running on battery power.
        ///// If we are, then disable CPU wasting things like animations, background operations, network, I/O, etc
        ///// </summary>
        //public static Boolean IsRunningOnBattery
        //{
        //    get
        //    {
        //        PowerLineStatus pls = System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus;

        //        //Offline means running on battery
        //        return (pls == PowerLineStatus.Offline);
        //    }
        //}
    }
}
