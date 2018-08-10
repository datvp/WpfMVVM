using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSpaceWinApp.Processor
{
    public class QueueItem
    {
        #region Constructors

        public QueueItem(BackgroundWorker backgroundWorker, object argument)
        {
            this.BackgroundWorker = backgroundWorker;
            this.Argument = argument;
        }

        #endregion

        #region Properties

        public object Argument { get; private set; }

        public BackgroundWorker BackgroundWorker { get; private set; }

        #endregion

        #region Methods

        public void RunWorkerAsync()
        {
            this.BackgroundWorker.RunWorkerAsync(this.Argument);
        }

        #endregion
    }
}
