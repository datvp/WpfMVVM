using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSpaceWinApp.Ultility;

namespace zSpaceWinApp.Processor
{
    public class HardDriveInfo
    {
        public HardDriveInfo() { }

        public ObservableCollection<Model.HardDriveModel> getHardDriveInfo()
        {
            ObservableCollection<Model.HardDriveModel> hddCollection = new ObservableCollection<Model.HardDriveModel>();
            foreach (System.IO.DriveInfo label in System.IO.DriveInfo.GetDrives())
            {
                Model.HardDriveModel hdd = new Model.HardDriveModel();
                hdd.DriverName = label.Name;
                hdd.TotalSize = Helper.ConvertToGigabytes(label.TotalSize);
                hdd.TotalFreeSpace = Helper.ConvertToGigabytes(label.TotalFreeSpace);
                hdd.AvailableFreeSpace = Helper.ConvertToGigabytes(label.AvailableFreeSpace);
                hdd.DriveFormat = label.DriveFormat;
                hdd.DriveType = label.DriveType;
                hdd.UsageSize = hdd.TotalSize - hdd.TotalFreeSpace;
                hddCollection.Add(hdd);
            }
            return hddCollection;
        }
    }
}
