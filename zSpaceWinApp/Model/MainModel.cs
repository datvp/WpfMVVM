using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
