using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSpaceWinApp.Model;

namespace zSpaceWinApp.Logs
{
    public interface ILog
    {
        void Info(DownHis m);
        void Error(string err, string funcId);
        ObservableCollection<Errors> GetListErrors();
        ObservableCollection<Model.DownHis> GetListDownHis();
    }
}
