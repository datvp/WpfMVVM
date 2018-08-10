using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void AddTask(Model.Program state)
        {
            string s = state.ProgramName;
            if (!set.Contains(s))
            {
                set.Add(s);
                StartThread(state.Position, state.ProgramName, state);
            }
        }

        private void StartThread(int id, string parameters, Model.Program state)
        {
            DownloadBW thread = new DownloadBW(id, parameters, state);
            thread.DoWork += new DoWorkEventHandler(doWork);
            thread.WorkerReportsProgress = true;
            thread.WorkerSupportsCancellation = true;            
            thread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completeWork);
            thread.ProgressChanged += new ProgressChangedEventHandler(progressChanged);
            
            thread.RunWorkerAsync(thread.Name);
        }

        private void progressChanged(object sender, ProgressChangedEventArgs e)
        {
            DownloadBW threat = (DownloadBW)sender;
            if (threat != null)
            {
                Console.Out.WriteLine(String.Format("progressChanged: '{0}'", threat.Position));
                Model.Program st = threat.State;
                if (st != null)
                {
                    st.Progress = e.ProgressPercentage;                 
                }
            }
        }

        private void doWork(object sender, DoWorkEventArgs e)
        {
            DownloadBW threat = (DownloadBW) sender;
            Model.Program st = threat.State;

            if(threat != null)
            {
                int count = 0;
                while (count < threat.Position * 100 )
                {
                    if(st.Status != Model.Program.DOWNLOAD)
                    {
                        threat.CancelAsync();
                        Console.Out.WriteLine(String.Format("cancel: '{0}' progress: '{1}'", threat.Position, count));
                        break;
                    }
                    Console.Out.WriteLine(String.Format("process: '{0}' progress: '{1}'", threat.Position, count));
                    Thread.Sleep(200);
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
                Console.Out.WriteLine(String.Format("complete: '{0}'", threat.Position));
                set.Remove(threat.Name);
            }
        }
    }

    class DownloadBW : BackgroundWorker
    {
        public int Position { get; }
        public string Name { get; }
        public Model.Program State { get; }
        public Boolean stop { get; }
        public DownloadBW (int position, string name, Model.Program state)
        {
            this.Position = position;
            this.Name = name;
            this.State = state;
        }
    }
}
