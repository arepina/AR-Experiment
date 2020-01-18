using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class StorageEditor
    {
        private string silentGroupKey = "_silent_";

        public Dictionary<string, NotificationsStorage> addToStorage(Notification notification)
        {
            Stack<Notification> sourceNotifications = new Stack<Notification>();
            string sourceName = notification.SourceName;
            if (notification.isSilent)
            {
                sourceName = silentGroupKey;
            }
            if (Global.notifications.ContainsKey(sourceName))
            {
                sourceNotifications = Global.notifications[sourceName].Storage;
            }
            sourceNotifications.Push(notification);
            NotificationsStorage newNotificationsStorage = new NotificationsStorage(sourceNotifications,
                                                                                    notification.Timestamp);
            Global.notifications[sourceName] = newNotificationsStorage;
            NotificationsStorage silentGroup = null;
            if (Global.notifications.ContainsKey(silentGroupKey))
            {
                silentGroup = Global.notifications[silentGroupKey];
                Global.notifications.Remove(silentGroupKey);
            }
            Dictionary<string, NotificationsStorage> orderedNotifications = Global.notifications.OrderByDescending(x => x.Value.LatestTimestamp)
                                                                                         .ToDictionary(d => d.Key, d => d.Value);
            if (silentGroup != null || sourceName == silentGroupKey)
            {
                orderedNotifications.Add(silentGroupKey, silentGroup); // silent are always the last
                Global.notifications.Add(silentGroupKey, silentGroup);
            }
            return orderedNotifications;
        }

        public void removeFromStorage(Notification notification)
        {
            //todo
            // Global.notifications.Remove()
        }
    }
}
