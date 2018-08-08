using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace zSpaceWinApp.ViewModel
{
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel()
        {
            ButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                MessageBox.Show("Hello world");
            });            
        }

        public ICommand ButtonClickCommand { get; set; }
    }
}
