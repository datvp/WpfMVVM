using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSpaceWinApp.Model
{
    public class DownHis
    {
        private string _DriverName;

        public string DriverName
        {
            get { return _DriverName; }
            set { _DriverName = value; }
        }
        private int _TotalSize;

        public int TotalSize
        {
            get { return _TotalSize; }
            set { _TotalSize = value; }
        }
        private int _Progress;

        public int Progress
        {
            get { return _Progress; }
            set { _Progress = value; }
        }
        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
