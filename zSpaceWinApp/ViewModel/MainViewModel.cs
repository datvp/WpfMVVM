using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using zSpaceWinApp.Processor;

namespace zSpaceWinApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private DownloadProcessor download = new DownloadProcessor();
        private ObservableCollection<Model.Program> programList = new ObservableCollection<Model.Program>();
        public ObservableCollection<Model.Program> ProgramList
        {
            get { return programList; }
            set { programList = value; }
        }

        public void initData()
        {
            //string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            //using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            //{
            //    foreach (string subkey_name in key.GetSubKeyNames())
            //    {
            //        using (RegistryKey subkey = key.OpenSubKey(subkey_name))
            //        {
            //            if (subkey.GetValue("DisplayName") != null)
            //            {
            //                string name = subkey.GetValue("DisplayName").ToString();
            //                var version = subkey.GetValue("Version") != null ? subkey.GetValue("Version").ToString() : "";
            //                var versionDisplay = subkey.GetValue("VersionMajor") != null ? subkey.GetValue("VersionMajor").ToString() : "";
            //                var versionMinor = subkey.GetValue("VersionMinor") != null ? subkey.GetValue("VersionMinor").ToString() : "";
            //                string info = String.Format("version: '{0}' versionD: '{1}' versionM '{2}' Name '{3}'", version, versionDisplay, versionMinor, name);
            //                Console.Out.WriteLine(info);
            //                programList.Add(new Model.Program(name, version));
            //            }
            //        }
            //    }
            //}

            for (int i = 1; i <= 10; i++) {
                Model.Program program = new Model.Program(String.Format("thread '{0}'", i), "");
                program.Position = i;
                program.Progress = 0;
                program.TotalSize = i * 100;
                program.ClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    var programRunning = programList[(int)p - 1] as Model.Program;
                    if(programRunning.Status == Model.Program.NORMAL)
                    {
                        programRunning.Status = Model.Program.DOWNLOAD;
                        download.AddTask(programRunning);
                        programRunning.ButtonText = "stop";
                    } else
                    {
                        programRunning.Status = Model.Program.NORMAL;
                        programRunning.ButtonText = "download";

                    }                   
                });
                programList.Add(program);
            }

            //ManagementObjectSearcher objSearcher = new ManagementObjectSearcher("Select * from Win32_PnPSignedDriver");

            //ManagementObjectCollection objCollection = objSearcher.Get();

            //foreach (ManagementObject obj in objCollection)
            //{
            //    string info = String.Format("Device='{0}',Manufacturer='{1}',DriverVersion='{2}' ", obj["DeviceName"], obj["Manufacturer"], obj["DriverVersion"]);
            //    Console.Out.WriteLine(info);
            //}

        }

        public MainViewModel()
        {
            initData();
            ButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBox.Show(p.ToString());
            });

            foreach (Model.Program pro in programList)
            {
                
            }
        }

        public ICommand ButtonClickCommand { get; set; }
    }
}
