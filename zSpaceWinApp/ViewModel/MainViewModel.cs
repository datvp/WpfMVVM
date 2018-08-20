using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using zSpaceWinApp.Processor;
using zSpaceWinApp.Model;
using zSpaceWinApp.Ultility;
using System.Windows;
using System.Threading;
using System.Windows.Threading;
using zSpaceWinApp.Logs;

namespace zSpaceWinApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private DownloadProcessor download = new DownloadProcessor();
        private static ObservableCollection<Program> programList = new ObservableCollection<Program>();
        private MainModel _MainModel = new MainModel();
        private InstallUnInstallProcessor iup = new InstallUnInstallProcessor();
        private ObservableCollection<HardDriveModel> _hddCollection;
              

        public ObservableCollection<HardDriveModel> hddCollection
        {
            get { return _hddCollection; }
            set { _hddCollection = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Program> ProgramList
        {
            get { return programList; }
            set { programList = value; OnPropertyChanged(); }
        }
        public MainModel MainModel
        {
            get
            {
                return _MainModel;
            }

            set
            {
                _MainModel = value;
                OnPropertyChanged();
            }
        }

        public void initData()
        {
            ShowProcess();
        }

        /**
         * 
         * 
         */
        private void doJob()
        {
            //ObservableCollection<Program> temp = Helper.GetAppsInstalledInSystem();
            //foreach (Program p in temp)
            //{
            //    //var uiContext = SynchronizationContext.Current;
            //    //uiContext.Send(x => changeData(p), null);
            //    App.Current.Dispatcher.Invoke(new Action(() => {
            //        changeData(p);
            //    }));                               
            //}
            IEnumerable<System.IO.FileInfo> files = iup.queryInstallFiles();
            int test = 1;
            foreach (System.IO.FileInfo field in files)
            {
                Model.Program program = new Model.Program(field.Name, "");
                program.Position = test;
                program.Progress = 0;
                program.TotalSize = test * 10;
                program.Status = Program.DOWNLOADING;
                program.ClickCommand = new RelayCommand<object>(p => { return true; }, p => {
                    var item = programList[(int)p - 1] as Model.Program;
                    if (item.ButtonText == "download")
                    {
                        item.Status = Program.DOWNLOADING;
                        item.ButtonText = "pause";
                        download.AddTask(item);
                    }
                    else if (item.ButtonText == "pause")
                    {
                        item.Status = Program.PAUSE;
                        item.ButtonText = "download";
                    }
                });
                program.InstallCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
                {
                    Console.Out.WriteLine("InstallCommand called");
                    var programRunning = programList[(int)p - 1] as Model.Program;
                    try
                    {
                        // Call to install app.
                        Console.Out.WriteLine("Installing...");
                        iup.Install(String.Format(@"D:\Files\{0}", program.ProgramName));
                        Console.Out.WriteLine("Completed");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Install Exception: '{0}'", e.Message);
                    }

                });
                Console.WriteLine(field.Name);
                App.Current.Dispatcher.Invoke(new Action(() => {
                    changeData(program);
                }));
                //programList.Add(program);
                test++;
            }
        }

        private void changeData(Program p)
        {           
            programList.Add(p);
        }

        public MainViewModel()
        {
            CheckPowerStatusCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                MainModel = MainProcessor.getPowerStatus();
            });
            GetHDDInfoCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                hddCollection = MainProcessor.getHardDriveInfo();
            });
            CheckInternetCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                MainModel.IsConnectedInternet = Helper.IsNetworkAvailable();
            });
            
            EnglishCommand = new RelayCommand<FrameworkElement>((e) => { return true; }, (e) => {
                LocUtil.SwitchLanguage(e, "en-US");             
            });
            ChinaCommand = new RelayCommand<FrameworkElement>((e) => { return true; }, (e) => {
                LocUtil.SwitchLanguage(e, "zh-CN");
                //AboutWindow aw = new AboutWindow();
                //aw.ShowDialog();
            });
            //Log.Error("Test2", "Test2");
            //var lst = Log.GetListErrors();
            initData();
            //iup.UnInstall("{c166523c-fe0c-4a94-a586-f1a80cfbbf3e}");
        }
        public ICommand CheckPowerStatusCommand { get; set; }
        public ICommand GetHDDInfoCommand { get; set; }
        public ICommand CheckInternetCommand { get; set; }
        public ICommand EnglishCommand { get; set; }
        public ICommand ChinaCommand { get; set; }

        private Thread thProcess;
        public void ShowProcess()
        {
            try
            {
                thProcess = new Thread(doJob);
                thProcess.Start();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }           
        }
        public void HideProcess()
        {
            try
            {
                thProcess.Abort();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }
}


}
