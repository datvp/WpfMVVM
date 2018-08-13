using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WindowsInstaller;
using zSpaceWinApp.Processor;

namespace zSpaceWinApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private DownloadProcessor download = new DownloadProcessor();
        private ObservableCollection<Model.Program> programList = new ObservableCollection<Model.Program>();
        private InstallAppProcessor installProcessor = new InstallAppProcessor();
        public ObservableCollection<Model.Program> ProgramList
        {
            get { return programList; }
            set { programList = value; OnPropertyChanged(); }
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
            //                //programList.Add(new Model.Program(name, version));
            //            }
            //        }
            //    }
            //}

            for (int i = 1; i <= 10; i++) {
                Model.Program program = new Model.Program(String.Format("thread '{0}'", i), "");
                program.Position = i;
                program.Progress = 0;
                program.TotalSize = i * 10;
                program.ClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    var programRunning = programList[(int)p - 1] as Model.Program;
                    if(programRunning.Status == Model.Program.NORMAL)
                    {
                        programRunning.Status = Model.Program.DOWNLOADING;
                        download.AddTask(programRunning);
                        programRunning.ButtonText = "stop";
                    } else
                    {
                        programRunning.IsPause = true;
                        programRunning.Status = Model.Program.NORMAL;
                        programRunning.ButtonText = "download";

                    }                   
                });               
                programList.Add(program);
            }

            IEnumerable<System.IO.FileInfo> files = installProcessor.queryInstallFiles();
            int test = programList.Count;
            foreach (System.IO.FileInfo field in files)
            {
                Model.Program program = new Model.Program(field.Name, "");
                program.Position = test;
                program.Progress = 0;
                program.TotalSize = test * 10;
                program.InstallCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    Console.Out.WriteLine("InstallCommand called");
                    var programRunning = programList[(int)p - 1] as Model.Program;
                    try
                    {
                       // Call to install app.
                        Console.Out.WriteLine("Installing...");
                        installProcessor.installApp(String.Format(@"D:\Files\{0}", program.ProgramName));
                        Console.Out.WriteLine("Completed");
                    }
                    catch (Exception e)
                    {             
                        Console.WriteLine("Install Exception: '{0}'", e.Message);
                    }

                });
                Console.WriteLine(field.Name);
                programList.Add(program);
                test++;
            }
        }

        public MainViewModel()
        {
            initData();           
        }
    }
}
