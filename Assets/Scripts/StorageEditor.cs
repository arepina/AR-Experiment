using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class StorageEditor
    {
        public Dictionary<string, NotificationsStorage> addToStorage(Notification notification)
        {
            Stack<Notification> sourceNotifications = new Stack<Notification>();
            string sourceName = notification.SourceName;
            if (notification.isSilent) sourceName = Global.silentGroupKey;
            if (Global.notifications.ContainsKey(sourceName)) sourceNotifications = Global.notifications[sourceName].Storage;
            sourceNotifications.Push(notification);
            NotificationsStorage newNotificationsStorage = new NotificationsStorage(sourceNotifications, notification.Timestamp);
            Global.notifications[sourceName] = newNotificationsStorage;
            return createOrderedStorage(sourceName);           
        }

        public Dictionary<string, NotificationsStorage> removeFromStorage(string id, string sourceName)
        {
            Dictionary<string, NotificationsStorage> notifications = Global.notifications;
            NotificationsStorage newStorage = notifications[sourceName];
            Stack<Notification> newNotificationsStorage = new Stack<Notification>();
            foreach (Notification notification in newStorage.Storage) //todo check that order is the same
            {
                if (!notification.Id.Equals(id))
                {
                    newNotificationsStorage.Push(notification);
                }
            }
            newStorage.Storage = newNotificationsStorage;
            notifications[sourceName] = newStorage;
            return createOrderedStorage(sourceName);
        }

        private Dictionary<string, NotificationsStorage> createOrderedStorage(string sourceName)
        {
            NotificationsStorage silentGroup = null;
            if (Global.notifications.ContainsKey(Global.silentGroupKey))
            {
                silentGroup = Global.notifications[Global.silentGroupKey];
                Global.notifications.Remove(Global.silentGroupKey);
            }
            Dictionary<string, NotificationsStorage> orderedNotifications = Global.notifications.OrderByDescending(x => x.Value.LatestTimestamp)
                                                                                         .ToDictionary(d => d.Key, d => d.Value);
            if (silentGroup != null || sourceName == Global.silentGroupKey)
            {
                orderedNotifications.Add(Global.silentGroupKey, silentGroup); // silent are always the last
                Global.notifications.Add(Global.silentGroupKey, silentGroup);
            }
            return orderedNotifications;
        }
    }
}
