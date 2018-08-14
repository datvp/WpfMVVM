using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSpaceWinApp.Processor;

namespace zSpaceWinApp.Model
{
    public class MainModel: BaseModel
    {
        private string _Title = "";
        public string Title {
            get
            {
                return _Title;
            }

            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }

        private ACLineStatus _ACLineStatus;
        public ACLineStatus ACLineStatus {
            get
            {
                return _ACLineStatus;
            }

            set
            {
                _ACLineStatus = value;
                OnPropertyChanged();
            }
        }

        private BatteryFlag _BatteryFlag;
        public BatteryFlag BatteryFlag
        {
            get
            {
                return _BatteryFlag;
            }

            set
            {
                _BatteryFlag = value;
                OnPropertyChanged();
            }
        }

        private Byte _BatteryLifePercent;
        public Byte BatteryLifePercent {
            get
            {
                return _BatteryLifePercent;
            }

            set
            {
                _BatteryLifePercent = value;
                OnPropertyChanged();
            }
        }


        private int _BatteryLifeTime;
        public int BatteryLifeTime
        {
            get
            {
                return _BatteryLifeTime;
            }

            set
            {
                _BatteryLifeTime = value;
                OnPropertyChanged();
            }
        }

        private int _BatteryFullLifeTime;
        public int BatteryFullLifeTime
        {
            get
            {
                return _BatteryFullLifeTime;
            }

            set
            {
                _BatteryFullLifeTime = value;
                OnPropertyChanged();
            }
        }

    }
}
