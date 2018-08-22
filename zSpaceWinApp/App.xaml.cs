using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using zSpace.Notification.Services;
using zSpaceWinApp.IoC;
using zSpaceWinApp.Logs;

namespace zSpaceWinApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            loadConfig();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        #region methods
        private void loadConfig()
        {
            var unityContainer = new UnityContainer();
            IoCContainerFactory.Initialize(new IoCContainer(unityContainer));
            unityContainer.RegisterType<ILog, Log>();
            unityContainer.RegisterType<INotificationDialogService, NotificationDialogService>();
        }
        #endregion
    }
}
