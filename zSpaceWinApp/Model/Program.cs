using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace zSpaceWinApp.Model
{
    public class Program : BaseModel
    {
        public static int NORMAL = 1;
        public static int DOWNLOAD = 2;        

        private int position;
        private string v1;
        private string v2;
        private long totalSize;
        private int progress;
        private string buttonText = "download";
        private int status = NORMAL;
        private bool _isPause = false;

        public Program(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
        public int Position { get { return position; } set { position = value; OnPropertyChanged(); } }
        public string ProgramName { get { return v1; } set { v1 = value; OnPropertyChanged(); } }
        public string ProgramVersion { get { return v2; } set { v2 = value; OnPropertyChanged(); } }

        public long TotalSize { get { return totalSize; } set { totalSize = value; OnPropertyChanged(); } }

        public int Progress { get { return progress; } set { progress = value; OnPropertyChanged(); } }

        public string ButtonText { get { return buttonText; } set { buttonText = value;  OnPropertyChanged(); } }

        public int Status { get { return status;  } set { status = value; OnPropertyChanged(); } }

        public bool IsPause { get { return _isPause; } set { _isPause = value; OnPropertyChanged(); } }
        public ICommand ClickCommand { get; set; }
    }
}
