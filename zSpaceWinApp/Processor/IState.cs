using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSpaceWinApp.Processor
{
    public interface IState
    {
        void startDownload();

        void inProgress(int percenentage);
        void finished();
    }
}
