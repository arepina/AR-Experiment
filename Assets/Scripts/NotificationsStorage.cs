using System.Collections.Generic;

namespace Logic
{
    public class NotificationsStorage
    {
        private Stack<Notification> notificationsStorage;
        private long latestTimestamp;

        public NotificationsStorage(Stack<Notification> notificationsStorage, long latestTimestamp)
        {
            this.notificationsStorage = notificationsStorage;
            this.latestTimestamp = latestTimestamp;
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
    }
}
