using System.Collections.Generic;
using UnityEngine;

namespace Logic
{

    class NotificationsStorage
    {
        private Stack<Notification> notificationsStorage;
        private long latestTimestamp;
        private Color sourceColor;

        public NotificationsStorage(Stack<Notification> notificationsStorage, long latestTimestamp, Color sourceColor)
        {
            this.notificationsStorage = notificationsStorage;
            this.latestTimestamp = latestTimestamp;
            this.sourceColor = sourceColor;
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
    }
}
