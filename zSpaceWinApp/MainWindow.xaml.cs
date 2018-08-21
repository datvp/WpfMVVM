using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using zSpaceWinApp.Processor;

namespace zSpaceWinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon ni;
        public MainWindow()
        {
            InitializeComponent();
            LocUtil.SetDefaultLanguage(this);
            this.ShowSystemTray();        
        }
        
        private void ShowSystemTray()
        {
            ni = new NotifyIcon();
            ni.Icon = new System.Drawing.Icon("light.ico");
            ni.Visible = true;
            ni.Text = "zSpace Manager";
            ni.DoubleClick += Ni_DoubleClick;
        }

        private void Ni_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (WindowState == WindowState.Minimized) this.Hide();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (ni != null) ni.Dispose();
        }

    }
}
