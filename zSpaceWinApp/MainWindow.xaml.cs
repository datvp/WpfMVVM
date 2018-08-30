using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Forms;
using zSpaceWinApp.Processor;
using zSpaceWinApp.Ultility;

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
            this.SourceInitialized += MainWindow_SourceInitialized;           
            this.ShowSystemTray();
        }

        #region methods
        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            var w = sender as Window;
            if (w == null) return;
            Helper.SourceIntialized(w);
            LocUtil.SetDefaultLanguage(w);            
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

        #endregion

        #region override methods
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
        #endregion       
    }
}
