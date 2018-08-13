﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Threading;

namespace zSpaceWinApp.Processor
{
    public class DownloadProcessor
    {
        private readonly Queue<KeyValuePair<Delegate, object[]>> Queue = new Queue<KeyValuePair<Delegate, object[]>>();
        private HashSet<string> set = new HashSet<string>();
        private readonly object queueSync = new object();
        public DownloadProcessor ()
        {
        }

        public void AddTask(Model.Program program)
        {
            if (!set.Contains(program.ProgramName))
            {
                set.Add(program.ProgramName);
                this.StartThread(program);
            }
        }

        private void StartThread(Model.Program program)
        {
            DownloadBW thread = new DownloadBW(program);
            thread.DoWork += new DoWorkEventHandler(doWork);
            thread.WorkerReportsProgress = true;
            thread.WorkerSupportsCancellation = true;            
            thread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completeWork);
            thread.ProgressChanged += new ProgressChangedEventHandler(progressChanged);
            
            thread.RunWorkerAsync(thread.program.ProgramName);
        }

        private void progressChanged(object sender, ProgressChangedEventArgs e)
        {
            DownloadBW threat = (DownloadBW)sender;
            if (threat != null)
            {
                //Console.Out.WriteLine(String.Format("progressChanged: '{0}'", threat.program.Position));
                if (threat.program != null)
                {
                    threat.program.Progress = e.ProgressPercentage;                 
                }
            }
        }

        private void doWork(object sender, DoWorkEventArgs e)
        {
            DownloadBW threat = (DownloadBW) sender;
            
            if(threat != null)
            {
                //installApp(@"D:\Softs\Snoop.msi");
                int count = 0;
                while (count < threat.program.Position * 100)
                {
                    if (threat.program.Status != Model.Program.DOWNLOADING)
                    {
                        threat.CancelAsync();
                        Console.Out.WriteLine(String.Format("cancel: '{0}' progress: '{1}'", threat.program.Position, count));
                        break;
                    }
                    Console.Out.WriteLine(String.Format("process: '{0}' progress: '{1}'", threat.program.Position, count));
                    Thread.Sleep(1);
                    threat.ReportProgress(count);
                    count++;
                }
            }

        }

        private void completeWork(object sender, RunWorkerCompletedEventArgs e)
        {
            DownloadBW threat = (DownloadBW)sender;
            if(threat != null)
            {
                Console.Out.WriteLine(String.Format("complete: '{0}'", threat.program.Position));
                set.Remove(threat.program.ProgramName);
            }
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
