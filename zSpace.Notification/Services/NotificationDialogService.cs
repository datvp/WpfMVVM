﻿using zSpace.Notification.Core;
using zSpace.Notification.Core.Configuration;

namespace zSpace.Notification.Services
{
    public class NotificationDialogService : INotificationDialogService
    {
        /// <summary>
        /// Show notification window.
        /// </summary>
        /// <param name="content">The notification object.</param>
        public void Show(object content)
        {
            NotifyBox.Show(content);
        }

        /// <summary>
        /// Show notification window.
        /// </summary>
        /// <param name="content">The notification object.</param>
        /// <param name="configuration">The notification configuration object.</param>
        public void Show(object content, NotificationConfiguration configuration)
        {
            NotifyBox.Show(content, configuration);
        }

        /// <summary>
        ///  Remove all notifications from notification list and buffer list.
        /// </summary>
        public void Clear()
        {
            NotifyBox.ClearNotifications();
        }
    }
}
