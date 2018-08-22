using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using zSpaceWinApp.Logs;

namespace zSpaceWinApp.Processor
{
    public class DownloadProcessor
    {
        private ILog log = new Log();
        private readonly Queue<KeyValuePair<Delegate, object[]>> Queue = new Queue<KeyValuePair<Delegate, object[]>>();
        private HashSet<string> set = new HashSet<string>();
        private readonly object queueSync = new object();
        public DownloadProcessor ()
        {
        }

        public void AddTask(Model.Program program)
        {
            if (set.Contains(program.ProgramName)) return;

            set.Add(program.ProgramName);
            this.StartThread(program);
        }

        private void StartThread(Model.Program program)
        {
            DownloadBW thread = new DownloadBW(program);
            thread.WorkerReportsProgress = true;
            thread.WorkerSupportsCancellation = true;            
            thread.DoWork += new DoWorkEventHandler(doWork);
            thread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completeWork);
            thread.ProgressChanged += new ProgressChangedEventHandler(progressChanged);
            
            thread.RunWorkerAsync(thread.program.ProgramName);
        }

        private void progressChanged(object sender, ProgressChangedEventArgs e)
        {
            DownloadBW threadDownloading = sender as DownloadBW;

            if (threadDownloading == null || threadDownloading.program == null) return;

            threadDownloading.program.Progress = e.ProgressPercentage;
        }

        private void doWork(object sender, DoWorkEventArgs e)
        {
            DownloadBW threadDownloading = sender as DownloadBW;

            if (threadDownloading == null) return;

            int count = threadDownloading.program.Progress;
           
            while (count < threadDownloading.program.TotalSize)
            {
                if (threadDownloading.program.Status == Model.Program.STOP)
                {
                    threadDownloading.program.Progress = 0;
                    threadDownloading.CancelAsync();
                    Console.WriteLine($"Canceled: '{threadDownloading.program.Position}' thread: '{count}'");
                    break;
                }
                else if (threadDownloading.program.Status == Model.Program.PAUSE)
                {
                    // write log
                    log.Info(new Model.DownHis() {
                        DriverName = threadDownloading.program.ProgramName,
                        TotalSize = (int)threadDownloading.program.TotalSize,
                        Progress = threadDownloading.program.Progress,
                        Status = threadDownloading.program.Status.ToString(),
                    });
                    //cancel thread
                    threadDownloading.CancelAsync();
                    break;
                }
                Console.WriteLine($"Processing: '{threadDownloading.program.Position}' thread: '{count}'");
                threadDownloading.ReportProgress(count);
                count++;
                Thread.Sleep(1);
            }
        }

        private void completeWork(object sender, RunWorkerCompletedEventArgs e)
        {
            DownloadBW threadDownloading = sender as DownloadBW;

            if (threadDownloading == null) return;
            threadDownloading.program.ButtonText = "download";
            Console.WriteLine($"Completed: {threadDownloading.program.Position}");
            set.Remove(threadDownloading.program.ProgramName);
        }
    }

    class DownloadBW : BackgroundWorker
    {
        public Model.Program program { get; }
        public DownloadBW (Model.Program program)
        {           
            this.program = program;
        }
    }
}
