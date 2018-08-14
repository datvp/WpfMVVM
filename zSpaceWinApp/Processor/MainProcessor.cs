using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace zSpaceWinApp.Processor
{
    public class MainProcessor
    {
        public MainProcessor() { }

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
