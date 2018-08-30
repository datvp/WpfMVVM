using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using zSpaceWinApp.Processor;
using zSpaceWinApp.Ultility;

namespace zSpaceWinApp
{
    /// <summary>
    /// Interaction logic for LanguageWindow.xaml
    /// </summary>
    public partial class LanguageWindow : Window
    {
        public LanguageWindow()
        {
            InitializeComponent();
            this.SourceInitialized += LanguageWindow_SourceInitialized;
        }

        private void LanguageWindow_SourceInitialized(object sender, EventArgs e)
        {
            var w = sender as Window;
            if (w == null) return;
            Helper.SourceIntialized(w);
            LocUtil.SetDefaultLanguage(w);
        }
    }
}
