using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{

    class NotificationsStorage
    {
        private Stack<Notification> notificationsStorage;
        private long latestTimestamp;
        private Color sourceColor;
        private Image sourceIcon;

        public NotificationsStorage(Stack<Notification> notificationsStorage, long latestTimestamp, Color sourceColor, Image sourceIcon)
        {
            this.notificationsStorage = notificationsStorage;
            this.latestTimestamp = latestTimestamp;
            this.sourceColor = sourceColor;
            this.sourceIcon = sourceIcon;
        }

        public Stack<Notification> Storage
        {
            get
            {
                return notificationsStorage;
            }
        }

        public long LatestTimestamp
        {
            get
            {
                return latestTimestamp;
            }
        }

        public Color SourceColor
        {
            get
            {
                return sourceColor;
            }
        }

        public Image SourceIcon
        {
            get
            {
                return sourceIcon;
            }
        }
    }
}
