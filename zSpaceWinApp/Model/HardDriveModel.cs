using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSpaceWinApp.Model
{
    public class HardDriveModel: BaseModel
    {
        private string _DriverName;

        public string DriverName
        {
            get { return _DriverName; }
            set { _DriverName = value; OnPropertyChanged(); }
        }

        private float _TotalSize;

        public float TotalSize
        {
            get { return _TotalSize; }
            set { _TotalSize = value; OnPropertyChanged(); }
        }

        private float _TotalFreeSpace;

        public float TotalFreeSpace
        {
            get { return _TotalFreeSpace; }
            set { _TotalFreeSpace = value; OnPropertyChanged(); }
        }

        private float _AvailableFreeSpace;

        public float AvailableFreeSpace
        {
            get { return _AvailableFreeSpace; }
            set { _AvailableFreeSpace = value; OnPropertyChanged(); }
        }

        private string _DriveFormat;

        public string DriveFormat
        {
            get { return _DriveFormat; }
            set { _DriveFormat = value; OnPropertyChanged(); }
        }

        private System.IO.DriveType _DriveType;
        
        public System.IO.DriveType DriveType
        {
            get { return _DriveType; }
            set { _DriveType = value; OnPropertyChanged(); }
        }

        private float _UsageSize;

        public float UsageSize
        {
            get { return _UsageSize; }
            set { _UsageSize = value; OnPropertyChanged(); }
        }

    }
}
