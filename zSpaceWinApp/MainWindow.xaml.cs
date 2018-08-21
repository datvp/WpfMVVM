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
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var fullPath = System.IO.Path.Combine(directory, "light.ico");
            var found = System.IO.File.Exists(fullPath);
            ni = new NotifyIcon();
            ni.Icon = found ? new System.Drawing.Icon("light.ico") : new System.Drawing.Icon(System.Drawing.SystemIcons.Application, 50, 50);
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
